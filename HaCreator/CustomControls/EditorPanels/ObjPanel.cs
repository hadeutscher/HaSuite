/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using HaCreator.MapEditor;
using HaCreator.ThirdParty;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using MapleLib.WzLib.WzStructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HaCreator.CustomControls.EditorPanels
{
    public partial class ObjPanel : DockContent
    {
        private HaCreatorStateManager hcsm;

        public ObjPanel(HaCreatorStateManager hcsm)
        {
            this.hcsm = hcsm;
            InitializeComponent();

            List<string> sortedObjSets = new List<string>();
            foreach (DictionaryEntry oS in Program.InfoManager.ObjectSets)
                sortedObjSets.Add((string)oS.Key);
            sortedObjSets.Sort();
            foreach (string oS in sortedObjSets)
                objSetListBox.Items.Add(oS);
        }

        private void objSetListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objSetListBox.SelectedItem == null) return;
            objL0ListBox.Items.Clear();
            objL1ListBox.Items.Clear();
            objImagesContainer.Controls.Clear();
            WzImage oSImage = Program.InfoManager.ObjectSets[(string)objSetListBox.SelectedItem];
            if (!oSImage.Parsed) oSImage.ParseImage();
            foreach (IWzImageProperty l0Prop in oSImage.WzProperties)
                objL0ListBox.Items.Add(l0Prop.Name);
        }

        private void objL0ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objL0ListBox.SelectedItem == null) return;
            objL1ListBox.Items.Clear();
            objImagesContainer.Controls.Clear();
            IWzImageProperty l0Prop = Program.InfoManager.ObjectSets[(string)objSetListBox.SelectedItem][(string)objL0ListBox.SelectedItem];
            foreach (IWzImageProperty l1Prop in l0Prop.WzProperties)
                objL1ListBox.Items.Add(l1Prop.Name);
        }

        private void objL1ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objL1ListBox.SelectedItem == null) return;
            objImagesContainer.Controls.Clear();
            IWzImageProperty l1Prop = Program.InfoManager.ObjectSets[(string)objSetListBox.SelectedItem][(string)objL0ListBox.SelectedItem][(string)objL1ListBox.SelectedItem];
            foreach (WzSubProperty l2Prop in l1Prop.WzProperties)
            {
                if (l2Prop.HCTag == null)
                    l2Prop.HCTag = ObjectInfo.Load(l2Prop, (string)objSetListBox.SelectedItem, (string)objL0ListBox.SelectedItem, (string)objL1ListBox.SelectedItem, l2Prop.Name);
                ObjectInfo info = (ObjectInfo)l2Prop.HCTag;
                WzCanvasProperty objCanvas = (WzCanvasProperty)l2Prop["0"];
                KoolkLVItem item = objImagesContainer.createItem(objCanvas.PngProperty.GetPNG(false), l2Prop.Name, true);
                item.Tag = l2Prop.HCTag;
                item.MouseDown += new MouseEventHandler(objItem_Click);
                item.MouseUp += new MouseEventHandler(KoolkLVItem.item_MouseUp);
            }
        }

        private void objItem_Click(object sender, MouseEventArgs e)
        {
            hcsm.EnterEditMode(ItemTypes.Objects);
            hcsm.MultiBoard.SelectedBoard.Mouse.SetHeldInfo((ObjectInfo)((KoolkLVItem)sender).Tag);
            hcsm.MultiBoard.Focus();
            hcsm.MultiBoard.RenderFrame();
            ((KoolkLVItem)sender).Selected = true;
        }
    }
}
