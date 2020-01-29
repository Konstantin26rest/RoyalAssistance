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
    public partial class ToTop : Form
    {
        public ToTop()
        {
            InitializeComponent();

            BackColor = Color.Lime;
            TransparencyKey = Color.Lime;
        }

        private int nD = 1;
        private int nDelta = 5;

        private void timer_move_Tick(object sender, EventArgs e)
        {
            int nY = picArrow.Location.Y;
            int nH = picArrow.Height;
            int HH = Height;

            if (nY + nH > HH) nD = -1;
            if (nY < 1) nD = 1;

            picArrow.Location = new Point(picArrow.Location.X, picArrow.Location.Y + nDelta * nD);
        }
    }
}
