using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jjwshz
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private string _num;

        public string PeopleNum
        {
            get { return _num; }
            set { _num = value; }
        }
        private string _name;

        public string PeopleName
        {
            get { return _name; }
            set { _name = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _num = textBox1.Text;
            _name = textBox2.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _num = "";
            _name = "";
            this.Close();
        }
    }
}
