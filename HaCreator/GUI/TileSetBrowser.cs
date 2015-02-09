/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using HaCreator.ThirdParty;

namespace HaCreator
{
    public partial class TileSetBrowser : Form
    {
        private ListBox targetListBox;
        public KoolkLVItem selectedItem = null;

        public TileSetBrowser(ListBox target)
        {
            InitializeComponent();
            targetListBox = target;
            List<string> sortedTileSets = new List<string>();
            foreach (DictionaryEntry tS in Program.InfoManager.TileSets)
                sortedTileSets.Add((string)tS.Key);
            sortedTileSets.Sort();
            foreach (string tS in sortedTileSets)
            {
                WzImage tSImage = Program.InfoManager.TileSets[tS];
                if (!tSImage.Parsed) tSImage.ParseImage();
                IWzImageProperty enh0 = tSImage["enH0"];
                if (enh0 == null) continue;
                WzCanvasProperty image = (WzCanvasProperty)enh0["0"];
                if (image == null) continue;
                //image.PngProperty.GetPNG(true);
                KoolkLVItem item = koolkLVContainer.createItem(image.PngProperty.GetPNG(true), tS, true);
                item.MouseDown += new MouseEventHandler(item_Click);
                item.MouseDoubleClick += new MouseEventHandler(item_DoubleClick);
            }
        }

        void item_DoubleClick(object sender, MouseEventArgs e)
        {
            if (selectedItem == null) return;
            targetListBox.SelectedItem = selectedItem.Name;
            Close();
        }

        void item_Click(object sender, MouseEventArgs e)
        {
            if (selectedItem != null)
                selectedItem.Selected = false;
            selectedItem = (KoolkLVItem)sender;
            selectedItem.Selected = true;
        }
    }
}
