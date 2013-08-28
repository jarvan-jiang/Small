using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public partial class BaseBlock : UserControl
    {
        public BaseBlock()
        {
            InitializeComponent();
        }
        private bool _active = false;

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
        private int _x, _y;
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Size = new Size(16, 16);
            this.BackColor = _active ? Color.Black : Color.White;
        }

        private void BaseBlock_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = _active ? Color.Black : Color.White;
        }
        //public void ActiveBlock(bool flag)
        //{
        //    this.BackColor = flag ? Color.Black : Color.White;
        //    this.Refresh();
        //}
        
    }
}
