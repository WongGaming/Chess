using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Bishop : Piece
    {
        public override PieceType Type => PieceType.Bishop;
        public override Player Colour { get; }

        private static readonly Direction[] directions = new Direction[]
        {
            Direction.SouthEast,
            Direction.SouthWest,
            Direction.NorthEast,
            Direction.NorthWest,
        };
        public Bishop(Player colour)
        {
            Colour = colour;
        }
        public override Piece Copy()
        {
            Bishop copy = new Bishop(Colour);
            // Copy the moved state from the current piece
            if (Moved)
            {
                copy.MarkAsMoved(); // Set the moved state on the copy
            }

            return copy;
        }
        public override IEnumerable<MovementBaseClass> GetMove(Board board, Position start)
        {
            return (IEnumerable < MovementBaseClass > ) MovePositionsInDirections(board, directions, start).Select(end => new RegularMove(start, end));
        }
    }
}
