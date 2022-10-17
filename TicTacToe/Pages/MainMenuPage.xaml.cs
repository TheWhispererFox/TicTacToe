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
using TicTacToe.Pages;

namespace TicTacToe
{
    /// <summary>
    /// Логика взаимодействия для MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        private GameBoard.Size size;
        private GameManager.GameMode gameMode;
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GameViewPage(new GameManager(size, gameMode)));
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.GroupName == "Gamemode")
            {
                gameMode = radioButton.Content switch
                {
                    "PvP" => GameManager.GameMode.PvP,
                    "PvC" => GameManager.GameMode.PvC,
                    "CvC" => GameManager.GameMode.CvC,
                    _ => GameManager.GameMode.PvC,
                };
            }
            else
            {
                size = radioButton.Content switch
                {
                    "Small (3x3)" => GameBoard.Size.Small,
                    "Medium (4x4)" => GameBoard.Size.Medium,
                    "Large (5x5)" => GameBoard.Size.Large,
                    _ => GameBoard.Size.Small,
                };
            }
        }
    }
}
