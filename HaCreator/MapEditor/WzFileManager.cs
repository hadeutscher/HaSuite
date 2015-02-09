/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using MapleLib.WzLib.WzStructure.Data;

namespace HaCreator.MapEditor
{
    public class WzFileManager
    {
        private string baseDir;
        public Hashtable wzFiles = new Hashtable();
        private WzMapleVersion version;

        public WzFileManager(string directory, WzMapleVersion version)
        {
            baseDir = directory;
            this.version = version;
        }

        public WzFileManager(string directory)
        {
            baseDir = directory;
            this.version = WzMapleVersion.GENERATE;
        }

        public bool LoadWzFile(string name)
        {
            try
            {
                wzFiles.Add(name.ToLower(), new WzFile(Path.Combine(baseDir, name + ".wz"), version));
                ((WzFile)wzFiles[name.ToLower()]).ParseWzFile();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error initializing " + name + ".wz (" + e.Message + ").\r\nCheck that the directory is valid and the file is not in use.");
                return false;
            }
        }

        public WzFile GetWzFileByName(string name)
        {
            return (WzFile)wzFiles[name.ToLower()];
        }

        public WzFile this[string name]
        {
            get { return (WzFile)wzFiles[name.ToLower()]; }
        }

        public WzFile String
        {
            get { return GetWzFileByName("string"); }
        }

        public void ExtractMobFile()
        {
            WzImage mobImage = (WzImage)String["Mob.img"];
            if (!mobImage.Parsed) mobImage.ParseImage();
            foreach (WzSubProperty mob in mobImage.WzProperties)
            {
                WzStringProperty nameProp = (WzStringProperty)mob["name"];
                string name = nameProp == null ? "" : nameProp.Value;
                Program.InfoManager.Mobs.Add(WzInfoTools.AddLeadingZeros(mob.Name, 7), name);
            }
        }

        public void ExtractNpcFile()
        {
            WzImage npcImage = (WzImage)String["Npc.img"];
            if (!npcImage.Parsed) npcImage.ParseImage();
            foreach (WzSubProperty npc in npcImage.WzProperties)
            {
                WzStringProperty nameProp = (WzStringProperty)npc["name"];
                string name = nameProp == null ? "" : nameProp.Value;
                Program.InfoManager.NPCs.Add(WzInfoTools.AddLeadingZeros(npc.Name, 7), name);
            }
        }

        public void ExtractReactorFile()
        {
            foreach (WzImage reactorImage in this["reactor"].WzDirectory.WzImages)
            {
                ReactorInfo reactor = ReactorInfo.Load(reactorImage);
                Program.InfoManager.Reactors[reactor.ID] = reactor;
            }
        }

        public void ExtractSoundFile()
        {
            foreach (WzImage soundImage in this["sound"].WzDirectory.WzImages)
            {
                if (!soundImage.Name.ToLower().Contains("bgm")) continue;
                if (!soundImage.Parsed) soundImage.ParseImage();
                foreach (WzSoundProperty bgm in soundImage.WzProperties)
                    Program.InfoManager.BGMs[WzInfoTools.RemoveExtension(soundImage.Name) + @"/" + bgm.Name] = bgm;
            }
        }

        public void ExtractMapMarks()
        {
            WzImage mapHelper = (WzImage)this["map"]["MapHelper.img"];
            foreach (WzCanvasProperty mark in mapHelper["mark"].WzProperties)
                Program.InfoManager.MapMarks[mark.Name] = mark.PngProperty.GetPNG(false);
        }

        public void ExtractTileSets()
        {
            WzDirectory tileParent = (WzDirectory)this["map"]["Tile"];
            foreach (WzImage tileset in tileParent.WzImages)
                Program.InfoManager.TileSets[WzInfoTools.RemoveExtension(tileset.Name)] = tileset;
        }

        public void ExtractObjSets()
        {
            WzDirectory objParent = (WzDirectory)this["map"]["Obj"];
            foreach (WzImage objset in objParent.WzImages)
                Program.InfoManager.ObjectSets[WzInfoTools.RemoveExtension(objset.Name)] = objset;
        }

        public void ExtractBackgroundSets()
        {
            WzDirectory bgParent = (WzDirectory)this["map"]["Back"];
            foreach (WzImage bgset in bgParent.WzImages)
                Program.InfoManager.BackgroundSets[WzInfoTools.RemoveExtension(bgset.Name)] = bgset;
        }

        public void ExtractMaps()
        {
            WzImage mapStringsParent = (WzImage)String["Map.img"];
            if (!mapStringsParent.Parsed) mapStringsParent.ParseImage();
            foreach (WzSubProperty mapCat in mapStringsParent.WzProperties)
                foreach (WzSubProperty map in mapCat.WzProperties)
                {
                    WzStringProperty mapName = (WzStringProperty)map["mapName"];
                    string id;
                    if (map.Name.Length == 9) id = map.Name;
                    else id = WzInfoTools.AddLeadingZeros(map.Name, 9);

                    if (mapName == null) Program.InfoManager.Maps[id] = "";
                    else Program.InfoManager.Maps[id] = mapName.Value;
                }
        }

        public void ExtractPortals()
        {
            WzSubProperty portalParent = (WzSubProperty)this["map"].GetObjectFromPath("Map.wz/MapHelper.img/portal");
            WzSubProperty editorParent = (WzSubProperty)portalParent["editor"];
            Program.InfoManager.Portals = new PortalInfo[editorParent.WzProperties.Count];
            foreach (WzCanvasProperty portal in editorParent.WzProperties)
                PortalInfo.Load(portal);
            WzSubProperty gameParent = (WzSubProperty)portalParent["game"];
            foreach (WzSubProperty portal in gameParent.WzProperties)
                if (portal.WzProperties[0] is WzSubProperty)
                {
                    MapleTable<Bitmap> images = new MapleTable<Bitmap>();
                    Bitmap defaultImage = null;
                    foreach (WzSubProperty image in portal.WzProperties)
                    {
                        WzSubProperty portalContinue = (WzSubProperty)image["portalContinue"];
                        if (portalContinue == null) continue;
                        Bitmap portalImage = (Bitmap)portalContinue["0"];
                        if (image.Name == "default")
                            defaultImage = portalImage;
                        else
                            images.Add(image.Name, portalImage);
                    }
                    Program.InfoManager.GamePortals.Add(PortalInfo.ptByShortName[portal.Name], new PortalGameImageInfo(defaultImage, images));
                }
        }

        public void ExtractItems()
        {
            WzImage consImage = (WzImage)String["Consume.img"];
            if (!consImage.Parsed) consImage.ParseImage();
            foreach (WzSubProperty item in consImage.WzProperties)
            {
                WzStringProperty nameProp = (WzStringProperty)item["name"];
                string name = nameProp == null ? "" : nameProp.Value;
                Program.InfoManager.Items.Add(WzInfoTools.AddLeadingZeros(item.Name, 7), name);
            }
        }
    }

    public class PortalGameImageInfo
    {
        private Bitmap defaultImage;

        private MapleTable<Bitmap> imageList;

        public PortalGameImageInfo(Bitmap defaultImage, MapleTable<Bitmap> imageList)
        {
            this.defaultImage = defaultImage;
            this.imageList = imageList;
        }

        public Bitmap DefaultImage
        {
            get { return defaultImage; }
        }

        public Bitmap this[string name]
        {
            get
            {
                return imageList[name];
            }
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return imageList.GetEnumerator();
        }
    }

    //read "MapleTable" out loud, lulz are guaranteed
    public class MapleTable<T> : Hashtable 
    {
        public T this[string id]
        {
            get
            {
                return (T)this[(object)id];
            }
            set
            {
                this[(object)id] = value;
            }
        }
    }

    public class MapleTable<T1, T2> : Hashtable
    {
        public T2 this[T1 id]
        {
            get
            {
                return (T2)this[(object)id];
            }
            set
            {
                this[(object)id] = value;
            }
        }
    }

    public class WzInformationManager
    {
        public MapleTable<string> NPCs = new MapleTable<string>();
        public MapleTable<string> Mobs = new MapleTable<string>();
        public MapleTable<string> Items = new MapleTable<string>();
        public MapleTable<ReactorInfo> Reactors = new MapleTable<ReactorInfo>();
        public MapleTable<WzImage> TileSets = new MapleTable<WzImage>();
        public MapleTable<WzImage> ObjectSets = new MapleTable<WzImage>();
        public MapleTable<WzImage> BackgroundSets = new MapleTable<WzImage>();
        public MapleTable<WzSoundProperty> BGMs = new MapleTable<WzSoundProperty>();
        public MapleTable<Bitmap> MapMarks = new MapleTable<Bitmap>();
        public MapleTable<string> Maps = new MapleTable<string>();
        public PortalInfo[] Portals;
        public MapleTable<PortalType, PortalGameImageInfo> GamePortals = new MapleTable<PortalType, PortalGameImageInfo>();
    }

    public static class WzInfoTools
    {
        public static System.Drawing.Point VectorToSystemPoint(WzVectorProperty source)
        {
            return new System.Drawing.Point(source.X.Value, source.Y.Value);
        }

        public static Microsoft.Xna.Framework.Point VectorToXNAPoint(WzVectorProperty source)
        {
            return new Microsoft.Xna.Framework.Point(source.X.Value, source.Y.Value);
        }

        public static WzVectorProperty PointToVector(string name, System.Drawing.Point source)
        {
            return new WzVectorProperty(name, new WzCompressedIntProperty("X", source.X), new WzCompressedIntProperty("Y", source.Y));
        }

        public static WzVectorProperty PointToVector(string name, Microsoft.Xna.Framework.Point source)
        {
            return new WzVectorProperty(name, new WzCompressedIntProperty("X", source.X), new WzCompressedIntProperty("Y", source.Y));
        }

        public static string AddLeadingZeros(string source, int maxLength)
        {
            while (source.Length < maxLength)
                source = "0" + source;
            return source;
        }

        public static string RemoveLeadingZeros(string source)
        {
            int firstNonZeroIndex = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] != (char)0x30) //char at index i is not 0
                {
                    firstNonZeroIndex = i;
                    break;
                }
                else if (i == source.Length - 1) //all chars are 0, return 0
                    return "0";
            }
            return source.Substring(firstNonZeroIndex);
        }

        public static string GetMobNameById(string id)
        {
            id = RemoveLeadingZeros(id);
            WzStringProperty mobName = (WzStringProperty)Program.WzManager.String.GetObjectFromPath(@"String.wz/Mob.img/" + id + @"/name");
            if (mobName != null) return mobName.Value;
            else return "";
        }

        public static string GetNpcNameById(string id)
        {
            id = RemoveLeadingZeros(id);
            WzStringProperty npcName = (WzStringProperty)Program.WzManager.String.GetObjectFromPath(@"String.wz/Npc.img/" + id + @"/name");
            if (npcName != null) return npcName.Value;
            else return "";
        }

        public static string GetMapNameById(string id)
        {
            id = RemoveLeadingZeros(id);
            WzImage mapNameParent = (WzImage)Program.WzManager.String.WzDirectory["Map.img"];
            foreach (WzSubProperty mapNameCategory in mapNameParent.WzProperties)
            {
                WzSubProperty mapNameDirectory = (WzSubProperty)mapNameCategory[id];
                if (mapNameDirectory != null)
                {
                    WzStringProperty mapName = (WzStringProperty)mapNameDirectory["mapName"];
                    if (mapName != null) return mapName.Value;
                }
            }
            return "";
        }

        public static string GetStreetNameById(string id)
        {
            id = RemoveLeadingZeros(id);
            WzImage mapNameParent = (WzImage)Program.WzManager.String.WzDirectory["Map.img"];
            foreach (WzSubProperty mapNameCategory in mapNameParent.WzProperties)
            {
                WzSubProperty mapNameDirectory = (WzSubProperty)mapNameCategory[id];
                if (mapNameDirectory != null)
                {
                    WzStringProperty streetName = (WzStringProperty)mapNameCategory["streetName"];
                    if (streetName != null) return streetName.Value;
                }
            }
            return "";
        }

        public static IWzObject GetObjectByRelativePath(IWzObject currentObject, string path)
        {
            foreach (string directive in path.Split("/".ToCharArray()))
            {
                if (directive == "..") currentObject = currentObject.Parent;
                else if (currentObject is IWzImageProperty)
                    currentObject = ((IWzImageProperty)currentObject)[directive];
                else if (currentObject is WzImage)
                    currentObject = ((WzImage)currentObject)[directive];
                else if (currentObject is WzDirectory)
                    currentObject = ((WzDirectory)currentObject)[directive];
                else throw new Exception("invalid type");
            }
            return currentObject;
        }

        public static IWzObject ResolveUOL(WzUOLProperty uol)
        {
            return (IWzObject)GetObjectByRelativePath(uol.Parent, uol.Value);
        }

        public static string RemoveExtension(string source)
        {
            if (source.Substring(source.Length - 4) == ".img")
                return source.Substring(0, source.Length - 4);
            return source;
        }

        public static IWzImageProperty GetRealProperty(IWzImageProperty prop)
        {
            if (prop is WzUOLProperty) return (IWzImageProperty)ResolveUOL((WzUOLProperty)prop);
            else return prop;
        }

        public static WzCanvasProperty GetMobImage(WzImage parentImage)
        {
            WzSubProperty standParent = (WzSubProperty)parentImage["stand"];
            if (standParent != null)
            {
                WzCanvasProperty frame1 = (WzCanvasProperty)GetRealProperty(standParent["0"]);
                if (frame1 != null) return frame1;
            }
            WzSubProperty flyParent = (WzSubProperty)parentImage["fly"];
            if (flyParent != null)
            {
                WzCanvasProperty frame1 = (WzCanvasProperty)GetRealProperty(flyParent["0"]);
                if (frame1 != null) return frame1;
            }
            return null;
        }

        public static WzCanvasProperty GetNpcImage(WzImage parentImage)
        {
            WzSubProperty standParent = (WzSubProperty)parentImage["stand"];
            if (standParent != null)
            {
                WzCanvasProperty frame1 = (WzCanvasProperty)GetRealProperty(standParent["0"]);
                if (frame1 != null) return frame1;
            }
            return null;
        }

        public static WzCanvasProperty GetReactorImage(WzImage parentImage)
        {
            WzSubProperty action0 = (WzSubProperty)parentImage["0"];
            if (action0 != null)
            {
                WzCanvasProperty frame1 = (WzCanvasProperty)GetRealProperty(action0["0"]);
                if (frame1 != null) return frame1;
            }
            return null;
        }

        public static MobInfo GetMobInfoById(string id)
        {
            id = AddLeadingZeros(id, 7);
            WzImage image = (WzImage)Program.WzManager["mob"][id + ".img"];
            if (image.HCTag == null) image.HCTag = MobInfo.Load(image);
            return (MobInfo)image.HCTag;
        }

        public static NpcInfo GetNpcInfoById(string id)
        {
            id = AddLeadingZeros(id, 7);
            WzImage image = (WzImage)Program.WzManager["npc"][id + ".img"];
            if (image.HCTag == null) image.HCTag = NpcInfo.Load(image);
            return (NpcInfo)image.HCTag;
        }

        public static System.Drawing.Color XNAToDrawingColor(Microsoft.Xna.Framework.Color c)
        {
            return Color.FromArgb(c.A, c.R, c.G, c.B);
        }
    }
}
