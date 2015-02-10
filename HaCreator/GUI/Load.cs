/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

//uncomment the line below to create a space-time tradeoff (saving RAM by wasting more CPU cycles)
#define SPACETIME

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Linq;
using System.IO;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using HaCreator.MapEditor;
using XNA = Microsoft.Xna.Framework;
using HaCreator.ThirdParty.TabPages;
using MapleLib.WzLib.WzStructure.Data;
using MapleLib.WzLib.WzStructure;
using MapleLib.Helpers;
using HaCreator.WzStructure;
using MapleLib.WzLib.Serialization;
using HaRepackerLib;

namespace HaCreator
{
    public partial class Load : System.Windows.Forms.Form
    {
        public bool usebasepng = false;
        public int bufferzone = 100;
        private MultiBoard multiBoard;
        private HaCreator.ThirdParty.TabPages.PageCollection Tabs;
        private EventHandler rightClickHandler;

        public Load(MultiBoard board, HaCreator.ThirdParty.TabPages.PageCollection Tabs, EventHandler rightClickHandler)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            this.multiBoard = board;
            this.Tabs = Tabs;
            this.rightClickHandler = rightClickHandler;
            this.searchBox.TextChanged += this.mapBrowser.searchBox_TextChanged;
            this.mapBrowser.InitializeMaps(true);
        }

        private void Load_Load(object sender, EventArgs e)
        {
            newWidth.Text = ApplicationSettings.LastMapSize.Width.ToString();
            newHeight.Text = ApplicationSettings.LastMapSize.Height.ToString();
            if (multiBoard.Boards.Count == 0)
            {
                newTab.Checked = true;
                newTab.Enabled = false;
            }
            else
            {
                newTab.Checked = ApplicationSettings.newTab;
            }
            switch (ApplicationSettings.lastRadioIndex)
            {
                case 0:
                    NewSelect.Checked = true;
                    break;
                case 1:
                    XMLSelect.Checked = true;
                    break;
                case 2:
                    WZSelect.Checked = true;
                    break;
            }
        }

        private void selectionChanged(object sender, EventArgs e)
        {
            if (NewSelect.Checked)
            {
                newWidth.Enabled = true;
                newHeight.Enabled = true;
                XMLBox.Enabled = false;
                searchBox.Enabled = false;
                mapBrowser.IsEnabled = false;
                loadButton.Enabled = true;
            }
            else if (XMLSelect.Checked)
            {
                newWidth.Enabled = false;
                newHeight.Enabled = false;
                XMLBox.Enabled = true;
                searchBox.Enabled = false;
                mapBrowser.IsEnabled = false;
                loadButton.Enabled = XMLBox.Text != "";
            }
            else if (WZSelect.Checked)
            {
                newWidth.Enabled = false;
                newHeight.Enabled = false;
                XMLBox.Enabled = false;
                searchBox.Enabled = true;
                mapBrowser.IsEnabled = true;
                loadButton.Enabled = mapBrowser.LoadAvailable;
            }
        }

        private void browseXML_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select XML to load...";
            dialog.Filter = "eXtensible Markup Language file (*.xml)|*.xml";
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            XMLBox.Text = dialog.FileName;
            loadButton.Enabled = true;
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            if (newTab.Checked && multiBoard.Boards.Count != 0)
                ApplicationSettings.newTab = true;
            else if (!newTab.Checked)
            {
                ApplicationSettings.newTab = false;
                Board toRemove = (Board)Tabs.CurrentPage.Tag;
                Tabs.Remove(Tabs.CurrentPage);
                toRemove.Dispose();
            }
            WzImage mapImage = null;
            string mapName = null, streetName = null;
            if (NewSelect.Checked)
            {
                ApplicationSettings.lastRadioIndex = 0;
                MapLoader.CreateMap("<Untitled>", "<Untitled>", MapLoader.CreateStandardMapMenu(rightClickHandler), new XNA.Point(int.Parse(newWidth.Text), int.Parse(newHeight.Text)), new XNA.Point(int.Parse(newWidth.Text) / 2, int.Parse(newHeight.Text) / 2), 8, Tabs, multiBoard);
                DialogResult = DialogResult.OK;
                Close();
                return;
            }
            else if (XMLSelect.Checked)
            {
                ApplicationSettings.lastRadioIndex = 1;
                try
                {
                    mapImage = (WzImage)new WzXmlDeserializer(false, null).ParseXML(XMLBox.Text)[0];
                }
                catch
                {
                    Warning.Error("Error while loading XML. Aborted.");
                    return;
                }
            }
            else if (WZSelect.Checked)
            {
                ApplicationSettings.lastRadioIndex = 2;
                if (mapBrowser.SelectedItem == "MapLogin")
                {
                    mapImage = (WzImage)Program.WzManager["ui"]["MapLogin.img"];
                    mapName = "MapLogin";
                    streetName = "";
                }
                else if (mapBrowser.SelectedItem == "MapLogin1")
                {
                    mapImage = (WzImage)Program.WzManager["ui"]["MapLogin1.img"];
                    mapName = "MapLogin1";
                    streetName = "";
                }
                else if (mapBrowser.SelectedItem == "CashShopPreview")
                {
                    mapImage = (WzImage)Program.WzManager["ui"]["CashShopPreview.img"];
                    mapName = "CashShopPreview";
                    streetName = "";
                }
                else
                {
                    string mapid = mapBrowser.SelectedItem.Substring(0, 9);
                    string mapcat = "Map" + mapid.Substring(0, 1);
                    mapImage = (WzImage)Program.WzManager["map"].GetObjectFromPath("Map.wz/Map/" + mapcat + "/" + mapid + ".img");
                    mapName = WzInfoTools.GetMapNameById(mapid);
                    streetName = WzInfoTools.GetStreetNameById(mapid);
                }
            }
            MapLoader.CreateMapFromImage(mapImage, mapName, streetName, Tabs, multiBoard, rightClickHandler);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void mapBrowser_SelectionChanged()
        {
            loadButton.Enabled = mapBrowser.LoadAvailable;
        }
    }
}
