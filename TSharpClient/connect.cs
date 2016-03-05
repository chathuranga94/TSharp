using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


using System.Windows.Forms;
namespace TSharpClient
{
    class connect
    {
        Thread thread;
        view gameView;

        public connect(view v)
        {
            gameView = v;
        }
       
        public void send(String cmd) {
            try
            {

                TcpClient socket = new TcpClient();
                socket.Connect("127.0.0.1", 6000);
                if (socket.Connected)
                {
                    NetworkStream stream = socket.GetStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    Byte[] msg = Encoding.ASCII.GetBytes(cmd);
                    writer.Write(msg);
                    Console.WriteLine("Request Sent");
                    writer.Close();
                    stream.Close();
                    socket.Close();
                }
            }
            catch
            {
                return;
            }

        
        }

        public void listen()
        {
            thread = new Thread(new ThreadStart(receiveData));
            thread.Start();
            Console.WriteLine("Connect.listen() recievings");
        }

        public void receiveData()
        { 
            try
            {
                Console.WriteLine("Recieve Data Running");
                TcpListener listner = new TcpListener(IPAddress.Parse("127.0.0.1"), 7000);
                string msg = null;
                listner.Start();
                parser p2 = new parser();
                while (true)
                {
                    try
                    {
                        Socket s = listner.AcceptSocket();
                        if (s.Connected)
                        {
                            NetworkStream stream = new NetworkStream(s);
                            List<Byte> inputStr = new List<byte>();
                            int asw = 0;
                            while (asw != -1)
                            {
                                asw = stream.ReadByte();
                                inputStr.Add((Byte)asw);
                            }

                            msg = Encoding.UTF8.GetString(inputStr.ToArray());
                            //Console.WriteLine(reply);
                            //stream.Close();
                            //listner .Stop();
                            //gameViwe.drow(msg);
                            p2.evaluate(msg);
                        }
                        //Console.WriteLine(msg);
                        gameView.ShowDialog();
                        

                    }
                    catch
                    {
                        //Console.WriteLine("Error");
                    }
                    
                }
            }
            catch
            {
                Console.WriteLine("Error- Finished");
                return ;
            }
            //listner.Stop();
        }

       




    }
}
