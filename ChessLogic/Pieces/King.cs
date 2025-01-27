using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Colour { get; }
        private static readonly Direction[] directions = new Direction[]
        {
            Direction.East,
            Direction.North,
            Direction.South,
            Direction.West,
            Direction.NorthEast,
            Direction.SouthEast,
            Direction.NorthWest,
            Direction.SouthWest,
        };
        public King(Player colour)
        {
            Colour = colour;
        }
        public override Piece Copy()
        {
            King copy = new King(Colour);
            // Copy the moved state from the current piece
            if (Moved)
            {
                copy.MarkAsMoved(); // Set the moved state on the copy
            }

            return copy;
        }
        private IEnumerable<Position> MovePositions(Board board, Position start)
        {
            foreach (Direction directions in directions)
            {
                Position end = start + directions;

                if (!Board.IsInside(end))
                {
                    continue;
                }
                if (board.IsEmpty(end) || board[end].Colour != Colour)
                {
                    yield return end;
                }
            }
        }
        public override IEnumerable<MovementBaseClass> GetMove(Board board, Position start)
        {
            foreach (Position end in MovePositions(board,start)) 
            {
                yield return new RegularMove(start, end);
            }
        }
        public override bool AbleToCaptureOpponentsKing(Position start, Board board)
        {
            return MovePositions(board, start).Any(end =>
            {
                Piece piece = board[end];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }
}
