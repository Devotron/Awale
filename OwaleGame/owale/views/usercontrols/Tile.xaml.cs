using OwaleGame.owale.game.engine;
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

namespace OwaleGame.owale.views.usercontrols
{
    /// <summary>
    /// Logique d'interaction pour Tile.xaml
    /// </summary>
    public partial class Tile : UserControl
    {

        List<TileID> tilesMapping;

        public Tile()
        {
            InitializeComponent();

            tilesMapping = new List<TileID>();

            //Player 2 houses :
            tilesMapping.Add(new TileID("tile_0_0", 12));
            tilesMapping.Add(new TileID("tile_0_1", 11));
            tilesMapping.Add(new TileID("tile_0_2", 10));
            tilesMapping.Add(new TileID("tile_0_3", 9));
            tilesMapping.Add(new TileID("tile_0_4", 8));
            tilesMapping.Add(new TileID("tile_0_5", 7));

            //Player 1 houses :
            tilesMapping.Add(new TileID("tile_1_0", 1));
            tilesMapping.Add(new TileID("tile_1_1", 2));
            tilesMapping.Add(new TileID("tile_1_2", 3));
            tilesMapping.Add(new TileID("tile_1_3", 4));
            tilesMapping.Add(new TileID("tile_1_4", 5));
            tilesMapping.Add(new TileID("tile_1_5", 6));

        }

        //Mouse click
        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tile tile = (Tile)sender;

            int position = tilesMapping.Where(t => t.Name == tile.Name).Select(t => t.ID1).FirstOrDefault();

            Console.WriteLine("Click joueur - tile ID:{0}\n", position);

            

        }
    }
}
