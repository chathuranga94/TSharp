﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSharpClient
{
    class AI
    {
       // hard coded array
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

        public static int playerLocatoin=0;
        public static List<int> coinLocations;
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

                if (game[j, i] == "W" || game[j, i] == "S" || game[j, i] == "B")
                {
                    parents[block] = -2;
                    continue;//do no add childs if it is a blocked cell
                }

                if (i > 0)
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
                    int tmp = (i) * 10 + j - 1;
                    if (parents[tmp] == -1)
                    {
                        parents[tmp] = block;
                        queue.Enqueue(tmp);
                    }
                }
                if (i < 9)
                {
                    int tmp = (i + 1) * 10 + j;
                    if (parents[tmp] == -1)
                    {
                        parents[tmp] = block;
                        queue.Enqueue(tmp);
                    }
                }
                if (j < 9)
                {
                    int tmp = (i) * 10 + j + 1;
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



    }
}