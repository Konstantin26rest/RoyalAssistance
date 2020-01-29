namespace GameAuto
{
    partial class ToRight2
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
            this.picArrow = new System.Windows.Forms.PictureBox();
            this.timer_move = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picArrow)).BeginInit();
            this.SuspendLayout();
            // 
            // picArrow
            // 
            this.picArrow.Image = global::GameAuto.Properties.Resources.to_right2;
            this.picArrow.Location = new System.Drawing.Point(0, 0);
            this.picArrow.Name = "picArrow";
            this.picArrow.Size = new System.Drawing.Size(25, 20);
            this.picArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picArrow.TabIndex = 0;
            this.picArrow.TabStop = false;
            // 
            // timer_move
            // 
            this.timer_move.Enabled = true;
            this.timer_move.Interval = 20;
            this.timer_move.Tick += new System.EventHandler(this.timer_move_Tick);
            // 
            // ToRight2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(60, 20);
            this.Controls.Add(this.picArrow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ToRight2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Arrow";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.picArrow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picArrow;
        private System.Windows.Forms.Timer timer_move;
    }
}