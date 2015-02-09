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
    public partial class BackgroundPanel : DockContent
    {
        private HaCreatorStateManager hcsm;

        public BackgroundPanel(HaCreatorStateManager hcsm)
        {
            this.hcsm = hcsm;
            InitializeComponent();

            List<string> sortedBgSets = new List<string>();
            foreach (DictionaryEntry bS in Program.InfoManager.BackgroundSets)
                sortedBgSets.Add((string)bS.Key);
            sortedBgSets.Sort();
            foreach (string bS in sortedBgSets)
                bgSetListBox.Items.Add(bS);
        }

        private void bgSetListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bgSetListBox.SelectedItem == null) return;
            bgImageContainer.Controls.Clear();
            WzImage bgSetImage = Program.InfoManager.BackgroundSets[(string)bgSetListBox.SelectedItem];
            if (!bgSetImage.Parsed) bgSetImage.ParseImage();
            if (aniBg.Checked)
            {
                IWzImageProperty aniProp = bgSetImage["ani"];
                if (aniProp == null || aniProp.WzProperties == null) return;
                foreach (WzSubProperty aniBgProp in aniProp.WzProperties)
                {
                    if (aniBgProp.HCTag == null)
                        aniBgProp.HCTag = BackgroundInfo.Load(aniBgProp, (string)bgSetListBox.SelectedItem, true, aniBgProp.Name);
                    KoolkLVItem aniItem = bgImageContainer.createItem(((BackgroundInfo)aniBgProp.HCTag).Image, aniBgProp.Name, true);
                    aniItem.Tag = aniBgProp.HCTag;
                    aniItem.MouseDown += new MouseEventHandler(bgItem_Click);
                    aniItem.MouseUp += new MouseEventHandler(KoolkLVItem.item_MouseUp);
                }
            }
            else
            {
                IWzImageProperty backProp = bgSetImage["back"];
                foreach (WzCanvasProperty backBg in backProp.WzProperties)
                {
                    if (backBg.HCTag == null)
                        backBg.HCTag = BackgroundInfo.Load(backBg, (string)bgSetListBox.SelectedItem, false, backBg.Name);
                    KoolkLVItem aniItem = bgImageContainer.createItem(((BackgroundInfo)backBg.HCTag).Image, backBg.Name, true);
                    aniItem.Tag = backBg.HCTag;
                    aniItem.MouseDown += new MouseEventHandler(bgItem_Click);
                    aniItem.MouseUp += new MouseEventHandler(KoolkLVItem.item_MouseUp);
                }
            }
        }

        void bgItem_Click(object sender, MouseEventArgs e)
        {
            hcsm.EnterEditMode(ItemTypes.Backgrounds);
            hcsm.MultiBoard.SelectedBoard.Mouse.SetHeldInfo((BackgroundInfo)((KoolkLVItem)sender).Tag);
            hcsm.MultiBoard.Focus();
            hcsm.MultiBoard.RenderFrame();
            ((KoolkLVItem)sender).Selected = true;
        }
    }
}
