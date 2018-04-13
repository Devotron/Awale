using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OwaleGame.owale.game.engine.TileTypeEnum;

namespace OwaleGame.owale.game.engine
{
    class GameTile : INotifyPropertyChanged
    {
        private int id;
        private int seeds;
        private tileType type;

        public GameTile(int id, int seeds, tileType type)
        {
            this.Id = id;
            this.Seeds = seeds;
            this.Type = type;
        }

        public int Id { get => id; set => id = value; }
        public int Seeds
        {
            get { return seeds; }
            set {

                if ( this.seeds != value )
                {
                    seeds = value;
                    NotifyPropertyChanged("seeds");
                }

            }
        }
        public tileType Type { get => type; set => type = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override string ToString()
        {
            return String.Format("Tile > id : {0}, seeds : {1}, type : {2}", Id, Seeds, Type);
        }
    }
}
