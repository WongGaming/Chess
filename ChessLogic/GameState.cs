using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class GameState
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }
        public GameOver GameOver { get; private set; } = null;
        public GameState (Player player, Board board)
        {
            CurrentPlayer = player;
            Board = board;
        }
        public List<MovementBaseClass> LegalMoves(Position position, int depth)
        {
            // Base case: if depth is zero, stop recursion
            if (depth == 0)
            {
                return new List<MovementBaseClass>();
            }

            // If the position is invalid (empty or not the current player's piece), return an empty list
            if (Board.IsEmpty(position) || Board[position].Colour != CurrentPlayer)
            {
                return new List<MovementBaseClass>();
            }

            // Get the piece at the current position
            Piece piece = Board[position];
            List<MovementBaseClass> possibleMoves = piece.GetMove(Board, position).ToList();
            List<MovementBaseClass> legalMoves = new List<MovementBaseClass>();

            // Iterate through possible moves
            foreach (var move in possibleMoves)
            {
                if (move.Legal(Board)) // Check if the move is legal
                {
                    legalMoves.Add(move); // Add to the legal moves list

                    // Simulate the move by calling the ApplyMove function on the piece's move
                    var simulatedBoard = Board.Copy(); // Assuming Board.Clone creates a deep copy
                    move.ApplyMove(simulatedBoard); // Apply the move using the existing ApplyMove method

                    // Recursively explore further moves from this new board state
                    var nextPosition = move.EndingPos; // Assuming the move knows its destination
                    var deeperMoves = new Game(simulatedBoard).LegalMoves(nextPosition, depth - 1);

                    // Optionally, combine deeper moves into the result (e.g., for scoring or evaluation)
                    legalMoves.AddRange(deeperMoves);
                }
            }

            return legalMoves;
        }

        public void MakeMove (MovementBaseClass move) //executes move
        {
            move.ApplyMove(Board);
            CurrentPlayer = CurrentPlayer.Opponent();
            CheckForGameOver(); //need to check if the game has ended after every move.

        }
        public IEnumerable<MovementBaseClass> LegalMovesFor (Player player)
        {
            IEnumerable<MovementBaseClass> possibleMoves = Board.PiecePositionsFor(player).SelectMany(position =>
            {
                Piece piece = Board[position];
                return piece.GetMove(Board, position);
            });
            return possibleMoves.Where(move => move.Legal(Board));
        }
        private void CheckForGameOver()
        {
            if (LegalMovesFor(CurrentPlayer).Any())
            {
                 if (Board.InCheck(CurrentPlayer))
                {
                     GameOver = GameOver.Win(CurrentPlayer.Opponent());
                }
                else
                {
                    GameOver = GameOver.Draw(GameOverReason.StaleMate);
                }
            }
        }
        public bool GameIsOver()
        {
            return GameOver != null;
        }

        public IEnumerable<MovementBaseClass> LegalMoves(Position position)
        {
            // Check if the position is valid (i.e., contains a piece belonging to the current player)
            if (Board.IsEmpty(position) || Board[position].Colour != CurrentPlayer)
            {
                return Enumerable.Empty<MovementBaseClass>(); // Return an empty list if the position is invalid
            }

            // Get the piece at the given position
            Piece piece = Board[position];

            // Get all possible moves for the piece (you can adjust this method to match your logic)
            var possibleMoves = piece.GetMove(Board, position);

            // Filter the possible moves to include only those that are legal
            List<MovementBaseClass> legalMoves = new List<MovementBaseClass>();

            foreach (var move in possibleMoves)
            {
                // Assuming that the `Legal` method checks if the move is actually legal on the board
                if (move.Legal(Board))
                {
                    legalMoves.Add(move); // Add the move to the list of legal moves
                }
            }

            // Return the list of legal moves
            return legalMoves;
        }
    }
}
