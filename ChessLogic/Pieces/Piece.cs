
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public abstract class Piece
    {
        public abstract PieceType Type { get; }
        public abstract Player Colour { get; }
        public bool Moved { get; private set; } // Assuming 'Moved' is a property

        // Other properties and methods of the Piece class

        // Method to mark the piece as moved
        public void MarkAsMoved()
        {
            Moved = true;
        }


        public abstract Piece Copy();

        public abstract IEnumerable<MovementBaseClass> GetMove ( Board board, Position start);
        // ^^takes current position and board and return all the available moves for the piece
        protected IEnumerable<Position> MovePositionsInDirection(Board board, Direction direction, Position start)
        {
            for (Position position = start + direction; Board.IsInside(position); position += direction)
            {
                if (board.IsEmpty(position))
                {
                    yield return position;
                    continue;
                }
                Piece piece = board[position];
                if (piece.Colour != Colour)
                {
                    yield return position;
                    // ^^checks to see if the piece is the same colour as theirs or if its an opponents piece
                }
                yield break;
            }
        }

        protected IEnumerable<Position> MovePositionsInDirections(Board board, Direction[] directions, Position start)
            // ^^takes an array of directions rather than just one direction
        {
            return directions.SelectMany(direction => MovePositionsInDirection(board, direction, start));
            // ^^collection of all reachable positions in the given directions
        }
        public virtual bool AbleToCaptureOpponentsKing(Position start, Board board)
        {

            return GetMove(board, start).Any(move =>
            {
                Piece piece = board[move.EndingPos];
                return piece != null && piece.Type == PieceType.King;
            });
            
            
        }
    }
}
