using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048
{
    public partial class Form2 : Form
    {
        Form1 f1;
        Form f3; 
        public Form2(Form1 f)
        {
            InitializeComponent();
            f1 = f; 
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                if (f1.Score > Convert.ToUInt32(f1.PlayerScorearr[i]) ) 
                {    
                    f1.PlayerNamearr.Insert(i,textBox1.Text);
                    f1.PlayerScorearr.Insert(i, f1.Score.ToString());
                    break; 
                }
             
            }

            f3 = new Form3(f1.PlayerNamearr, f1.PlayerScorearr);
            f3.Show(); 
            Close(); 
        }
    }
}
