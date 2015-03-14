/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapleLib;
using MapleLib.WzLib;
using MapleLib.WzLib.Util;
using MapleLib.WzLib.WzProperties;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HaCreator.MapEditor;
using MapleLib.WzLib.WzStructure.Data;
using MapleLib.WzLib.WzStructure;

namespace HaCreator
{
    public static class UserSettings
    {
        public static bool ShowErrorsMessage = true;
        public static bool ShowMousePos = true;
        public static bool XGAResolution = false;
        public static bool ClipText = false;
        public static System.Drawing.Color TabColor = System.Drawing.Color.LightSteelBlue;
        public static int LineWidth = 1;
        public static int DotWidth = 3;
        public static Color SelectSquare = new Color(0, 0, 255, 255);
        public static Color SelectSquareFill = new Color(0, 0, 200, 200);
        public static Color SelectedColor = new Color(0, 0, 255, 250);
        public static Color VRColor = new Color(0, 0, 255, 255);
        public static Color FootholdColor = Color.Red;
        public static Color RopeColor = Color.Green;
        public static Color ChairColor = Color.Orange;
        public static Color ToolTipColor = Color.SkyBlue;
        public static Color ToolTipFill = new Color(0, 0, 255, 100);
        public static Color ToolTipSelectedFill = new Color(0, 0, 255, 150);
        public static Color ToolTipCharFill = new Color(0, 255, 0, 100);
        public static Color ToolTipCharSelectedFill = new Color(0, 255, 0, 150);
        public static Color ToolTipBindingLine = Color.Magenta;
        public static Color MiscColor = Color.Brown;
        public static Color MiscFill = new Color(150, 75, 0, 100);
        public static Color MiscSelectedFill = new Color(150, 75, 0, 150);
        public static int NonActiveAlpha = 63;
        public static int Mobrx0Offset = 200;
        public static int Mobrx1Offset = 200;
        public static int Npcrx0Offset = 20;
        public static int Npcrx1Offset = 20;
        public static int defaultMobTime = 0;
        public static int defaultReactorTime = 0;
        public static float SnapDistance = 10;
        public static float SignificantDistance = 10;
        public static int ScrollDistance = 120;
        public static double ScrollFactor = 1;
        public static double ScrollBase = 1.05;
        public static double ScrollExponentFactor = 1;
        public static int zShift = 1;
        public static int HiddenLifeR = 127;
        public static string FontName = "Arial";
        public static int FontSize = 15;
        public static System.Drawing.FontStyle FontStyle = System.Drawing.FontStyle.Regular;
        public static int dotDescriptionBoxSize = 100;
        public static int ImageViewerHeight = 100;
        public static int ImageViewerWidth = 100;

        public static bool useMiniMap = true;
        public static bool showVR = true;
        public static bool useSnapping = true;
        public static bool emulateParallax = true;
        public static bool suppressWarnings = false;
        public static bool FixFootholdMispositions = true;

        // Controls debug features such feature compatibility testing and special exception handling
        public static bool enableDebug =
#if DEBUG
            true;
#else
            false;
#endif
    }

    public static class ApplicationSettings
    {
        public static ItemTypes theoreticalVisibleTypes = ItemTypes.All; // These two are marked theoretical because the visible\edited types in effect (Board.VisibleTypes\EditedTypes)
        public static ItemTypes theoreticalEditedTypes = ItemTypes.All ^ ItemTypes.Backgrounds; // are subject to the current mode of operation
        public static int MapleVersionIndex = 3;
        public static string MapleFolder = "";
        public static int MapleFolderIndex = 0;
        public static System.Drawing.Size LastMapSize = new System.Drawing.Size(800, 800);
        public static int lastRadioIndex = 3;
        public static bool newTab = true;
        public static bool randomTiles = true;
        public static bool InfoMode = false;
        public static int lastDefaultLayer = -1;
        public static string AuthorEmail = new string(Encoding.ASCII.GetChars(new byte[] { 
                                            0x68,0x61,0x68,0x61,0x30,0x31,0x68,0x61,0x68,0x61,0x30,0x31,0x40,0x67,0x6d,0x61,0x69,0x6c,0x2e,0x63,0x6f,0x6d 
                                            })); // The email address is obfuscated to prevent spambots from finding it on the git webpage
    }
}
