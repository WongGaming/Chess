using ChessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for GameOverMenu.xaml
    /// </summary>
    public partial class GameOverMenu : UserControl
    {
        public event Action<Choices> ChoiceSelected;
        public GameOverMenu(GameState gameState)
        {
            InitializeComponent();

            GameOver result = gameState.GameOver;
            WinnerText.Text = RetrieveWinnerText(result.Winner);
            ReasonText.Text = RetrieveReasonText(result.Reason, gameState.CurrentPlayer);
        }

        private static string RetrieveWinnerText (Player winner)
        {
            return winner switch
            {
                Player.White => "WHITE IS THE WINNER!",
                Player.Black => "BLACK IS THE WINNER!",
                _ => "DRAW!"
            };
        }

        private static string PlayerText(Player player)
        {
            return player switch
            {
                Player.White => "WHITE",
                Player.Black => "BLACK",
                _ =>""
            };
        }

        private static string RetrieveReasonText(GameOverReason reason, Player currentPlayer)
        {
            return reason switch
            {
                GameOverReason.Stalemate => $"STALEMATE - {PlayerText(currentPlayer)} CANT MOVE",
                GameOverReason.Checkmate => $"CHECKMATE - {PlayerText(currentPlayer)} CANT MOVE",
                GameOverReason.FiftyMoveRule => $"FIFTYMOVERULE",
                GameOverReason.InsufficientMaterial => $"INSUFFICIENT MATERIAL",
                GameOverReason.ThreefoldRepetition => $"THREEFOLD REPETITION",
                _ =>""
            };
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            ChoiceSelected?.Invoke(Choices.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            ChoiceSelected?.Invoke(Choices.Exit);
        }
    }
}
