using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace GameAuto
{
    public class Global
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
      

        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        public enum SystemMetric
        {
            SM_CXSCREEN = 0,  // 0x00
            SM_CYSCREEN = 1,  // 0x01
            SM_CXVSCROLL = 2,  // 0x02
            SM_CYHSCROLL = 3,  // 0x03
            SM_REMOTECONTROL = 0x2001, // 0x2001
        }
        
        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(SystemMetric smIndex);

        public static void MouseDownTo(Point pt)
        {
            int x = pt.X; int y = pt.Y;
            int cx = GetSystemMetrics(SystemMetric.SM_CXSCREEN);
            int cy = GetSystemMetrics(SystemMetric.SM_CYSCREEN);

            int posX = 2 * 32768 * x / cx;
            int posY = 2 * 32768 * y / cy;
            mouse_event((int)MouseEventFlags.ABSOLUTE | (int)MouseEventFlags.MOVE, posX, posY, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            Thread.Sleep(500);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            Thread.Sleep(500);
        }

        public static void MouseMoveToAndUp(Point pt)
        {
            int x = pt.X; int y = pt.Y;
            int cx = GetSystemMetrics(SystemMetric.SM_CXSCREEN);
            int cy = GetSystemMetrics(SystemMetric.SM_CYSCREEN);

            int posX = 2 * 32768 * x / cx;
            int posY = 2 * 32768 * y / cy;
            mouse_event((int)MouseEventFlags.ABSOLUTE | (int)MouseEventFlags.MOVE, posX, posY, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            Thread.Sleep(500);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            Thread.Sleep(500);
        }

        public static void MouseMoveTo(Point pt)
        {
            int x = pt.X; int y = pt.Y;
            int cx = GetSystemMetrics(SystemMetric.SM_CXSCREEN);
            int cy = GetSystemMetrics(SystemMetric.SM_CYSCREEN);

            int posX = 2 * 32768 * x / cx;
            int posY = 2 * 32768 * y / cy;
            mouse_event((int)MouseEventFlags.ABSOLUTE | (int)MouseEventFlags.MOVE, posX, posY, 0, 0);
        }
        
        public static Rectangle g_rcROI = Rectangle.Empty;
        public struct MOVEMENT
        {
            public static readonly MOVEMENT Empty;

            public int nX;
            public int nY;
            public int nD;
            public int nScore;

            public static bool operator ==(MOVEMENT left, MOVEMENT right)
            {
                if (left.nX == right.nX && left.nY == right.nY && left.nD == right.nD && left.nScore == right.nScore)
                    return true;

                return false;
            }

            public static bool operator !=(MOVEMENT left, MOVEMENT right)
            {
                return !(left == right);
            }
        }

        public static MOVEMENT g_moveStep1 = MOVEMENT.Empty;
        public static MOVEMENT g_moveStep2 = MOVEMENT.Empty;

        public static bool g_bAssistantMode = false;
        public static MainWindow g_MainWindow = null;

        public enum CHARACTER_TYPE
        {
            CHAR_NONE = 0,
            CHAR_FROG = 1,
            CHAR_FROG_WATER =2,
            CHAR_DUCK = 3,
            CHAR_DUCK_WATER = 4,
            CHAR_SAMBALI = 5,
            CHAR_SAMBALI_WATER = 6,
            CHAR_WHALE = 7,
            CHAR_WHALE_WATER = 8,
            CHAR_OCTOPUS = 9,
            CHAR_OCTOPUS_WATER = 10,
            CHAR_WOOD = 11,
        };

        public static int DEF_WND_W = 831;
        public static int DEF_WND_H = 658;

        //public static int DEF_REMAIN_X = 10 + 16;
        //public static int DEF_REMAIN_Y = 39 + 13;
        //public static int DEF_REMAIN_W = 164;
        //public static int DEF_REMAIN_H = 170;

        public static int DEF_MARKS_X = 15;
        public static int DEF_MARKS_Y = 204;
        public static int DEF_MARKS_W = 189;
        public static int DEF_MARKS_H = 69;

        //public static int DEF_DUCK_X = 50 + 10;
        //public static int DEF_DUCK_Y = 256 + 39;
        //public static int DEF_DUCK_W = 131;
        //public static int DEF_DUCK_H = 131;

        public static int DEF_MAIN_BOARD_X = 230;
        public static int DEF_MAIN_BOARD_Y = 42;
        public static int DEF_MAIN_BOARD_W = 571;
        public static int DEF_MAIN_BOARD_H = 557;

        public static int DEF_ITEM_W = 44;
        public static int DEF_ITEM_H = 68;
        
        public static bool GetRatioCalcedValues(int nWid, int nHei, ref int X, ref int Y)
        {
            if (nWid * nHei * X * Y == 0)
                return false;

            float fRatioX = (float)nWid / (float)DEF_WND_W;
            float fRatioY = (float)nHei / (float)DEF_WND_H;

            X = (int)(X * fRatioX);
            Y = (int)(Y * fRatioY);

            return true;
        }
    }
}
