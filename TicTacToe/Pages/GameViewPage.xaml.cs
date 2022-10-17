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
        private GameManager manager;
        public GameViewPage(GameManager manager)
        {
            InitializeComponent();
            this.manager = manager;
        }
    }
}
