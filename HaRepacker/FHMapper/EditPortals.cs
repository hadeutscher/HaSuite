/* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MapleLib.WzLib.WzProperties;

namespace Footholds
{
    public partial class EditPortals : Form
    {
        public List<Object> Settings;
        public Portals.Portal portal;
        public EditPortals()
        {
            InitializeComponent();
        }

        private void EditPortals_Load(object sender, EventArgs e)
        {
            TypeLbl.Text = ((WzCompressedIntProperty)portal.Data["pt"]).Value.ToString();
            DestLbl.Text = ((WzCompressedIntProperty)portal.Data["tm"]).Value.ToString();
            XPosLbl.Text = ((WzCompressedIntProperty)portal.Data["x"]).Value.ToString();
            YPosLbl.Text = ((WzCompressedIntProperty)portal.Data["y"]).Value.ToString();
            if (!(bool)Settings.ToArray()[11])
                TypeTBox.Text = ((WzCompressedIntProperty)portal.Data["pt"]).Value.ToString();
            else
                TypeTBox.Text = Settings.ToArray()[10].ToString();
            if (!(bool)Settings.ToArray()[7])
                XTBox.Text = ((WzCompressedIntProperty)portal.Data["x"]).Value.ToString();
            else
                XTBox.Text = Settings.ToArray()[6].ToString();
            if (!(bool)Settings.ToArray()[9])
                YTBox.Text = ((WzCompressedIntProperty)portal.Data["y"]).Value.ToString();
            else
                YTBox.Text = Settings.ToArray()[8].ToString();
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (TypeTBox.Text != "")
                {
                    ((WzCompressedIntProperty)portal.Data["pt"]).Value = int.Parse(TypeTBox.Text);
                    portal.Data["pt"].ParentImage.Changed = true;
                }
                if (XTBox.Text != "")
                {
                    ((WzCompressedIntProperty)portal.Data["x"]).Value = int.Parse(XTBox.Text);
                    portal.Data["x"].ParentImage.Changed = true;
                }
                if (YTBox.Text != "")
                {
                    ((WzCompressedIntProperty)portal.Data["y"]).Value = int.Parse(YTBox.Text);
                    portal.Data["y"].ParentImage.Changed = true;
                }
            }
            catch (FormatException) { MessageBox.Show("Input was invalid.\nPlease provide valid values before confirming.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            this.Close();

        }
    }
}