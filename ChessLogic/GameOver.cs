using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class GameOver
    {
        public Player Winner { get; }
        public GameOverReason Reason { get; }  
        public GameOver(Player winner, GameOverReason reason)
        {
            Winner = winner;
            Reason = reason;
        }  
        public static GameOver Win(Player winner)
        {
            return new GameOver(winner, GameOverReason.Checkmate);
        }
        public static GameOver Draw(GameOverReason reason)
        {
            return new GameOver(Player.None, reason);
        }
    }
}
