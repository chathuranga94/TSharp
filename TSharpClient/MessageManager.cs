using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSharpClient
{
    class MessageManager
    {
        String msg;
        TimeSpan timeDuration;
        TimeSpan sentTime;
        connect con;
        bool sent;
        public MessageManager(connect con)
        {
            timeDuration = new TimeSpan(0,0,0,1,0);
            this.con = con;
        }

        public void refresh(TimeSpan t)
        {
            if ((t - sentTime) > timeDuration)
            {
                con.send(msg);
                sent = true;
            }

        }

        public void setMessage(String msg)
        {
            this.msg = msg;
            sent = false;
        }

    }
}
