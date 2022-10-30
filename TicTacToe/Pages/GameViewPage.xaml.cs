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
using TicTacToe.Models;

namespace TicTacToe.Pages
{
    /// <summary>
    /// Логика взаимодействия для GameViewPage.xaml
    /// </summary>
    public partial class GameViewPage : Page
    {
        private readonly GameManager manager;
        private readonly List<Button> buttons;
        public GameViewPage(GameManager manager)
        {
            InitializeComponent();
            buttons = new List<Button>();
            this.manager = manager;
            manager.BoardChanged += OnBoardChanged;
            manager.StateChanged += OnStateChanged;
            int size = manager.Board.BoardSize switch
            {
                GameBoard.Size.Small => 3,
                GameBoard.Size.Medium => 4,
                GameBoard.Size.Large => 5,
                _ => 3,
            };

            for (int i = 0; i < size; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition());
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Button btn = new();
                    buttons.Add(btn);
                    btn.Click += Btn_Click;
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                    GameGrid.Children.Add(btn);
                }
            }
        }

        private void OnStateChanged(GameManager obj)
        {
            StateText.Text = manager.State switch
            {
                GameManager.GameState.XTurn => "TURN X",
                GameManager.GameState.OTurn => "TURN O",
                GameManager.GameState.OWin => "WIN O",
                GameManager.GameState.XWin => "WIN X",
                GameManager.GameState.Tie => "DRAW",
                _ => "Unknown State",
            };
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button) return;
            manager.DoTurn(buttons.IndexOf(button));
        }

        private void OnBoardChanged(int index)
        {
            buttons[index].Content = manager.State switch
            {
                GameManager.GameState.XTurn => "X",
                GameManager.GameState.OTurn => "O",
                _ => "?",
            };
        }
    }
}
