using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    [Serializable]
    public class Jogo
    {

        public Jogo() {
            IniciarTabuleiro(true);
        }

        public string JogadorCirculo { get; set; }

        public string JogadorXis { get; set; }

        public Partida PartidaAtual { get; set; }

        public Placar PlacarGeral { get; set; }

        public bool VezCirculo { get; set; }

        public string Identificador { get; set; }

        public Library.Pecas?[,] Tabuleiro { get; set; }

        public int PecasAdicionadas { get; set; }

        public bool VerificarTabuleiro() {

            Library.ResultadoPartida resultado = Library.ResultadoPartida.nehum;

            resultado = VerificarLinhas();

            if (resultado == Library.ResultadoPartida.nehum) 
                resultado = VerificarColunas();

            if (resultado == Library.ResultadoPartida.nehum)
                resultado = VerificarDiagonais();

            if (resultado == Library.ResultadoPartida.nehum && PecasAdicionadas == 9)
                resultado = Library.ResultadoPartida.empate;

            if (resultado != Library.ResultadoPartida.nehum)
            {
                PartidaAtual.Resultado = resultado;
                PlacarGeral.Partidas.Add(resultado);
                return true;
            }

            return false;
        }

        public void IniciarTabuleiro(bool reiniciarPlacar = false) {
            this.Tabuleiro = new Library.Pecas?[3, 3];
            PartidaAtual = new Partida();

            this.Identificador = string.Empty;
            this.VezCirculo = true;
            PecasAdicionadas = 0;

            if (reiniciarPlacar)
            {
                PlacarGeral = new Placar();
            }
        }

        private Library.ResultadoPartida VerificarLinhas()
        {
            for (int linha = 0; linha <= 2; linha++) {
                if (Tabuleiro[linha, 0] != null && Tabuleiro[linha, 0] == Tabuleiro[linha, 1] && Tabuleiro[linha, 0] == Tabuleiro[linha, 2]) {
                    if (Tabuleiro[linha, 0] == Library.Pecas.circulo)
                        return Library.ResultadoPartida.jogadorCirculo;
                    else
                        return Library.ResultadoPartida.jogadorXis;
                }
            }

            return Library.ResultadoPartida.nehum;
        }

        private Library.ResultadoPartida VerificarColunas()
        {
            for (int coluna = 0; coluna <= 2; coluna++)
            {
                if (Tabuleiro[0, coluna] != null && Tabuleiro[0, coluna] == Tabuleiro[1, coluna] && Tabuleiro[0, coluna] == Tabuleiro[2, coluna])
                {
                    if (Tabuleiro[0, coluna] == Library.Pecas.circulo)
                        return Library.ResultadoPartida.jogadorCirculo;
                    else
                        return Library.ResultadoPartida.jogadorXis;
                }
            }

            return Library.ResultadoPartida.nehum;
        }

        private Library.ResultadoPartida VerificarDiagonais()
        {
            if (Tabuleiro[1, 1] == null)
                return Library.ResultadoPartida.nehum;

            if(Tabuleiro[0,0] != null && Tabuleiro[0,0] == Tabuleiro[1,1] && Tabuleiro[0,0] == Tabuleiro[2,2])
            {
                if (Tabuleiro[1,1] == Library.Pecas.circulo)
                    return Library.ResultadoPartida.jogadorCirculo;
                else
                    return Library.ResultadoPartida.jogadorXis;
            }

            if(Tabuleiro[0,2] != null && Tabuleiro[0,2] == Tabuleiro[1,1] && Tabuleiro[0,2] == Tabuleiro[2,0])
            {
                if (Tabuleiro[1,1] == Library.Pecas.circulo)
                    return Library.ResultadoPartida.jogadorCirculo;
                else
                    return Library.ResultadoPartida.jogadorXis;
            }

            return Library.ResultadoPartida.nehum;
        }

    }
}