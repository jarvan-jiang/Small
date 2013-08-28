using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace ShutDown
{
    public partial class MainForm : Form
    {
        TimeSpan ShutDownCount = new TimeSpan();
        public MainForm()
        {
            InitializeComponent();
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            btnPause.Enabled = false;
            dtCurTime.Format = DateTimePickerFormat.Custom;
            dtCurTime.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (rdCurTime.Checked)
            {
                if (dtCurTime.Value < DateTime.Now)
                {
                    MessageBox.Show("时间必须设置大于当前时间");
                    return;
                }
                ShutDownCount = dtCurTime.Value - DateTime.Now;
            }
            else if (rdDelay.Checked)
            {
                ValidInput(txtHour);
                ValidInput(txtMins);
                ShutDownCount = new TimeSpan(Convert.ToInt32(txtHour.Text), Convert.ToInt32(txtMins.Text), 0);
            }
            else
            {
                return;
            }
            btnStart.Enabled = false;
            rdCurTime.Enabled = false;
            rdDelay.Enabled = false;
            txtHour.Enabled = false;
            txtMins.Enabled = false;
            dtCurTime.Enabled = false;
            btnPause.Enabled = true;
            timer1.Start();
            btnStart.Text = "距离自动关机还有";
            CountLastTime();
        }
        private void ValidInput(TextBox t)
        {
            t.Text = t.Text == "" ? "0" : t.Text;
            if (!IsWholeNumber(t.Text) || Convert.ToInt32(t.Text) < 0)
            {
                MessageBox.Show("请输入合法数字");
                t.Text = "";
                return;
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (btnPause.Text == "暂停")
            {
                btnPause.Text = "继续";
                timer1.Stop();
            }
            else
            {
                btnPause.Text = "暂停";
                timer1.Start();
            }
        }

        private void btnImd_Click(object sender, EventArgs e)
        {
            OpenConfirm();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            rdCurTime.Enabled = true;
            rdDelay.Enabled = true;
            dtCurTime.Value = DateTime.Now;
            dtCurTime.Enabled = true;
            txtHour.Text = "";
            txtMins.Text = "";
            txtHour.Enabled = true;
            txtMins.Enabled = true;
            rdCurTime.Checked = false;
            rdDelay.Checked = false;
            btnPause.Text = "暂停";
            btnPause.Enabled = false;
            timer1.Stop();
            ShutDownCount = new TimeSpan();
            CountLastTime();
        }

        private void btnMinsize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ShutDownCount.TotalSeconds <= 0)
            {
                timer1.Stop();
                OpenConfirm();
                return;
            }
            ShutDownCount -= new TimeSpan(0, 0, 1);
            CountLastTime();

        }
        private void OpenConfirm()
        {
            LastConfirm l = new LastConfirm();
            l.StartPosition = FormStartPosition.CenterScreen;
            l.Show();
            l.Activate();
        }
        private void CountLastTime()
        {
            lbHour.Text = ShutDownCount.Hours == 0 ? "00" : ShutDownCount.Hours.ToString("D2");
            lbMins.Text = ShutDownCount.Minutes == 0 ? ":00:" : (":" + ShutDownCount.Minutes.ToString("D2") + ":");
            lbSec.Text = ShutDownCount.Seconds == 0 ? "00" : ShutDownCount.Seconds.ToString("D2");
            lbHour.Refresh();
            lbMins.Refresh();
            lbSec.Refresh();
        }
        public bool IsWholeNumber(string strNumber)
        {
            Regex notWholePattern = new Regex(@"^[-]?\d+[.]?\d*$");
            return notWholePattern.IsMatch(strNumber, 0);
        }

        private void itemBack_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }

        private void itemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
