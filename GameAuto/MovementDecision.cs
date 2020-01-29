using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GameAuto
{
    public class MovementDecision
    {
        public const int BOARD_SIZE_W = 8;
        public const int BOARD_SIZE_H = 8;
        public const int DIRECTION = 4;

        public static int[,] g_AllocCharacters = new int[BOARD_SIZE_H, BOARD_SIZE_W] {
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
        };

        public static int[,] g_TempCharacters = new int[BOARD_SIZE_H, BOARD_SIZE_W] {
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
        };
        public static int[,] g_Step2TempCharacters = new int[BOARD_SIZE_H, BOARD_SIZE_W] {
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
        };

        public static int[,,] g_ScoreByMovements = new int[BOARD_SIZE_H, BOARD_SIZE_W, DIRECTION];
        public static int[,,] g_ScoreByMovements2 = new int[BOARD_SIZE_H, BOARD_SIZE_W, DIRECTION];
        public static int[,,] g_Step2ScoreByMovements = new int[BOARD_SIZE_H, BOARD_SIZE_W, DIRECTION];

        private static List<List<Point>>    g_LstIdenticals = new List<List<Point>>();
        private static List<List<int>>      g_LstIdenticalItems = new List<List<int>>();
        private static List<Point>          g_LstRemainLands = new List<Point>();

        public static bool CompareCharacter(int c, int k)
        {
            if (c == k && c < 11 && c > 0) return true;
            else if (c == 1 && k == 2 || c == 2 && k == 1) return true;
            else if (c == 3 && k == 4 || c == 4 && k == 3) return true;
            else if (c == 5 && k == 6 || c == 6 && k == 5) return true;
            else if (c == 7 && k == 8 || c == 8 && k == 7) return true;
            else if (c == 9 && k == 10 || c == 10 && k == 9) return true;

            return false;
        }

        public static void GetEqualLinks(int[,] arrCharacters)
        {
            g_LstIdenticals.Clear();
            g_LstIdenticalItems.Clear();

            int i = 0, j = 0;

            // Horizontal Search                    
            for (; i < BOARD_SIZE_H; i++)
            {
                j = 0;
                while( j < BOARD_SIZE_W)
                {
                    int c = arrCharacters[i, j]; int k = arrCharacters[i, j];
                    int ii = 0;

                    List<Point> LstIdenticals = new List<Point>();
                    List<int> LstIdenticalItems = new List<int>();
                    LstIdenticals.Add(new Point(i, j)); LstIdenticalItems.Add(k);

                    while (true)
                    {
                        ii++;
                        if (j + ii >= BOARD_SIZE_W)
                            break;

                        k = arrCharacters[i, j + ii];
                        if (CompareCharacter(c, k))
                        {
                            LstIdenticals.Add(new Point(i, j + ii));
                            LstIdenticalItems.Add(k);
                        }
                        else break;
                    }

                    if (LstIdenticals.Count > 2)
                    {
                        g_LstIdenticals.Add(LstIdenticals);
                        g_LstIdenticalItems.Add(LstIdenticalItems);
                    }

                    j += ii;
                }
            }

            i = 0;
            // Vertical Search
            for ( j = 0; j < BOARD_SIZE_W; j ++)
            {
                i = 0;
                while (i < BOARD_SIZE_H)
                {
                    int ii = 0;
                    int c = arrCharacters[i, j]; int k = arrCharacters[i, j];

                    List<Point> LstIdenticals = new List<Point>();
                    List<int> LstIdenticalItems = new List<int>();
                    LstIdenticals.Add(new Point(i, j)); LstIdenticalItems.Add(k);

                    while (true)
                    {
                        ii ++;
                        if (i + ii >= BOARD_SIZE_H)
                            break;

                        k = arrCharacters[i+ii, j];
                        if (CompareCharacter(c, k))
                        {
                            LstIdenticals.Add(new Point(i + ii, j));
                            LstIdenticalItems.Add(k);
                        }
                        else break;
                    }

                    if (LstIdenticals.Count > 2)
                    {
                        g_LstIdenticals.Add(LstIdenticals);
                        g_LstIdenticalItems.Add(LstIdenticalItems);
                    }

                    i += ii;
                }
            }
        }
    
        public static int CalcScores()
        {
            int k = 0; int nTotalSum = 0, nSum = 0;

            int nLandCnt = 0;
            for ( int i = 0; i < BOARD_SIZE_H; i++)
            { 
                for ( int j = 0; j < BOARD_SIZE_W; j ++)
                {
                    if (g_TempCharacters[i, j] % 2 == 1 && g_TempCharacters[i, j] < 11)
                        nLandCnt++;
                }
            }

            int nAdvScore = 500;
            if (nLandCnt > 10)
                nAdvScore = 50;
            else if(nLandCnt > 5)
                nAdvScore = 200;
            else if (nLandCnt > 2)
                nAdvScore = 1500;
            else
                nAdvScore = 2000;

            foreach (List<Point> Link in g_LstIdenticals)
            {
                nSum = 0;
                List<int> ListCharacter = g_LstIdenticalItems[k];
                
                int nCntOdd = 0, nCntEven = 0;
                foreach(int Charac in ListCharacter)
                {
                    // if it contains sea increase by 3 points.
                    if (Charac > 0 && Charac % 2 == 0) nCntEven++;
                    if (Charac % 2 == 1) nCntOdd ++;
                }

                if (nCntEven > 1 && Link.Count > 4)
                    nSum = 5000;
                else if (nCntEven > 1 && Link.Count > 3)
                    nSum = 2000;
                else if (nCntEven == 0 || nCntOdd == 0)
                    nSum = ListCharacter.Count;
                else
                    nSum = nCntOdd * nAdvScore + ListCharacter.Count;

                nTotalSum += nSum;
                k++;
            }

            bool bFound = false;
            for ( int i=0; i<g_LstIdenticals.Count-1; i ++)
            {
                for( int j=i+1; j<g_LstIdenticals.Count; j++)
                {
                    int nCnt0 = g_LstIdenticals[i].Count; int nCnt1 = g_LstIdenticals[j].Count;

                    if ( g_LstIdenticals[i][0].X == g_LstIdenticals[j][0].X && g_LstIdenticals[i][0].Y == g_LstIdenticals[j][0].Y 
                        || g_LstIdenticals[i][0].X == g_LstIdenticals[j][nCnt1-1].X && g_LstIdenticals[i][0].Y == g_LstIdenticals[j][nCnt1-1].Y
                        || g_LstIdenticals[i][nCnt0-1].X == g_LstIdenticals[j][0].X && g_LstIdenticals[i][nCnt0-1].Y == g_LstIdenticals[j][0].Y
                        || g_LstIdenticals[i][nCnt0-1].X == g_LstIdenticals[j][nCnt1-1].X && g_LstIdenticals[i][nCnt0-1].Y == g_LstIdenticals[j][nCnt1-1].Y)
                    {
                        bool bSea = false;
                        foreach(int k1 in g_LstIdenticalItems[i])
                        {
                            if (k1 % 2 == 0 && k1 > 0 && k1 < 11)
                            {
                                bSea = true;
                                break;
                            }
                        }

                        if (!bSea)
                        {
                            foreach (int k1 in g_LstIdenticalItems[j])
                            {
                                if (k1 % 2 == 0 && k1 > 0 && k1 < 11)
                                {
                                    bSea = true;
                                    break;
                                }
                            }
                        }

                        if (!bSea) continue;

                        nTotalSum += 1000;
                        bFound = true;
                        break;
                    }
                }

                if (bFound) break;
            }

            return nTotalSum;
        }
    
        public static void ApplyMovementToArray(int[,] array)
        {
            foreach(List<Point> Links in g_LstIdenticals)
            {
                foreach(Point pt in Links)
                {
                    array[pt.X, pt.Y] = 0;
                }
            }

            int j = 0, k = 0, nDelta = 1;
            for( int i = 0; i < BOARD_SIZE_W; i ++)
            {
                j = BOARD_SIZE_H - 1;
                while (j > 0)
                {
                    if (array[j, i] == 0)
                    {
                        k = j - 1;
                        nDelta = 1;

                        while(k >= 0)
                        {
                            if (array[k, i] >= (int)Global.CHARACTER_TYPE.CHAR_WOOD || array[k, i] == 0)
                            {
                                k --;
                                nDelta ++;
                                continue;
                            }

                            array[k+nDelta, i] = array[k, i];
                            k --;
                        }

                        for (int t = 0; t < nDelta; t ++)
                        {
                            array[t, i] = 0;
                        }
                    }

                    j--;
                }
            }

            Random rnd = new Random();
            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (j = 0; j < BOARD_SIZE_W; j++)
                {
                    // still need to fill, then randomly fill it.
                    if (array[i, j] == 0)
                    {
                        array[i, j] = 12;
                        if (array[i, j] % 2 == 0) array[i, j]++;
                    }
                }
            }
        }

        public static void RefreshRemainLandsList(int[,] array)
        {
            g_LstRemainLands.Clear();
            for( int i = 0; i < BOARD_SIZE_H; i ++)
            {
                for ( int j = 0; j < BOARD_SIZE_W; j ++ )
                {
                    if (array[i,j] % 2 == 1)
                    {
                        g_LstRemainLands.Add(new Point(i, j));
                    }
                }
            }
        }

        private static int ProcessStep2Movement()
        {
            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (int j = 0; j < BOARD_SIZE_W; j++)
                {
                    g_Step2ScoreByMovements[i, j, 0] = g_Step2ScoreByMovements[i, j, 1] = g_Step2ScoreByMovements[i, j, 2] = g_Step2ScoreByMovements[i, j, 3] = 0;
                    // not empty & not wood
                    if (g_TempCharacters[i, j] != 0 && g_TempCharacters[i, j] < 11)
                    {
                        g_Step2ScoreByMovements[i, j, 0] = TryStep2SwapWithTop(i, j);
                        g_Step2ScoreByMovements[i, j, 1] = TryStep2SwapWithLeft(i, j);
                        g_Step2ScoreByMovements[i, j, 2] = TryStep2SwapWithRight(i, j);
                        g_Step2ScoreByMovements[i, j, 3] = TryStep2SwapWithBottom(i, j);
                    }
                }
            }

            int nMaxVal = 0;

            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (int j = 0; j < BOARD_SIZE_W; j++)
                {
                    for (int k = 0; k < DIRECTION; k++)
                    {
                        if (nMaxVal < g_Step2ScoreByMovements[i, j, k])
                            nMaxVal = g_Step2ScoreByMovements[i, j, k];
                    }
                }
            }

            return nMaxVal;
        }

        public static int TrySwapping(int i, int j, int d)
        {
            GetEqualLinks(g_TempCharacters);
            //RefreshRemainLandsList(g_TempCharacters);

            int nScore = CalcScores();
            int nTotalScore = nScore;

            int nRepeatCnt = 0;
            while (nScore > 1) // predict 2 times
            {
                ApplyMovementToArray(g_TempCharacters);
                GetEqualLinks(g_TempCharacters);
                nScore = CalcScores();
                nTotalScore += nScore;
                nRepeatCnt ++;
            }

            int nStep2Score = 0;
            if (nTotalScore > 1)
                nStep2Score = ProcessStep2Movement();

            g_ScoreByMovements2[i, j, d] = nStep2Score;
            
            nTotalScore += nStep2Score;
            return nTotalScore;
        }

        public static void CopyCharactersToStep2Temp()
        {
            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (int j = 0; j < BOARD_SIZE_W; j++)
                {
                    g_Step2TempCharacters[i, j] = g_TempCharacters[i, j];
                }
            }
        }

        public static int TryStep2SwapWithTop(int i, int j)
        {
            if (i < 1) return 0;
            if (g_TempCharacters[i-1, j] >= 11) // wood
                return 0;
            
            CopyCharactersToStep2Temp();

            int nTemp = g_Step2TempCharacters[i - 1, j];
            g_Step2TempCharacters[i - 1, j] = g_Step2TempCharacters[i, j];
            g_Step2TempCharacters[i, j] = nTemp;

            return TryStep2Swapping();
        }

        public static int TryStep2SwapWithLeft(int i, int j)
        {
            if (j < 1) return 0;
            if (g_TempCharacters[i, j - 1] >= 11) // wood
                return 0;
            
            CopyCharactersToStep2Temp();

            int nTemp = g_Step2TempCharacters[i, j - 1];
            g_Step2TempCharacters[i, j - 1] = g_Step2TempCharacters[i, j];
            g_Step2TempCharacters[i, j] = nTemp;

            return TryStep2Swapping();
        }

        public static int TryStep2SwapWithRight(int i, int j)
        {
            if (j >= BOARD_SIZE_W - 1) return 0;
            if (g_TempCharacters[i, j + 1] >= 11) // wood
                return 0;
            
            CopyCharactersToStep2Temp();
            
            int nTemp = g_Step2TempCharacters[i, j + 1];
            g_Step2TempCharacters[i, j + 1] = g_Step2TempCharacters[i, j];
            g_Step2TempCharacters[i, j] = nTemp;

            return TryStep2Swapping();
        }

        public static int TryStep2SwapWithBottom(int i, int j)
        {
            if (i >= BOARD_SIZE_H - 1) return 0;
            if (g_TempCharacters[i + 1, j] >= 11) // wood
                return 0;

            CopyCharactersToStep2Temp();

            int nTemp = g_Step2TempCharacters[i + 1, j];
            g_Step2TempCharacters[i + 1, j] = g_Step2TempCharacters[i, j];
            g_Step2TempCharacters[i, j] = nTemp;

            return TryStep2Swapping();
        }

        public static int TryStep2Swapping()
        {
            GetEqualLinks(g_Step2TempCharacters);
            //RefreshRemainLandsList(g_TempCharacters);

            int nScore = CalcScores();
            int nTotalScore = nScore;

            int nRepeatCnt = 0;
            while (nScore > 1) // predict 2 times
            {
                ApplyMovementToArray(g_Step2TempCharacters);
                GetEqualLinks(g_Step2TempCharacters);
                nScore = CalcScores();
                nTotalScore += nScore;
                nRepeatCnt++;
            }

            return nTotalScore;
        }

        public static int TrySwapWithTop(int i, int j)
        {
            if (i < 1) return 0;
            if (g_AllocCharacters[i-1, j] >= 11) // wood
                return 0;

            CopyCharactersToTemp();

            int nTemp = g_TempCharacters[i-1, j];
            g_TempCharacters[i-1, j] = g_TempCharacters[i, j];
            g_TempCharacters[i, j] = nTemp;

            return TrySwapping(i, j, 0);
        }

        public static int TrySwapWithLeft(int i, int j)
        {
            if (j < 1) return 0;
            if (g_AllocCharacters[i, j-1] >= 11) // wood
                return 0;

            CopyCharactersToTemp();

            int nTemp = g_TempCharacters[i, j-1];
            g_TempCharacters[i, j-1] = g_TempCharacters[i, j];
            g_TempCharacters[i, j] = nTemp;

            return TrySwapping(i, j, 1);
        }

        public static int TrySwapWithRight(int i, int j)
        {
            if (j >= BOARD_SIZE_W-1) return 0;
            if (g_AllocCharacters[i, j+1] >= 11) // wood
                return 0;

            CopyCharactersToTemp();

            int nTemp = g_TempCharacters[i, j+1];
            g_TempCharacters[i, j+1] = g_TempCharacters[i, j];
            g_TempCharacters[i, j] = nTemp;

            return TrySwapping(i, j, 2);
        }

        public static int TrySwapWithBottom(int i, int j)
        {
            if (i >= BOARD_SIZE_H - 1) return 0;
            if (g_AllocCharacters[i+1, j] >= 11) // wood
                return 0;

            CopyCharactersToTemp();

            int nTemp = g_TempCharacters[i+1, j];
            g_TempCharacters[i+1, j] = g_TempCharacters[i, j];
            g_TempCharacters[i, j]   = nTemp;

            return TrySwapping(i, j, 3);
        }

        public static void CopyCharactersToTemp()
        {
            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (int j = 0; j < BOARD_SIZE_W; j++)
                {
                    g_TempCharacters[i, j] = g_AllocCharacters[i, j];
                }
            }
        }

        public static int Process()
        {
            Global.g_moveStep1 = Global.MOVEMENT.Empty;
            Global.g_moveStep2 = Global.MOVEMENT.Empty;

            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (int j = 0; j < BOARD_SIZE_W; j++)
                {
                    g_ScoreByMovements[i, j, 0] = g_ScoreByMovements[i, j, 1] = g_ScoreByMovements[i, j, 2] = g_ScoreByMovements[i, j, 3] = 0;
                    g_ScoreByMovements2[i, j, 0] = g_ScoreByMovements2[i, j, 1] = g_ScoreByMovements2[i, j, 2] = g_ScoreByMovements2[i, j, 3] = 0;
                    
                    // not empty & not wood
                    if (g_AllocCharacters[i, j] != 0 && g_AllocCharacters[i, j] < 11)
                    {
                        g_ScoreByMovements[i, j, 0] = TrySwapWithTop(i, j);
                        g_ScoreByMovements[i, j, 1] = TrySwapWithLeft(i, j);
                        g_ScoreByMovements[i, j, 2] = TrySwapWithRight(i, j);
                        g_ScoreByMovements[i, j, 3] = TrySwapWithBottom(i, j);
                    }
                }
            }

            int nMaxI = 0, nMaxJ = 0, nMaxDirection = 0;
            int nMaxI2 = 0, nMaxJ2 = 0, nMaxDirection2 = 0;
            int nMaxVal1 = 0, nMaxVal2 = 0;

            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (int j = 0; j < BOARD_SIZE_W; j++)
                {
                    for (int k = 0; k < DIRECTION; k++)
                    {
                        if (nMaxVal1 < (g_ScoreByMovements[i, j, k] - g_ScoreByMovements2[i, j, k]))
                        {
                            nMaxVal1 = g_ScoreByMovements[i, j, k] - g_ScoreByMovements2[i, j, k];
                            nMaxI = i; nMaxJ = j; nMaxDirection = k;
                        }

                        if (nMaxVal2 < g_ScoreByMovements2[i, j, k])
                        {
                            nMaxVal2 = g_ScoreByMovements2[i, j, k];
                            nMaxI2 = i; nMaxJ2 = j; nMaxDirection2 = k;
                        }
                    }
                }
            }

            if (nMaxVal1 < 1 && nMaxVal2 < 1)
                return 0;

            //if (nMaxVal1 >= 8000)
            //    EmulateMovement(nMaxI, nMaxJ, nMaxDirection);
            //else if (nMaxVal1 >= 2000)
            //{
            //    if (nMaxVal2 >= 8000)
            //        EmulateMovement(nMaxI2, nMaxJ2, nMaxDirection2);
            //    else
            //        EmulateMovement(nMaxI, nMaxJ, nMaxDirection);
            //}
            //else
            //{
            //    if (nMaxVal2 >= 2000)
            //        EmulateMovement(nMaxI2, nMaxJ2, nMaxDirection2);
            //    else if (nMaxVal1 > 1)
            //        EmulateMovement(nMaxI, nMaxJ, nMaxDirection);
            //    else
            //        EmulateMovement(nMaxI2, nMaxJ2, nMaxDirection2);
            //}

            Global.g_moveStep1.nX = nMaxI; Global.g_moveStep1.nY = nMaxJ; 
            Global.g_moveStep1.nD= nMaxDirection; Global.g_moveStep1.nScore = nMaxVal1;
            Global.g_moveStep2.nX = nMaxI2; Global.g_moveStep2.nY = nMaxJ2; 
            Global.g_moveStep2.nD = nMaxDirection2; Global.g_moveStep2.nScore = nMaxVal2;

            if (Global.g_bAssistantMode)
                return 0;

            if (nMaxVal1 >= 5000)
                EmulateMovement(nMaxI, nMaxJ, nMaxDirection);
            else if (nMaxVal2 >= 5000)
                EmulateMovement(nMaxI2, nMaxJ2, nMaxDirection2);
            else if (nMaxVal1 >= 2000)
                EmulateMovement(nMaxI, nMaxJ, nMaxDirection);
            else if(nMaxVal2 >= 4000)
                EmulateMovement(nMaxI2, nMaxJ2, nMaxDirection2);
            else if (nMaxVal1 > 1)
                EmulateMovement(nMaxI, nMaxJ, nMaxDirection);
            else
                EmulateMovement(nMaxI2, nMaxJ2, nMaxDirection2);
            
            return 0;
        }

        public static void EmulateMovement(int nMaxI, int nMaxJ, int nMaxDirection)
        {
            if (Global.g_MainWindow == null)
                return;

            Global.g_MainWindow.RemoveHintArrows();

            int X = Global.g_rcROI.X + 10 + 217;
            int Y = Global.g_rcROI.Y + 39 + 39;
            Global.GetRatioCalcedValues(Global.g_rcROI.Width, Global.g_rcROI.Height, ref X, ref Y);

            int nStepX = Global.DEF_MAIN_BOARD_W / 8;
            int nStepY = Global.DEF_MAIN_BOARD_H / 8;

            Point pt = new Point(X + nMaxJ * nStepX+nStepX/2, Y + nMaxI * nStepY + nStepY/2);
            Point ptTarget = Point.Empty;

            if (nMaxDirection == 0)// up
                ptTarget = new Point(X + nMaxJ * nStepX + nStepX / 2, Y + (nMaxI - 1) * nStepY + nStepY / 2);
            else if (nMaxDirection == 1) // left
                ptTarget = new Point(X + (nMaxJ-1) * nStepX + nStepX / 2, Y + nMaxI * nStepY + nStepY / 2);
            else if (nMaxDirection == 2) // right
                ptTarget = new Point(X + (nMaxJ + 1) * nStepX + nStepX / 2, Y + nMaxI * nStepY + nStepY / 2);
            else if (nMaxDirection == 3) // bottom
                ptTarget = new Point(X + nMaxJ * nStepX + nStepX / 2, Y + (nMaxI+1) * nStepY + nStepY / 2);
            
            SendMouseEventToPoint(pt, ptTarget);
        }

        public static bool SendMouseEventToPoint(Point ptStart, Point ptTarget)
        {
            try
            {
                Global.MouseDownTo(ptStart);
                Global.MouseMoveToAndUp(ptTarget);
                if (!Global.g_bAssistantMode)
                    Global.MouseMoveTo(new Point(10,100));

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
