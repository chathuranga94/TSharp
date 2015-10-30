using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TSharpClient
{
    public partial class view : Form
    {

        private string player;

        public view()
        {
            InitializeComponent();

            connect con = new connect(this);
            con.send("JOIN#");
            con.listen();
        }

        delegate void drowCallback(String msg);
        public void drow(String msg)
        {
            if(lblCMD.InvokeRequired)
            {
                drowCallback d = new drowCallback(drow);
                this.Invoke(d, new object[] { msg });
            }
            lblCMD.Text+= msg;
        }
        

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbl.GetControlFromPosition(1, 2).Text   = "fsf";
        }

        private void label58_Click(object sender, EventArgs e)
        {

        }

        private void view_Load(object sender, EventArgs e)
        {

        }


        public void set(String data)
        {

            if (data[0].ToString() == "S"){
                String temp = "";
                Console.WriteLine(data);
                Console.WriteLine(data.Substring(2, 4));

                if (data[9].ToString() == "0") { temp = "^"; }
                else if (data[9].ToString() == "1") { temp = ">"; }
                else if (data[9].ToString() == "2") { temp = "<"; }
                else if (data[9].ToString() == "3") { temp = "v"; }
                tbl.GetControlFromPosition(Int32.Parse(data[5].ToString()), Int32.Parse(data[7].ToString())).Text = temp; 

            }

            








        }

        public void join(String data) {
            //Console.WriteLine(data[5]);
            tbl.GetControlFromPosition(Int32.Parse(data[5].ToString()), Int32.Parse(data[7].ToString())).Text = "fsf";  
              
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            join("S:P1:1,1:0#");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //set("S:P1:4,6:1#");

            String cmd = "I:P1:2,3;3,4:4,4;5,5:6,7;9,9";
            lblCMD.Text = cmd;
            string[] info = cmd.Split(':');
            Console.WriteLine(info[2]);


            string[] brick = info[2].Split(';');
            foreach (string place in brick)
            {   
                tbl.GetControlFromPosition(Int32.Parse(place[0].ToString()), Int32.Parse(place[2].ToString())).BackColor = Color.Brown;
            }

            string[] stone = info[3].Split(';');
            foreach (string place in stone)
            {
                tbl.GetControlFromPosition(Int32.Parse(place[0].ToString()), Int32.Parse(place[2].ToString())).BackColor = Color.Black;
            }

            string[] water = info[4].Split(';');
            foreach (string place in water)
            {
                tbl.GetControlFromPosition(Int32.Parse(place[0].ToString()), Int32.Parse(place[2].ToString())).BackColor = Color.Blue;
            }



        }



        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void btnUP_Click(object sender, EventArgs e)
        {
            connect co = new connect(this);
            co.send("UP#");
        }

        private void btnLEFT_Click(object sender, EventArgs e)
        {
            connect co = new connect(this);
            co.send("LEFT#");
        }

        private void btnRIGHT_Click(object sender, EventArgs e)
        {
            connect co = new connect(this);
            co.send("RIGHT#");
        }

        private void btnDOWN_Click(object sender, EventArgs e)
        {
            connect co = new connect(this);
            co.send("DOWN#");
        }

        private void btnSHOOT_Click(object sender, EventArgs e)
        {
            connect co = new connect(this);
            co.send("SHOOT#");
        }

        private void button4_Click(object sender, EventArgs e)
        {

            String data = "S:P2:4,5:1";
            player = data.Substring(2,2);
            Console.WriteLine(player);


            if (data[0].ToString() == "S")
            {
                String temp = "";
                if (data[9].ToString() == "0") {    temp = "^";}
                else if (data[9].ToString() == "1") { temp = ">"; }
                else if (data[9].ToString() == "2") { temp = "<"; }
                else if (data[9].ToString() == "3") { temp = "v"; }
                tbl.GetControlFromPosition(Int32.Parse(data[5].ToString()), Int32.Parse(data[7].ToString())).Text = temp;
                tbl.GetControlFromPosition(Int32.Parse(data[5].ToString()), Int32.Parse(data[7].ToString())).BackColor = Color.Red;
            }



        }

    }

}
