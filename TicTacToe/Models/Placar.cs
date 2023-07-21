using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models.Library;

namespace TicTacToe.Models
{
    public class Placar
    {
        public Placar() {
        }

        private List<ResultadoPartida> _placar { get; set; }
        public List<ResultadoPartida> Partidas
        {
            get
            {

                if (_placar == null)
                {
                    _placar = new List<ResultadoPartida>();
                }

                return _placar;
            }
            set
            {
                _placar = value;
            }
        }

    }
}
