using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models.Library;

namespace TicTacToe.Models
{
    public class Partida
    {
        public Partida() {
            Resultado = ResultadoPartida.nehum;
        }

        public ResultadoPartida Resultado { get; set; }


    }
}
