/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using HaCreator.MapEditor;
using HaCreator.ThirdParty;
using MapleLib.WzLib.WzStructure.Data;
using System;
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
    public partial class CommonPanel : DockContent
    {
        HaCreatorStateManager hcsm;

        public CommonPanel(HaCreatorStateManager hcsm)
        {
            this.hcsm = hcsm;
            InitializeComponent();

            KoolkLVItem[] commonItems = new KoolkLVItem[] { 
                miscItemsContainer.createItem(CreateColoredBitmap(WzInfoTools.XNAToDrawingColor(UserSettings.FootholdColor)), "Foothold", true),
                miscItemsContainer.createItem(CreateColoredBitmap(WzInfoTools.XNAToDrawingColor(UserSettings.RopeColor)), "Rope", true),
                miscItemsContainer.createItem(CreateColoredBitmap(WzInfoTools.XNAToDrawingColor(UserSettings.ChairColor)), "Chair", true),
                miscItemsContainer.createItem(CreateColoredBitmap(WzInfoTools.XNAToDrawingColor(UserSettings.ToolTipColor)), "Tooltip", true)
            };
            foreach (KoolkLVItem item in commonItems)
            {
                item.MouseDown += new MouseEventHandler(commonItem_Click);
                item.MouseUp += new MouseEventHandler(KoolkLVItem.item_MouseUp);
            }
        }

        private Bitmap CreateColoredBitmap(Color color)
        {
            int containerSize = UserSettings.dotDescriptionBoxSize;
            int DotWidth = Math.Min(UserSettings.DotWidth, containerSize);
            Bitmap result = new Bitmap(containerSize, containerSize);
            using (Graphics g = Graphics.FromImage(result))
                g.FillRectangle(new SolidBrush(color), new Rectangle((containerSize / 2) - (DotWidth / 2), (containerSize / 2) - (DotWidth / 2), DotWidth, DotWidth));
            return result;
        }

        void commonItem_Click(object sender, MouseEventArgs e)
        {
            KoolkLVItem item = (KoolkLVItem)sender;
            switch (item.Name)
            {
                case "Foothold":
                    hcsm.EnterEditMode(ItemTypes.Footholds);
                    hcsm.MultiBoard.SelectedBoard.Mouse.SetFootholdMode();
                    hcsm.MultiBoard.Focus();
                    hcsm.MultiBoard.RenderFrame();
                    break;
                case "Rope":
                    hcsm.EnterEditMode(ItemTypes.Ropes);
                    hcsm.MultiBoard.SelectedBoard.Mouse.SetRopeMode();
                    hcsm.MultiBoard.Focus();
                    hcsm.MultiBoard.RenderFrame();
                    break;
                case "Chair":
                    hcsm.EnterEditMode(ItemTypes.Chairs);
                    hcsm.MultiBoard.SelectedBoard.Mouse.SetChairMode();
                    hcsm.MultiBoard.Focus();
                    hcsm.MultiBoard.RenderFrame();
                    break;
                case "Tooltip":
                    hcsm.EnterEditMode(ItemTypes.Footholds);
                    hcsm.MultiBoard.SelectedBoard.Mouse.SetTooltipMode();
                    hcsm.MultiBoard.Focus();
                    hcsm.MultiBoard.RenderFrame();
                    break;
            }
            item.Selected = true;
        }
    }
}
