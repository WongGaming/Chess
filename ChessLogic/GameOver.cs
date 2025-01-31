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
        public GameOver(Player winner, GameOverReason reason) //constructor
        {
            Winner = winner;
            Reason = reason;
        }  
        public static GameOver Win(Player winner) //returns the winner and the reason - always checkmate
        {
            return new GameOver(winner, GameOverReason.Checkmate);
        }
        public static GameOver Draw(GameOverReason reason) //doesn't return a particular player but returns the reason for draw (4)
        {
            return new GameOver(Player.None, reason);
        }
    }
}
