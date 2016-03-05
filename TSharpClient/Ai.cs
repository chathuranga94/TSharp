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



        public static int playerLocation=0;
        //public static List<int> coinLocations;
        static view v;
        //public  static connect netClient; --------------i added next line instead this
        static connect netClient = new connect(v);
        


        public static List<int> getPath(int from, int to)
        {
            List<int> path = new List<int>();

            // mark cells with parent cells id
            int[] parents = new int[100];
            for (int i = 0; i < 100; i++)
                parents[i] = -1;

           
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(from);

            while (queue.Count > 0)
            {
                int block = queue.Dequeue();
                int i = block / 10, j = block % 10;

                if (parser.game[i, j] == "W" || parser.game[i, j] == "S" || parser.game[i, j] == "B")
                {
                    parents[block] = -2;
                    continue;//no children to blocked cells
                }

                if (i > 0)//left side
                {
                    int tmp = (i - 1) * 10 + j;
                    if (parents[tmp] == -1)
                    {
                        parents[tmp] = block;
                        queue.Enqueue(tmp);
                    }
                }
                if (j > 0)
                {
                    int tmp = (i) * 10 + j - 1;//up
                    if (parents[tmp] == -1)
                    {
                        parents[tmp] = block;
                        queue.Enqueue(tmp);
                    }
                }
                if (i < 9)
                {
                    int tmp = (i + 1) * 10 + j;//right
                    if (parents[tmp] == -1)
                    {
                        parents[tmp] = block;
                        queue.Enqueue(tmp);
                    }
                }
                if (j < 9)
                {
                    int tmp = (i) * 10 + j + 1;//down
                    if (parents[tmp] == -1)
                    {
                        parents[tmp] = block;
                        queue.Enqueue(tmp);
                    }
                }
            }

            //null if no path
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
            int currentLoc = playerLocation;
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

            int from = playerLocation;
            int to = minLoc;

            List<int> path = getPath(from, to);

            if (path == null || path.Count == 0)
            {
                Console.WriteLine("Error:" + from + " " + to);
                return "SHOOT#";
            }
            /////// ravi added this
            Console.WriteLine(path);
            ////////////

            Console.WriteLine(path[0]);
            //choosing which side to turn
            if (path[0] - from == 10) return "RIGHT#";
            if (path[0] - from == -10) return "LEFT#";
            if (path[0] - from == 1) return "DOWN#";
            else return "UP#";
        }
        public static void move()// sending msg to the net client
        {
            String msg = getMove();
            Console.WriteLine(msg);
            netClient.send(msg);
        }

    }
}
