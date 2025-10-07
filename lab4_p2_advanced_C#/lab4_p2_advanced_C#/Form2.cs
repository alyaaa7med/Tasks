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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            ////to draw on the form 
            //Graphics grx = CreateGraphics();
            //if (e.Button == MouseButtons.Left) //draw
            //{
            //    grx.FillEllipse(Brushes.Red, e.X - 20, e.Y - 20, 40, 40);//circle is ellipse width = height 
            //}
            //else if (e.Button == MouseButtons.Right) //clear
            //{
            //    //var x = BackColor; to create a brush from the same color of back
            //    grx.FillEllipse(new SolidBrush(this.BackColor), e.X - 20, e.Y - 20, 40, 40);//circle is ellipse width = height 
            //}
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            //to draw on the form 
            Graphics grx = CreateGraphics();
            if (e.Button == MouseButtons.Left) //draw
            {
                grx.FillEllipse(Brushes.Red, e.X - 20, e.Y - 20, 40, 40);//circle is ellipse width = height 
            }
            else if (e.Button == MouseButtons.Right) //clear
            {
                //var x = BackColor; to create a brush from the same color of back
                grx.FillEllipse(new SolidBrush(this.BackColor), e.X - 20, e.Y - 20, 40, 40);//circle is ellipse width = height 
            }
        }
    }
}
