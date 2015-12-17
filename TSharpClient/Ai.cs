using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSharpClient
{
    class AI
    {
       // hard coded array
        /*
        public static string[,] game = new string[10, 10]
        {   {"O0","","","","","","","","",""},
            {"","","","","","","","W","",""},
            {"W","","","B","","","","S","",""},
            {"S","S","W","","","","","","","W"},
            {"S","B","S","","","","","W","",""},
            {"","","","","","","O1","","",""},
            {"","","W","B","","","","S","S",""},
            {"","W","W","","","B","","","",""},
            {"W","","","","","B","B","W","","S"},
            {"","","","P3","","","","","",""}  };
        */



        public static int playerLocatoin=0;
        //public static List<int> coinLocations;
        public static connect netClient;


        public static List<int> getPath(int from, int to)
        {
            List<int> path = new List<int>();

            //Mark cells unvisited | when we visit them, mark with parent cell's id
            int[] parents = new int[100];
            for (int i = 0; i < 100; i++)
                parents[i] = -1;

            //Queue to enque tmperol cells
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(from);

            while (queue.Count > 0)
            {
                int block = queue.Dequeue();
                int i = block / 10, j = block % 10;

                if (parser.game[i, j] == "W" || parser.game[i, j] == "S" || parser.game[i, j] == "B")
                {
                    parents[block] = -2;
                    continue;//do no add childs if it is a blocked cell
                }

                if (j > 0)
                {
                    int tmp = (j - 1) * 10 + i;
                    if (parents[tmp] == -1)
                    {
                        parents[tmp] = block;
                        queue.Enqueue(tmp);
                    }
                }
                if (i > 0)
                {
                    int tmp = (j) * 10 + i - 1;
                    if (parents[tmp] == -1)
                    {
                        parents[tmp] = block;
                        queue.Enqueue(tmp);
                    }
                }
                if (j < 9)
                {
                    int tmp = (j + 1) * 10 + i;
                    if (parents[tmp] == -1)
                    {
                        parents[tmp] = block;
                        queue.Enqueue(tmp);
                    }
                }
                if (i < 9)
                {
                    int tmp = (j) * 10 + i + 1;
                    if (parents[tmp] == -1)
                    {
                        parents[tmp] = block;
                        queue.Enqueue(tmp);
                    }
                }
            }

            //Return null if we can't find a possible path
            if (parents[to] < 0) return path;


            //Find the path
            int movingCell = to;
            while (movingCell != from)
            {
                path.Add(movingCell);
                movingCell = parents[movingCell];
            }
            path.Reverse();
            return path;
        }
        public static String getMove()
        {
            //coinLocations.Add(11);
            //Find the closest coin
            int currentLoc = playerLocatoin;
            int minLoc = currentLoc;
            int minsDist = 100;
            foreach (dynamic coinLoc in parser.coins)
            {
                int dist = getPath(currentLoc, coinLoc).Count;
                if (dist < minsDist)
                {
                    minLoc = coinLoc;
                    minsDist = dist;
                }
            }


            if (minsDist == 100) return "SHOOT#";

            int from = playerLocatoin;
            int to = minLoc;

            List<int> path = getPath(from, to);

            if (path == null || path.Count == 0)
            {
                Console.WriteLine("Error:" + from + " " + to);
                return "SHOOT#";
            }

            Console.WriteLine(path[0]);
            //choosing the side to turn
            if (path[0] - from == 10) return "RIGHT#";
            if (path[0] - from == -10) return "LEFT#";
            if (path[0] - from == 1) return "DOWN#";
            else return "UP#";
        }
        public static void move()
        {
            String msg = getMove();
            Console.WriteLine(msg);
            netClient.send(msg);
        }

    }
}
