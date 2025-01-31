using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public enum GameOverReason //different ways a game could end
    {
        Checkmate, //win
        Stalemate, //draw
        FiftyMoveRule, //draw
        InsufficientMaterial, //draw
        ThreefoldRepetition //draw
    }
}
