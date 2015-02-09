/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Windows.Forms;
using HaRepackerLib;
using MapleLib.WzLib;
using HaRepackerLib.Controls;

namespace HaRepacker.GUI
{
    public partial class NewForm : Form
    {
        private HaRepackerMainPanel panel;

        public NewForm(HaRepackerMainPanel panel)
        {
            this.panel = panel;
            InitializeComponent();
            encryptionBox.SelectedIndex = (int)ApplicationSettings.MapleVersion;
            versionBox.Value = 1;
        }

        private void regBox_CheckedChanged(object sender, EventArgs e)
        {
            copyrightBox.Enabled = regBox.Checked;
            versionBox.Enabled = regBox.Checked;
            nameBox.Enabled = regBox.Checked;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (regBox.Checked)
            {
                WzFile file = new WzFile((short)versionBox.Value, (WzMapleVersion)encryptionBox.SelectedIndex);
                file.Header.Copyright = copyrightBox.Text;
                file.Header.RecalculateFileStart();
                file.Name = nameBox.Text + ".wz";
                file.WzDirectory.Name = nameBox.Text + ".wz";
                panel.DataTree.Nodes.Add(new WzNode(file));
            }
            else
                new ListEditor(null, (WzMapleVersion)encryptionBox.SelectedIndex).Show();
            Close();
        }
    }
}
