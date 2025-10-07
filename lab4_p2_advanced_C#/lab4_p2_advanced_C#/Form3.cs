using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4_p2_advanced_C_
{
    public partial class Form3 : Form
    {
        bool tog = false ,start_stop = false ;
        int x=0, y = 0;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Form3_Paint(object sender, PaintEventArgs e)
        {
            Graphics grx = e.Graphics;
            //(0,0) the top left corner   // (x,0)
            //(0,y)                       // (x,y)
            grx.FillEllipse(Brushes.Red, x, y, 30, 30);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true) button1.Text = "start";
            else if (timer1.Enabled == false) button1.Text = "stop";
            timer1.Enabled = !timer1.Enabled; //togle the timer 

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //each tic = each 500ms 
            if(x >= this.Width-30)
            {
                tog = true;
            }
            if(x<0)
            {
                tog = false;
            }
            if (tog  == false)
            {
                x += 5;
            }
            if (tog == true)
            {
                x -= 5;
            }

            this.Invalidate(); // to redraw the form again 
        }
    }
}
