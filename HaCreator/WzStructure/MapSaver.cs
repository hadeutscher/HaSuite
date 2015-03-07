/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaCreator.MapEditor;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using MapleLib.WzLib.WzStructure.Data;
using MapleLib.WzLib.WzStructure;
using HaCreator.MapEditor.TilesDesign;

namespace HaCreator.WzStructure
{
    public class MapSaver
    {
        Board board;
        WzImage image;
        public MapSaver(Board board)
        {
            this.board = board;
        }

        private void CreateImage()
        {
            string name = "";
            switch (board.MapInfo.mapType)
            {
                case MapType.RegularMap:
                    name = WzInfoTools.AddLeadingZeros(board.MapInfo.id.ToString(), 9);
                    break;
                case MapType.MapLogin:
                case MapType.CashShopPreview:
                    name = board.MapInfo.strMapName;
                    break;
                default:
                    throw new Exception("Unknown map type");
            }
            this.image = new WzImage(name + ".img");
            this.image.Parsed = true;
        }

        private void InsertImage()
        {
            string cat = "Map" + image.Name.Substring(0, 1);
            WzDirectory mapDir = (WzDirectory)Program.WzManager["map"]["Map"];
            WzDirectory catDir = (WzDirectory)mapDir[cat];
            if (catDir == null)
            {
                catDir = new WzDirectory(cat);
                mapDir.AddDirectory(catDir);
            }
            WzImage mapImg = (WzImage)catDir[image.Name];
            if (mapImg != null)
            {
                mapImg.Remove();
            }
            catDir.AddImage(image);
            Program.WzManager.SetUpdated("map", image);
        }

        private void SaveMapInfo()
        {
            board.MapInfo.Save(image);
            if (board.MapInfo.mapType == MapType.RegularMap)
            {
                WzImage strMapImg = (WzImage)Program.WzManager.String["Map.img"];
                WzSubProperty strCatProp = (WzSubProperty)strMapImg[board.MapInfo.strCategoryName];
                if (strCatProp == null)
                {
                    strCatProp = new WzSubProperty();
                    strMapImg[board.MapInfo.strCategoryName] = strCatProp;
                    Program.WzManager.SetUpdated("string", strMapImg);
                }
                WzSubProperty strMapProp = (WzSubProperty)strCatProp[board.MapInfo.id.ToString()];
                if (strMapProp == null)
                {
                    strMapProp = new WzSubProperty();
                    strCatProp[board.MapInfo.id.ToString()] = strMapProp;
                    Program.WzManager.SetUpdated("string", strMapImg);
                }
                WzStringProperty strMapName = (WzStringProperty)strMapProp["mapName"];
                if (strMapName == null)
                {
                    strMapName = new WzStringProperty();
                    strMapProp["mapName"] = strMapName;
                    Program.WzManager.SetUpdated("string", strMapImg);
                }
                WzStringProperty strStreetName = (WzStringProperty)strMapProp["streetName"];
                if (strStreetName == null)
                {
                    strStreetName = new WzStringProperty();
                    strMapProp["streetName"] = strStreetName;
                    Program.WzManager.SetUpdated("string", strMapImg);
                }
                UpdateString(strMapName, board.MapInfo.strMapName, strMapImg);
                UpdateString(strStreetName, board.MapInfo.strStreetName, strMapImg);
            }
        }

        private void UpdateString(WzStringProperty strProp, string val, WzImage img)
        {
            if (strProp.Value != val)
            {
                strProp.Value = val;
                Program.WzManager.SetUpdated("string", img);
            }
        }

        private void SaveMiniMap()
        {
            if (board.MiniMap != null)
            {
                WzSubProperty miniMap = new WzSubProperty();
                WzCanvasProperty canvas = new WzCanvasProperty();
                canvas.PngProperty = new WzPngProperty();
                canvas.PngProperty.SetPNG(board.MiniMap);
                miniMap["canvas"] = canvas;
                miniMap["width"] = InfoTool.SetInt(board.MiniMapSize.X);
                miniMap["height"] = InfoTool.SetInt(board.MiniMapSize.Y);
                miniMap["centerX"] = InfoTool.SetInt(board.CenterPoint.X);
                miniMap["centerY"] = InfoTool.SetInt(board.CenterPoint.Y);
                miniMap["mag"] = InfoTool.SetInt(4);
                image["miniMap"] = miniMap;
            }
        }

        public void SaveLayers()
        {
            for (int layer = 0; layer <= 7; layer++)
            {
                WzSubProperty layerProp = new WzSubProperty();
                WzSubProperty infoProp = new WzSubProperty();
                
                // Info
                Layer l = board.Layers[layer];
                if (l.tS != null) 
                {
                    infoProp["tS"] = InfoTool.SetString(l.tS);
                }
                layerProp["info"] = infoProp;

                // Organize items and save objects
                List<TileInstance> tiles = new List<TileInstance>();
                WzSubProperty objParent = new WzSubProperty();
                int objIndex = 0;
                foreach (LayeredItem item in l.Items)
                {
                    if (item is ObjectInstance)
                    {
                        WzSubProperty obj = new WzSubProperty();
                        ObjectInstance objInst = (ObjectInstance)item;
                        ObjectInfo objInfo = (ObjectInfo)objInst.BaseInfo;

                        obj["x"] = InfoTool.SetInt(objInst.UnflippedX);
                        obj["y"] = InfoTool.SetInt(objInst.Y);
                        obj["z"] = InfoTool.SetInt(objInst.Z);
                        obj["zM"] = InfoTool.SetInt(objInst.zM);
                        obj["oS"] = InfoTool.SetString(objInfo.oS);
                        obj["l0"] = InfoTool.SetString(objInfo.l0);
                        obj["l1"] = InfoTool.SetString(objInfo.l1);
                        obj["l2"] = InfoTool.SetString(objInfo.l2);
                        obj["name"] = InfoTool.SetOptionalString(objInst.Name);
                        obj["r"] = InfoTool.SetOptionalBool(objInst.r);
                        obj["hide"] = InfoTool.SetOptionalBool(objInst.hide);
                        obj["reactor"] = InfoTool.SetOptionalBool(objInst.reactor);
                        obj["flow"] = InfoTool.SetOptionalBool(objInst.flow);
                        obj["rx"] = InfoTool.SetOptionalTranslatedInt(objInst.rx);
                        obj["ry"] = InfoTool.SetOptionalTranslatedInt(objInst.ry);
                        obj["cx"] = InfoTool.SetOptionalTranslatedInt(objInst.cx);
                        obj["cy"] = InfoTool.SetOptionalTranslatedInt(objInst.cy);
                        obj["tags"] = InfoTool.SetOptionalString(objInst.tags);
                        if (objInst.QuestInfo != null)
                        {
                            WzSubProperty questParent = new WzSubProperty();
                            foreach (ObjectInstanceQuest objQuest in objInst.QuestInfo)
                            {
                                questParent[objQuest.questId.ToString()] = InfoTool.SetInt((int)objQuest.state);
                            }
                            obj["quest"] = questParent;
                        }
                        obj["f"] = InfoTool.SetBool(objInst.Flip);

                        objParent[objIndex.ToString()] = obj;
                        objIndex++;
                    }
                    else if (item is TileInstance)
                    {
                        tiles.Add((TileInstance)item);
                    }
                    else
                    {
                        throw new Exception("Unkown type in layered lists");
                    }
                }
                layerProp["obj"] = objParent;

                // Save tiles
                tiles.Sort((a,b) => a.Z.CompareTo(b.Z));
                WzSubProperty tileParent = new WzSubProperty();
                for (int j = 0; j < tiles.Count; j++)
                {
                    TileInstance tileInst = tiles[j];
                    TileInfo tileInfo = (TileInfo)tileInst.BaseInfo;
                    WzSubProperty tile = new WzSubProperty();

                    tile["x"] = InfoTool.SetInt(tileInst.X);
                    tile["y"] = InfoTool.SetInt(tileInst.Y);
                    tile["zM"] = InfoTool.SetInt(tileInst.zM);
                    tile["u"] = InfoTool.SetString(tileInfo.u);
                    tile["no"] = InfoTool.SetInt(int.Parse(tileInfo.no));

                    tileParent[j.ToString()] = tile;
                }
                layerProp["tile"] = tileParent;

                image[layer.ToString()] = layerProp;
            }
        }

        public void SaveRopes()
        {
            WzSubProperty ropeParent = new WzSubProperty();
            for (int i = 0; i < board.BoardItems.Ropes.Count; i++)
            {
                Rope ropeInst = board.BoardItems.Ropes[i];
                WzSubProperty rope = new WzSubProperty();

                rope["x"] = InfoTool.SetInt(ropeInst.FirstAnchor.X);
                rope["y1"] = InfoTool.SetInt(ropeInst.FirstAnchor.Y);
                rope["y2"] = InfoTool.SetInt(ropeInst.SecondAnchor.Y);
                rope["uf"] = InfoTool.SetBool(ropeInst.uf);
                rope["page"] = InfoTool.SetInt(ropeInst.LayerNumber);
                rope["l"] = InfoTool.SetBool(ropeInst.ladder);

                ropeParent[i.ToString()] = rope;
            }
            image["ladderRope"] = ropeParent;
        }

        public void SaveChairs()
        {
            if (board.BoardItems.Chairs.Count == 0)
            {
                return;
            }
            WzSubProperty chairParent = new WzSubProperty();
            for (int i = 0; i < board.BoardItems.Chairs.Count; i++)
            {
                Chair chairInst = board.BoardItems.Chairs[i];
                WzVectorProperty chair = new WzVectorProperty();
                chair.X = new WzIntProperty("X", chairInst.X);
                chair.Y = new WzIntProperty("Y", chairInst.Y);
                chairParent[i.ToString()] = chair;
            }
            image["seat"] = chairParent;
        }

        public void SavePortals()
        {
            WzSubProperty portalParent = new WzSubProperty();
            for (int i = 0; i < board.BoardItems.Portals.Count; i++)
            {
                PortalInstance portalInst = board.BoardItems.Portals[i];
                WzSubProperty portal = new WzSubProperty();

                portal["x"] = InfoTool.SetInt(portalInst.X);
                portal["y"] = InfoTool.SetInt(portalInst.Y);
                portal["pt"] = InfoTool.SetInt(Program.InfoManager.PortalIdByType[portalInst.pt]);
                portal["tm"] = InfoTool.SetInt(portalInst.tm);
                portal["tn"] = InfoTool.SetString(portalInst.tn);
                portal["pn"] = InfoTool.SetString(portalInst.pn);
                portal["image"] = InfoTool.SetOptionalString(portalInst.image);
                portal["script"] = InfoTool.SetOptionalString(portalInst.script);
                portal["verticalImpact"] = InfoTool.SetOptionalInt(portalInst.verticalImpact);
                portal["horizontalImpact"] = InfoTool.SetOptionalInt(portalInst.horizontalImpact);
                portal["hRange"] = InfoTool.SetOptionalInt(portalInst.hRange);
                portal["vRange"] = InfoTool.SetOptionalInt(portalInst.vRange);
                portal["delay"] = InfoTool.SetOptionalInt(portalInst.delay);
                portal["hideTooltip"] = InfoTool.SetOptionalBool(portalInst.hideTooltip);
                portal["onlyOnce"] = InfoTool.SetOptionalBool(portalInst.onlyOnce);

                portalParent[i.ToString()] = portal;
            }
            image["portal"] = portalParent;
        }

        public void SaveReactors()
        {
            WzSubProperty reactorParent = new WzSubProperty();
            for (int i = 0; i < board.BoardItems.Reactors.Count; i++)
            {
                ReactorInstance reactorInst = board.BoardItems.Reactors[i];
                WzSubProperty reactor = new WzSubProperty();

                reactor["x"] = InfoTool.SetInt(reactorInst.UnflippedX);
                reactor["y"] = InfoTool.SetInt(reactorInst.Y);
                reactor["reactorTime"] = InfoTool.SetInt(reactorInst.ReactorTime);
                reactor["name"] = InfoTool.SetOptionalString(reactorInst.Name);
                reactor["id"] = InfoTool.SetString(((ReactorInfo)reactorInst.BaseInfo).ID);
                reactor["f"] = InfoTool.SetBool(reactorInst.Flip);

                reactorParent[i.ToString()] = reactor;
            }
            image["reactor"] = reactorParent;
        }

        public void SaveTooltips()
        {
            if (board.BoardItems.ToolTips.Count == 0)
            {
                return;
            }
            bool retainTooltipStrings = true;
            WzSubProperty tooltipParent = new WzSubProperty();
            WzImage strTooltipImg = (WzImage)Program.WzManager.String["ToolTipHelp.img"];
            WzSubProperty strTooltipCat = (WzSubProperty)strTooltipImg["Mapobject"];
            WzSubProperty strTooltipParent = (WzSubProperty)strTooltipCat[board.MapInfo.id.ToString()];
            if (strTooltipParent == null)
            {
                strTooltipParent = new WzSubProperty();
                strTooltipCat[board.MapInfo.id.ToString()] = strTooltipParent;
                Program.WzManager.SetUpdated("string", strTooltipImg);
                retainTooltipStrings = false;
            }

            HashSet<int> caughtNumbers = new HashSet<int>();

            // Check if the tooltips' original numbers can still be used
            if (retainTooltipStrings)
            {
                for (int i = 0; i < board.BoardItems.ToolTips.Count; i++)
                {
                    if (board.BoardItems.ToolTips[i].OriginalNumber == -1)
                    {
                        retainTooltipStrings = false;
                        break;
                    }
                }
            }

            // If they do not, we need to update string.wz and rebuild the string tooltip props
            if (!retainTooltipStrings)
            {
                Program.WzManager.SetUpdated("string", strTooltipImg);
                strTooltipParent.ClearProperties();
            }

            for (int i = 0; i < board.BoardItems.ToolTips.Count; i++)
            {
                ToolTip ttInst = board.BoardItems.ToolTips[i];
                string tooltipPropStr = retainTooltipStrings ? ttInst.OriginalNumber.ToString() : i.ToString();
                tooltipParent[tooltipPropStr] = PackRectangle(ttInst);
                if (ttInst.CharacterToolTip != null)
                {
                    tooltipParent[tooltipPropStr + "char"] = PackRectangle(ttInst.CharacterToolTip);
                }

                if (retainTooltipStrings)
                {
                    // This prop must exist if we are retaining, otherwise the map would not load
                    WzSubProperty strTooltipProp = (WzSubProperty)strTooltipParent[tooltipPropStr];

                    if (ttInst.Title != null)
                    {
                        WzStringProperty titleProp = (WzStringProperty)strTooltipProp["Title"];
                        if (titleProp == null)
                        {
                            titleProp = new WzStringProperty();
                            Program.WzManager.SetUpdated("string", strTooltipImg);
                        }
                        UpdateString(titleProp, ttInst.Title, strTooltipImg);
                    } 
                    if (ttInst.Desc != null)
                    {
                        WzStringProperty descProp = (WzStringProperty)strTooltipProp["Desc"];
                        if (descProp == null)
                        {
                            descProp = new WzStringProperty();
                            Program.WzManager.SetUpdated("string", strTooltipImg);
                        }
                        UpdateString(descProp, ttInst.Desc, strTooltipImg);
                    }
                }
                else
                {
                    WzSubProperty strTooltipProp = new WzSubProperty();
                    strTooltipProp["Title"] = InfoTool.SetOptionalString(ttInst.Title);
                    strTooltipProp["Desc"] = InfoTool.SetOptionalString(ttInst.Desc);
                    strTooltipParent[tooltipPropStr] = strTooltipProp;
                }
            }

            image["ToolTip"] = tooltipParent;
        }

        private static WzSubProperty PackRectangle(MapleRectangle rect)
        {
            WzSubProperty prop = new WzSubProperty();
            prop["x1"] = InfoTool.SetInt(rect.Left);
            prop["x2"] = InfoTool.SetInt(rect.Right);
            prop["y1"] = InfoTool.SetInt(rect.Top);
            prop["y2"] = InfoTool.SetInt(rect.Bottom);
            return prop;
        }

        public void SaveBackgrounds()
        {
            WzSubProperty bgParent = new WzSubProperty();
            int backCount = board.BoardItems.BackBackgrounds.Count;
            int frontCount = board.BoardItems.FrontBackgrounds.Count;
            for (int i = 0; i < backCount + frontCount; i++)
            {
                BackgroundInstance bgInst = i < backCount ? board.BoardItems.BackBackgrounds[i] : board.BoardItems.FrontBackgrounds[i - backCount];
                BackgroundInfo bgInfo = (BackgroundInfo)bgInst.BaseInfo;
                WzSubProperty bgProp = new WzSubProperty();
                bgProp["x"] = InfoTool.SetInt(bgInst.UnflippedX);
                bgProp["y"] = InfoTool.SetInt(bgInst.BaseY);
                bgProp["rx"] = InfoTool.SetInt(bgInst.rx);
                bgProp["ry"] = InfoTool.SetInt(bgInst.ry);
                bgProp["cx"] = InfoTool.SetInt(bgInst.cx);
                bgProp["cy"] = InfoTool.SetInt(bgInst.cy);
                bgProp["a"] = InfoTool.SetInt(bgInst.a);
                bgProp["type"] = InfoTool.SetInt((int)bgInst.type);
                bgProp["front"] = InfoTool.SetOptionalBool(bgInst.front);
                bgProp["f"] = InfoTool.SetOptionalBool(bgInst.Flip);
                bgProp["bS"] = InfoTool.SetString(bgInfo.bS);
                bgProp["ani"] = InfoTool.SetBool(bgInfo.ani);
                bgProp["no"] = InfoTool.SetInt(int.Parse(bgInfo.no));
                bgParent[i.ToString()] = bgProp;
            }
            image["back"] = bgParent;
        }

        private void SavePlatform(FootholdLine start, WzSubProperty prop)
        {
            // Edge foothold, map the platform in the correct direction
            if (start.FirstDot.connectedLines.Count == 1)
            {
                SavePlatformWithDirection((FootholdAnchor)start.FirstDot, start, prop);
            }
            else if (start.SecondDot.connectedLines.Count == 1)
            {
                SavePlatformWithDirection((FootholdAnchor)start.SecondDot, start, prop);
            }
            else
            {
                // Non-edge foothold, map the platform in both directions
                SavePlatformWithDirection((FootholdAnchor)start.FirstDot, start, prop);
                SavePlatformWithDirection((FootholdAnchor)start.SecondDot, start, prop);
            }
        }

        private void SavePlatformWithDirection(FootholdAnchor startAnchor, FootholdLine start, WzSubProperty prop)
        {
            foreach (FootholdLine line in new FootholdEnumerator(start, startAnchor))
            {
                FootholdOrientation orientation = GetFootholdOrientation(line);
                int prev = GetFootholdPrevNext(line, orientation, FootholdDirection.Prev);
                int next = GetFootholdPrevNext(line, orientation, FootholdDirection.Next);
                FootholdAnchor anchor1 = (FootholdAnchor)(orientation == FootholdOrientation.PrevFirstNextSecond ? line.FirstDot : line.SecondDot);
                FootholdAnchor anchor2 = (FootholdAnchor)(orientation == FootholdOrientation.PrevFirstNextSecond ? line.SecondDot : line.FirstDot);

                WzSubProperty fhProp = new WzSubProperty();
                fhProp["x1"] = InfoTool.SetInt(anchor1.X);
                fhProp["y1"] = InfoTool.SetInt(anchor1.Y);
                fhProp["x2"] = InfoTool.SetInt(anchor2.X);
                fhProp["y2"] = InfoTool.SetInt(anchor2.Y);
                fhProp["prev"] = InfoTool.SetInt(prev);
                fhProp["next"] = InfoTool.SetInt(next);
                fhProp["cantThrough"] = InfoTool.SetOptionalBool(line.CantThrough);
                fhProp["forbidFallDown"] = InfoTool.SetOptionalBool(line.ForbidFallDown);
                fhProp["piece"] = InfoTool.SetOptionalInt(line.Piece);
                fhProp["force"] = InfoTool.SetOptionalInt(line.Force);
                prop[line.num.ToString()] = fhProp;
                
                line.saved = true;
            }
        }

        private FootholdOrientation GetFootholdOrientation(FootholdLine line)
        {
            FootholdOrientation result;
            if (TryGetSimpleFootholdOrientation(line, out result))
            {
                return result;
            }
            else
            {
                // Vertical foothold, search for near nonvertical foothold as orientation reference
                
                // Obtain vertical orientation of the foothold
                FootholdAnchor top, bottom;
                if (line.FirstDot.Y < line.SecondDot.Y)
                {
                    top = (FootholdAnchor)line.FirstDot;
                    bottom = (FootholdAnchor)line.SecondDot;
                }
                else if (line.FirstDot.Y > line.SecondDot.Y)
                {
                    bottom = (FootholdAnchor)line.FirstDot;
                    top = (FootholdAnchor)line.SecondDot;
                }
                else
                {
                    throw new Exception("Zero length foothold in saving");
                }

                // For starters, we search the footholds linking downards from us.
                // This is because we are looking for the conventional foothold U scheme:
                //
                // |     |
                // |     |
                // |_ _ _|
                //
                // or the Z/S schemes:
                //_ _ _                _ _ _
                //     |              |
                //     |              |
                //     |_ _ _ or _ _ _|
                FootholdEnumerator referenceEnumerator = new FootholdEnumerator(line, top);
                foreach (FootholdLine reference in referenceEnumerator)
                {
                    if (!reference.IsWall)
                    {
                        // We found a suiting foothold reference, find what is our orientation
                        return GetVerticalFootholdOrientationByReference(line, top, reference, referenceEnumerator.CurrentAnchor);
                    }
                }

                // If downard-search failed, search upwards, to resolve the n scheme:
                //  _ _ _
                // |     |
                // |     |
                // |     |
                //
                // Note that the order of searches is important; we MUST search downwards before upwards, to resolve schemes such as the tower scheme:
                //
                // |           |
                // |           |
                // |_ _     _ _|
                // |           |
                // |           |
                // |_ _ _ _ _ _|
                //
                // If we searched upwards-first, the footholds between the two tower "floors" would be treated as n scheme footholds, while they should be U schemed.

                referenceEnumerator = new FootholdEnumerator(line, bottom);
                foreach (FootholdLine reference in referenceEnumerator)
                {
                    if (!reference.IsWall)
                    {
                        // We found a suiting foothold reference, find what is our orientation
                        return GetVerticalFootholdOrientationByReference(line, bottom, reference, referenceEnumerator.CurrentAnchor);
                    }
                }

                // If all else failed, we are dealing with a pure-wall foothold platform (i.e. a foothold graph consisting only of vertical footholds)
                // In this case, we arbitrarily select the Normal orientation, since there's no more actual logic we can perform to know what is the correct orientation.
                return FootholdOrientation.PrevFirstNextSecond;
            }
        }

        private FootholdOrientation GetNonverticalFootholdOrientation(FootholdLine line)
        {
            if (line.FirstDot.X < line.SecondDot.X)
            {
                // Normal foothold orientation
                return FootholdOrientation.PrevFirstNextSecond;
            }
            else // (line.FirstDot.X > line.SecondDot.X)
            {
                // Inverted foothold orientation
                return FootholdOrientation.NextFirstPrevSecond;
            }
        }

        private bool TryGetSimpleFootholdOrientation(FootholdLine line, out FootholdOrientation result)
        {
            if (line.prevOverride != null && line.FirstDot.connectedLines.Contains(line.prevOverride))
            {
                result = FootholdOrientation.PrevFirstNextSecond;
                return true;
            }
            else if (line.prevOverride != null && line.SecondDot.connectedLines.Contains(line.prevOverride))
            {
                result = FootholdOrientation.NextFirstPrevSecond;
                return true;
            }
            else if (line.nextOverride != null && line.FirstDot.connectedLines.Contains(line.nextOverride))
            {
                result = FootholdOrientation.NextFirstPrevSecond;
                return true;
            }
            else if (line.nextOverride != null && line.SecondDot.connectedLines.Contains(line.nextOverride))
            {
                result = FootholdOrientation.PrevFirstNextSecond;
                return true;
            }
            else if (!line.IsWall)
            {
                result = GetNonverticalFootholdOrientation(line);
                return true;
            }
            else
            {
                // Result doesn't really matter here since we're returning false
                result = FootholdOrientation.PrevFirstNextSecond;
                return false;
            }
        }

        private FootholdOrientation GetVerticalFootholdOrientationByReference(FootholdLine line, FootholdAnchor anchor, FootholdLine reference, FootholdAnchor referenceAnchor)
        {
            FootholdOrientation referenceOrientation = GetNonverticalFootholdOrientation(reference);
            bool leadingAnchorIsFirst = referenceAnchor == reference.FirstDot;
            bool firstIsPrev = referenceOrientation == FootholdOrientation.PrevFirstNextSecond;
            bool startAnchorIsFirst = anchor == line.FirstDot;

            // LAIF | FIP | RESULT (leadingAnchorIsPrev "LAIP")
            //  1   |  1  |   1
            //  1   |  0  |   0
            //  0   |  1  |   0
            //  0   |  0  |   1

            // LAIP | SAIF | RESULT (Orientation: 0 normal, 1 inverted)
            //  1   |  1   |   0
            //  1   |  0   |   1
            //  0   |  1   |   1
            //  0   |  0   |   0

            return !(leadingAnchorIsFirst ^ firstIsPrev) ^ startAnchorIsFirst ? FootholdOrientation.NextFirstPrevSecond : FootholdOrientation.PrevFirstNextSecond;
        }

        private int GetFootholdPrevNext(FootholdLine line, FootholdOrientation orientation, FootholdDirection dir)
        {
            FootholdLine overrideLine = dir == FootholdDirection.Prev ? line.prevOverride : line.nextOverride;
            if (overrideLine != null && (line.FirstDot.connectedLines.Contains(overrideLine) || line.SecondDot.connectedLines.Contains(overrideLine)))
            {
                return overrideLine.num;
            }
            else
            {
                FootholdAnchor anchor = (FootholdAnchor)((orientation == FootholdOrientation.PrevFirstNextSecond) ^ (dir == FootholdDirection.Next) ? line.FirstDot : line.SecondDot);
                if (anchor.connectedLines.Count < 2)
                {
                    return 0;
                }
                else
                {
                    return anchor.GetOtherLine(line).num;
                }
            }
        }

        public void SaveFootholds()
        {
            WzSubProperty fhParent = new WzSubProperty();
            board.BoardItems.FootholdLines.ForEach(x => x.saved = false);
            board.BoardItems.FootholdLines.Sort(FootholdLine.FHSorter);
            int fhIndex = 1;
            foreach (FootholdLine line in board.BoardItems.FootholdLines)
            {
                line.num = fhIndex++;
            }
            int platformIndex = 0;
            for (int layer = 0; layer <= 7; layer++)
            {
                WzSubProperty fhLayerProp = new WzSubProperty();
                foreach (FootholdLine fhInst in board.BoardItems.FootholdLines)
                {
                    // Search only footholds in our layer, that weren't already saved, and are "edge" footholds (we will take care of circles soon)
                    if (fhInst.LayerNumber != layer || fhInst.saved || (fhInst.FirstDot.connectedLines.Count > 1 && fhInst.SecondDot.connectedLines.Count > 1))
                    {
                        continue;
                    }
                    WzSubProperty fhPlatProp = new WzSubProperty();
                    SavePlatform(fhInst, fhPlatProp);
                    fhLayerProp[platformIndex++.ToString()] = fhPlatProp;
                }

                // Rerun the loop, this time processing all nonsaved footholds
                foreach (FootholdLine fhInst in board.BoardItems.FootholdLines)
                {
                    if (fhInst.LayerNumber != layer || fhInst.saved)
                    {
                        continue;
                    }
                    WzSubProperty fhPlatProp = new WzSubProperty();
                    SavePlatform(fhInst, fhPlatProp);
                    fhLayerProp[platformIndex++.ToString()] = fhPlatProp;
                }
                if (fhLayerProp.WzProperties.Count > 0)
                {
                    fhParent[layer.ToString()] = fhLayerProp;
                }
            }

            image["foothold"] = fhParent;
        }

        public void SaveLife()
        {
            WzSubProperty lifeParent = new WzSubProperty();
            int mobCount = board.BoardItems.Mobs.Count;
            int npcCount = board.BoardItems.NPCs.Count;
            for (int i = 0; i < mobCount + npcCount; i++)
            {
                bool mob = i < mobCount;
                LifeInstance lifeInst = mob ? (LifeInstance)board.BoardItems.Mobs[i] : (LifeInstance)board.BoardItems.NPCs[i - mobCount];
                WzSubProperty lifeProp = new WzSubProperty();
                
                lifeProp["id"] = InfoTool.SetString(mob ? ((MobInfo)lifeInst.BaseInfo).ID : ((NpcInfo)lifeInst.BaseInfo).ID);
                lifeProp["x"] = InfoTool.SetInt(lifeInst.UnflippedX);
                lifeProp["y"] = InfoTool.SetInt(lifeInst.Y - lifeInst.yShift);
                lifeProp["cy"] = InfoTool.SetInt(lifeInst.Y);
                lifeProp["mobTime"] = InfoTool.SetOptionalInt(lifeInst.MobTime);
                lifeProp["info"] = InfoTool.SetOptionalInt(lifeInst.Info);
                lifeProp["team"] = InfoTool.SetOptionalInt(lifeInst.Team);
                lifeProp["rx0"] = InfoTool.SetInt(lifeInst.X - lifeInst.rx0Shift);
                lifeProp["rx1"] = InfoTool.SetInt(lifeInst.X + lifeInst.rx1Shift);
                lifeProp["f"] = InfoTool.SetOptionalBool(lifeInst.Flip);
                lifeProp["hide"] = InfoTool.SetOptionalBool(lifeInst.Hide);
                lifeProp["type"] = InfoTool.SetString(mob ? "m" : "n");
                lifeProp["limitedname"] = InfoTool.SetOptionalString(lifeInst.LimitedName);
                lifeProp["fh"] = InfoTool.SetInt(GetFootholdBelow(lifeInst.X, lifeInst.Y));
                lifeParent[i.ToString()] = lifeProp;
            }
            image["life"] = lifeParent;
        }

        public void SaveMisc()
        {
            WzSubProperty areaParent = new WzSubProperty();
            WzSubProperty buffParent = new WzSubProperty();
            WzSubProperty swimParent = new WzSubProperty();
            foreach (BoardItem item in board.BoardItems.MiscItems)
            {
                if (item is Clock)
                {
                    Clock clock = (Clock)item;
                    WzSubProperty clockProp = new WzSubProperty();
                    clockProp["x"] = InfoTool.SetInt(item.Left);
                    clockProp["y"] = InfoTool.SetInt(item.Top);
                    clockProp["width"] = InfoTool.SetInt(item.Width);
                    clockProp["height"] = InfoTool.SetInt(item.Height);
                    image["clock"] = clockProp;
                }
                else if (item is ShipObject)
                {
                    ShipObject ship = (ShipObject)item;
                    ObjectInfo shipInfo = (ObjectInfo)ship.BaseInfo;
                    WzSubProperty shipProp = new WzSubProperty();
                    shipProp["shipObj"] = InfoTool.SetString("Map/Obj/" + shipInfo.oS + ".img/" + shipInfo.l0 + "/" + shipInfo.l1 + "/" + shipInfo.l2);
                    shipProp["x"] = InfoTool.SetInt(ship.UnflippedX);
                    shipProp["y"] = InfoTool.SetInt(ship.Y);
                    shipProp["z"] = InfoTool.SetOptionalInt(ship.zValue);
                    shipProp["x0"] = InfoTool.SetOptionalInt(ship.X0);
                    shipProp["tMove"] = InfoTool.SetInt(ship.TimeMove);
                    shipProp["shipKind"] = InfoTool.SetInt(ship.ShipKind);
                    shipProp["f"] = InfoTool.SetBool(ship.Flip);
                    image["shipObj"] = shipProp;
                }
                else if (item is Area)
                {
                    Area area = (Area)item;
                    areaParent[area.Identifier] = PackRectangle(area);
                }
                else if (item is Healer)
                {
                    Healer healer = (Healer)item;
                    ObjectInfo healerInfo = (ObjectInfo)healer.BaseInfo;
                    WzSubProperty healerProp = new WzSubProperty();
                    healerProp["healer"] = InfoTool.SetString("Map/Obj/" + healerInfo.oS + ".img/" + healerInfo.l0 + "/" + healerInfo.l1 + "/" + healerInfo.l2);
                    healerProp["x"] = InfoTool.SetInt(healer.X);
                    healerProp["yMin"] = InfoTool.SetInt(healer.yMin);
                    healerProp["yMax"] = InfoTool.SetInt(healer.yMax);
                    healerProp["healMin"] = InfoTool.SetInt(healer.healMin);
                    healerProp["healMax"] = InfoTool.SetInt(healer.healMax);
                    healerProp["fall"] = InfoTool.SetInt(healer.fall);
                    healerProp["rise"] = InfoTool.SetInt(healer.rise);
                    image["healer"] = healerProp;
                }
                else if (item is Pulley)
                {
                    Pulley pulley = (Pulley)item;
                    ObjectInfo pulleyInfo = (ObjectInfo)pulley.BaseInfo;
                    WzSubProperty pulleyProp = new WzSubProperty();
                    pulleyProp["pulley"] = InfoTool.SetString("Map/Obj/" + pulleyInfo.oS + ".img/" + pulleyInfo.l0 + "/" + pulleyInfo.l1 + "/" + pulleyInfo.l2);
                    pulleyProp["x"] = InfoTool.SetInt(pulley.X);
                    pulleyProp["y"] = InfoTool.SetInt(pulley.Y);
                    image["pulley"] = pulleyProp;
                }
                else if (item is BuffZone)
                {
                    BuffZone buff = (BuffZone)item;
                    WzSubProperty buffProp = PackRectangle(buff);
                    buffProp["ItemID"] = InfoTool.SetInt(buff.ItemID);
                    buffProp["Interval"] = InfoTool.SetInt(buff.Interval);
                    buffProp["Duration"] = InfoTool.SetInt(buff.Duration);
                    buffParent[buff.ZoneName] = buffProp;
                }
                else if (item is SwimArea)
                {
                    SwimArea swim = (SwimArea)item;
                    swimParent[swim.Identifier] = PackRectangle(swim);
                }
            }
            if (areaParent.WzProperties.Count > 0)
            {
                image["area"] = areaParent;
            }
            if (buffParent.WzProperties.Count > 0)
            {
                image["BuffZone"] = buffParent;
            }
            if (swimParent.WzProperties.Count > 0)
            {
                image["swimArea"] = swimParent;
            }
        }

        private void SaveAdditionals()
        {
            foreach (WzImageProperty prop in board.MapInfo.additionalNonInfoProps)
            {
                image.AddProperty(prop);
            }
        }

        public void SaveMapImage()
        {
            CreateImage();
            SaveMapInfo();
            SaveMiniMap();
            SaveLayers();
            SaveRopes();
            SaveChairs();
            SavePortals();
            SaveReactors();
            SaveTooltips();
            SaveBackgrounds();
            SaveFootholds();
            SaveLife();
            SaveMisc();
            SaveAdditionals();
            InsertImage();
        }

        public WzImage MapImage
        {
            get { return image; }
        }

        private int GetFootholdBelow(int x, int y)
        {
            int bestDistance = int.MaxValue;
            int bestFoothold = -1;
            foreach (FootholdLine fh in board.BoardItems.FootholdLines)
            {
                if (Math.Min(fh.FirstDot.X, fh.SecondDot.X) <= x && Math.Max(fh.FirstDot.X, fh.SecondDot.X) >= x)
                {
                    int fhY = fh.CalculateY(x);
                    if (fhY >= y && (fhY - y) < bestDistance)
                    {
                        bestDistance = fhY - y;
                        bestFoothold = fh.num;
                        if (bestDistance == 0)
                        {
                            // Not going to find anything better than 0
                            return bestFoothold;
                        }
                    }
                }
            }
            if (bestFoothold == -1)
            {
                // 0 stands in the game for flying or nonexistant foothold; I do not know what are the results of putting an NPC there,
                // however, if the user puts an NPC with no floor under it he should expect weird things to happen.
                return 0;
            }
            return bestFoothold;
        }

        public static void ConvertToMapleFootholds2(ref MapleList<FootholdLine> oldFootholds, ref MapleList<FootholdAnchor> oldAnchors)
        {
            //Part 1 - copying and filtering out unused anchors
            List<FootholdLine> footholds = new List<FootholdLine>(oldFootholds.Count);
            List<FootholdAnchor> anchors = new List<FootholdAnchor>(oldAnchors.Count);
            foreach (FootholdAnchor oldAnchor in oldAnchors)
            {
                if (oldAnchor.connectedLines.Count == 0) continue;
                //if (oldAnchor.IsMoveHandled) throw new Exception();
                FootholdAnchor anchor = new FootholdAnchor(oldAnchor.Board, oldAnchor.X, oldAnchor.Y, oldAnchor.LayerNumber, true);
                anchor.connectedLines = new List<MapleLine>(oldAnchor.connectedLines.Count);
                foreach (FootholdLine oldLine in oldAnchor.connectedLines)
                {
                    if (oldLine.cloneLine == null)
                    {
                        FootholdAnchor firstDot = null;
                        FootholdAnchor secondDot = null;
                        if (oldLine.FirstDot.X > oldLine.SecondDot.X)
                        {
                            firstDot = (FootholdAnchor)oldLine.SecondDot;
                            secondDot = (FootholdAnchor)oldLine.FirstDot;
                        }
                        else if (oldLine.FirstDot.X < oldLine.SecondDot.X)
                        {
                            firstDot = (FootholdAnchor)oldLine.FirstDot;
                            secondDot = (FootholdAnchor)oldLine.SecondDot;
                        }
                        else
                        {
                            if (oldLine.FirstDot.Y > oldLine.SecondDot.Y)
                            {
                                firstDot = (FootholdAnchor)oldLine.SecondDot;
                                secondDot = (FootholdAnchor)oldLine.FirstDot;
                            }
                            else
                            {
                                firstDot = (FootholdAnchor)oldLine.FirstDot;
                                secondDot = (FootholdAnchor)oldLine.SecondDot;

                            }
                        }
                        if (anchor.X == firstDot.X && anchor.Y == firstDot.Y) //we are firstdot
                        {
                            firstDot = anchor;
                            secondDot = null;
                        }
                        else //we are seconddot
                        {
                            firstDot = null;
                            secondDot = anchor;
                        }
                        oldLine.cloneLine = FootholdLine.CreateCustomFootholdLine(oldLine.Board, firstDot, secondDot);
                        footholds.Add(oldLine.cloneLine);
                    }
                    else
                    {
                        if (oldLine.cloneLine.FirstDot == null) oldLine.cloneLine.FirstDot = anchor;
                        else oldLine.cloneLine.SecondDot = anchor;
                    }
                    anchor.connectedLines.Add(oldLine.cloneLine);
                }
                anchors.Add(anchor);
            }
            foreach (FootholdLine fhLine in oldFootholds)
                fhLine.cloneLine = null;

            //Part 2 - combining anchors that are on the exact same position
            anchors.Sort(new Comparison<FootholdAnchor>(FootholdAnchor.FHAnchorSorter));
            int groupStart = 0; //inclusive
            int groupEnd = 0; //inclusive
            FootholdAnchor a;
            FootholdAnchor b;
            FootholdAnchor c;
            FootholdAnchor d;
            FootholdLine line;
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
                        if (c.connectedLines.Count == 1 && d.connectedLines.Count == 1)
                        {
                            d.removed = true;
                            line = (FootholdLine)d.connectedLines[0];
                            if (line.FirstDot == d)
                                line.FirstDot = c;
                            else
                                line.SecondDot = c;
                            c.connectedLines.Add(line);
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
                if (c.connectedLines.Count == 1 && d.connectedLines.Count == 1)
                {
                    d.removed = true;
                    line = (FootholdLine)d.connectedLines[0];
                    if (line.FirstDot == d)
                        line.FirstDot = c;
                    else
                        line.SecondDot = c;
                    c.connectedLines.Add(line);
                }
            }

            //Part 3 - connecting footholds to anchors they pass through, selectively 
            //this part is made purely to fix problems in the foothold structure caused
            //by the auto foothold connection feature for tiles
            for (int i = 0; i < footholds.Count; i++)
            {
                line = footholds[i];
                if (line.FirstDot.Y != line.SecondDot.Y) continue;
                foreach (FootholdAnchor anchor in anchors)
                {
                    if (anchor.X == line.FirstDot.Y && ((IContainsLayerInfo)line.FirstDot).LayerNumber == anchor.LayerNumber)
                    {
                        if (anchor.connectedLines.Count == 1)
                            line = (FootholdLine)anchor.connectedLines[0];
                        else
                            line = (FootholdLine)(anchor.connectedLines[0].FirstDot.Y == anchor.connectedLines[0].SecondDot.Y ? anchor.connectedLines[0] : anchor.connectedLines[1]);
                        if (line.FirstDot.Y == line.SecondDot.Y) //blueCave2 tiles, first anchor of sloped tiles
                        {
                            //footholds.RemoveAt(i);
                            line.remove = true;
                            footholds.Add(new FootholdLine(line.Board, line.FirstDot, anchor, true));
                            footholds.Add(new FootholdLine(line.Board, anchor, line.SecondDot, true));
                            //i--;
                            break;
                            //anchor.keyAnchor = line.FirstDot.Y == line.SecondDot.Y && IsValidKeyAnchor(anchor);
                        }
                        else //junction anchor for sloped tiles
                        {
                            bool direction = line.FirstDot == anchor; //true = anchor is the left dot, so we need to delete the line going to the right
                            //footholds.RemoveAt(i);
                            line.remove = true;
                            if (direction)
                                footholds.Add(new FootholdLine(line.Board, line.FirstDot, anchor, true));
                            else
                                footholds.Add(new FootholdLine(line.Board, anchor, line.SecondDot, true));
                            break;
                        }
                    }
                }
                /*                     if (((line.FirstDot.X == line.SecondDot.X) && (anchor.X == line.FirstDot.X && anchor.Y > line.FirstDot.Y && anchor.Y < line.SecondDot.Y)
                 || ((line.FirstDot.Y == line.SecondDot.Y) && (anchor.Y == line.FirstDot.Y && anchor.X > line.FirstDot.X && anchor.X < line.SecondDot.X))) 
                 && ((IContainsLayerInfo)line.FirstDot).LayerNumber == anchor.LayerNumber)*/
            }

            FootholdLine lineA;
            FootholdLine lineB;
            //Part 4 - removing duplicate footholds (caused by step 3, case 1)
            footholds.Sort(new Comparison<FootholdLine>(FHSorter));
            for (int i = 0; i < footholds.Count - 1; i++)
            {
                lineA = footholds[i];
                lineB = footholds[i + 1];
                if (lineA.FirstDot.X == lineB.FirstDot.X && lineA.FirstDot.Y == lineB.FirstDot.Y &&
                    lineA.SecondDot.X == lineB.SecondDot.X && lineA.SecondDot.Y == lineB.SecondDot.Y)
                {
                    //footholds.RemoveAt(i);
                    lineA.remove = true;
                    i--;
                }
            }

            //Part 5 - executing foothold changes and updating anchors
            foreach (FootholdAnchor anchor in anchors)
                for (int i = 0; i < anchor.connectedLines.Count; i++)
                    if (((FootholdLine)anchor.connectedLines[i]).remove)
                        anchor.connectedLines.RemoveAt(i);
            List<FootholdLine> newFootholds = new List<FootholdLine>(footholds.Count);
            foreach (FootholdLine fh in footholds)
                if (!fh.remove)
                    newFootholds.Add(fh);
            footholds = newFootholds;


            //Part 6 - dealing with 3 way (or more) foothold junctions
            /*foreach (FootholdAnchor anchor in anchors)
            {
                if (anchor.connectedLines.Count > 2)
                {
                    if (anchor.keyAnchor)
                    {
                        int leftLineIndex = -1;
                        int rightLineIndex = -1;
                        int slopedLineIndex = -1;
                        for(int i=0; i<anchor.connectedLines.Count; i++)
                        {
                            FootholdLine line = (FootholdLine)anchor.connectedLines[i];
                            if (line.FirstDot.Y == line.SecondDot.Y)
                            {
                                if (anchor == line.FirstDot) rightLineIndex = i;
                                else if (anchor == line.SecondDot) leftLineIndex = i;
                                else
                                {
                                    MessageBox.Show("Error at foothold parsing"); //TODO
                                }
                            }
                            else if (line.FirstDot.X != line.SecondDot.X) slopedLineIndex = i;
                        }
                        if (leftLineIndex == -1 || rightLineIndex == -1 || slopedLineIndex == -1)
                        {
                            MessageBox.Show("Error at foothold parsing"); //TODO
                        }
                        FootholdLine slopedLine = (FootholdLine)anchor.connectedLines[slopedLineIndex];
                        if (slopedLine.FirstDot.Y < slopedLine.SecondDot.Y)
                        { //eliminate left
                            EliminateFootholdTree(anchor, leftLineIndex, ref footholds);
                        }
                        else
                        {
                            //eliminate right
                            EliminateFootholdTree(anchor, rightLineIndex, ref footholds);
                        }
                    }
                    else
                    {
                        double lowestLength = double.MaxValue;
                        int shortestLineIndex = -1;
                        for (int i = 0; i < anchor.connectedLines.Count; i++)
                        {
                            double length = CalculateFootholdTreeLength(anchor, i);
                            if (length < lowestLength)
                            {
                                lowestLength = length;
                                shortestLineIndex = i;
                            }
                        }
                        EliminateFootholdTree(anchor, shortestLineIndex, ref footholds);
                    }
                }
            }

            //Part 7 - executing foothold changes and updating anchors (again)
            foreach (FootholdAnchor anchor in anchors)
                for (int i = 0; i < anchor.connectedLines.Count; i++)
                    if (((FootholdLine)anchor.connectedLines[i]).remove)
                        anchor.connectedLines.RemoveAt(i);*/

            oldAnchors.Clear();
            foreach (FootholdAnchor anchor in anchors) { /*anchor.keyAnchor = false; */oldAnchors.Add(anchor); }
            oldFootholds.Clear();
            foreach (FootholdLine fh in footholds) { /*anchor.keyAnchor = false; */oldFootholds.Add(fh); }
        }

        /*private static FootholdAnchor GetOtherAnchor(FootholdLine line, FootholdAnchor currAnchor)
        {
            return (FootholdAnchor)(line.FirstDot == currAnchor ? line.SecondDot : line.FirstDot);
        }*/

        /*private static double CalculateFootholdTreeLength(FootholdAnchor anchor, int lineIndex)
        {
            double length = 0;
            FootholdLine line = (FootholdLine)anchor.connectedLines[lineIndex];
            int dx = line.FirstDot.X - line.SecondDot.X;
            int dy = line.FirstDot.Y - line.SecondDot.Y;
            length += Math.Sqrt(dx * dx + dy * dy);
            FootholdAnchor otherAnchor = GetOtherAnchor(line, anchor);
            for (int i = 0; i < otherAnchor.connectedLines.Count; i++)
                if (otherAnchor.connectedLines[i] != line) 
                    length += CalculateFootholdTreeLength(anchor, i);
            return length;
        }*/

        private static bool FootholdTreeLengthExceeds(FootholdAnchor anchor, int lineIndex, int max) //max is inclusive
        {
            FootholdLine line = (FootholdLine)anchor.connectedLines[0];
            int length = 1;
            while (length <= max)
            {
                anchor = line.GetOtherAnchor(anchor);
                line = anchor.GetOtherLine(line);
                if (line == null) return false;
            }
            return true;
        }

        private static void EliminateFootholdTree(FootholdAnchor anchor, int lineIndex, ref List<FootholdLine> footholds)
        {
            FootholdLine line = (FootholdLine)anchor.connectedLines[lineIndex];
            footholds.Remove(line);
            line.remove = true;
            FootholdAnchor otherAnchor = line.GetOtherAnchor(anchor);
            for (int i = 0; i < otherAnchor.connectedLines.Count; i++)
                if (otherAnchor.connectedLines[i] != line) EliminateFootholdTree(otherAnchor, i, ref footholds);
        }

        /*private static bool IsValidKeyAnchor(FootholdAnchor anchor)
        {
            int horizontalLines = 0;
            int nonHorizontalLines = 0;
            foreach (FootholdLine line in anchor.connectedLines)
            {
                if (line.FirstDot.Y == line.SecondDot.Y) horizontalLines++;
                else if (line.FirstDot.X != line.SecondDot.X) nonHorizontalLines++;
            }
            return horizontalLines == 1 && nonHorizontalLines == 1;
        }*/

        private static int FHSorter(FootholdLine a, FootholdLine b)
        {
            if (a.FirstDot.X > b.FirstDot.X)
                return 1;
            else if (a.FirstDot.X < b.FirstDot.X)
                return -1;
            else
            {
                if (a.FirstDot.Y > b.FirstDot.Y)
                    return 1;
                else if (a.FirstDot.Y < b.FirstDot.Y)
                    return -1;
                else
                {
                    if (a.SecondDot.X > b.SecondDot.X)
                        return 1;
                    else if (a.SecondDot.X < b.SecondDot.X)
                        return -1;
                    else
                    {
                        if (a.SecondDot.Y > b.SecondDot.Y)
                            return 1;
                        else if (a.SecondDot.Y < b.SecondDot.Y)
                            return -1;
                        else
                            return 0;
                    }
                }
            }
        }

        private static int FHAnchorSorter(FootholdAnchor a, FootholdAnchor b)
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
        }

        private static bool FootholdExists(FootholdAnchor a, FootholdAnchor b)
        {
            foreach (FootholdLine line in a.connectedLines)
            {
                if ((line.FirstDot == a && line.SecondDot == b) || (line.SecondDot == a && line.FirstDot == b))
                {
                    return true;
                }
            }
            return false;
        }

        private bool SnappedTilesConnected(TileInstance a, TileInstance b)
        {
            foreach (FootholdAnchor anchor in a.BoundItems)
            {
                foreach (FootholdLine line in anchor.connectedLines)
                {
                    if ((line.FirstDot == anchor && b.BoundItems.Contains(line.SecondDot))
                     || (line.SecondDot == anchor && b.BoundItems.Contains(line.FirstDot)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private FootholdAnchor FindOptimalContinuationAnchor(int y, int x0, int x1, int layer)
        {
            FootholdAnchor result = null;
            int distance = int.MaxValue;
            foreach (FootholdAnchor anchor in board.BoardItems.FHAnchors)
            {
                // Find an anchor on the same layer, with 1 connected line, in the X range of our target line, whose line is not vertical
                if (anchor.LayerNumber != layer || anchor.connectedLines.Count != 1 || anchor.X < x0 || anchor.X > x1 || anchor.connectedLines[0].FirstDot.X == anchor.connectedLines[0].SecondDot.X)
                {
                    continue;
                }

                int d = Math.Abs(anchor.Y - y);
                if (d < distance)
                {
                    distance = d;
                    result = anchor;
                }

                if (distance == 0)
                {
                    // Not going to find anything better
                    return result;
                }
            }
            return distance < 100 ? result : null;
        }

        private static FootholdLine GetConnectingLine(FootholdAnchor a, FootholdAnchor b)
        {
            foreach (FootholdLine line in a.connectedLines)
            {
                if ((line.FirstDot == a && line.SecondDot == b) || (line.SecondDot == a && line.FirstDot == b))
                {
                    return line;
                }
            }
            return null;
        }

        /*private void SplitLine(FootholdLine line, FootholdAnchor anchor)
        {
            // Create first line
            if (!FootholdExists((FootholdAnchor)line.FirstDot, anchor))
            {
                board.BoardItems.FootholdLines.Add(new FootholdLine(board, line.FirstDot, anchor, line.ForbidFallDown, line.CantThrough, line.Piece, line.Force, true));
            }

            // Create second line
            if (!FootholdExists((FootholdAnchor)line.SecondDot, anchor))
            {
                board.BoardItems.FootholdLines.Add(new FootholdLine(board, line.SecondDot, anchor, line.ForbidFallDown, line.CantThrough, line.Piece, line.Force, true));
            }

            // Remove long line
            line.FirstDot.DisconnectLine(line);
            line.SecondDot.DisconnectLine(line);
            board.BoardItems.FootholdLines.RemoveAt(i);

        }*/

        public void ActualizeFootholds()
        {
            board.BoardItems.FHAnchors.Sort(new Comparison<FootholdAnchor>(FootholdAnchor.FHAnchorSorter));
            
            // Merge foothold anchors
            // This sorts out all foothold inconsistencies in all non-edU tiles
            for (int i = 0; i < board.BoardItems.FHAnchors.Count - 1; i++)
            {
                FootholdAnchor a = board.BoardItems.FHAnchors[i];
                FootholdAnchor b = board.BoardItems.FHAnchors[i + 1];
                if (a.X == b.X && a.Y == b.Y && a.LayerNumber == b.LayerNumber && (a.user || b.user))
                {
                    if (a.user != b.user)
                    {
                        a.user = false;
                    }
                    FootholdAnchor.MergeAnchors(a, b); // Transfer lines from b to a
                    b.RemoveItem(null); // Remove b
                    i--; // Fix index after we removed b
                }
            }

            // Organize edU tiles
            foreach (LayeredItem li in board.BoardItems.TileObjs)
            {
                if (!(li is TileInstance))
                {
                    continue;
                }
                TileInstance tileInst = (TileInstance)li;
                TileInfo tileInfo = (TileInfo)li.BaseInfo;
                // Ensure that the tile is an edU, that it was created by the user in this session, and that it doesnt have some messed up foothold structure we can't deal with
                if (tileInfo.u == "edU" && tileInst.BoundItemsList.Count >= 4)
                {
                    int nitems = tileInst.BoundItemsList.Count;
                    if (tileInst.BoundItemsList[0].Y != tileInst.BoundItemsList[nitems - 1].Y ||
                        tileInst.BoundItemsList[0].X != tileInst.BoundItemsList[1].X ||
                        tileInst.BoundItemsList[nitems - 1].X != tileInst.BoundItemsList[nitems - 2].X)
                    {
                        continue;
                    }

                    // Only work with snapped edU's
                    if (tileInst.FindSnappableTiles(0, x => ((TileInfo)x.BaseInfo).u == "enH0" || ((TileInfo)x.BaseInfo).u == "slLU" || ((TileInfo)x.BaseInfo).u == "slRU").Count == 0)
                    {
                        continue;
                    }

                    /*FootholdLine surfaceLine = GetConnectingLine((FootholdAnchor)tileInst.BoundItemsList[1], (FootholdAnchor)tileInst.BoundItemsList[2]);
                    if (surfaceLine == null)
                    {
                        continue;
                    }*/

                    FootholdAnchor contAnchor = FindOptimalContinuationAnchor((tileInst.BoundItemsList[1].Y + tileInst.BoundItemsList[nitems - 2].Y) / 2,
                        tileInst.BoundItemsList[1].X, tileInst.BoundItemsList[nitems - 2].X, tileInst.LayerNumber);
                    if (contAnchor == null)
                    {
                        continue;
                    }

                    // The anchor is guaranteed to have exactly 1 line
                    FootholdLine anchorLine = (FootholdLine)contAnchor.connectedLines[0];
                    // The line is guaranteed to be non-vertical
                    Direction direction = anchorLine.GetOtherAnchor(contAnchor).X > contAnchor.X ? Direction.Right : Direction.Left;
                    FootholdAnchor remainingAnchor = null;
                    int remainingIndex = -1;

                    // Remove the rightmost/leftmost footholds
                    for (int i = direction == Direction.Right ? 0 : (nitems - 1);
                        direction == Direction.Right ? i < nitems : i > 0;
                        i += direction == Direction.Right ? 1 : -1)
                    {
                        FootholdAnchor anchor = (FootholdAnchor)tileInst.BoundItemsList[i];
                        if (direction == Direction.Right ? anchor.X >= contAnchor.X : anchor.X <= contAnchor.X)
                        {
                            break;
                        }
                        remainingIndex = i;
                    }
                    if (remainingIndex == -1)
                    {
                        continue;
                    }
                    remainingAnchor = (FootholdAnchor)tileInst.BoundItemsList[remainingIndex];
                    int deleteStart = direction == Direction.Right ? (remainingIndex + 1) : 0;
                    int deleteEnd = direction == Direction.Right ? nitems : remainingIndex;

                    for (int i = deleteStart; i < deleteEnd; i++)
                    {
                        ((FootholdAnchor)tileInst.BoundItemsList[deleteStart]).RemoveItem(null);
                    }

                    board.BoardItems.FootholdLines.Add(new FootholdLine(board, remainingAnchor, contAnchor, anchorLine.ForbidFallDown, anchorLine.CantThrough, anchorLine.Piece, anchorLine.Force, true));
                }
            }

            // Remove all Tile-FH bindings since they have no meaning now
            foreach (LayeredItem li in board.BoardItems.TileObjs)
            {
                if (!(li is TileInstance))
                {
                    continue;
                }
                TileInstance tileInst = (TileInstance)li;

                while (tileInst.BoundItemsList.Count > 0)
                {
                    tileInst.ReleaseItem(tileInst.BoundItemsList[0]);
                }
            }

            board.UndoRedoMan.UndoList.Clear();
            board.UndoRedoMan.RedoList.Clear();

            // Break foothold lines
            /*for (int i = 0; i < board.BoardItems.FootholdLines.Count; i++)
            {
                FootholdLine line = board.BoardItems.FootholdLines[i];
                if (line.FirstDot.X == line.SecondDot.X || line.FirstDot.Y == line.SecondDot.Y)
                {
                    foreach (FootholdAnchor anchor in board.BoardItems.FHAnchors)
                    {
                        if ((anchor.X == line.FirstDot.X && anchor.X == line.SecondDot.X && Math.Min(line.FirstDot.Y, line.SecondDot.Y) < anchor.Y && Math.Max(line.FirstDot.Y, line.SecondDot.Y) > anchor.Y && anchor.AllConnectedLinesVertical())
                         || (anchor.Y == line.FirstDot.Y && anchor.Y == line.SecondDot.Y && Math.Min(line.FirstDot.X, line.SecondDot.X) < anchor.X && Math.Max(line.FirstDot.X, line.SecondDot.X) > anchor.X && anchor.AllConnectedLinesHorizontal()))
                        {
                            // Create first line
                            if (!FootholdExists((FootholdAnchor)line.FirstDot, anchor)) 
                            {
                                board.BoardItems.FootholdLines.Add(new FootholdLine(board, line.FirstDot, anchor, line.ForbidFallDown, line.CantThrough, line.Piece, line.Force, true));
                            }

                            // Create second line
                            if (!FootholdExists((FootholdAnchor)line.SecondDot, anchor))
                            {
                                board.BoardItems.FootholdLines.Add(new FootholdLine(board, line.SecondDot, anchor, line.ForbidFallDown, line.CantThrough, line.Piece, line.Force, true));
                            }

                            // Remove long line
                            line.FirstDot.DisconnectLine(line);
                            line.SecondDot.DisconnectLine(line);
                            board.BoardItems.FootholdLines.RemoveAt(i);
                            i--; // To conserve the current loop position
                        }
                    }
                }
            }

            // Special tile snapping cases
            MapleTable<LayeredItem, bool> leftSnapping = new MapleTable<LayeredItem,bool>();
            MapleTable<LayeredItem, bool> rightSnapping = new MapleTable<LayeredItem,bool>();
            foreach (LayeredItem li in board.BoardItems.TileObjs) {
                if (!(li is TileInstance))
                    continue;
                TileInstance tileInst = (TileInstance)li;
                TileInfo tileInfo = (TileInfo)li.BaseInfo;
                if (tileInst.BoundItems.Count > 0) // This if statement ensures in one check: 1.that the tile is foothold-containing type and 2.that it was created by the user in this session
                {
                    Tuple<TileInstance, TileInstance> sideSnaps = tileInst.FindExactSideSnaps();
                    TileInstance prev = sideSnaps.Item1;
                    TileInstance next = sideSnaps.Item2;
                }
            }*/
        }

        public void ChangeMapID(int newId)
        {
            int oldId = board.MapInfo.id;
            if (oldId == newId)
            {
                return;
            }
            board.MapInfo.id = newId;
            foreach (PortalInstance portalInst in board.BoardItems.Portals)
            {
                if (portalInst.tm == oldId)
                {
                    portalInst.tm = newId;
                }
            }
        }
    }

    enum Direction
    {
        Left,
        Right
    }

    enum Dots
    {
        FirstDot,
        SecondDot
    }

    enum FootholdDirection
    {
        Prev,
        Next
    }

    enum FootholdOrientation
    {
        PrevFirstNextSecond = 0, // "Normal"
        NextFirstPrevSecond = 1 // "Inverted"
    }
}
