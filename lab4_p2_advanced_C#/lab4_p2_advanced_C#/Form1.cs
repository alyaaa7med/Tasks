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
    public partial class Form1 : Form
    {
        List<student> std = new List<student>();

        public Form1()
        {
            InitializeComponent();
            student s1 = new student { Id = 1, Name = "Alyaa", age = 22 };
            student s2 = new student { Id = 2, Name = "Omar", age = 21 };
            student s3 = new student { Id = 3, Name = "Mona", age = 23 };

            std.Add(s1);
            std.Add(s2);
            std.Add(s3);
            

            
            cbox.DataSource = std;
            cbox.ValueMember = "Id";
            cbox.DisplayMember = "Name";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public string user_name;
        public string password;

        private void btn_Click(object sender, EventArgs e)
        {
            user_name = txt_name.Text;
            password = txt_pass.Text;
            if(user_name == "alyaa" && password == "123")
            {
                MessageBox.Show("login successfully");
            }
            else
            {
                MessageBox.Show("user name or password not valid");
            }
        }
       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            student chosenstd = (student)cbox.SelectedItem;
            txtStddata.Text=(
                chosenstd.Id + "  " + chosenstd.Name + "  " + chosenstd.age);

        }
    }
}
