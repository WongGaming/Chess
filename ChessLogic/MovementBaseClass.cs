using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public abstract class MovementBaseClass
    {
        public abstract Movement Type { get; } 
        public abstract Position StartingPos { get; } //pieces move from here
        public abstract Position EndingPos { get; } //pieces move to here

        public abstract void ApplyMove(Board board);
        public virtual bool Legal(Board board)
        {
            return IsMoveLegal(board, StartingPos);
        }
        private bool IsMoveLegal(Board board, Position startingPos)
        {
            Player currentPlayer = board[startingPos].Colour;
            Board copyOfBoard = board.Copy();
            ApplyMove(copyOfBoard);
            return !copyOfBoard.InCheck(currentPlayer);
        }
    }

}
