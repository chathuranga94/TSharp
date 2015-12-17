using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TSharpClient
{
    class Program
    {
        static void Main(string[] args)
        {

            view v1 = new view();
            v1.ShowDialog();

            Console.WriteLine("fdf");

            /*
           connect con = new connect();
           con.send("JOIN#");
           con.listen();

           char str = ' ';
           while (str!='q')
           {
               str = Console.ReadKey().KeyChar;
               if (str == 'w')
                   con.send("UP#");
               else if (str == 's')
                   con.send("DOWN#");
               else if (str == 'a')
                   con.send("LEFT#");
               else if (str == 'd')
                   con.send("RIGHT#");
               else if (str == 'i')
                   con.send("SHOOT#");


           }*/


        }



        
    }
}
