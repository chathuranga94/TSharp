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
        view gameViwe;

        public connect(view v)
        {
            gameViwe = v;
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
        }

        public void receiveData()
        {
            try
            {
                TcpListener listner = new TcpListener(IPAddress.Parse("127.0.0.1"), 7000);
                string msg = null;
                listner.Start();
                while (true)
                {
                    Socket s = listner.AcceptSocket();
                    if (s.Connected)
                    {
                        NetworkStream stream = new NetworkStream(s);
                        List<Byte> inputStr = new List<byte>();
                        //Console.WriteLine("connected to server");
                        int asw = 0;
                        while (asw != -1)
                        {
                            asw = stream.ReadByte();
                            inputStr.Add((Byte)asw);
                        }

                        msg = Encoding.UTF8.GetString(inputStr.ToArray());
                        //Console.WriteLine(reply);
                        stream.Close();
                        //listner.Stop();

                        gameViwe.drow(msg);
                    }
                    System.Console.WriteLine(msg);
                    return ;
                }
            }
            catch
            {
                Console.WriteLine("Error");
                return ;
            }
        }




    }
}
