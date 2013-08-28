using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ShutDown
{
    public partial class LastConfirm : Form
    {
        public LastConfirm()
        {
            InitializeComponent();
        }

        private void LastConfirm_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(button2.Text.Replace("取消(", "").Replace(")", ""));
            if (count == 0)
            {
                timer1.Stop();
                timer1.Dispose();
                ShutDown();
            }
            count--;
            button2.Text = "取消(" + count + ")";
            button2.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShutDown();
            timer1.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            timer1.Stop();
        }
        private void ShutDown()
        {
            //MessageBox.Show("关机");
            timer1.Stop();

            //System.Diagnostics.Process.Start("shutdown", "/s /t 0 /f");
            Process.Start("c:/windows/system32/shutdown.exe", "-s -t 10 -f");
            //Process p = new Process();

            //p.StartInfo.FileName = "cmd.exe";

            //p.StartInfo.RedirectStandardError = true;

            //p.StartInfo.RedirectStandardInput = true;

            //p.StartInfo.RedirectStandardOutput = true;

            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.WorkingDirectory = "c:\\";
            //p.Start();

            //p.StandardInput.WriteLine("shutdown -s -t 0 -f");

            ////p.StandardInput.WriteLine("Exit");

            ////p.WaitForExit();

            //p.Close();



            this.Close();
        }
    }
}
