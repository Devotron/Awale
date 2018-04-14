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

        static int TURN = 1;

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
        // retourne le score de l'action
        public int sow(int startPosition)
        {

            int score = 0;

            Console.WriteLine("> TURN {0} <\n", TURN);
            TURN++;

            int selectedHouseSeeds = OwaleBoard.Where(tile => tile.Id == startPosition).Select(tile => tile.Seeds).Single();
            int nextPosition = startPosition + selectedHouseSeeds;


            // Remove seed from house
            GameTile startingTile = OwaleBoard.FirstOrDefault(t => t.Id == startPosition);
            startingTile.Seeds = 0;

            Console.WriteLine("selected house : [position : {0} | seeds : {1} | next position : {2} ]\n", startPosition, selectedHouseSeeds, nextPosition);

            List<GameTile> modifiedTiles = new List<GameTile>();
            List<GameTile> newTiles = new List<GameTile>();

            if ( selectedHouseSeeds < 12 )
            {

                //Tiles on wich player has sown some seeds
                modifiedTiles = OwaleBoard.Where(tile => tile.Id <= nextPosition && tile.Id > startPosition).ToList();
                Console.WriteLine("Targeted Tiles : ");
                foreach (GameTile tile in modifiedTiles)
                {
                    Console.WriteLine("-> " + tile);
                }
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++\n\n");

                // Adding seeds 
                foreach (GameTile tile in OwaleBoard)
                {

                    foreach (GameTile modTile in modifiedTiles)
                    {

                        if (tile.Id == modTile.Id)
                        {
                            Console.WriteLine("Adding seed to house {0} : \n", tile.Id);
                            tile.Seeds = modTile.Seeds + 1;
                            modTile.Seeds = tile.Seeds;
                            Console.WriteLine("--> " + tile + "\n");
                        }

                    }
                }

                // Case > gametile array size
                if (nextPosition >= 12)
                {
                    int remainingTiles = nextPosition - 12;
                    Console.WriteLine("Remaining tile : {0}\n", remainingTiles);

                    for (int i = 0; i < remainingTiles; i++)
                    {
                        modifiedTiles.Add(OwaleBoard[i]);
                        Console.WriteLine("Adding seed to house {0} : \n", OwaleBoard[i].Id);
                        OwaleBoard[i].Seeds = OwaleBoard[i].Seeds + 1;
                        modifiedTiles.Last().Seeds = OwaleBoard[i].Seeds;
                        Console.WriteLine("--> " + OwaleBoard[i] + "\n");
                        Console.WriteLine("******> " + modifiedTiles.Last().Seeds + "\n");
                    }

                }




                //calculate point

                modifiedTiles.Reverse();

                

                foreach (GameTile tile in modifiedTiles)
                {

                    if (tile.Seeds <= (int)RULES.ALLOW_CAPTURE_SEED_MAX && tile.Seeds >= (int)RULES.ALLOW_CAPTURE_SEED_MIN)
                    {
                        Console.WriteLine("add score > " + tile.Seeds);
                        score += tile.Seeds;
                        //remove seeds :
                        tile.Seeds = 0;
                    } else
                    {
                        Console.WriteLine("seeds > 3 or < 2");
                        Console.WriteLine("score : {0}", score);
                        break;
                    }

                }
                Console.WriteLine("---------------------");

              

            } else
            {

                Console.WriteLine("seeds > 12");

                int position = startPosition - 1;

                while ( selectedHouseSeeds != 0 )
                {
                    if ( position == (startPosition -1))
                    { // starting tile
                        Console.WriteLine("cas A");
                        if ( position == 11 )
                        {
                            position = 0;
                        }

                        modifiedTiles.Add(OwaleBoard[position]);
                        position++;

                    } else if ( position == 11 ) { // [12]
                        Console.WriteLine("cas B");
                        OwaleBoard[position].Seeds += 1;
                        selectedHouseSeeds--;
                        modifiedTiles.Add(OwaleBoard[position]);
                        position = 0;

                    } else
                    { // [1 -> 11]
                        Console.WriteLine("cas C");
                        OwaleBoard[position].Seeds += 1;
                        selectedHouseSeeds--;

                        modifiedTiles.Add(OwaleBoard[position]);
                        position++;
                    }
                }

                Console.WriteLine("Targeted Tiles : ");
                foreach (GameTile tile in modifiedTiles)
                {
                    Console.WriteLine("-> " + tile);
                }
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++\n\n");

                //calculate point

                modifiedTiles.Reverse();

                foreach (GameTile tile in modifiedTiles)
                {

                    if (tile.Seeds <= (int)RULES.ALLOW_CAPTURE_SEED_MAX && tile.Seeds >= (int)RULES.ALLOW_CAPTURE_SEED_MIN)
                    {
                        Console.WriteLine("add score > " + tile.Seeds);
                        score += tile.Seeds;
                        //remove seeds :
                        tile.Seeds = 0;
                    }
                    else
                    {
                        Console.WriteLine("seeds > 3 or < 2");
                        Console.WriteLine("score : {0}", score);
                        break;
                    }

                }
                Console.WriteLine("---------------------");


            }


            return score;


        }


        public override string ToString()
        {
            string s = "++++++++++++++++\n" + "+ Board state : \n" + 
                       "++++++++++++++++\n";

            foreach (GameTile tile in OwaleBoard)
            {
                s += tile.ToString() + "\n";
            }

            s += "_________________________________\n";

            return s;
        }
    }

}
