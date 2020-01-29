using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameAuto
{
    public class ImageMatcher
    {
        public static bool CompareImage(Image<Bgr, Byte> imgModel, Image<Bgr, Byte> imgCandi)
        {
            bool bFound = false;

            byte[,,] pDataModel = imgModel.Data;
            byte[,,] pDataCandi = imgCandi.Data;

            int nCompX = 0, nCompY = 0, nCount = 0;
            int nWid = imgModel.Width; int nHei = imgModel.Height;

            while (nCompY < imgCandi.Height - nHei)
            {
                nCount = 0;
                for (int y = 0; y < nHei; y++)
                {
                    for (int x = 0; x < nWid; x++)
                    {
                        int B = pDataModel[y, x, 0]; int G = pDataModel[y, x, 1]; int R = pDataModel[y, x, 2];
                        int cB = pDataCandi[y + nCompY, x + nCompX, 0]; int cG = pDataCandi[y + nCompY, x + nCompX, 1]; int cR = pDataCandi[y, x, 2];

                        int diffR = Math.Abs(R - cR); int diffG = Math.Abs(G - cG); int diffB = Math.Abs(B - cB);
                        if (diffB < 5 && diffG < 5 && diffR < 5)
                            nCount++;
                    }
                }

                if (nCount >= nWid * nHei * 8 / 10)
                {
                    bFound = true;
                    break;
                }

                nCompX += 5;

                if (nCompX + nWid >= imgCandi.Width)
                {
                    nCompX = 0;
                    nCompY += 5;
                }
            }

            return bFound;
        }

        private static Rgb COL_FROG = new Rgb(208, 216, 78);
        private static Rgb COL_FROG_BT = new Rgb(64, 110, 24);

        private static Rgb COL_FROG_WATER = new Rgb(209, 217, 79);
        private static Rgb COL_FROG_WATER_AB = new Rgb(123, 0, 15);

        private static Rgb COL_DUCK = new Rgb(247, 187, 70);
        private static Rgb COL_DUCK_BT = new Rgb(250, 195, 47);

        private static Rgb COL_DUCK_WATER = new Rgb(245, 172, 50);
        private static Rgb COL_DUCK_WATER_BT = new Rgb(156, 153, 136);

        private static Rgb COL_SAMBALI = new Rgb(203, 43, 123);
        private static Rgb COL_SAMBALI_BT = new Rgb(203, 43, 123);

        private static Rgb COL_SAMBALI_WATER = new Rgb(193, 35, 116);
        private static Rgb COL_SAMBALI_WATER_BT = new Rgb(110, 2, 79);

        private static Rgb COL_WHALE = new Rgb(57, 67, 85);
        private static Rgb COL_WHALE_CT = new Rgb(254, 255, 255);

        private static Rgb COL_WHALE_WATER = new Rgb(49, 53, 63);
        private static Rgb COL_WHALE_WATER_CT = new Rgb(255, 213, 194);

        private static Rgb COL_OCTOPUS = new Rgb(202, 48, 7);
        private static Rgb COL_OCTOPUS_BT = new Rgb(157, 69, 34);

        private static Rgb COL_WATER_OCTOPUS = new Rgb(214, 75, 24);
        private static Rgb COL_WATER_OCTOPUS_BT = new Rgb(87, 82, 107);

        private static Rgb COL_WOOD = new Rgb(172, 73, 26);
        private static Rgb COL_WOOD_CT = new Rgb(73, 145, 213);
        private static Rgb COL_WOOD_BT = new Rgb(89, 32, 29);

        public static Global.CHARACTER_TYPE DetermineCharacter(Image<Bgr, Byte> imgCandi)
        {
            if (DetermineFrog(imgCandi))
                return Global.CHARACTER_TYPE.CHAR_FROG;
            else if (DetermineFrogWater(imgCandi))
                return Global.CHARACTER_TYPE.CHAR_FROG_WATER;
            else if (DetermineDuck(imgCandi))
                return Global.CHARACTER_TYPE.CHAR_DUCK;
            else if (DetermineDuckWater(imgCandi))
                return Global.CHARACTER_TYPE.CHAR_DUCK_WATER;
            else if (DetermineSambali(imgCandi))
                return Global.CHARACTER_TYPE.CHAR_SAMBALI;
            else if (DetermineSambaliWater(imgCandi))
                return Global.CHARACTER_TYPE.CHAR_SAMBALI_WATER;
            else if (DetermineWhale(imgCandi))
                return Global.CHARACTER_TYPE.CHAR_WHALE;
            else if (DetermineWhaleWater(imgCandi))
                return Global.CHARACTER_TYPE.CHAR_WHALE_WATER;
            else if (DetermineOctopus(imgCandi))
                return Global.CHARACTER_TYPE.CHAR_OCTOPUS;
            else if (DetermineOctopusWater(imgCandi))
                return Global.CHARACTER_TYPE.CHAR_OCTOPUS_WATER;
            else if (DetermineWood(imgCandi))
                return Global.CHARACTER_TYPE.CHAR_WOOD;

            return Global.CHARACTER_TYPE.CHAR_NONE;
        }

        private static bool DetermineFrog(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_FROG.Blue) < 10 && Math.Abs(G - COL_FROG.Green) < 10 && Math.Abs(R - COL_FROG.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei - 15; y < nHei; y++)
            {
                for (int x = 5; x < 15; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_FROG_BT.Blue) < 10 && Math.Abs(G - COL_FROG_BT.Green) < 10 && Math.Abs(R - COL_FROG_BT.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return bFound;
        }

        private static bool DetermineFrogWater(Image<Bgr, Byte> imgCandi)
        {
            //water_frog: 209, 217, 79, center above: 123, 0, 15
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_FROG_WATER.Blue) < 10 && Math.Abs(G - COL_FROG_WATER.Green) < 10 && Math.Abs(R - COL_FROG_WATER.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei / 3 - 5; y < nHei / 3 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_FROG_WATER_AB.Blue) < 10 && Math.Abs(G - COL_FROG_WATER_AB.Green) < 10 && Math.Abs(R - COL_FROG_WATER_AB.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return bFound;
        }

        private static bool DetermineDuck(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_DUCK.Blue) < 10 && Math.Abs(G - COL_DUCK.Green) < 10 && Math.Abs(R - COL_DUCK.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei / 6 * 5 - 5; y < nHei / 6 * 5 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_DUCK_BT.Blue) < 10 && Math.Abs(G - COL_DUCK_BT.Green) < 10 && Math.Abs(R - COL_DUCK_BT.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return bFound;
        }

        private static bool DetermineDuckWater(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_DUCK_WATER.Blue) < 10 && Math.Abs(G - COL_DUCK_WATER.Green) < 10 && Math.Abs(R - COL_DUCK_WATER.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei / 6 * 5 - 5; y < nHei / 6 * 5 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_DUCK_WATER_BT.Blue) < 10 && Math.Abs(G - COL_DUCK_WATER_BT.Green) < 10 && Math.Abs(R - COL_DUCK_WATER_BT.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return bFound;
        }

        private static bool DetermineSambali(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_SAMBALI.Blue) < 10 && Math.Abs(G - COL_SAMBALI.Green) < 10 && Math.Abs(R - COL_SAMBALI.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei / 2; y < nHei / 2 + 10; y++)
            {
                for (int x = nWid / 2; x < nWid / 2 + 10; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_SAMBALI_WATER_BT.Blue) < 10 && Math.Abs(G - COL_SAMBALI_WATER_BT.Green) < 10 && Math.Abs(R - COL_SAMBALI_WATER_BT.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return !bFound;
        }

        private static bool DetermineSambaliWater(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_SAMBALI.Blue) < 10 && Math.Abs(G - COL_SAMBALI.Green) < 10 && Math.Abs(R - COL_SAMBALI.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei / 2; y < nHei / 2 + 10; y++)
            {
                for (int x = nWid / 2; x < nWid / 2 + 10; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_SAMBALI_WATER_BT.Blue) < 10 && Math.Abs(G - COL_SAMBALI_WATER_BT.Green) < 10 && Math.Abs(R - COL_SAMBALI_WATER_BT.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return bFound;
        }

        private static bool DetermineWhale(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WHALE.Blue) < 10 && Math.Abs(G - COL_WHALE.Green) < 10 && Math.Abs(R - COL_WHALE.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WHALE_CT.Blue) < 10 && Math.Abs(G - COL_WHALE_CT.Green) < 10 && Math.Abs(R - COL_WHALE_CT.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return bFound;
        }

        private static bool DetermineWhaleWater(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WHALE_WATER.Blue) < 10 && Math.Abs(G - COL_WHALE_WATER.Green) < 10 && Math.Abs(R - COL_WHALE_WATER.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei / 2; y < nHei / 2 + 10; y++)
            {
                for (int x = nWid / 2; x < nWid / 2 + 10; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WHALE_WATER_CT.Blue) < 10 && Math.Abs(G - COL_WHALE_WATER_CT.Green) < 10 && Math.Abs(R - COL_WHALE_WATER_CT.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return bFound;
        }

        private static bool DetermineOctopus(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_OCTOPUS.Blue) < 10 && Math.Abs(G - COL_OCTOPUS.Green) < 10 && Math.Abs(R - COL_OCTOPUS.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei / 6 * 5 - 5; y < nHei / 6 * 5 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_OCTOPUS_BT.Blue) < 10 && Math.Abs(G - COL_OCTOPUS_BT.Green) < 10 && Math.Abs(R - COL_OCTOPUS_BT.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return bFound;
        }

        private static bool DetermineOctopusWater(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WATER_OCTOPUS.Blue) < 10 && Math.Abs(G - COL_WATER_OCTOPUS.Green) < 10 && Math.Abs(R - COL_WATER_OCTOPUS.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei - 15; y < nHei - 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WATER_OCTOPUS_BT.Blue) < 10 && Math.Abs(G - COL_WATER_OCTOPUS_BT.Green) < 10 && Math.Abs(R - COL_WATER_OCTOPUS_BT.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return bFound;
        }

        private static bool DetermineWood(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WOOD.Blue) < 10 && Math.Abs(G - COL_WOOD.Green) < 10 && Math.Abs(R - COL_WOOD.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            //if (!bFound) return false;
            //bFound = false;
            //for (int y = nHei / 2 - 10; y < nHei / 2; y++)
            //{
            //    for (int x = 5; x < nWid / 2 - 10; x++)
            //    {
            //        int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
            //        if (Math.Abs(B - COL_WOOD_CT.Blue) < 10 && Math.Abs(G - COL_WOOD_CT.Green) < 10 && Math.Abs(R - COL_WOOD_CT.Red) < 10)
            //        {
            //            bFound = true;
            //            break;
            //        }
            //    }
            //}

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei-15; y<nHei; y++)
            {
                for (int x = 5; x<15; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WOOD_BT.Blue) < 10 && Math.Abs(G - COL_WOOD_BT.Green) < 10 && Math.Abs(R - COL_WOOD_BT.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return bFound;
        }
    }
}