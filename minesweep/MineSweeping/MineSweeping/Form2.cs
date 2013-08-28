using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;
namespace MineSweeping
{
    public partial class Form2 : Form
    {
        public ArrayList al = new ArrayList();
        public bool begin = false;
        public Form2()
        {
            InitializeComponent();
            
        }
        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (!IsNumR(textBoxHeight.Text) || !IsNumR(textBoxWidth.Text) || !IsNumR(textBoxMines.Text))
            {
                MessageBox.Show("不是整数喔 o(>_<)o ");
                ResetBtn_Click(sender, e);
            }
            else if (Convert.ToInt32(textBoxHeight.Text) * Convert.ToInt32(textBoxWidth.Text) < Convert.ToInt32(textBoxMines.Text))
            {
                MessageBox.Show("雷太多啦 ╮(╯▽╰)╭");
                ResetBtn_Click(sender, e);
            }
            else
            {
                /*Form1 f1 = new Form1(Convert.ToInt32(textBoxHeight.Text), Convert.ToInt32(textBoxWidth.Text), Convert.ToInt32(textBoxMines.Text));
                f1.Show();
                f1.start_game();*/
                al.Add(""); al.Add(""); al.Add("");
                al[0]=(Convert.ToInt32(textBoxHeight.Text));
                al[1]=(Convert.ToInt32(textBoxWidth.Text));
                al[2]=(Convert.ToInt32(textBoxMines.Text));
                begin = true;
                this.Close();
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            textBoxHeight.Text = "";
            textBoxWidth.Text = "";
            textBoxMines.Text = "";
        }

        private bool IsNumR(string str)
        {
            double r;
            if (!double.TryParse(str, out   r)) return false;
            if (r <= 0) return false;
            if ((int)r != r) return false;
            return true;
        }

    }
}
