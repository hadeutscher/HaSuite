/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HaCreator.MapEditor;
using Microsoft.Xna.Framework;
using MapleLib.WzLib;
using MapleLib.WzLib.WzStructure;
using MapleLib.WzLib.WzStructure.Data;
using MapleLib.Helpers;
using MapleLib.WzLib.WzProperties;
using HaCreator.ThirdParty.TabPages;
using System.Collections;

namespace HaCreator.WzStructure
{
    public static class MapLoader
    {
        private static void VerifyMapPropsKnown(WzImage mapImage)
        {
            foreach (IWzImageProperty prop in mapImage.WzProperties) switch (prop.Name)
                {
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "info":
                    case "life":
                    case "ladderRope":
                    case "reactor":
                    case "back":
                    case "foothold":
                    case "miniMap":
                    case "portal":
                    case "seat":
                    case "ToolTip":
                        continue;
                    default:
                        string error = "Unknown property " + prop.Name + ". While this map can still be edited it is recommended to report this to haha01haha01 for better functionality of the editor.";
                        //MessageBox.Show(error);
                        MapleLib.Helpers.ErrorLogger.Log(ErrorLevel.MissingFeature, error);
                        break;
                }
        }

        private static MapType GetMapType(WzImage mapImage)
        {
            switch (mapImage.Name)
            {
                case "MapLogin1.img":
                case "MapLogin.img":
                    return MapType.MapLogin;
                case "CashShopPreview.img":
                    return MapType.CashShopPreview;
                default:
                    return MapType.RegularMap;
            }
        }

        private static bool GetMapVR(WzImage mapImage, ref System.Drawing.Rectangle? VR)
        {
            WzSubProperty fhParent = (WzSubProperty)mapImage["foothold"];
            if (fhParent == null) { VR = null; return false; }
            int mostRight = int.MinValue, mostLeft = int.MaxValue, mostTop = int.MaxValue, mostBottom = int.MinValue;
            foreach (WzSubProperty fhLayer in fhParent.WzProperties)
            {
                foreach (WzSubProperty fhCat in fhLayer.WzProperties)
                {
                    foreach (WzSubProperty fh in fhCat.WzProperties)
                    {
                        int x1 = InfoTool.GetInt(fh["x1"]);
                        int x2 = InfoTool.GetInt(fh["x2"]);
                        int y1 = InfoTool.GetInt(fh["y1"]);
                        int y2 = InfoTool.GetInt(fh["y2"]);
                        if (x1 > mostRight) mostRight = x1;
                        if (x1 < mostLeft) mostLeft = x1;
                        if (x2 > mostRight) mostRight = x2;
                        if (x2 < mostLeft) mostLeft = x2;
                        if (y1 > mostBottom) mostBottom = y1;
                        if (y1 < mostTop) mostTop = y1;
                        if (y2 > mostBottom) mostBottom = y2;
                        if (y2 < mostTop) mostTop = y2;
                    }
                }
            }
            if (mostRight == int.MinValue || mostLeft == int.MaxValue || mostTop == int.MaxValue || mostBottom == int.MinValue)
            {
                VR = null; return false;
            }
            int VRLeft = mostLeft - 10;
            int VRRight = mostRight + 10;
            int VRBottom = mostBottom + 110;
            int VRTop = Math.Min(mostBottom - 600, mostTop - 360);
            VR = new System.Drawing.Rectangle(VRLeft, VRTop, VRRight - VRLeft, VRBottom - VRTop);
            return true;
            /*
            int left = int.MaxValue, top = int.MaxValue, right = int.MinValue, bottom = int.MinValue;
            for (int layer = 0; layer <= 7; layer++)
            {
                WzSubProperty layerProp = (WzSubProperty)mapImage[layer.ToString()];
                foreach (IWzImageProperty obj in layerProp["obj"].WzProperties)
                {
                    int x = InfoTool.GetInt(obj["x"]);
                    int y = InfoTool.GetInt(obj["y"]);
                    if (x < left) left = x;
                    if (x > right) right = x;
                    if (y < top) top = y;
                    if (y > bottom) bottom = y;
                }
                foreach (IWzImageProperty tile in layerProp["tile"].WzProperties)
                {
                    int x = InfoTool.GetInt(tile["x"]);
                    int y = InfoTool.GetInt(tile["y"]);
                    if (x < left) left = x;
                    if (x > right) right = x;
                    if (y < top) top = y;
                    if (y > bottom) bottom = y;
                }
            }
            size = new XNA.Point(right - left + 200, bottom - top + 200);
            center = new XNA.Point(100 - left, 100 - top);*/
        }

        private static void LoadLayers(WzImage mapImage, Board mapBoard)
        {
            for (int layer = 0; layer <= 7; layer++)
            {
                WzSubProperty layerProp = (WzSubProperty)mapImage[layer.ToString()];
                IWzImageProperty tSprop = layerProp["info"]["tS"];
                string tS = null;
                if (tSprop != null) tS = InfoTool.GetString(tSprop);
                foreach (IWzImageProperty obj in layerProp["obj"].WzProperties)
                {
                    int x = InfoTool.GetInt(obj["x"]);
                    int y = InfoTool.GetInt(obj["y"]);
                    int z = InfoTool.GetInt(obj["z"]);
                    //int zM = InfoTool.GetInt(obj["zM"]);
                    string oS = InfoTool.GetString(obj["oS"]);
                    string l0 = InfoTool.GetString(obj["l0"]);
                    string l1 = InfoTool.GetString(obj["l1"]);
                    string l2 = InfoTool.GetString(obj["l2"]);
                    string name = InfoTool.GetOptionalString(obj["name"]);
                    MapleBool r = InfoTool.GetOptionalBool(obj["r"]);
                    MapleBool hide = InfoTool.GetOptionalBool(obj["hide"]);
                    MapleBool reactor = InfoTool.GetOptionalBool(obj["reactor"]);
                    MapleBool flow = InfoTool.GetOptionalBool(obj["flow"]);
                    int? rx = InfoTool.GetOptionalTranslatedInt(obj["rx"]);
                    int? ry = InfoTool.GetOptionalTranslatedInt(obj["ry"]);
                    int? cx = InfoTool.GetOptionalTranslatedInt(obj["cx"]);
                    int? cy = InfoTool.GetOptionalTranslatedInt(obj["cy"]);
                    string tags = InfoTool.GetOptionalString(obj["tags"]);
                    IWzImageProperty questParent = obj["quest"];
                    List<ObjectInstanceQuest> questInfo = null;
                    if (questParent != null)
                        foreach (WzCompressedIntProperty info in questParent.WzProperties)
                            questInfo.Add(new ObjectInstanceQuest(int.Parse(info.Name), (QuestState)info.Value));
                    bool flip = InfoTool.GetBool(obj["f"]);
                    IWzImageProperty objInfoProp = Program.InfoManager.ObjectSets[oS][l0][l1][l2];
                    if (objInfoProp.HCTag == null)
                        objInfoProp.HCTag = ObjectInfo.Load((WzSubProperty)objInfoProp, oS, l0, l1, l2);
                    ObjectInfo objInfo = (ObjectInfo)objInfoProp.HCTag;
                    mapBoard.BoardItems.TileObjs.Add((LayeredItem)objInfo.CreateInstance(mapBoard.Layers[layer], mapBoard, x, y, z, r, hide, reactor, flow, rx, ry, cx, cy, name, tags, questInfo, flip, false, false));
                }
                IWzImageProperty tileParent = layerProp["tile"];
                for (int i = 0; i < tileParent.WzProperties.Count; i++)
                //foreach (IWzImageProperty tile in layerProp["tile"].WzProperties)
                {
                    IWzImageProperty tile = tileParent.WzProperties[i];
                    int x = InfoTool.GetInt(tile["x"]);
                    int y = InfoTool.GetInt(tile["y"]);
                    //int zM = InfoTool.GetInt(tile["zM"]);
                    string u = InfoTool.GetString(tile["u"]);
                    int no = InfoTool.GetInt(tile["no"]);
                    IWzImageProperty tileInfoProp = Program.InfoManager.TileSets[tS][u][no.ToString()];
                    if (tileInfoProp.HCTag == null)
                        tileInfoProp.HCTag = TileInfo.Load((WzCanvasProperty)tileInfoProp, tS, u, no.ToString());
                    TileInfo tileInfo = (TileInfo)tileInfoProp.HCTag;
                    mapBoard.BoardItems.TileObjs.Add((LayeredItem)tileInfo.CreateInstance(mapBoard.Layers[layer], mapBoard, x, y, i /*zM*/, false, false, false));
                }
            }
        }

        private static void LoadLife(WzImage mapImage, Board mapBoard)
        {
            IWzImageProperty lifeParent = mapImage["life"];
            if (lifeParent == null) return;
            foreach (WzSubProperty life in lifeParent.WzProperties)
            {
                string id = InfoTool.GetString(life["id"]);
                int x = InfoTool.GetInt(life["x"]);
                //int y = InfoTool.GetInt(life["y"]);
                int cy = InfoTool.GetInt(life["cy"]);
                int? mobTime = InfoTool.GetOptionalInt(life["mobTime"]);
                int? info = InfoTool.GetOptionalInt(life["info"]);
                int? team = InfoTool.GetOptionalInt(life["team"]);
                int rx0 = InfoTool.GetInt(life["rx0"]);
                int rx1 = InfoTool.GetInt(life["rx1"]);
                MapleBool flip = InfoTool.GetOptionalBool(life["f"]);
                MapleBool hide = InfoTool.GetOptionalBool(life["hide"]);
                string type = InfoTool.GetString(life["type"]);
                string limitedname = InfoTool.GetOptionalString(life["limitedname"]);

                switch (type)
                {
                    case "m":
                        WzImage mobImage = (WzImage)Program.WzManager["mob"][id + ".img"];
                        if (!mobImage.Parsed) mobImage.ParseImage();
                        if (mobImage.HCTag == null)
                            mobImage.HCTag = MobInfo.Load(mobImage);
                        MobInfo mobInfo = (MobInfo)mobImage.HCTag;
                        mapBoard.BoardItems.Mobs.Add((MobInstance)mobInfo.CreateInstance(mapBoard, x, cy, rx0, rx1, limitedname, mobTime, flip, hide, info, team, false));
                        break;
                    case "n":
                        WzImage npcImage = (WzImage)Program.WzManager["npc"][id + ".img"];
                        if (!npcImage.Parsed) npcImage.ParseImage();
                        if (npcImage.HCTag == null)
                            npcImage.HCTag = NpcInfo.Load(npcImage);
                        NpcInfo npcInfo = (NpcInfo)npcImage.HCTag;
                        mapBoard.BoardItems.NPCs.Add((NPCInstance)npcInfo.CreateInstance(mapBoard, x, cy, rx0, rx1, limitedname, mobTime, flip, hide, info, team, false));
                        break;
                    default:
                        throw new Exception("invalid life type " + type);
                }
            }
        }

        private static void LoadReactors(WzImage mapImage, Board mapBoard)
        {
            WzSubProperty reactorParent = (WzSubProperty)mapImage["reactor"];
            if (reactorParent == null) return;
            foreach (WzSubProperty reactor in reactorParent.WzProperties)
            {
                int x = InfoTool.GetInt(reactor["x"]);
                int y = InfoTool.GetInt(reactor["y"]);
                int reactorTime = InfoTool.GetInt(reactor["reactorTime"]);
                string name = InfoTool.GetOptionalString(reactor["name"]);
                string id = InfoTool.GetString(reactor["id"]);
                bool flip = InfoTool.GetBool(reactor["f"]);
                mapBoard.BoardItems.Reactors.Add((ReactorInstance)Program.InfoManager.Reactors[id].CreateInstance(mapBoard, x, y, reactorTime, name, flip, false));
            }
        }

        private static void LoadChairs(WzImage mapImage, Board mapBoard)
        {
            WzSubProperty chairParent = (WzSubProperty)mapImage["seat"];
            if (chairParent != null) foreach (WzVectorProperty chair in chairParent.WzProperties)
                    mapBoard.BoardItems.Chairs.Add(new Chair(mapBoard, chair.X.Value, chair.Y.Value, false));
            mapBoard.BoardItems.Chairs.Sort(new Comparison<Chair>(
                    delegate(Chair a, Chair b)
                    {
                        if (a.X > b.X)
                            return 1;
                        else if (a.X < b.X)
                            return -1;
                        else
                        {
                            if (a.Y > b.Y)
                                return 1;
                            else if (a.Y < b.Y)
                                return -1;
                            else return 0;
                        }
                    }));
            for (int i = 0; i < mapBoard.BoardItems.Chairs.Count - 1; i++)
            {
                Chair a = mapBoard.BoardItems.Chairs[i];
                Chair b = mapBoard.BoardItems.Chairs[i + 1];
                if (a.Y == b.Y && a.X == b.X) //removing b is more comfortable because that way we don't need to decrease i
                {
                    if (a.Parent == null && b.Parent != null)
                    {
                        mapBoard.BoardItems.Chairs.Remove(a);
                        i--;
                    }
                    else
                        mapBoard.BoardItems.Chairs.Remove(b);

                }
            }
        }

        private static void LoadRopes(WzImage mapImage, Board mapBoard)
        {
            WzSubProperty ropeParent = (WzSubProperty)mapImage["ladderRope"];
            foreach (WzSubProperty rope in ropeParent.WzProperties)
            {
                int x = InfoTool.GetInt(rope["x"]);
                int y1 = InfoTool.GetInt(rope["y1"]);
                int y2 = InfoTool.GetInt(rope["y2"]);
                bool uf = InfoTool.GetBool(rope["uf"]);
                int page = InfoTool.GetInt(rope["page"]);
                bool l = InfoTool.GetBool(rope["l"]);
                mapBoard.BoardItems.Ropes.Add(new Rope(mapBoard, x, y1, y2, l, page, uf));
            }
        }

        private static void CheckAnchorPairMerge(FootholdAnchor a, FootholdAnchor b)
        {
            if (a.X == b.X && a.Y == b.Y && a.LayerNumber == b.LayerNumber)
            {
                FootholdLine lineA = (FootholdLine)a.connectedLines[0];
                FootholdLine lineB = (FootholdLine)b.connectedLines[0];
                if ((lineA.next == lineB.num && lineB.prev == lineA.num)
                    || (lineA.prev == lineB.num && lineB.next == lineA.num))
                {
                    b.removed = true;
                    if (lineB.FirstDot == b) //b is firstdot
                        lineB.FirstDot = a;
                    else
                        lineB.SecondDot = a;
                    a.connectedLines.Add(lineB);
                }
            }
        }

        private static void LoadFootholds(WzImage mapImage, Board mapBoard)
        {
            List<FootholdAnchor> anchors = new List<FootholdAnchor>();
            WzSubProperty footholdParent = (WzSubProperty)mapImage["foothold"];
            int layer;
            FootholdAnchor a;
            FootholdAnchor b;
            FootholdAnchor c;
            FootholdAnchor d;
            foreach (WzSubProperty layerProp in footholdParent.WzProperties)
            {
                layer = int.Parse(layerProp.Name);
                foreach (WzSubProperty platProp in layerProp.WzProperties)
                    foreach (WzSubProperty fhProp in platProp.WzProperties)
                    {
                        a = new FootholdAnchor(mapBoard, InfoTool.GetInt(fhProp["x1"]), InfoTool.GetInt(fhProp["y1"]), layer, false);
                        b = new FootholdAnchor(mapBoard, InfoTool.GetInt(fhProp["x2"]), InfoTool.GetInt(fhProp["y2"]), layer, false);
                        int num = int.Parse(fhProp.Name);
                        int next = InfoTool.GetInt(fhProp["next"]);
                        int prev = InfoTool.GetInt(fhProp["prev"]);
                        if (a.X != b.X || a.Y != b.Y)
                        {
                            FootholdLine fh = new FootholdLine(mapBoard, a, b);
                            fh.num = num;
                            fh.prev = prev;
                            fh.next = next;
                            mapBoard.BoardItems.FootholdLines.Add(fh);
                            anchors.Add(a);
                            anchors.Add(b);
                        }
                    }

                anchors.Sort(new Comparison<FootholdAnchor>(FootholdAnchor.FHAnchorSorter));
                int groupStart = 0; //inclusive
                int groupEnd = 0; //inclusive
                for (int i = 0; i < anchors.Count - 1; i++)
                {
                    a = anchors[i];
                    b = anchors[i + 1];
                    if (a.Y != b.Y && a.X != b.X && a.LayerNumber == b.LayerNumber)
                    {
                        groupEnd = i;
                        if (groupEnd - groupStart == 1) //there are 2 objects in the group, since groupEnd and groupStart are inclusive
                        {
                            c = anchors[groupStart];
                            d = anchors[groupEnd];
                            CheckAnchorPairMerge(c, d);
                        }
                        else if (groupEnd - groupStart != 0)
                        {
                            for (int j = groupStart; j <= groupEnd; j++)
                            {
                                c = anchors[j];
                                for (int k = groupStart; k <= groupEnd; k++)
                                {
                                    if (c.removed) break;
                                    d = anchors[k];
                                    if (d.removed) continue;
                                    CheckAnchorPairMerge(c, d);
                                }
                            }
                        }
                        groupStart = groupEnd + 1;
                    }
                }

                groupEnd = anchors.Count - 1;
                if (groupEnd - groupStart == 1) //there are 2 objects in the group, since groupEnd and groupStart are inclusive
                {
                    c = anchors[groupStart];
                    d = anchors[groupEnd];
                    CheckAnchorPairMerge(c, d);
                }
                else if (groupEnd - groupStart != 0)
                {
                    for (int j = groupStart; j <= groupEnd; j++)
                    {
                        c = anchors[j];
                        for (int k = groupStart; k <= groupEnd; k++)
                        {
                            if (c.removed) break;
                            d = anchors[k];
                            if (d.removed) continue;
                            CheckAnchorPairMerge(c, d);
                        }
                    }
                }

                //FootholdAnchor.MergeAnchors(ref anchors);
                /*if (mapBoard.BoardItems.FHAnchors.Count + anchors.Count > mapBoard.BoardItems.FHAnchors.Capacity)
                    mapBoard.BoardItems.FHAnchors.Capacity = mapBoard.BoardItems.FHAnchors.Count + anchors.Count;
                anchors.ForEach(X => if  mapBoard.BoardItems.FHAnchors.Add(X));
                anchors.Clear();*/
                mapBoard.BoardItems.FHAnchors.Capacity = mapBoard.BoardItems.FHAnchors.Count + anchors.Count * 2;
                foreach (FootholdAnchor anchor in anchors)
                    if (!anchor.removed)
                        mapBoard.BoardItems.FHAnchors.Add(anchor);
                anchors.Clear();
            }
        }

        private static void LoadPortals(WzImage mapImage, Board mapBoard)
        {
            WzSubProperty portalParent = (WzSubProperty)mapImage["portal"];
            foreach (WzSubProperty portal in portalParent.WzProperties)
            {
                int x = InfoTool.GetInt(portal["x"]);
                int y = InfoTool.GetInt(portal["y"]);
                PortalType pt = (PortalType)InfoTool.GetInt(portal["pt"]);
                int tm = InfoTool.GetInt(portal["tm"]);
                string tn = InfoTool.GetString(portal["tn"]);
                string pn = InfoTool.GetString(portal["pn"]);
                string image = InfoTool.GetOptionalString(portal["image"]);
                string script = InfoTool.GetOptionalString(portal["script"]);
                int? verticalImpact = InfoTool.GetOptionalInt(portal["verticalImpact"]);
                int? horizontalImpact = InfoTool.GetOptionalInt(portal["horizontalImpact"]);
                int? hRange = InfoTool.GetOptionalInt(portal["hRange"]);
                int? vRange = InfoTool.GetOptionalInt(portal["vRange"]);
                int? delay = InfoTool.GetOptionalInt(portal["delay"]);
                MapleBool hideTooltip = InfoTool.GetOptionalBool(portal["hideTooltip"]);
                MapleBool onlyOnce = InfoTool.GetOptionalBool(portal["onlyOnce"]);
                mapBoard.BoardItems.Portals.Add(PortalInfo.GetPortalInfoByType(pt).CreateInstance(mapBoard, x, y, false, pn, tn, tm, script, delay, hideTooltip, onlyOnce, horizontalImpact, verticalImpact, image, hRange, vRange));
            }
        }

        private static void LoadToolTips(WzImage mapImage, Board mapBoard)
        {
            WzSubProperty tooltipsParent = (WzSubProperty)mapImage["ToolTip"];
            WzImage tooltipsStringImage = (WzImage)Program.WzManager.String["ToolTipHelp.img"];
            if (!tooltipsStringImage.Parsed) tooltipsStringImage.ParseImage();
            WzSubProperty tooltipStrings = (WzSubProperty)tooltipsStringImage["Mapobject"][mapBoard.MapInfo.id.ToString()];
            if (tooltipStrings == null || tooltipsParent == null) return;
            //if (tooltipStrings == null ^ tooltipsParent == null) throw new Exception("at LoadToolTips, only one tooltip parent is null");
            for (int i = 0; true; i++)
            {
                string num = i.ToString();
                WzSubProperty tooltipString = (WzSubProperty)tooltipStrings[num];
                WzSubProperty tooltipProp = (WzSubProperty)tooltipsParent[num];
                WzSubProperty tooltipChar = (WzSubProperty)tooltipsParent[num + "char"];
                if (tooltipString == null && tooltipProp == null) break;
                if (tooltipString == null ^ tooltipProp == null) continue;
                string title = InfoTool.GetOptionalString(tooltipString["Title"]);
                string desc = InfoTool.GetOptionalString(tooltipString["Desc"]);
                int x1 = InfoTool.GetInt(tooltipProp["x1"]);
                int x2 = InfoTool.GetInt(tooltipProp["x2"]);
                int y1 = InfoTool.GetInt(tooltipProp["y1"]);
                int y2 = InfoTool.GetInt(tooltipProp["y2"]);
                Microsoft.Xna.Framework.Rectangle tooltipPos = new Microsoft.Xna.Framework.Rectangle(x1, y1, x2 - x1, y2 - y1);
                HaCreator.MapEditor.ToolTip tt = new HaCreator.MapEditor.ToolTip(mapBoard, tooltipPos, title, desc);
                mapBoard.BoardItems.ToolTips.Add(tt);
                if (tooltipChar != null)
                {
                    x1 = InfoTool.GetInt(tooltipChar["x1"]);
                    x2 = InfoTool.GetInt(tooltipChar["x2"]);
                    y1 = InfoTool.GetInt(tooltipChar["y1"]);
                    y2 = InfoTool.GetInt(tooltipChar["y2"]);
                    tooltipPos = new Microsoft.Xna.Framework.Rectangle(x1, y1, x2 - x1, y2 - y1);
                    ToolTipChar ttc = new ToolTipChar(mapBoard, tooltipPos, tt);
                    mapBoard.BoardItems.CharacterToolTips.Add(ttc);
                }
            }
        }

        private static void LoadBackgrounds(WzImage mapImage, Board mapBoard)
        {
            WzSubProperty bgParent = (WzSubProperty)mapImage["back"];
            WzSubProperty bgProp;
            int i = 0;
            while ((bgProp = (WzSubProperty)bgParent[(i++).ToString()]) != null)
            {
                int x = InfoTool.GetInt(bgProp["x"]);
                int y = InfoTool.GetInt(bgProp["y"]);
                int rx = InfoTool.GetInt(bgProp["rx"]);
                int ry = InfoTool.GetInt(bgProp["ry"]);
                int cx = InfoTool.GetInt(bgProp["cx"]);
                int cy = InfoTool.GetInt(bgProp["cy"]);
                int a = InfoTool.GetInt(bgProp["a"]);
                BackgroundType type = (BackgroundType)InfoTool.GetInt(bgProp["type"]);
                bool front = InfoTool.GetBool(bgProp["front"]);
                bool flip = InfoTool.GetBool(bgProp["f"]);
                string bS = InfoTool.GetString(bgProp["bS"]);
                bool ani = InfoTool.GetBool(bgProp["ani"]);
                string no = InfoTool.GetInt(bgProp["no"]).ToString();
                WzImage bsImg = Program.InfoManager.BackgroundSets[bS];
                if (bsImg == null) continue;
                IWzImageProperty bgInfoProp = bsImg[ani ? "ani" : "back"][no];
                if (bgInfoProp.HCTag == null)
                    bgInfoProp.HCTag = BackgroundInfo.Load(bgInfoProp, bS, ani, no);
                BackgroundInfo bgInfo = (BackgroundInfo)bgInfoProp.HCTag;
                IList list = front ? mapBoard.BoardItems.FrontBackgrounds : mapBoard.BoardItems.BackBackgrounds;
                list.Add((BackgroundInstance)bgInfo.CreateInstance(mapBoard, x, y, i, rx, ry, cx, cy, type, a, front, flip, false));
            }
        }

        public static ContextMenuStrip CreateStandardMapMenu(EventHandler rightClickHandler)
        {
            ContextMenuStrip result = new ContextMenuStrip();
            result.Items.Add(new ToolStripMenuItem("Edit map info...", Properties.Resources.mapEditMenu, rightClickHandler));
            return result;
        }

        public static void CreateMapFromImage(WzImage mapImage, string mapName, string streetName, PageCollection Tabs, MultiBoard multiBoard, EventHandler rightClickHandler)
        {
            if (!mapImage.Parsed) mapImage.ParseImage();
            VerifyMapPropsKnown(mapImage);
            MapInfo info = new MapInfo(mapImage, mapName, streetName);
            MapType type = GetMapType(mapImage);
            if (type == MapType.RegularMap)
                info.id = int.Parse(WzInfoTools.RemoveLeadingZeros(WzInfoTools.RemoveExtension(mapImage.Name)));
            info.mapType = type;
            Point center = new Point();
            Point size = new Point();
            if (mapImage["miniMap"] == null)
            {
                if (info.VR == null)
                {
                    if (!GetMapVR(mapImage, ref info.VR))
                    {
                        MessageBox.Show("Error - map does not contain size information and HaCreator was unable to generate it. An error has been logged.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ErrorLogger.Log(ErrorLevel.IncorrectStructure, "no size @map " + info.id.ToString());
                        return;
                    }
                }
                size = new Point(info.VR.Value.Width + 10, info.VR.Value.Height + 10); //leave 5 pixels on each side
                center = new Point(5 - info.VR.Value.Left, 5 - info.VR.Value.Top);
            }
            else
            {
                IWzImageProperty miniMap = mapImage["miniMap"];
                size = new Point(InfoTool.GetInt(miniMap["width"]), InfoTool.GetInt(miniMap["height"]));
                center = new Point(InfoTool.GetInt(miniMap["centerX"]), InfoTool.GetInt(miniMap["centerY"]));
            }
            CreateMap(mapName, WzInfoTools.RemoveLeadingZeros(WzInfoTools.RemoveExtension(mapImage.Name)), CreateStandardMapMenu(rightClickHandler), size, center, 8, Tabs, multiBoard);
            Board mapBoard = multiBoard.SelectedBoard;
            mapBoard.MapInfo = info;
            if (mapImage["miniMap"] != null)
                mapBoard.MiniMap = ((WzCanvasProperty)mapImage["miniMap"]["canvas"]).PngProperty.GetPNG(false);
            LoadLayers(mapImage, mapBoard);
            LoadLife(mapImage, mapBoard);
            LoadFootholds(mapImage, mapBoard);
            LoadRopes(mapImage, mapBoard);
            LoadChairs(mapImage, mapBoard);
            LoadPortals(mapImage, mapBoard);
            LoadReactors(mapImage, mapBoard);
            LoadToolTips(mapImage, mapBoard);
            LoadBackgrounds(mapImage, mapBoard);
            mapBoard.BoardItems.Sort();

            if (ErrorLogger.ErrorsPresent())
            {
                ErrorLogger.SaveToFile("errors.txt");
                if (UserSettings.ShowErrorsMessage)
                {
                    MessageBox.Show("Errors were encountered during the loading process. These errors were saved to \"errors.txt\". Please send this file to the author, either via mail (" + ApplicationSettings.AuthorEmail + ") or from the site you got this software from.\n\n(In the case that this program was not updated in so long that this message is now thrown on every map load, you may cancel this message from the settings)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                ErrorLogger.ClearErrors();
            }
        }

        public static void CreateMap(string text, string tooltip, ContextMenuStrip menu, Point size, Point center, int layers, HaCreator.ThirdParty.TabPages.PageCollection Tabs, MultiBoard multiBoard)
        {
            Board newBoard = multiBoard.CreateBoard(size, center, layers);
            HaCreator.ThirdParty.TabPages.TabPage page = new HaCreator.ThirdParty.TabPages.TabPage(text, multiBoard, tooltip, menu);
            page.Tag = newBoard;
            Tabs.Add(page);
            Tabs.CurrentPage = page;
            multiBoard.SelectedBoard = newBoard;
            menu.Tag = newBoard;
            foreach (ToolStripItem item in menu.Items)
                item.Tag = newBoard;
        }
    }
}
