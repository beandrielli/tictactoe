using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class Sessao
    {
        public Sessao(){}

        public Sessao(string identificador, string novoJogo) {
            this.Identificador = identificador;
            this.JogoJson = novoJogo;
        }

        public string JogoJson { get; set; }

        public string Identificador { get; set; }

    }
}
