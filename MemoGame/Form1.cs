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
    public partial class MemoGame : Form
    {
        
        Random random = new Random();
        List<string> words = new List<string>(System.IO.File
            .ReadAllLines(System.Windows.Forms.Application.StartupPath +@"\Words.txt"));
        List<string> wordsa = new List<string>();
        int sec;
        int chances = 10;




        Label firstClick, secondClick;
        public MemoGame()
        {
            InitializeComponent();
            WordsToFields();
            lbChances.Text = "Chances left:" + chances;
        }

        private void WordsToFields()
        {
            Label label;
            Label labela;

            var randomField = Enumerable.Range(0, tableLayoutPanel1.Controls.Count)
                .OrderBy(g => Guid.NewGuid()).ToArray();
            var randomFieldNumb = randomField.ToList();

            int randomNumber;

            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = (Label)tableLayoutPanel1.Controls[randomFieldNumb[i]];
                labela = (Label)tableLayoutPanel1.Controls[randomFieldNumb[i + 1]];
                i = i + 1;
               

                randomNumber = random.Next(0, words.Count);
                label.Text = words[randomNumber];
                labela.Text = words[randomNumber];

                words.RemoveAt(randomNumber);

            }

        }

    

           
        public void field_Click(object sender, EventArgs e)
        {

            if (firstClick != null && secondClick != null)
                return;

            Label clickedField = sender as Label;

            if (clickedField == null)
                return;

            if (clickedField.ForeColor == Color.Black)
                return;
            if (firstClick == null)
            {
                firstClick = clickedField;
                firstClick.ForeColor = Color.Black;
                return;
            }

            secondClick = clickedField;
            secondClick.ForeColor = Color.Black;

            Victory();
            GameOver();
            if (firstClick.Text == secondClick.Text)
            {
                firstClick = null;
                secondClick = null;
            }
            else
            { 
                timer1.Start();         
            }   

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            firstClick.ForeColor = firstClick.BackColor;
            secondClick.ForeColor = secondClick.BackColor;

            firstClick = null;
            secondClick = null;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Start();
            sec += 1;
            lbTimer.Text = sec + " s";
        }

        private void Victory()
        {
            Label label;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label;

                if (label != null && label.ForeColor == label.BackColor)
                    return;
            }

            timer2.Stop();
          
            List<string> MessageList = new List<string>()
                {"Congratulations you have completed game within " + sec, 
                " seconds and with " +chances, " chances left. Retry?"};
            string delimiter = "";
            string messageBoxContent = String.Join(delimiter, MessageList);
            var win = MessageBox.Show(messageBoxContent,"Victory",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (win == DialogResult.Yes)
            {
                Form2 start = new Form2();
                start.Show();
                this.Close();
            }
            else
                Environment.Exit(0);

        }
        private void GameOver()
        {
            if (firstClick.Text != secondClick.Text)
            {
                chances -= 1;
                lbChances.Text = "Chances left: " +chances;
                if (chances == 0)
                {
                    timer2.Stop();
                    var lose = MessageBox.Show("Game over! Retry?", "Game over",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
                    if (lose == DialogResult.Yes)
                    {
                        Form2 start = new Form2();
                        start.Show();
                        this.Close();
                    }
                    else
                        Environment.Exit(0);
                }
            }

            
        }
      
    }
}
