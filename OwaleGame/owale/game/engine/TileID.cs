using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwaleGame.owale.game.engine
{
    class TileID
    {
        string name;
        int ID;

        public TileID(string name, int iD)
        {
            this.name = name;
            ID = iD;
        }

        public string Name { get => name; set => name = value; }
        public int ID1 { get => ID; set => ID = value; }
    }
}
