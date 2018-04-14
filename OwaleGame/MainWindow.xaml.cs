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
using OwaleGame.owale.game.engine;

namespace OwaleGame
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameBoard game;

        public MainWindow()
        {
            
            InitializeComponent();

            //game = new GameState();
            game = new GameBoard();

            Console.WriteLine(game.GameState.ToString() + "\n");

           /* game.OwaleBoard[9].Seeds = 13;
            game.OwaleBoard[1].Seeds = 1;
            game.OwaleBoard[0].Seeds = 2;
            game.OwaleBoard[2].Seeds = 1;
            */

            //game.sow(10);

            this.DataContext = game;

        }

       
    }
}
