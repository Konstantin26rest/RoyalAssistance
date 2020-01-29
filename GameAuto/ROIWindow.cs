using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Configuration;

namespace GameAuto
{
    public partial class ROIWindow : Form
    {
        public Bitmap m_bmpScr = null;

        public ROIWindow()
        {
            InitializeComponent();
        }

        private void ROIWindow_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            timer_imgload.Enabled = true;
        }

        private void timer_imgload_Tick(object sender, EventArgs e)
        {
            timer_imgload.Enabled = false;
            BackgroundImage = m_bmpScr;

            int W = Width; int H = Height;
            int W1 = picCheck.Width + picCross.Width + 50;
            int X1 = (W - W1) / 2; int Y = H / 20;

            picCheck.Location = new Point(X1, Y);
            picCross.Location = new Point(X1 + 50 + picCheck.Width, Y);
        }

        private bool m_bROIDrawing = false;
        private Point m_ptStart = Point.Empty;
        private Point m_ptEnd = Point.Empty;

        private void ROIWindow_MouseDown(object sender, MouseEventArgs e)
        {
            picCheck.Visible = false; picCross.Visible = false;
            m_bROIDrawing = true;
            m_ptStart = e.Location;
        }

        private void ROIWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (!m_bROIDrawing)
                return;
            
            m_ptEnd = e.Location;
            Invalidate();
        }

        private void ROIWindow_MouseUp(object sender, MouseEventArgs e)
        {
            m_bROIDrawing = false;

            int W = Math.Abs(m_ptEnd.X - m_ptStart.X);
            int H = Math.Abs(m_ptEnd.Y - m_ptStart.Y);

            if (W < 100 || H < 100) return;

            picCheck.Visible = true;
            picCross.Visible = true;
        }

        private int MIN(int x, int y)
        {
            return x > y ? y : x;
        }

        private void ROIWindow_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if ( m_ptEnd != Point.Empty && m_ptStart != Point.Empty )
            {
                Pen p = new Pen(Color.FromArgb(255, 0, 0), 3);
                p.DashStyle = DashStyle.Dash;

                int X = MIN(m_ptStart.X, m_ptEnd.X); int Y = MIN(m_ptStart.Y, m_ptEnd.Y);
                int W = Math.Abs(m_ptEnd.X - m_ptStart.X); int H = Math.Abs(m_ptEnd.Y - m_ptStart.Y);

                Rectangle rect = new Rectangle(new Point(X, Y), new Size(W, H));
                g.DrawRectangle(p, rect);
            }

            if ( Global.g_rcROI != Rectangle.Empty )
            {
                Pen p1 = new Pen(Color.FromArgb(0, 0, 255), 4);
                g.DrawRectangle(p1, Global.g_rcROI);
            }
        }

        private void picCheck_MouseDown(object sender, MouseEventArgs e)
        {
            picCheck.BackColor = Color.DarkGray;
        }

        private void picCheck_MouseUp(object sender, MouseEventArgs e)
        {
            picCheck.BackColor = Color.Black;
            picCheck.Visible = false; picCross.Visible = false;

            int X = MIN(m_ptStart.X, m_ptEnd.X); int Y = MIN(m_ptStart.Y, m_ptEnd.Y);
            int W = Math.Abs(m_ptEnd.X - m_ptStart.X); int H = Math.Abs(m_ptEnd.Y - m_ptStart.Y);

            m_ptEnd = Point.Empty;
            m_ptStart = Point.Empty;

            Global.g_rcROI = new Rectangle(X, Y, W, H);
            
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings.Remove("ROIx");
            config.AppSettings.Settings.Add("ROIx", Convert.ToString(Global.g_rcROI.X));
            config.AppSettings.Settings.Remove("ROIy");
            config.AppSettings.Settings.Add("ROIy", Convert.ToString(Global.g_rcROI.Y));
            config.AppSettings.Settings.Remove("ROIw");
            config.AppSettings.Settings.Add("ROIw", Convert.ToString(Global.g_rcROI.Width));
            config.AppSettings.Settings.Remove("ROIh");
            config.AppSettings.Settings.Add("ROIh", Convert.ToString(Global.g_rcROI.Height));
            config.Save(ConfigurationSaveMode.Minimal);

            Invalidate();
        }

        private void picCross_MouseDown(object sender, MouseEventArgs e)
        {
            picCross.BackColor = Color.DarkGray;
        }

        private void picCross_MouseUp(object sender, MouseEventArgs e)
        {
            picCross.BackColor = Color.Black;
            picCheck.Visible = false; picCross.Visible = false;
            m_ptEnd = Point.Empty;
            m_ptStart = Point.Empty;

            Invalidate();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
