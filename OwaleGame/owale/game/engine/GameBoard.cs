using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwaleGame.owale.game.engine
{
    class GameBoard
    {
        Player player1;
        Player player2;

        static string PLAYER_1 = "Player 1";
        static string PLAYER_2 = "Player 2";

        static int END_SCORE = 25;

        GameState gameState;

        // 1 or 2
        int playerTurn;

        public GameBoard(int startingPLayer = 1)
        {
            player1 = new Player(PLAYER_1, 0);
            player2 = new Player(PLAYER_2, 0);
            gameState = new GameState();
            playerTurn = startingPLayer;
        }

        public void startGame()
        {
            while( Player1.Score != END_SCORE || Player2.Score != END_SCORE)
            {
                if (playerTurn == 1)
                {
                    Console.WriteLine("PLAYER 1 TURN : \n");
                } else
                {
                    Console.WriteLine("PLAYER 2 TURN : \n");
                }
            }
        }

        public void play(int position)
        {

            GameTile tile = gameState.OwaleBoard.Where(t => t.Id == position).Single();
            int score = 0;

            if (playerTurn == 1)
            {
                Console.WriteLine("PLAYER 1\n");
                if ( (tile.Type == TileTypeEnum.tileType.TILE_PLAYER_1) || (tile.Type == TileTypeEnum.tileType.END_TILE_PLAYER_1) || (tile.Type == TileTypeEnum.tileType.START_TILE_PLAYER_1) )
                {
                    score = GameState.sow(position);
                    player1.Score += score;
                } else
                {
                    Console.WriteLine("This house doesn't belong to you, as such you can't sow seeds from it\n");
                }

            } else
            {
                Console.WriteLine("PLAYER 2\n");
                if ((tile.Type == TileTypeEnum.tileType.TILE_PLAYER_2) || (tile.Type == TileTypeEnum.tileType.END_TILE_PLAYER_2) || (tile.Type == TileTypeEnum.tileType.START_TILE_PLAYER_2))
                {
                    score = GameState.sow(position);
                    player2.Score += score;
                }
                else
                {
                    Console.WriteLine("This house doesn't belong to you, as such you can't sow seeds from it\n");
                }
            }
        }


        public Player Player1 { get => player1; set => player1 = value; }
        public Player Player2 { get => player2; set => player2 = value; }
        public GameState GameState { get => gameState; set => gameState = value; }
        public int PlayerTurn { get => playerTurn; set => playerTurn = value; }
       
    }
}
