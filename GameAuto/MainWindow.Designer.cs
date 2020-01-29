namespace GameAuto
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.lbconfig = new System.Windows.Forms.Label();
            this.timer_process = new System.Windows.Forms.Timer(this.components);
            this.lbProcessTime = new System.Windows.Forms.Label();
            this.lstBox = new System.Windows.Forms.ListBox();
            this.picBtnStart = new System.Windows.Forms.PictureBox();
            this.picBtnExit = new System.Windows.Forms.PictureBox();
            this.picRet2 = new System.Windows.Forms.PictureBox();
            this.picRet1 = new System.Windows.Forms.PictureBox();
            this.lbStep1 = new System.Windows.Forms.Label();
            this.lbStep2 = new System.Windows.Forms.Label();
            this.btnProceed1 = new System.Windows.Forms.Button();
            this.btnProceed2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAssistMode = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRet1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbconfig
            // 
            this.lbconfig.AutoSize = true;
            this.lbconfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbconfig.Location = new System.Drawing.Point(16, 33);
            this.lbconfig.Name = "lbconfig";
            this.lbconfig.Size = new System.Drawing.Size(240, 16);
            this.lbconfig.TabIndex = 1;
            this.lbconfig.Text = "Press \'A\' to on or off the assistant mode";
            // 
            // timer_process
            // 
            this.timer_process.Interval = 1000;
            this.timer_process.Tick += new System.EventHandler(this.timer_process_Tick);
            // 
            // lbProcessTime
            // 
            this.lbProcessTime.AutoSize = true;
            this.lbProcessTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProcessTime.Location = new System.Drawing.Point(32, 146);
            this.lbProcessTime.Name = "lbProcessTime";
            this.lbProcessTime.Size = new System.Drawing.Size(51, 20);
            this.lbProcessTime.TabIndex = 3;
            this.lbProcessTime.Text = "label1";
            // 
            // lstBox
            // 
            this.lstBox.FormattingEnabled = true;
            this.lstBox.Location = new System.Drawing.Point(117, 60);
            this.lstBox.Name = "lstBox";
            this.lstBox.Size = new System.Drawing.Size(189, 108);
            this.lstBox.TabIndex = 4;
            // 
            // picBtnStart
            // 
            this.picBtnStart.BackColor = System.Drawing.Color.Transparent;
            this.picBtnStart.Image = ((System.Drawing.Image)(resources.GetObject("picBtnStart.Image")));
            this.picBtnStart.Location = new System.Drawing.Point(12, 60);
            this.picBtnStart.Name = "picBtnStart";
            this.picBtnStart.Size = new System.Drawing.Size(82, 71);
            this.picBtnStart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBtnStart.TabIndex = 5;
            this.picBtnStart.TabStop = false;
            this.picBtnStart.Click += new System.EventHandler(this.picBtnStart_Click);
            // 
            // picBtnExit
            // 
            this.picBtnExit.Image = ((System.Drawing.Image)(resources.GetObject("picBtnExit.Image")));
            this.picBtnExit.Location = new System.Drawing.Point(269, 3);
            this.picBtnExit.Name = "picBtnExit";
            this.picBtnExit.Size = new System.Drawing.Size(44, 30);
            this.picBtnExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBtnExit.TabIndex = 5;
            this.picBtnExit.TabStop = false;
            this.picBtnExit.Click += new System.EventHandler(this.picBtnExit_Click);
            // 
            // picRet2
            // 
            this.picRet2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.picRet2.Location = new System.Drawing.Point(13, 560);
            this.picRet2.Name = "picRet2";
            this.picRet2.Size = new System.Drawing.Size(290, 290);
            this.picRet2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picRet2.TabIndex = 2;
            this.picRet2.TabStop = false;
            // 
            // picRet1
            // 
            this.picRet1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.picRet1.Location = new System.Drawing.Point(13, 233);
            this.picRet1.Name = "picRet1";
            this.picRet1.Size = new System.Drawing.Size(290, 290);
            this.picRet1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picRet1.TabIndex = 2;
            this.picRet1.TabStop = false;
            // 
            // lbStep1
            // 
            this.lbStep1.AutoSize = true;
            this.lbStep1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStep1.Location = new System.Drawing.Point(13, 213);
            this.lbStep1.Name = "lbStep1";
            this.lbStep1.Size = new System.Drawing.Size(45, 15);
            this.lbStep1.TabIndex = 3;
            this.lbStep1.Text = "Step 1 ";
            // 
            // lbStep2
            // 
            this.lbStep2.AutoSize = true;
            this.lbStep2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStep2.Location = new System.Drawing.Point(13, 541);
            this.lbStep2.Name = "lbStep2";
            this.lbStep2.Size = new System.Drawing.Size(42, 15);
            this.lbStep2.TabIndex = 3;
            this.lbStep2.Text = "Step 2";
            // 
            // btnProceed1
            // 
            this.btnProceed1.Enabled = false;
            this.btnProceed1.Location = new System.Drawing.Point(241, 199);
            this.btnProceed1.Name = "btnProceed1";
            this.btnProceed1.Size = new System.Drawing.Size(62, 29);
            this.btnProceed1.TabIndex = 6;
            this.btnProceed1.Text = "Proceed";
            this.btnProceed1.UseVisualStyleBackColor = true;
            this.btnProceed1.Click += new System.EventHandler(this.btnProceed1_Click);
            // 
            // btnProceed2
            // 
            this.btnProceed2.Enabled = false;
            this.btnProceed2.Location = new System.Drawing.Point(241, 527);
            this.btnProceed2.Name = "btnProceed2";
            this.btnProceed2.Size = new System.Drawing.Size(62, 29);
            this.btnProceed2.TabIndex = 6;
            this.btnProceed2.Text = "Proceed";
            this.btnProceed2.UseVisualStyleBackColor = true;
            this.btnProceed2.Click += new System.EventHandler(this.btnProceed2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Automation Bot";
            // 
            // chkAssistMode
            // 
            this.chkAssistMode.AutoSize = true;
            this.chkAssistMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAssistMode.Location = new System.Drawing.Point(187, 174);
            this.chkAssistMode.Name = "chkAssistMode";
            this.chkAssistMode.Size = new System.Drawing.Size(119, 20);
            this.chkAssistMode.TabIndex = 7;
            this.chkAssistMode.Text = "Assistant mode";
            this.chkAssistMode.UseVisualStyleBackColor = true;
            this.chkAssistMode.CheckedChanged += new System.EventHandler(this.btnModeAssist_CheckedChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(315, 863);
            this.Controls.Add(this.chkAssistMode);
            this.Controls.Add(this.btnProceed2);
            this.Controls.Add(this.btnProceed1);
            this.Controls.Add(this.picBtnStart);
            this.Controls.Add(this.picBtnExit);
            this.Controls.Add(this.lstBox);
            this.Controls.Add(this.lbStep2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbStep1);
            this.Controls.Add(this.lbProcessTime);
            this.Controls.Add(this.picRet2);
            this.Controls.Add(this.picRet1);
            this.Controls.Add(this.lbconfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainWindow";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbconfig;
        private System.Windows.Forms.Timer timer_process;
        private System.Windows.Forms.Label lbProcessTime;
        private System.Windows.Forms.PictureBox picRet1;
        private System.Windows.Forms.ListBox lstBox;
        private System.Windows.Forms.PictureBox picRet2;
        private System.Windows.Forms.PictureBox picBtnExit;
        private System.Windows.Forms.PictureBox picBtnStart;
        private System.Windows.Forms.Label lbStep1;
        private System.Windows.Forms.Label lbStep2;
        private System.Windows.Forms.Button btnProceed1;
        private System.Windows.Forms.Button btnProceed2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAssistMode;
    }
}

