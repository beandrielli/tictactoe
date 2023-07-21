using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Models.Library
{
    public enum ResultadoPartida { 
        jogadorCirculo = 1,
        jogadorXis,
        empate,
        nehum
    }

    public enum ItensSessao { 
        erro = 1,
        idenficadorJogo
    }

    public enum TempDatas { 
        modelJogo = 1,
        mensagemErro
    }

    public enum Pecas { 
        circulo = 1,
        xis
    }
}
