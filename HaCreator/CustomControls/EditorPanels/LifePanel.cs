/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using HaCreator.MapEditor;
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
    public partial class LifePanel : DockContent
    {
        List<string> reactors = new List<string>();
        List<string> npcs = new List<string>();
        List<string> mobs = new List<string>();

        private HaCreatorStateManager hcsm;

        public LifePanel(HaCreatorStateManager hcsm)
        {
            this.hcsm = hcsm;
            InitializeComponent();

            foreach (DictionaryEntry entry in Program.InfoManager.Reactors)
            {
                reactors.Add(((ReactorInfo)entry.Value).ID);
            }
            foreach (DictionaryEntry entry in Program.InfoManager.NPCs)
            {
                npcs.Add((string)entry.Key + " - " + (string)entry.Value);
            }
            foreach (DictionaryEntry entry in Program.InfoManager.Mobs)
            {
                mobs.Add((string)entry.Key + " - " + (string)entry.Value);
            }

            ReloadLifeList();
        }

        private void lifeModeChanged(object sender, EventArgs e)
        {
            ReloadLifeList();
        }

        private void ReloadLifeList()
        {
            string searchText = lifeSearchBox.Text.ToLower();
            bool getAll = searchText == "";
            lifeListBox.Items.Clear();
            List<string> items = new List<string>();
            if (reactorRButton.Checked)
            {
                items.AddRange(getAll ? reactors : reactors.Where(x => x.Contains(searchText)));
            }
            else if (npcRButton.Checked)
            {
                items.AddRange(getAll ? npcs : npcs.Where(x => x.Contains(searchText)));
            }
            else if (mobRButton.Checked)
            {
                items.AddRange(getAll ? mobs : mobs.Where(x => x.Contains(searchText)));
            }
            items.Sort();
            lifeListBox.Items.AddRange(items.Cast<object>().ToArray());
        }

        private void lifeListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            lifePictureBox.Image = new Bitmap(1, 1);
            if (lifeListBox.SelectedItem == null) return;
            if (reactorRButton.Checked)
            {
                ReactorInfo info = Program.InfoManager.Reactors[(string)lifeListBox.SelectedItem];
                lifePictureBox.Image = info.Image;
                hcsm.EnterEditMode(ItemTypes.Reactors);
                hcsm.MultiBoard.SelectedBoard.Mouse.SetHeldInfo(info);
                hcsm.MultiBoard.Focus();
                hcsm.MultiBoard.RenderFrame();
            }
            else if (npcRButton.Checked)
            {
                string id = ((string)lifeListBox.SelectedItem).Substring(0, ((string)lifeListBox.SelectedItem).IndexOf(" - "));
                NpcInfo info = WzInfoTools.GetNpcInfoById(id);
                lifePictureBox.Image = info.Image;
                hcsm.EnterEditMode(ItemTypes.NPCs);
                hcsm.MultiBoard.SelectedBoard.Mouse.SetHeldInfo(info);
                hcsm.MultiBoard.Focus();
                hcsm.MultiBoard.RenderFrame();
            }
            else if (mobRButton.Checked)
            {
                string id = ((string)lifeListBox.SelectedItem).Substring(0, ((string)lifeListBox.SelectedItem).IndexOf(" - "));
                MobInfo info = WzInfoTools.GetMobInfoById(id);
                lifePictureBox.Image = info.Image;
                hcsm.EnterEditMode(ItemTypes.Mobs);
                hcsm.MultiBoard.SelectedBoard.Mouse.SetHeldInfo(info);
                hcsm.MultiBoard.Focus();
                hcsm.MultiBoard.RenderFrame();
            }
        }
    }
}
