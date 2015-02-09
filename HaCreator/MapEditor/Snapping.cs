/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using Microsoft.Xna.Framework;

namespace HaCreator.MapEditor
{
    public static class TileSnap
    {
        public static Hashtable tileCats = CreateSnapData();

        private static Hashtable CreateSnapData()
        {
            Hashtable result = new Hashtable();
            result["bsc"] = new bsc();
            result["edU"] = new edU();
            result["edD"] = new edD();
            result["enH0"] = new enH0();
            result["enH1"] = new enH1();
            result["enV0"] = new enV0();
            result["enV1"] = new enV1();
            result["slLD"] = new slLD();
            result["slLU"] = new slLU();
            result["slRD"] = new slRD();
            result["slRU"] = new slRU();
            return result;
        }
    }

    internal class TileSnapInfo
    {
        // Fields
        public string tileCat;
        public int snapx;
        public int snapy;


        public TileSnapInfo(string tileCat, int snapx, int snapy)
        {
            this.tileCat = tileCat;
            this.snapx = snapx;
            this.snapy = snapy;
        }

/*        public bool Snaps(string A_0, int A_1, int A_2, int A_3)
        {
            return ((this.tileCat == A_0) && (MishpatPithagoras((double)(snapx * A_3), (double)(snapy * A_3), (double)A_1, (double)A_2) <= 10.0));
        }

        public static double MishpatPithagoras(double A_0, double A_1, double A_2, double A_3) //allowedxSnap, allowedySnap, distancex, distancey snapping other -> this
        {
            return Math.Sqrt(Math.Pow(A_2 - A_0, 2.0) + Math.Pow(A_3 - A_1, 2.0));
        }*/
    }



    internal abstract class TileSnapGroup
    {
        public string tileCat;
        public List<TileSnapInfo> tileList = new List<TileSnapInfo>();

/*        public Point Snap(string tS, int A_1, int A_2, int A_3)
        {
            A_1 -= base.b.f("x");
            A_2 -= base.b.f("y");
            foreach (TileSnapInfo calc in this.tileList)
            {
                if (calc.Snaps(tS, A_1, A_2, A_3))
                {
                    return new Point((calc.snapx * A_3) + base.b.f("x"), (calc.snapy * A_3) + base.b.f("y"));
                }
            }
            return new Point(0xffff, 0xffff);
        }*/
    }

    //Credits to Koolk
    internal class enH0 : TileSnapGroup
    {
        public enH0()
        {
            base.tileCat = "enH0";//static tile
            base.tileList.Add(new TileSnapInfo("bsc", 0, 0));//dynamic tile
            base.tileList.Add(new TileSnapInfo("enH0", -90, 0));
            base.tileList.Add(new TileSnapInfo("enH0", 90, 0));
            base.tileList.Add(new TileSnapInfo("enH1", 0, 0));
            base.tileList.Add(new TileSnapInfo("edU", 0, 0));
            base.tileList.Add(new TileSnapInfo("edU", 90, 0));
            base.tileList.Add(new TileSnapInfo("enV0", 90, -60));
            base.tileList.Add(new TileSnapInfo("enV1", 0, -60));
            base.tileList.Add(new TileSnapInfo("slLU", 0, 60));
            base.tileList.Add(new TileSnapInfo("slLU", 180, 0));
            base.tileList.Add(new TileSnapInfo("slRU", 90, 60));
            base.tileList.Add(new TileSnapInfo("slRU", -90, 0));
            base.tileList.Add(new TileSnapInfo("slLD", 90, 0));
            base.tileList.Add(new TileSnapInfo("slRD", 0, 0));
        }
    }

    internal class slLD : TileSnapGroup
    {
        public slLD()
        {
            base.tileCat = "slLD";
            base.tileList.Add(new TileSnapInfo("bsc", 0, 0));
            base.tileList.Add(new TileSnapInfo("bsc", -90, -60));
            base.tileList.Add(new TileSnapInfo("enH0", -90, 0));
            base.tileList.Add(new TileSnapInfo("enH1", 0, 60));
            base.tileList.Add(new TileSnapInfo("enH1", -180, 0));
            base.tileList.Add(new TileSnapInfo("enV1", 0, 0));
            base.tileList.Add(new TileSnapInfo("edD", -90, 0));
            base.tileList.Add(new TileSnapInfo("edD", 0, 60));
            base.tileList.Add(new TileSnapInfo("slLU", 0, 0));
            base.tileList.Add(new TileSnapInfo("slRU", 0, 0));
            base.tileList.Add(new TileSnapInfo("slRU", 0, 60));
            base.tileList.Add(new TileSnapInfo("slRU", -90, 0));
            base.tileList.Add(new TileSnapInfo("slLD", 90, 60));
            base.tileList.Add(new TileSnapInfo("slLD", -90, -60));
            base.tileList.Add(new TileSnapInfo("slRD", 0, 0));
            base.tileList.Add(new TileSnapInfo("slRD", -180, 0));
        }
    }

    internal class slRD : TileSnapGroup
    {
        public slRD()
        {
            base.tileCat = "slRD";
            base.tileList.Add(new TileSnapInfo("bsc", -90, 0));
            base.tileList.Add(new TileSnapInfo("bsc", 0, -60));
            base.tileList.Add(new TileSnapInfo("enH0", 0, 0));
            base.tileList.Add(new TileSnapInfo("enH1", -90, 60));
            base.tileList.Add(new TileSnapInfo("enH1", 90, 0));
            base.tileList.Add(new TileSnapInfo("enV0", 0, 0));
            base.tileList.Add(new TileSnapInfo("edD", 90, 0));
            base.tileList.Add(new TileSnapInfo("edD", 0, 60));
            base.tileList.Add(new TileSnapInfo("slLU", 0, 0));
            base.tileList.Add(new TileSnapInfo("slLU", 0, 60));
            base.tileList.Add(new TileSnapInfo("slLU", 90, 0));
            base.tileList.Add(new TileSnapInfo("slRU", 0, 0));
            base.tileList.Add(new TileSnapInfo("slLD", 0, 0));
            base.tileList.Add(new TileSnapInfo("slLD", 180, 0));
            base.tileList.Add(new TileSnapInfo("slRD", 90, -60));
            base.tileList.Add(new TileSnapInfo("slRD", -90, 60));
        }
    }

    internal class slRU : TileSnapGroup
    {
        public slRU()
        {
            base.tileCat = "slRU";
            base.tileList.Add(new TileSnapInfo("bsc", -90, -60));
            base.tileList.Add(new TileSnapInfo("bsc", 0, 0));
            base.tileList.Add(new TileSnapInfo("enH0", -90, -60));
            base.tileList.Add(new TileSnapInfo("enH0", 90, 0));
            base.tileList.Add(new TileSnapInfo("enH1", 0, 0));
            base.tileList.Add(new TileSnapInfo("enV0", 0, -60));
            base.tileList.Add(new TileSnapInfo("edU", 90, 0));
            base.tileList.Add(new TileSnapInfo("edU", 0, -60));
            base.tileList.Add(new TileSnapInfo("slLU", 0, 0));
            base.tileList.Add(new TileSnapInfo("slLU", 180, 0));
            base.tileList.Add(new TileSnapInfo("slRU", 90, 60));
            base.tileList.Add(new TileSnapInfo("slRU", -90, -60));
            base.tileList.Add(new TileSnapInfo("slLD", 0, 0));
            base.tileList.Add(new TileSnapInfo("slLD", 0, -60));
            base.tileList.Add(new TileSnapInfo("slLD", 90, 0));
            base.tileList.Add(new TileSnapInfo("slRD", 0, 0));
        }
    }

    internal class enV0 : TileSnapGroup
    {
        public enV0()
        {
            base.tileCat = "enV0";
            base.tileList.Add(new TileSnapInfo("bsc", 0, 0));
            base.tileList.Add(new TileSnapInfo("enH0", -90, 60));
            base.tileList.Add(new TileSnapInfo("enH1", -90, 0));
            base.tileList.Add(new TileSnapInfo("enV0", 0, -60));
            base.tileList.Add(new TileSnapInfo("enV0", 0, 60));
            base.tileList.Add(new TileSnapInfo("enV1", 0, 0));
            base.tileList.Add(new TileSnapInfo("edU", 0, 0));
            base.tileList.Add(new TileSnapInfo("edD", 0, 60));
            base.tileList.Add(new TileSnapInfo("slRU", 0, 60));
            base.tileList.Add(new TileSnapInfo("slRD", 0, 0));
        }
    }

    internal class slLU : TileSnapGroup
    {
        public slLU()
        {
            base.tileCat = "slLU";
            base.tileList.Add(new TileSnapInfo("bsc", 0, -60));
            base.tileList.Add(new TileSnapInfo("bsc", -90, 0));
            base.tileList.Add(new TileSnapInfo("enH0", 0, -60));
            base.tileList.Add(new TileSnapInfo("enH0", -180, 0));
            base.tileList.Add(new TileSnapInfo("enH1", -90, 0));
            base.tileList.Add(new TileSnapInfo("enV1", 0, -60));
            base.tileList.Add(new TileSnapInfo("edU", -90, 0));
            base.tileList.Add(new TileSnapInfo("edU", 0, -60));
            base.tileList.Add(new TileSnapInfo("slLU", 90, -60));
            base.tileList.Add(new TileSnapInfo("slLU", -90, 60));
            base.tileList.Add(new TileSnapInfo("slRU", 0, 0));
            base.tileList.Add(new TileSnapInfo("slRU", -180, 0));
            base.tileList.Add(new TileSnapInfo("slLD", 0, 0));
            base.tileList.Add(new TileSnapInfo("slRD", 0, 0));
            base.tileList.Add(new TileSnapInfo("slRD", 0, -60));
            base.tileList.Add(new TileSnapInfo("slRD", -90, 0));
        }
    }

    internal class enV1 : TileSnapGroup
    {
        public enV1()
        {
            base.tileCat = "enV1";
            base.tileList.Add(new TileSnapInfo("bsc", -90, 0));
            base.tileList.Add(new TileSnapInfo("enH0", 0, 60));
            base.tileList.Add(new TileSnapInfo("enH1", 0, 0));
            base.tileList.Add(new TileSnapInfo("enV0", 0, 0));
            base.tileList.Add(new TileSnapInfo("enV1", 0, -60));
            base.tileList.Add(new TileSnapInfo("enV1", 0, 60));
            base.tileList.Add(new TileSnapInfo("edU", 0, 0));
            base.tileList.Add(new TileSnapInfo("edD", 0, 60));
            base.tileList.Add(new TileSnapInfo("slLU", 0, 60));
            base.tileList.Add(new TileSnapInfo("slLD", 0, 0));
        }
    }

    internal class enH1 : TileSnapGroup
    {
        public enH1()
        {
            base.tileCat = "enH1";
            base.tileList.Add(new TileSnapInfo("bsc", 0, -60));
            base.tileList.Add(new TileSnapInfo("enH0", 0, 0));
            base.tileList.Add(new TileSnapInfo("enH1", -90, 0));
            base.tileList.Add(new TileSnapInfo("enH1", 90, 0));
            base.tileList.Add(new TileSnapInfo("edD", 0, 0));
            base.tileList.Add(new TileSnapInfo("edD", 90, 0));
            base.tileList.Add(new TileSnapInfo("enV0", 90, 0));
            base.tileList.Add(new TileSnapInfo("enV1", 0, 0));
            base.tileList.Add(new TileSnapInfo("slLU", 90, 0));
            base.tileList.Add(new TileSnapInfo("slRU", 0, 0));
            base.tileList.Add(new TileSnapInfo("slLD", 0, -60));
            base.tileList.Add(new TileSnapInfo("slLD", 180, 0));
            base.tileList.Add(new TileSnapInfo("slRD", 90, -60));
            base.tileList.Add(new TileSnapInfo("slRD", -90, 0));
        }
    }

    internal class edU : TileSnapGroup
    {
        public edU()
        {
            base.tileCat = "edU";
            base.tileList.Add(new TileSnapInfo("enH0", -90, 0));
            base.tileList.Add(new TileSnapInfo("enH0", 0, 0));
            base.tileList.Add(new TileSnapInfo("enV0", 0, 0));
            base.tileList.Add(new TileSnapInfo("enV1", 0, 0));
            base.tileList.Add(new TileSnapInfo("edD", 0, 0));
            base.tileList.Add(new TileSnapInfo("slLU", 90, 0));
            base.tileList.Add(new TileSnapInfo("slLU", 0, 60));
            base.tileList.Add(new TileSnapInfo("slRU", -90, 0));
            base.tileList.Add(new TileSnapInfo("slRU", 0, 60));
        }
    }

    internal class edD : TileSnapGroup
    {
        public edD()
        {
            base.tileCat = "edD";
            base.tileList.Add(new TileSnapInfo("enH1", -90, 0));
            base.tileList.Add(new TileSnapInfo("enH1", 0, 0));
            base.tileList.Add(new TileSnapInfo("enV0", 0, -60));
            base.tileList.Add(new TileSnapInfo("enV1", 0, -60));
            base.tileList.Add(new TileSnapInfo("edU", 0, 0));
            base.tileList.Add(new TileSnapInfo("slLD", 90, 0));
            base.tileList.Add(new TileSnapInfo("slLD", 0, -60));
            base.tileList.Add(new TileSnapInfo("slRD", -90, 0));
            base.tileList.Add(new TileSnapInfo("slRD", 0, -60));
        }
    }

    internal class bsc : TileSnapGroup
    {
        public bsc()
        {
            base.tileCat = "bsc";
            base.tileList.Add(new TileSnapInfo("bsc", -90, 0));
            base.tileList.Add(new TileSnapInfo("bsc", 90, 0));
            base.tileList.Add(new TileSnapInfo("bsc", 0, -60));
            base.tileList.Add(new TileSnapInfo("bsc", 0, 60));
            base.tileList.Add(new TileSnapInfo("enH0", 0, 0));
            base.tileList.Add(new TileSnapInfo("enH1", 0, 60));
            base.tileList.Add(new TileSnapInfo("enV0", 0, 0));
            base.tileList.Add(new TileSnapInfo("enV1", 90, 0));
            base.tileList.Add(new TileSnapInfo("slLU", 0, 60));
            base.tileList.Add(new TileSnapInfo("slLU", 90, 0));
            base.tileList.Add(new TileSnapInfo("slRU", 90, 60));
            base.tileList.Add(new TileSnapInfo("slRU", 0, 0));
            base.tileList.Add(new TileSnapInfo("slLD", 0, 0));
            base.tileList.Add(new TileSnapInfo("slLD", 90, 60));
            base.tileList.Add(new TileSnapInfo("slRD", 90, 0));
            base.tileList.Add(new TileSnapInfo("slRD", 0, 60));
        }
    }
}

 


