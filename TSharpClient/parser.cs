using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSharpClient
{
    class parser
    {
        //public static string[,] game = new string[3, 2] { { "one", "two" }, { "three", "four" }, { "five", "six" } };

        public static string[,] game = new string[10,10];
        public static string playerName;
        public static int playerLocation;
        public static int playerDirection;
        public static List<int> coins;
        public static List<int> lifes;
        
        public parser() {
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++){
                    game[i,j] = "0";
                }
            
            }

            coins = new List<int>();
            lifes = new List<int>();
        }

        public void printMap(){
            for (int i = 0; i < 10; i++){
                for (int j = 0; j < 10; j++){
                    Console.Write(game[i,j] + " ");    
                }
                Console.WriteLine();
            }
        }

        public void evaluate(string msg){
            //Console.WriteLine(msg);


            //Game Initiation Command. We analyze Player Name and Obstacles from this command.
            if (msg.Substring(0, 2) == "I:")
            {
                //Console.WriteLine("I Command IN");
                String[] arr = msg.Split(':', '#');

                //Get Player Name
                playerName = arr[1];

                //Add bricks,stones,water------------
                String[] brick = arr[2].Split(';');
                for (int i = 0; i < brick.Length; i++)
                {
                    int x = int.Parse(brick[i][0].ToString());
                    int y = int.Parse(brick[i][2].ToString());
                    game[x, y] = "B";
                }

                String[] stone = arr[3].Split(';');
                for (int i = 0; i < stone.Length; i++)
                {
                    int x = int.Parse(stone[i][0].ToString());
                    int y = int.Parse(stone[i][2].ToString());
                    game[x, y] = "S";
                }

                String[] water = arr[4].Split(';');
                for (int i = 0; i < water.Length; i++)
                {
                    int x = int.Parse(water[i][0].ToString());
                    int y = int.Parse(water[i][2].ToString());
                    game[x, y] = "W";
                }

                printMap();
            }


            // Intial starting coordinates of our user is determined here.
            if (msg.Substring(0, 2) == "S:"){
                String[] arr = msg.Split(':', ';', '#');

                int x = int.Parse(arr[2][0].ToString());
                int y = int.Parse(arr[2][2].ToString());

                playerLocation = x * 10 + y;
                playerDirection = int.Parse(arr[3][0].ToString());

                Console.WriteLine( "Player " + playerLocation + "  " + playerDirection);
            }


            // Check for coin notifications and add location to coins list.
            if (msg.Substring(0, 2) == "C:"){
                String[] arr = msg.Split(':', '#');

                int x = int.Parse(arr[1][0].ToString());
                int y = int.Parse(arr[1][2].ToString());

                int z = 10 * x + y;
                Console.WriteLine("Coin in " + z);
                coins.Add(z);
            }


            // Check for life notifications and locations to lifes list.
            if (msg.Substring(0, 2) == "L:"){
                String[] arr = msg.Split(':', '#');

                int x = int.Parse(arr[1][0].ToString());
                int y = int.Parse(arr[1][2].ToString());

                int z = 10 * x + y;
                Console.WriteLine("Life in " + z);
                lifes.Add(z);
            }



            //  G COMMAND CODE HERE.....
            //
            //
            //
            //  ........






            ///////////////////CALL AI HERE///////
            AI.move();
            





        }

    }
}
