using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameAuto
{
    public partial class ToRight : Form
    {
        public ToRight()
        {
            InitializeComponent();

            BackColor = Color.Lime;
            TransparencyKey = Color.Lime;
        }

        private int nD = 1;
        private int nDelta = 5;

        private void timer_move_Tick(object sender, EventArgs e)
        {
            int nX = picArrow.Location.X;
            int nW = picArrow.Width;
            int WW = Width/2;

            if (nX + nW > WW) nD = -1;
            if (nX < 1) nD = 1;

            picArrow.Location = new Point(picArrow.Location.X + nDelta * nD, picArrow.Location.Y);
        }
    }
}
