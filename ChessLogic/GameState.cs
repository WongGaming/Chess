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
        public GameState (Player player, Board board) //constructor
        {
            CurrentPlayer = player;
            Board = board;
        }
        public List<MovementBaseClass> LegalMoves(Position position, int depth) //depth-first search, returns list of legal moves
        {
            //base case: if depth is zero, stop recursion
            if (depth == 0)
            {
                return new List<MovementBaseClass>();
            }

            //if the position is invalid (empty or not the current player's piece), return an empty list
            if (Board.IsEmpty(position) || Board[position].Colour != CurrentPlayer)
            {
                return new List<MovementBaseClass>();
            }

            //get the piece at the current position
            Piece piece = Board[position];
            List<MovementBaseClass> possibleMoves = piece.GetMove(Board, position).ToList();
            List<MovementBaseClass> legalMoves = new List<MovementBaseClass>();
            //iterate through possible moves
            foreach (var move in possibleMoves)
            {
                if (move.Legal(Board)) //check if the move is legal
                {
                    legalMoves.Add(move); //add to the legal moves list
                    //simulate the move by calling the ApplyMove function on the piece's move
                    var simulatedBoard = Board.Copy(); 
                    move.ApplyMove(simulatedBoard); //apply the move
                    //recursively explore further moves from this new board state
                    var nextPosition = move.EndingPos;
                    var deeperMoves = new Game(simulatedBoard).LegalMoves(nextPosition, depth - 1);
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
                Piece piece = Board[position]; //get piece at current position
                return piece.GetMove(Board, position); //get all possible moves for that piece
            });
            return possibleMoves.Where(move => move.Legal(Board)); //filters out illegal moves
        }
        private void CheckForGameOver() //checks to see if game is over
        {
            if (!LegalMovesFor(CurrentPlayer).Any()) //if current player has no legal moves left
            {
                if (Board.InCheck(CurrentPlayer))
                {
                     GameOver = GameOver.Win(CurrentPlayer.Opponent()); //opponent wins 
                }
                else
                {
                    GameOver = GameOver.Draw(GameOverReason.Stalemate); //its a draw due to stalemate
                    //ONLY FOR STALEMATE AT THE MOMENT
                    //WILL NEED TO CHANGE FOR OTHER GAME OVER REASONS
                }
            }
        }
        public bool GameIsOver() //returns whether game is over or not
        {
            return GameOver != null;
        }

        public IEnumerable<MovementBaseClass> LegalMoves(Position position)
        {
            //check if the position is valid (i.e., contains a piece belonging to the current player)
            if (Board.IsEmpty(position) || Board[position].Colour != CurrentPlayer)
            {
                return Enumerable.Empty<MovementBaseClass>(); //return an empty list if the position is invalid
            }

            //get the piece at the given position
            Piece piece = Board[position];

            //get all possible moves for the piece (you can adjust this method to match your logic)
            var possibleMoves = piece.GetMove(Board, position);

            //filter the possible moves to include only those that are legal
            List<MovementBaseClass> legalMoves = new List<MovementBaseClass>();

            foreach (var move in possibleMoves)
            {
                //assuming that the `Legal` method checks if the move is actually legal on the board
                if (move.Legal(Board))
                {
                    legalMoves.Add(move); //add the move to the list of legal moves
                }
            }

            //return the list of legal moves
            return legalMoves;
        }
    }
}
