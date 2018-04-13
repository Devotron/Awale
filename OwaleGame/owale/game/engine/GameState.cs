using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwaleGame.owale.game.engine
{
    class GameState
    {
        ObservableCollection<GameTile> owaleBoard;

        static int TILE_STARTING_SEEDS_NUMBER = 4;

        enum RULES : int
        {
            PLAYER_MAX_SEEDS = 25,
            ALLOW_CAPTURE_SEED_MIN = 2,
            ALLOW_CAPTURE_SEED_MAX = 3,
            ALLOW_SKIP_CAPTURE = 12
        }

        public ObservableCollection<GameTile> OwaleBoard { get => owaleBoard; set => owaleBoard = value; }

        public GameState()
        {
            this.OwaleBoard = new ObservableCollection<GameTile>();

            //OwaleBoard.Add(new GameTile(1, 2, TileTypeEnum.tileType.START_TILE_PLAYER_1));

            GameTile tile = null;

            // # Génération des tuiles du joueur 1
            for ( int i = 1; i <= 6; i++)
            {
                //1,2,3,4,5,6
                tile = new GameTile(i, TILE_STARTING_SEEDS_NUMBER, TileTypeEnum.tileType.TILE_PLAYER_1);
                
                switch (tile.Id)
                {
                    case 1:
                        tile.Type = TileTypeEnum.tileType.START_TILE_PLAYER_1;
                        break;
                    case 6:
                        tile.Type = TileTypeEnum.tileType.END_TILE_PLAYER_1;
                        break;
                }

                OwaleBoard.Add(tile);
            }

            // # Génération des tuiles du joueur 2
            for (int i = 7; i <= 12; i++)
            {
                //7,8,9,10,11,12
                tile = new GameTile(i, TILE_STARTING_SEEDS_NUMBER, TileTypeEnum.tileType.TILE_PLAYER_2);

                switch (tile.Id)
                {
                    case 7:
                        tile.Type = TileTypeEnum.tileType.START_TILE_PLAYER_2;
                        break;
                    case 12:
                        tile.Type = TileTypeEnum.tileType.END_TILE_PLAYER_2;
                        break;
                }

                OwaleBoard.Add(tile);
            }
        }

        // - Récupérer les graines a un emplacement <t> et les ditribué jusqu'à l'emplacement <t> + <nb seeds> à partir de <t + 1>
        public void sow(int startPosition)
        {
            // Objet move action (int start, seeds)
            Console.WriteLine("selected house position : {0}\n", startPosition);


            int selectedHouseSeeds = OwaleBoard.Where(tile => tile.Id == startPosition).Select(tile=> tile.Seeds).Single();
            Console.WriteLine("selected house seed : {0}\n", selectedHouseSeeds);
            // si nextPosition = 12 skipper le tile startposition
            // End P2 -> return P1 START
            // End P1 -> return P2 Start

            int nextPosition = startPosition + selectedHouseSeeds;
            Console.WriteLine("next position : {0}\n", nextPosition);

            List<GameTile> modifiedTiles = new List<GameTile>();

            if ( nextPosition <= 12 )
            {
                //Tiles on wich player has sown some seeds
                modifiedTiles = OwaleBoard.Where(tile => tile.Id <= nextPosition && tile.Id > startPosition).ToList();

                // Adding seeds 

                foreach (GameTile tile in OwaleBoard)
                {
                    int seeds = OwaleBoard.Where(t => t.Id == tile.Id).First().Seeds;

                    OwaleBoard.Where(t => t.Id == tile.Id).First().Seeds = seeds + tile.Seeds;
                }

               
                //calculate point

                modifiedTiles.Reverse();
                Console.WriteLine("Modified tiles : ");  
                foreach (GameTile tile in modifiedTiles)
                {
                    Console.WriteLine("-> " + tile);
                }
                Console.WriteLine("---------------------");

                foreach (GameTile tile in modifiedTiles)
                {
                    if ( tile.Seeds >= (int)RULES.ALLOW_CAPTURE_SEED_MIN && tile.Seeds <= (int)RULES.ALLOW_CAPTURE_SEED_MAX)
                    {
                        Console.WriteLine(">" + tile);
                    }
                }

            }



            //List<GameTile> result = OwaleBoard.Where(tile => tile.Id <= nextPosition && tile.Id > startPosition).ToList();

            //recursivité startposition
            /*int remainder = 0;

            if ( result.Any(tile => tile.Type == TileTypeEnum.tileType.END_TILE_PLAYER_2) ) {
                remainder = nextPosition - result.Last().Id;
                Console.WriteLine("remainder : {0}\n", remainder);
            }

            Console.WriteLine("Case atteignable :");
            foreach (GameTile tile in result) {
                Console.WriteLine(tile);
            }
            Console.WriteLine("---------------------");
            */



        }


        public override string ToString()
        {
            string s = "Board state :\n" + 
                       "++++++++++++++++\n";

            foreach (GameTile tile in OwaleBoard)
            {
                s += "\n" + tile.ToString();
            }

            return s;
        }
    }

}
