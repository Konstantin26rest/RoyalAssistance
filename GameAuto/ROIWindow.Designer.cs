namespace GameAuto
{
    partial class ROIWindow
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
            this.timer_imgload = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.picCross = new System.Windows.Forms.PictureBox();
            this.picCheck = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCross)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // timer_imgload
            // 
            this.timer_imgload.Tick += new System.EventHandler(this.timer_imgload_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "*Please draw a rectangle to set ROI of game.";
            // 
            // picClose
            // 
            this.picClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picClose.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.picClose.Image = global::GameAuto.Properties.Resources.exit;
            this.picClose.Location = new System.Drawing.Point(744, 12);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(50, 50);
            this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picClose.TabIndex = 3;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // picCross
            // 
            this.picCross.BackColor = System.Drawing.SystemColors.ControlText;
            this.picCross.Image = global::GameAuto.Properties.Resources.cross;
            this.picCross.Location = new System.Drawing.Point(422, 101);
            this.picCross.Name = "picCross";
            this.picCross.Size = new System.Drawing.Size(50, 50);
            this.picCross.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCross.TabIndex = 2;
            this.picCross.TabStop = false;
            this.picCross.Visible = false;
            this.picCross.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCross_MouseDown);
            this.picCross.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCross_MouseUp);
            // 
            // picCheck
            // 
            this.picCheck.BackColor = System.Drawing.SystemColors.ControlText;
            this.picCheck.Image = global::GameAuto.Properties.Resources.check;
            this.picCheck.Location = new System.Drawing.Point(346, 101);
            this.picCheck.Name = "picCheck";
            this.picCheck.Size = new System.Drawing.Size(50, 50);
            this.picCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCheck.TabIndex = 2;
            this.picCheck.TabStop = false;
            this.picCheck.Visible = false;
            this.picCheck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCheck_MouseDown);
            this.picCheck.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCheck_MouseUp);
            // 
            // ROIWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(806, 453);
            this.Controls.Add(this.picClose);
            this.Controls.Add(this.picCross);
            this.Controls.Add(this.picCheck);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ROIWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ROIWindow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ROIWindow_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ROIWindow_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ROIWindow_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ROIWindow_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ROIWindow_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCross)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer_imgload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picCheck;
        private System.Windows.Forms.PictureBox picCross;
        private System.Windows.Forms.PictureBox picClose;
    }
}