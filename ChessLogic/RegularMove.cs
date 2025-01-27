 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class RegularMove : MovementBaseClass
    {
        public override Movement Type => Movement.Normal;
        public override Position StartingPos { get; }
        public override Position EndingPos { get; }
        public RegularMove(Position start, Position end)
        {
            StartingPos = start;
            EndingPos = end;
        }
        public override void ApplyMove(Board board) //makes the move happen
        {
            // Retrieve the piece from the starting position
            Piece movingPiece = board[StartingPos];

            // Update the board with the new positions
            board[EndingPos] = movingPiece; 
            board[StartingPos] = null;

            // Mark the piece as having moved
            if (movingPiece != null)
            {
                movingPiece.MarkAsMoved();
            }
        }
    }
}
