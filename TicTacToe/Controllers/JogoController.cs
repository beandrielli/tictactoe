using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models.Library;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    public class JogoController : Controller
    {
        private ContextoJogos _contexto;

        public JogoController(ContextoJogos contexto) {
            _contexto = contexto;
        }
        
        public IActionResult NovoJogo(Jogo model)
        {
            try
            {
                if (model == null || string.IsNullOrWhiteSpace(model.JogadorCirculo) || string.IsNullOrWhiteSpace(model.JogadorXis)) {
                
                    TempData[TempDatas.mensagemErro.ToString()] = "Valores incorretos, por favor preencha os nomes dos dois jogadores!";

                    TempData[TempDatas.modelJogo.ToString()] = JsonConvert.SerializeObject(model);

                    return RedirectToAction("Index", "Home");
                }

                string identificador = string.Empty;

                identificador = HttpContext.Session.GetString(ItensSessao.idenficadorJogo.ToString());
                
                if(string.IsNullOrWhiteSpace(identificador))
                    identificador = Guid.NewGuid().ToString();

                Sessao sessao;

                sessao = _contexto.Sessoes.Where(sessao => sessao.Identificador == identificador).FirstOrDefault();

                Jogo jogoRetorno;

                if (sessao == null)
                {
                    jogoRetorno = new Jogo();

                    jogoRetorno.Identificador = identificador;
                    jogoRetorno.JogadorCirculo = model.JogadorCirculo;
                    jogoRetorno.JogadorXis = model.JogadorXis;

                    string novoJogoJson = JsonConvert.SerializeObject(jogoRetorno);

                    sessao = new Sessao(identificador, novoJogoJson);

                    _contexto.Sessoes.Add(sessao);
                    _contexto.SaveChanges();

                }
                else {
                    jogoRetorno = JsonConvert.DeserializeObject<Jogo>(sessao.JogoJson);
                    jogoRetorno.Identificador = identificador;
                }

                HttpContext.Session.SetString(ItensSessao.idenficadorJogo.ToString(), identificador);

                return View("Jogo", jogoRetorno);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar nova partida. ex:" + ex.Message);
            }
        }

        [HttpPost]
        public JsonResult RegistrarJogada(string identificador, int linha, int coluna) {

            try
            {
                JsonResult resultado = null;
                Sessao sessaoAtual = _contexto.Sessoes.Where(sessao => sessao.Identificador == identificador).FirstOrDefault();

                if (sessaoAtual == null)
                    throw new Exception("Erro: Sessão inexistente");

                Models.Jogo JogoAtual = null;

                JogoAtual = JsonConvert.DeserializeObject<Models.Jogo>(sessaoAtual.JogoJson);

                if (JogoAtual.PecasAdicionadas <= 8)
                {

                    if (JogoAtual.Tabuleiro[linha, coluna] != null)
                    {
                        return new JsonResult(new { success = false, message = "Erro ao processar jogada. Espaço já ocupado." });
                    }

                    if (JogoAtual.VezCirculo)
                    {
                        JogoAtual.Tabuleiro[linha, coluna] = Pecas.circulo;
                        JogoAtual.VezCirculo = false;
                    }
                    else
                    {
                        JogoAtual.Tabuleiro[linha, coluna] = Pecas.xis;
                        JogoAtual.VezCirculo = true;
                    }

                    JogoAtual.PecasAdicionadas++;
                }

                JogoAtual.VerificarTabuleiro();


                string vencedor = string.Empty;
                if (JogoAtual.PartidaAtual.Resultado != ResultadoPartida.nehum)
                {
                    vencedor = AnunciarVencedor(JogoAtual);

                    resultado = new JsonResult(new { success = true, winner = vencedor, result = JogoAtual.PartidaAtual.Resultado.ToString() });

                    JogoAtual.IniciarTabuleiro(false);
                }
                else {
                    resultado = new JsonResult(new { success = true, winner = string.Empty, result = string.Empty });
                }

                sessaoAtual.JogoJson = JsonConvert.SerializeObject(JogoAtual);

                _contexto.SaveChanges();

                return resultado;
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = "Erro ao processar jogada. ex: " + ex.Message });
            }
        }

        public IActionResult RestartJogo() {

            HttpContext.Session.SetString(ItensSessao.idenficadorJogo.ToString(), string.Empty);

            return RedirectToAction("Index", "Home");
        }

        private string AnunciarVencedor(Jogo JogoAtual) {
            string vencedor = string.Empty;

            switch (JogoAtual.PartidaAtual.Resultado)
            {
                case ResultadoPartida.jogadorCirculo:
                    vencedor = JogoAtual.JogadorCirculo + " venceu!";
                    break;

                case ResultadoPartida.jogadorXis:
                    vencedor = JogoAtual.JogadorXis + " venceu!";
                    break;
                case ResultadoPartida.empate:
                    vencedor = "Deu Velha!";
                    break;
            }

            return vencedor;
        }
    }
}
