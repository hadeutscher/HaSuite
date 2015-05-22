/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using HaCreator.Exceptions;
using HaCreator.MapEditor.Info;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.MapEditor
{
    public class UserObjectsManager
    {
        public const string oS = "haha01haha01";
        public const string l0 = "HaCreator";
        public const string l1 = "userObjs";

        private MultiBoard multiBoard;
        private WzImageProperty l1prop;
        private List<ObjectInfo> newObjects = new List<ObjectInfo>();

        public UserObjectsManager(MultiBoard multiBoard)
        {
            this.multiBoard = multiBoard;

            // Make sure that all our structures exist
            if (!Program.InfoManager.ObjectSets.ContainsKey(oS))
            {
                Program.InfoManager.ObjectSets[oS] = new WzImage(oS);
                Program.InfoManager.ObjectSets[oS].Changed = true;
            }
            WzImage osimg = Program.InfoManager.ObjectSets[oS];
            if (osimg[l0] == null)
            {
                osimg[l0] = new WzSubProperty();
            }
            WzImageProperty l0prop = osimg[l0];
            if (l0prop[l1] == null)
            {
                l0prop[l1] = new WzSubProperty();
            }
            l1prop = l0prop[l1];
        }

        public ObjectInfo Add(Bitmap bmp, string name)
        {
            if (!IsNameValid(name))
                throw new NameAlreadyUsedException();

            Point origin = new Point(bmp.Width / 2, bmp.Height / 2);

            WzSubProperty prop = new WzSubProperty();
            WzCanvasProperty canvasProp = new WzCanvasProperty();
            canvasProp.PngProperty = new WzPngProperty();
            canvasProp.PngProperty.SetPNG(bmp);
            canvasProp["origin"] = new WzVectorProperty("", new WzIntProperty("X", origin.X), new WzIntProperty("Y", origin.Y));
            canvasProp["z"] = new WzIntProperty("", 0);
            prop["0"] = canvasProp;

            ObjectInfo oi = new ObjectInfo(bmp, origin, oS, l0, l1, name, prop);
            newObjects.Add(oi);
            l1prop[name] = prop;

            return oi;
        }

        public void Remove(string l2)
        {
            foreach (ObjectInfo oi in newObjects)
            {
                if (oi.l2 == l2)
                {
                    newObjects.Remove(oi);
                    oi.ParentObject.Remove();
                    return;
                }
            }
            foreach (WzImageProperty prop in l1prop.WzProperties)
            {
                if (prop.Name == l2)
                {
                    prop.Remove();
                    // We removed a property that existed in the file already, so we must set it as updated
                    SetOsUpdated();
                }
            }
            throw new Exception("Could not find " + l2 + " in userObjs");
        }

        public void Flush()
        {
            if (newObjects.Count == 0)
                return;
            WzDirectory objsDir = (WzDirectory)Program.WzManager["map"]["Obj"];
            if (objsDir[oS + ".img"] == null)
                objsDir[oS + ".img"] = Program.InfoManager.ObjectSets[oS];
            SetOsUpdated();
            newObjects.Clear();
        }

        private void SetOsUpdated()
        {
            Program.WzManager.SetUpdated("map", Program.InfoManager.ObjectSets[oS]);
        }

        private bool IsNameValid(string name)
        {
            return l1prop[name] == null;
        }

        public WzImageProperty L1Property
        {
            get { return l1prop; }
        }

        public List<ObjectInfo> NewObjects
        {
            get { return newObjects; }
        }

        public MultiBoard MultiBoard
        {
            get { return multiBoard; }
        }
    }
}
