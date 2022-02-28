using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoGame
{
    public partial class Form2 : Form
    {   
        public Form2()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                MemoGame easy = new MemoGame();
                easy.Show();
                this.Hide();
            }
           else if (checkBox2.Checked)
            {
                Form3 hard = new Form3();
                hard.Show();
                this.Hide();
            }
        }
    }
}
