using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;

namespace MineSweeping
{
    public partial class MineControl : UserControl
    {
        enum ButtonStatus
        {
            initial,
            press,
            flag,
            open
            //disable
        }
        ButtonStatus BtnStatus;
        public int mouseStatus;
        public int Lx;
        public int Ly;
        public int value;
        public bool IsOpen
        {
            get
            {
                if (BtnStatus == ButtonStatus.open)
                    return true;
                else
                    return false;
            }
        }
        public bool IsFlag
        {
            get
            {
                if (BtnStatus == ButtonStatus.flag)
                    return true;
                else
                    return false;
            }
        }
        public MineControl()
        {
            InitializeComponent();
            BtnStatus = ButtonStatus.initial;
            mouseStatus = 0;
        }
        public void Press()
        {
            if (BtnStatus == ButtonStatus.initial)
            {
                BtnStatus = ButtonStatus.press;
                this.Refresh();
            }
        }
        public void UnPress()
        {
            if (BtnStatus == ButtonStatus.press)
            {
                BtnStatus = ButtonStatus.initial;
                this.Refresh();
            }
        }
        public void Putflag()
        {
            if (BtnStatus == ButtonStatus.initial)
            {
                BtnStatus = ButtonStatus.flag;
                this.Refresh();
            }
            else if (BtnStatus == ButtonStatus.flag)
            {
                BtnStatus = ButtonStatus.initial;
                this.Refresh();
            }
        }

        public void Open()
        {
            if (BtnStatus == ButtonStatus.press || BtnStatus == ButtonStatus.initial)
            {
                BtnStatus = ButtonStatus.open;
                this.Refresh();
            }
        }

        //public void disable()
        //{
        //    if (BtnStatus == ButtonStatus.disable)
        //        BtnStatus = ButtonStatus.disable;
        //    this.Refresh();

        //}
        public EventHandler MouseStatusDown;
        public EventHandler MouseStatusUp;
        System.Timers.Timer tLeft = new System.Timers.Timer(10);
        private int lcount = 0;
        System.Timers.Timer tRight = new System.Timers.Timer(10);
        private int rcount = 0;

        private void lCount(object source, System.Timers.ElapsedEventArgs e)
        {
            lcount++;
        }
        private void rCount(object source, System.Timers.ElapsedEventArgs e)
        {
            rcount++;
        }

        private void MineControl_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                tLeft.Elapsed += new System.Timers.ElapsedEventHandler(lCount);
                tLeft.SynchronizingObject = this;
                tLeft.Start();
                if (rcount > 5)
                    this.mouseStatus = 3;
                else
                    this.mouseStatus = 1;
            }
            else if (e.Button == MouseButtons.Right)
            {
                tRight.Elapsed += new System.Timers.ElapsedEventHandler(rCount);
                tRight.SynchronizingObject = this;
                tRight.Start();
                if (lcount > 5)
                    this.mouseStatus = 3;
                else
                    this.mouseStatus = 2;
            }
            else if (e.Button == MouseButtons.Middle)
                this.mouseStatus = 3;
            if (MouseStatusDown != null)
                MouseStatusDown(sender, e);
        }

        private void MineControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (rcount > 5)
                    this.mouseStatus = 6;
                else
                    this.mouseStatus = 4;
                lcount = 0;
                tLeft.Stop();
                tLeft.Dispose();
                tLeft = new System.Timers.Timer(10);
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (lcount > 5)
                    this.mouseStatus = 6;
                else
                    this.mouseStatus = 5;
                rcount = 0;
                tRight.Stop();
                tLeft.Dispose();
                tRight = new System.Timers.Timer(10);
            }
            else if (e.Button == MouseButtons.Middle)
                this.mouseStatus = 6;
            if (MouseStatusUp != null)
                MouseStatusUp(sender, e);

        }

        private void MineControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            switch (BtnStatus)
            {
                case ButtonStatus.initial:
                    g.DrawImage(Properties.Resources.bt, 0, 0); break;
                case ButtonStatus.press:
                    g.DrawImage(Properties.Resources.empty, 0, 0); break;
                case ButtonStatus.flag:
                    g.DrawImage(Properties.Resources.flag, 0, 0); break;
                case ButtonStatus.open:
                    switch (value)
                    {
                        case 0:
                            g.DrawImage(Properties.Resources.empty, 0, 0); break;
                        case 1:
                            g.DrawImage(Properties.Resources._1, 0, 0); break;
                        case 2:
                            g.DrawImage(Properties.Resources._2, 0, 0); break;
                        case 3:
                            g.DrawImage(Properties.Resources._3, 0, 0); break;
                        case 4:
                            g.DrawImage(Properties.Resources._4, 0, 0); break;
                        case 5:
                            g.DrawImage(Properties.Resources._5, 0, 0); break;
                        case 6:
                            g.DrawImage(Properties.Resources._6, 0, 0); break;
                        case 7:
                            g.DrawImage(Properties.Resources._7, 0, 0); break;
                        case 8:
                            g.DrawImage(Properties.Resources._8, 0, 0); break;
                        case 9:
                            g.DrawImage(Properties.Resources.mine, 0, 0); break;
                    }
                    break;
            }

        }

        private void MineControl_Load(object sender, EventArgs e)
        {

        }
    }
}
