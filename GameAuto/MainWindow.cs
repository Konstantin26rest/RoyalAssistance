using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Cvb;
using Emgu.CV.Structure;

namespace GameAuto
{
    public partial class MainWindow : Form
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (vkCode == 65) // pressed 'A'
                    Global.g_MainWindow.ProcessClickOnAssistant();
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private static CvBlobDetector _blobDetector;
        private List<int> m_LstCharacter = new List<int>();
        private Image<Gray, Byte> mPrevScoreImage = null;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSetROI_Click(object sender, EventArgs e)
        {
            Point pt = Location;
            Location = new Point(-2000, -2000);

            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            ROIWindow roiWnd = new ROIWindow();
            roiWnd.m_bmpScr = bitmap;
            roiWnd.ShowDialog();

            Location = pt;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Global.g_MainWindow = this;

            _blobDetector = new CvBlobDetector();
            Mat mat = new Mat(".\\Res\\subImage.png",Emgu.CV.CvEnum.LoadImageType.Grayscale);
            mPrevScoreImage = mat.ToImage<Gray, Byte>();

            Location = new Point(Screen.PrimaryScreen.Bounds.Width - Width, Screen.PrimaryScreen.Bounds.Height- Height);

            _hookID = SetHook(_proc);
        }
        
        private MCvScalar[] cols = { new MCvScalar(0,0,0),
            new MCvScalar(255,255,255), new MCvScalar(0,125,255),
            new MCvScalar(125,0,255), new MCvScalar(125,125,0),
            new MCvScalar(255,0,0), new MCvScalar(0,255,0),
            new MCvScalar(0,0,255), new MCvScalar(0,255,255), 
            new MCvScalar(255,255,0), new MCvScalar(255,0,255),};

        private Mat GetScreenCapture()
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            bitmap.Save("4.png");

            return new Mat("4.png", Emgu.CV.CvEnum.LoadImageType.Color);
        }

        private Rectangle GetBlackGameBoxRegion(Image<Gray, Byte> imgGray)
        {
            int nWid = imgGray.Width; int nHei = imgGray.Height;

            byte[,,] pData = imgGray.Data;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    byte c = pData[y, x, 0];
                    if (c > 5) pData[y, x, 0] = 0;
                    else pData[y, x, 0] = 255;
                }
            }

            CvBlobs blobs = new CvBlobs();
            _blobDetector.Detect(imgGray, blobs);
            blobs.FilterByArea(100, int.MaxValue);
            //_tracker.Process(smoothedFrame, forgroundMask);
            if (blobs.Count < 1)
                return Rectangle.Empty;

            //-------------------------------
            Rectangle rc = Rectangle.Empty;
            foreach (var pair in blobs)
            {
                CvBlob b = pair.Value;
                rc = b.BoundingBox;
                //CvInvoke.Rectangle(imgBgr, b.BoundingBox, new MCvScalar(255.0, 0, 0), 2);
                break;
            }

            return rc;
        }

        private Rectangle GetBlueGameBoxRegion(Rectangle rcGame, Image<Gray, Byte> imgGray)
        {
            byte[,,] pData = imgGray.Data;
            int nWid = imgGray.Width; int nHei = imgGray.Height;

            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    if (!rcGame.Contains(x, y))
                    {
                        pData[y, x, 0] = 0;
                        continue;
                    }

                    byte c = pData[y, x, 0];
                    if (c >= 100 && c <= 120) pData[y, x, 0] = 255;
                    else pData[y, x, 0] = 0;
                }
            }

            CvBlobs blobs = new CvBlobs();
            _blobDetector.Detect(imgGray, blobs);
            blobs.FilterByArea(100, int.MaxValue);
            //_tracker.Process(smoothedFrame, forgroundMask);
            if (blobs.Count < 1)
                return Rectangle.Empty;

            //-------------------------------
            Rectangle rc = Rectangle.Empty;

            int nSizeMax = 0;
            foreach (var pair in blobs)
            {
                CvBlob b = pair.Value;
                if (b.BoundingBox.Width * b.BoundingBox.Height > nSizeMax)
                {
                    rc = b.BoundingBox;
                    nSizeMax = rc.Width * rc.Height;
                }
                //break;
            }

            return rc;
        }
        //CalcBasePostions(rc, ref nMarksX, ref nMarksY, ref nGameBoardX, ref nGameBoardY);

        private bool CalcBasePostions(Rectangle rc, ref int nMarksX, ref int nMarksY, ref int nGameBoardX, ref int nGameBoardY)
        {
            Global.g_rcROI = rc;
            Global.DEF_MAIN_BOARD_X = 238;
            Global.DEF_MAIN_BOARD_Y = 42;
            Global.DEF_MAIN_BOARD_W = 570;
            Global.DEF_MAIN_BOARD_H = 570;

            nGameBoardX = Global.DEF_MAIN_BOARD_X + rc.X;
            nGameBoardY = Global.DEF_MAIN_BOARD_Y + rc.Y;

            Global.GetRatioCalcedValues(rc.Width, rc.Height, ref nGameBoardX, ref nGameBoardY);
            Global.GetRatioCalcedValues(rc.Width, rc.Height, ref Global.DEF_MAIN_BOARD_W, ref Global.DEF_MAIN_BOARD_H);

            //CvInvoke.Rectangle(imgBgr, new Rectangle(nGameBoardX, nGameBoardY, Global.DEF_MAIN_BOARD_W, Global.DEF_MAIN_BOARD_H), new MCvScalar(255, 255, 0), 2);

            Global.DEF_MARKS_X = 15;
            Global.DEF_MARKS_Y = 204;
            Global.DEF_MARKS_W = 189;
            Global.DEF_MARKS_H = 69;

            nMarksX = Global.DEF_MARKS_X + rc.X;
            nMarksY = Global.DEF_MARKS_Y + rc.Y;

            Global.GetRatioCalcedValues(rc.Width, rc.Height, ref nMarksX, ref nMarksY);
            Global.GetRatioCalcedValues(rc.Width, rc.Height, ref Global.DEF_MARKS_W, ref Global.DEF_MARKS_H);
            //CvInvoke.Rectangle(imgBgr, new Rectangle(nMarksX, nMarksY, Global.DEF_MARKS_W, Global.DEF_MARKS_H), new MCvScalar(255, 255, 0), 2);

            return true;
        }
        
        private bool CheckIfMovementStable(Image<Bgr, Byte> imgBgr, int nMarksX, int nMarksY)
        {
            imgBgr.ROI = new Rectangle(nMarksX, nMarksY, Global.DEF_MARKS_W, Global.DEF_MARKS_H);
            Image<Bgr, Byte> subImage = imgBgr.Copy();

            byte[,,] pSubImageData = subImage.Data;
            int nWidSub = subImage.Width; int nHeiSub = subImage.Height;

            int nWhiteCnt = 0, nPrevWhiteCnt = 0;
            for (int i = 0; i < nHeiSub; i++)
            {
                for (int j = 0; j < nWidSub; j++)
                {
                    byte B = pSubImageData[i, j, 0];
                    if (B > 100)
                    {
                        pSubImageData[i, j, 0] = 0; pSubImageData[i, j, 1] = 0; pSubImageData[i, j, 2] = 0;
                    }
                    else
                    {
                        pSubImageData[i, j, 0] = 255; pSubImageData[i, j, 1] = 255; pSubImageData[i, j, 2] = 255;
                        nWhiteCnt++;
                    }
                }
            }

            if (mPrevScoreImage == null)
            {
                mPrevScoreImage = subImage.Convert<Gray, Byte>();
                return false;
            }

            int nWidPrev = 0, nHeiPrev = 0;

            byte[,,] pData = mPrevScoreImage.Data;
            nWidPrev = mPrevScoreImage.Width; nHeiPrev = mPrevScoreImage.Height;
            for ( int i = 0; i < nHeiPrev; i ++ )
            {
                for ( int j = 0; j < nWidPrev; j ++ )
                {
                    byte C = pData[i, j, 0];
                    if (C > 0)
                        nPrevWhiteCnt++;
                }
            }

            mPrevScoreImage = null;
            mPrevScoreImage = subImage.Convert<Gray, Byte>();

            if (Math.Abs(nWhiteCnt - nPrevWhiteCnt) > nWhiteCnt / 10)
                return false;

            imgBgr.ROI = Rectangle.Empty;
            return true;
        }

        private bool GetAllocatedCharacters(Image<Bgr, Byte> imgBgr, int nGameBoardX, int nGameBoardY)
        {
            int nStepX = Global.DEF_MAIN_BOARD_W / 8; int nStepY = Global.DEF_MAIN_BOARD_H / 8;

            var rois = new List<Rectangle>(); // List of rois
            var imageparts = new List<Image<Bgr, byte>>(); // List of extracted image parts

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Rectangle roi = new Rectangle(nGameBoardX + j * nStepX, nGameBoardY + i * nStepY, Global.DEF_ITEM_W, Global.DEF_ITEM_H);
                    rois.Add(roi);
                    imgBgr.ROI = roi;
                    imageparts.Add(imgBgr.Copy());
                }
            }
            
            imgBgr.ROI = Rectangle.Empty;
            m_LstCharacter.Clear();

            bool bCanProcess = true;
            int k = 0, nRow = 0, nCol = 0;
            foreach (Image<Bgr, Byte> img in imageparts)
            {
                int nCharac = (int)ImageMatcher.DetermineCharacter(img);
                m_LstCharacter.Add(nCharac);

                MovementDecision.g_AllocCharacters[nRow, nCol] = nCharac;
                nCol++;

                if (nCol >= 8)
                { nRow++; nCol = 0; }

                //if (nCharac != 0)
                //CvInvoke.Rectangle(imgBgr, rois[k], new MCvScalar(255, 255, 0), 2);
                //CvInvoke.Rectangle(imgBgr, rois[k], cols[nCharac - 1], 2);

                if (nCharac == 0)
                    bCanProcess = false;

                k++;
            }

            string szLine = "";
            lstBox.Items.Clear();
            for (int i = 0; i < 8; i++)
            {
                szLine = "";
                for (int j = 0; j < 8; j++)
                {
                    szLine += "" + MovementDecision.g_AllocCharacters[i, j] + " ";
                }
                lstBox.Items.Add(szLine);
            }

            return bCanProcess;
        }

        private string[] SZ_DRIECTION = { "Up", "Left", "Right", "Down" };

        private void GenerateHintImage(Image<Bgr, Byte> imgBgr, int nGameBoardX, int nGameBoardY)
        {
            imgBgr.ROI = new Rectangle(nGameBoardX, nGameBoardY, Global.DEF_MAIN_BOARD_W, Global.DEF_MAIN_BOARD_H);
            Image<Bgr, byte> bgGameboard = imgBgr.Copy();
            Image<Bgr, byte> bgGameboard2 = imgBgr.Copy();

            lbStep1.Text = "Step 1:Score=" + Global.g_moveStep1.nScore + ", Pos=(" + Global.g_moveStep1.nX + ", " + Global.g_moveStep1.nY + "), D=" + SZ_DRIECTION[Global.g_moveStep1.nD];
            lbStep2.Text = "Step 2:Score=" + Global.g_moveStep2.nScore + ", Pos=(" + Global.g_moveStep2.nX + ", " + Global.g_moveStep2.nY + "), D=" + SZ_DRIECTION[Global.g_moveStep1.nD];

            int STEPX = Global.DEF_MAIN_BOARD_W / 8; int STEPY = Global.DEF_MAIN_BOARD_H / 8;
            CvInvoke.Rectangle(bgGameboard, new Rectangle(STEPX*Global.g_moveStep1.nY, STEPY*Global.g_moveStep1.nX, STEPX, STEPY), new MCvScalar(255, 255, 0), 4);
            CvInvoke.Rectangle(bgGameboard2, new Rectangle(STEPX*Global.g_moveStep2.nY, STEPY*Global.g_moveStep2.nX, STEPX, STEPY), new MCvScalar(255, 255, 0), 4);

            int nD = Global.g_moveStep1.nD;
            if ( nD == 0 ) // toTop
                CvInvoke.Rectangle(bgGameboard, new Rectangle(STEPX * Global.g_moveStep1.nY, STEPY * (Global.g_moveStep1.nX-1), STEPX, STEPY), new MCvScalar(255, 0, 255), 4);
            else if (nD == 1 ) // toLeft
                CvInvoke.Rectangle(bgGameboard, new Rectangle(STEPX * (Global.g_moveStep1.nY-1), STEPY * Global.g_moveStep1.nX, STEPX, STEPY), new MCvScalar(255, 0, 255), 4);
            else if (nD == 2) // toRight
                CvInvoke.Rectangle(bgGameboard, new Rectangle(STEPX * (Global.g_moveStep1.nY+1), STEPY * Global.g_moveStep1.nX, STEPX, STEPY), new MCvScalar(255, 0, 255), 4);
            else if (nD == 3) // toDown
                CvInvoke.Rectangle(bgGameboard, new Rectangle(STEPX * Global.g_moveStep1.nY, STEPY * (Global.g_moveStep1.nX+1), STEPX, STEPY), new MCvScalar(255, 0, 255), 4);

            nD = Global.g_moveStep2.nD;
            if (nD == 0) // toTop
                CvInvoke.Rectangle(bgGameboard2, new Rectangle(STEPX * Global.g_moveStep2.nY, STEPY * (Global.g_moveStep2.nX - 1), STEPX, STEPY), new MCvScalar(255, 0, 255), 4);
            else if (nD == 1) // toLeft
                CvInvoke.Rectangle(bgGameboard2, new Rectangle(STEPX * (Global.g_moveStep2.nY - 1), STEPY * Global.g_moveStep2.nX, STEPX, STEPY), new MCvScalar(255, 0, 255), 4);
            else if (nD == 2) // toRight
                CvInvoke.Rectangle(bgGameboard2, new Rectangle(STEPX * (Global.g_moveStep2.nY + 1), STEPY * Global.g_moveStep2.nX, STEPX, STEPY), new MCvScalar(255, 0, 255), 4);
            else if (nD == 3) // toDown
                CvInvoke.Rectangle(bgGameboard2, new Rectangle(STEPX * Global.g_moveStep2.nY, STEPY * (Global.g_moveStep2.nX + 1), STEPX, STEPY), new MCvScalar(255, 0, 255), 4);

            picRet1.Image = bgGameboard.Bitmap;
            picRet2.Image = bgGameboard2.Bitmap;

            imgBgr.ROI = Rectangle.Empty;
        }

        private Arrow   m_ArrowRed1 = null;
        private ToTop   m_ToTop1    = null;
        private ToLeft  m_ToLeft1   = null;
        private ToRight m_ToRight1  = null;
        private ToDown  m_ToDown1   = null;

        private BlackArrow  m_ArrowBlack1   = null;
        private ToTop2      m_ToTop2        = null;
        private ToLeft2     m_ToLeft2       = null;
        private ToRight2    m_ToRight2      = null;
        private ToDown2     m_ToDown2       = null;

        private void PlaceHintArrows(int nLeft, int nTop)
        {
            int STEPX = Global.DEF_MAIN_BOARD_W / 8;
            int STEPY = Global.DEF_MAIN_BOARD_H / 8;

            if ( Global.g_moveStep1 != Global.MOVEMENT.Empty )
            {
                if (m_ArrowRed1 == null) { m_ArrowRed1 = new Arrow(); m_ArrowRed1.Show(); }

                int nX = nLeft + Global.g_moveStep1.nY * STEPX + STEPX / 2 - 20;
                int nY = nTop + Global.g_moveStep1.nX * STEPY;
                m_ArrowRed1.Location = new Point(nX, nY);

                int nD = Global.g_moveStep1.nD;
                // top
                if (nD == 0)
                {
                    if (m_ToTop1 == null) { m_ToTop1 = new ToTop(); m_ToTop1.Show(); }
                    nY = nTop + Global.g_moveStep1.nX * STEPY - (STEPY/2-10);

                    m_ToTop1.Location = new Point(nX, nY);
                }
                else if (nD == 1) // left
                {
                    if (m_ToLeft1 == null) { m_ToLeft1 = new ToLeft(); m_ToLeft1.Show(); }
                    
                    nX = nLeft + Global.g_moveStep1.nY * STEPX -(STEPX / 2 - 20);
                    nY += m_ToLeft1.Height/2;

                    m_ToLeft1.Location = new Point(nX, nY);
                }
                else if (nD == 2) // right
                {
                    if (m_ToRight1 == null) { m_ToRight1 = new ToRight(); m_ToRight1.Show(); }
                    nX = nLeft + Global.g_moveStep1.nY * STEPX + (STEPX / 2 - 10);
                    nY += m_ToRight1.Height/2;

                    m_ToRight1.Location = new Point(nX, nY);
                }
                else if (nD == 3) // bottom
                {
                    if (m_ToDown1 == null) { m_ToDown1 = new ToDown(); m_ToDown1.Show(); }
                    nY = nTop + Global.g_moveStep1.nX * STEPY + STEPY / 2 - 10;
                    m_ToDown1.Location = new Point(nX, nY);
                }
            }

            if (Global.g_moveStep2 != Global.MOVEMENT.Empty)
            {
                if (m_ArrowBlack1 == null) { m_ArrowBlack1 = new BlackArrow(); m_ArrowBlack1.Show(); }

                int nX = nLeft + Global.g_moveStep2.nY * STEPX + STEPX / 2 - 20;
                int nY = nTop + Global.g_moveStep2.nX * STEPY;
                m_ArrowBlack1.Location = new Point(nX, nY);

                int nD = Global.g_moveStep2.nD;
                // top
                if (nD == 0)
                {
                    if (m_ToTop2 == null) { m_ToTop2 = new ToTop2(); m_ToTop2.Show(); }
                    nY = nTop + Global.g_moveStep2.nX * STEPY - (STEPY / 2 - 10);

                    m_ToTop2.Location = new Point(nX, nY);
                }
                else if (nD == 1) // left
                {
                    if (m_ToLeft2 == null) { m_ToLeft2 = new ToLeft2(); m_ToLeft2.Show(); }

                    nX = nLeft + Global.g_moveStep2.nY * STEPX - (STEPX / 2 - 20);
                    nY += m_ToLeft2.Height / 2;

                    m_ToLeft2.Location = new Point(nX, nY);
                }
                else if (nD == 2) // right
                {
                    if (m_ToRight2 == null) { m_ToRight2 = new ToRight2(); m_ToRight2.Show(); }
                    nX = nLeft + Global.g_moveStep2.nY * STEPX + (STEPX / 2 - 10);
                    nY += m_ToRight2.Height / 2;

                    m_ToRight2.Location = new Point(nX, nY);
                }
                else if (nD == 3) // bottom
                {
                    if (m_ToDown2 == null) { m_ToDown2 = new ToDown2(); m_ToDown2.Show(); }
                    nY = nTop + Global.g_moveStep2.nX * STEPY + STEPY / 2 - 10;
                    m_ToDown2.Location = new Point(nX, nY);
                }
            }
        }

        public void RemoveHintArrows()
        {
            if (m_ArrowRed1 != null) { m_ArrowRed1.Close(); m_ArrowRed1 = null;}

            if (m_ToTop1    != null) { m_ToTop1.Close(); m_ToTop1 = null; }
            if (m_ToLeft1   != null) { m_ToLeft1.Close(); m_ToLeft1 = null; }
            if (m_ToRight1  != null) { m_ToRight1.Close(); m_ToRight1 = null; }
            if (m_ToDown1   != null) { m_ToDown1.Close(); m_ToDown1 = null; }

            int nCntZero = 0;
            for( int i = 0; i < 8; i ++ )
            {
                for (int j = 0; j < 8; j ++)
                {
                    if (MovementDecision.g_AllocCharacters[i, j] == 0)
                        nCntZero++;
                }
            }

            if (nCntZero >= 8*8/2)
            {
                picRet1.Image = null; picRet2.Image = null; lbStep1.Text = "Step 1"; lbStep2.Text = "Step 2";
            }
        }

        private void timer_process_Tick(object sender, EventArgs e)
        {
            timer_process.Enabled = false;

            if (!m_bAutoStarted)
                return;

            Mat mat = GetScreenCapture();
            Image<Bgr, Byte> imgBgr = mat.ToImage<Bgr, Byte>();
            Image<Gray, Byte> imgGray = mat.ToImage<Gray, Byte>();
            
            Rectangle rt = GetBlackGameBoxRegion(imgGray);
            if( rt == Rectangle.Empty )
            {
                timer_process.Enabled = true;
                return;
            }

            // -------Detect Blue Region ---- /
            imgGray = imgBgr.Convert<Gray, Byte>();
            Rectangle rc = GetBlueGameBoxRegion(rt, imgGray);
            if (rc == Rectangle.Empty)
            {
                timer_process.Enabled = true;
                return;
            }
            //----------------------------------

            int nMarksX = 0, nMarksY = 0, nGameBoardX = 0, nGameBoardY = 0;
            CalcBasePostions(rc, ref nMarksX, ref nMarksY, ref nGameBoardX, ref nGameBoardY);
            
            bool bCanProcess = GetAllocatedCharacters(imgBgr, nGameBoardX, nGameBoardY);
            if (!bCanProcess) { RemoveHintArrows(); timer_process.Enabled = true; return; }
            
            if (!CheckIfMovementStable(imgBgr, nMarksX, nMarksY)) { timer_process.Enabled = true; return; }

            int nScore = MovementDecision.Process();
            GenerateHintImage(imgBgr, nGameBoardX, nGameBoardY);
            if (Global.g_bAssistantMode)
                PlaceHintArrows(nGameBoardX, nGameBoardY);

            lbProcessTime.Text = "" + nScore;
            timer_process.Enabled = true;

            //-----------
            mat.Dispose();
            GC.Collect();
        }

        private void picBtnExit_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Are you sure to exit?", "Automation Bot", MessageBoxButtons.YesNo);
            if (ret != DialogResult.Yes)
                return;

            Application.Exit();
        }

        private bool m_bAutoStarted = false;

        private void picBtnStart_Click(object sender, EventArgs e)
        {
            string szQuiz = "Are you sure to start automation?";
            if (m_bAutoStarted)
                szQuiz = "Are you sure to stop automation?";

            DialogResult ret = MessageBox.Show(szQuiz, "Automation Bot", MessageBoxButtons.YesNo);
            if (ret != DialogResult.Yes)
                return;

            if( !m_bAutoStarted)
            {
                picBtnStart.Image = Properties.Resources.stop;
                timer_process.Enabled = true;
            }
            else
            {
                picBtnStart.Image = Properties.Resources.start;
                timer_process.Enabled = false;
            }

            m_bAutoStarted = !m_bAutoStarted;
        }

        private void btnProceed1_Click(object sender, EventArgs e)
        {
            if (Global.g_moveStep1 == Global.MOVEMENT.Empty)
                return;
         
            MovementDecision.EmulateMovement(Global.g_moveStep1.nX, Global.g_moveStep1.nY, Global.g_moveStep1.nD);
        }

        private void btnProceed2_Click(object sender, EventArgs e)
        {
            if (Global.g_moveStep2 == Global.MOVEMENT.Empty)
                return;

            MovementDecision.EmulateMovement(Global.g_moveStep2.nX, Global.g_moveStep2.nY, Global.g_moveStep2.nD);
        }

        public void ProcessClickOnAssistant()
        {
            chkAssistMode.Checked = !chkAssistMode.Checked;
        }

        private void btnModeAssist_CheckedChanged(object sender, EventArgs e)
        {
            btnProceed1.Enabled = chkAssistMode.Checked;
            btnProceed2.Enabled = chkAssistMode.Checked;
            Global.g_bAssistantMode = chkAssistMode.Checked;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
        }
    }
}
