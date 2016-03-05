using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TSharpClient
{
    class MessageManager
    {
     
        TimeSpan timeDuration;
        TimeSpan sentTime = new TimeSpan(0,0,0,0,0);
        static view v;
        static connect con = new connect(v);
        bool sent;
        String msg;

     /**   public MessageManager(connect con)
        {
            timeDuration = new TimeSpan(0,0,0,0,1000);
            this.con = con;
        }**/

        public void send()
        {          
                con.send(msg);
                sent = true;
        }

        public void setMessage(String msg)
        {
            this.msg = msg;
        }


    }
}
