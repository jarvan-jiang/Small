namespace ShutDown
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.rdCurTime = new System.Windows.Forms.RadioButton();
            this.rdDelay = new System.Windows.Forms.RadioButton();
            this.dtCurTime = new System.Windows.Forms.DateTimePicker();
            this.txtHour = new System.Windows.Forms.TextBox();
            this.txtMins = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnImd = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lbHour = new System.Windows.Forms.Label();
            this.lbMins = new System.Windows.Forms.Label();
            this.lbSec = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnMinsize = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemBack = new System.Windows.Forms.ToolStripMenuItem();
            this.itemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdCurTime
            // 
            this.rdCurTime.AutoSize = true;
            this.rdCurTime.Location = new System.Drawing.Point(45, 49);
            this.rdCurTime.Name = "rdCurTime";
            this.rdCurTime.Size = new System.Drawing.Size(71, 16);
            this.rdCurTime.TabIndex = 0;
            this.rdCurTime.TabStop = true;
            this.rdCurTime.Text = "定时关机";
            this.rdCurTime.UseVisualStyleBackColor = true;
            // 
            // rdDelay
            // 
            this.rdDelay.AutoSize = true;
            this.rdDelay.Location = new System.Drawing.Point(45, 106);
            this.rdDelay.Name = "rdDelay";
            this.rdDelay.Size = new System.Drawing.Size(71, 16);
            this.rdDelay.TabIndex = 1;
            this.rdDelay.TabStop = true;
            this.rdDelay.Text = "延迟关机";
            this.rdDelay.UseVisualStyleBackColor = true;
            // 
            // dtCurTime
            // 
            this.dtCurTime.Location = new System.Drawing.Point(156, 47);
            this.dtCurTime.Name = "dtCurTime";
            this.dtCurTime.Size = new System.Drawing.Size(170, 21);
            this.dtCurTime.TabIndex = 2;
            // 
            // txtHour
            // 
            this.txtHour.Location = new System.Drawing.Point(156, 104);
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(44, 21);
            this.txtHour.TabIndex = 3;
            // 
            // txtMins
            // 
            this.txtMins.Location = new System.Drawing.Point(241, 104);
            this.txtMins.Name = "txtMins";
            this.txtMins.Size = new System.Drawing.Size(48, 21);
            this.txtMins.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(206, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "小时";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(295, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "分";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(40, 173);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(278, 38);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "启动自动关机";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnImd
            // 
            this.btnImd.Location = new System.Drawing.Point(143, 294);
            this.btnImd.Name = "btnImd";
            this.btnImd.Size = new System.Drawing.Size(75, 26);
            this.btnImd.TabIndex = 8;
            this.btnImd.Text = "立即关机";
            this.btnImd.UseVisualStyleBackColor = true;
            this.btnImd.Click += new System.EventHandler(this.btnImd_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(40, 294);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 26);
            this.btnPause.TabIndex = 9;
            this.btnPause.Text = "暂停";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(241, 294);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 26);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lbHour
            // 
            this.lbHour.AutoSize = true;
            this.lbHour.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHour.Location = new System.Drawing.Point(98, 238);
            this.lbHour.Name = "lbHour";
            this.lbHour.Size = new System.Drawing.Size(53, 32);
            this.lbHour.TabIndex = 11;
            this.lbHour.Text = "00";
            // 
            // lbMins
            // 
            this.lbMins.AutoSize = true;
            this.lbMins.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMins.Location = new System.Drawing.Point(140, 238);
            this.lbMins.Name = "lbMins";
            this.lbMins.Size = new System.Drawing.Size(75, 32);
            this.lbMins.TabIndex = 12;
            this.lbMins.Text = ":00:";
            // 
            // lbSec
            // 
            this.lbSec.AutoSize = true;
            this.lbSec.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSec.Location = new System.Drawing.Point(206, 238);
            this.lbSec.Name = "lbSec";
            this.lbSec.Size = new System.Drawing.Size(53, 32);
            this.lbSec.TabIndex = 13;
            this.lbSec.Text = "00";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(184, 337);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 26);
            this.btnExit.TabIndex = 14;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnMinsize
            // 
            this.btnMinsize.Location = new System.Drawing.Point(85, 337);
            this.btnMinsize.Name = "btnMinsize";
            this.btnMinsize.Size = new System.Drawing.Size(75, 26);
            this.btnMinsize.TabIndex = 15;
            this.btnMinsize.Text = "最小化";
            this.btnMinsize.UseVisualStyleBackColor = true;
            this.btnMinsize.Click += new System.EventHandler(this.btnMinsize_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "自动关机";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemBack,
            this.itemExit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            // 
            // itemBack
            // 
            this.itemBack.Name = "itemBack";
            this.itemBack.Size = new System.Drawing.Size(152, 22);
            this.itemBack.Text = "恢复";
            this.itemBack.Click += new System.EventHandler(this.itemBack_Click);
            // 
            // itemExit
            // 
            this.itemExit.Name = "itemExit";
            this.itemExit.Size = new System.Drawing.Size(152, 22);
            this.itemExit.Text = "退出";
            this.itemExit.Click += new System.EventHandler(this.itemExit_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 429);
            this.Controls.Add(this.btnMinsize);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lbSec);
            this.Controls.Add(this.lbMins);
            this.Controls.Add(this.lbHour);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnImd);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMins);
            this.Controls.Add(this.txtHour);
            this.Controls.Add(this.dtCurTime);
            this.Controls.Add(this.rdDelay);
            this.Controls.Add(this.rdCurTime);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "自动关机";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdCurTime;
        private System.Windows.Forms.RadioButton rdDelay;
        private System.Windows.Forms.DateTimePicker dtCurTime;
        private System.Windows.Forms.TextBox txtHour;
        private System.Windows.Forms.TextBox txtMins;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnImd;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lbHour;
        private System.Windows.Forms.Label lbMins;
        private System.Windows.Forms.Label lbSec;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnMinsize;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem itemBack;
        private System.Windows.Forms.ToolStripMenuItem itemExit;
        private System.Windows.Forms.Timer timer1;
    }
}

