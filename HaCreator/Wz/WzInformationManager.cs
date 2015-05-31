/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using HaCreator.MapEditor.Info;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using MapleLib.WzLib.WzStructure.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.Wz
{
    public class WzInformationManager
    {
        public MapleTable<string> NPCs = new MapleTable<string>();
        public MapleTable<string> Mobs = new MapleTable<string>();
        public MapleTable<ReactorInfo> Reactors = new MapleTable<ReactorInfo>();
        public MapleTable<WzImage> TileSets = new MapleTable<WzImage>();
        public MapleTable<WzImage> ObjectSets = new MapleTable<WzImage>();
        public MapleTable<WzImage> BackgroundSets = new MapleTable<WzImage>();
        public MapleTable<WzSoundProperty> BGMs = new MapleTable<WzSoundProperty>();
        public MapleTable<Bitmap> MapMarks = new MapleTable<Bitmap>();
        public MapleTable<string> Maps = new MapleTable<string>();
        public MapleTable<string, PortalInfo> Portals;
        public List<string> PortalTypeById;
        public MapleTable<string, int> PortalIdByType;
        public MapleTable<string, PortalGameImageInfo> GamePortals = new MapleTable<string, PortalGameImageInfo>();
    }
}
