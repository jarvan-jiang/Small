using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LLK
{
    public partial class BlockControl : UserControl
    {
        public BlockControl()
        {
            InitializeComponent();
            BlStatus = BlockStatus.initial;
        }
        enum BlockStatus
        {
            initial,
            click,
            clear,
            line
        }
        BlockStatus BlStatus;
        public int Lx;
        public int Ly;
        public int value;
        public int ClickT = 0;

        public bool isClick
        {
            get
            {
                if (BlStatus == BlockStatus.click)
                    return true;
                else
                    return false;
            }
        }
        public bool isClear
        {
            get
            {
                if (BlStatus == BlockStatus.clear)
                    return true;
                else
                    return false;
            }
            set
            {
                BlStatus = BlockStatus.clear;
            }
        }
        public void _Click()
        {
            if (BlStatus == BlockStatus.initial)
            {
                BlStatus = BlockStatus.click;
                this.Refresh();
            }
        }
        public void unClick()
        {
            if (BlStatus == BlockStatus.click)
            {
                BlStatus = BlockStatus.initial;
                ClickT = 0;
                this.Refresh();
            }
        }
        public void Clear()
        {
            if (BlStatus == BlockStatus.click || BlStatus == BlockStatus.line)
            {
                BlStatus = BlockStatus.clear;
                ClickT = 0;
                value = 0;
                this.Refresh();
            }
        }
        public void DrawLine()
        {
            if (BlStatus == BlockStatus.clear)
            {
                BlStatus = BlockStatus.line;
                ClickT = 0;
                this.Refresh();
            }
        }
      

        private void BlockControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            switch (BlStatus)
            {
                case BlockStatus.clear:
                    g.DrawImage(Properties.Resources.img0, 0, 0); break;

                case BlockStatus.line:
                    switch (value)
                    {
                        case 0: g.DrawImage(Properties.Resources.img0, 0, 0); break;
                        case 11: g.DrawImage(Properties.Resources.line_1, 0, 0); break;
                        case 12: g.DrawImage(Properties.Resources.line_2, 0, 0); break;
                        case 13: g.DrawImage(Properties.Resources.line_3, 0, 0); break;
                        case 14: g.DrawImage(Properties.Resources.line_4, 0, 0); break;
                        case 15: g.DrawImage(Properties.Resources.line_5, 0, 0); break;
                        case 16: g.DrawImage(Properties.Resources.line_6, 0, 0); break;
                    }
                    break;
                case BlockStatus.click:
                    switch (value)
                    {
                        case 1: g.DrawImage(Properties.Resources.img_1, 0, 0); break;
                        case 2: g.DrawImage(Properties.Resources.img_2, 0, 0); break;
                        case 3: g.DrawImage(Properties.Resources.img_3, 0, 0); break;
                        case 4: g.DrawImage(Properties.Resources.img_4, 0, 0); break;
                        case 5: g.DrawImage(Properties.Resources.img_5, 0, 0); break;
                    }
                    break;
                case BlockStatus.initial:
                    switch (value)
                    {
                        case 1: g.DrawImage(Properties.Resources.img1, 0, 0); break;
                        case 2: g.DrawImage(Properties.Resources.img2, 0, 0); break;
                        case 3: g.DrawImage(Properties.Resources.img3, 0, 0); break;
                        case 4: g.DrawImage(Properties.Resources.img4, 0, 0); break;
                        case 5: g.DrawImage(Properties.Resources.img5, 0, 0); break;
                    }
                    break;
            }
        }

        public EventHandler MouseStaClick;

        private void BlockControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
                ClickT++;
            if (MouseStaClick != null)
                MouseStaClick(sender, e);
        }
    }
}
