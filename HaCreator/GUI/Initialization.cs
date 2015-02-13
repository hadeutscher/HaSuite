/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MapleLib.WzLib;
using HaCreator.MapEditor;
using MapleLib.WzLib.Util;
using HaCreator.WzStructure;
using MapleLib.WzLib.WzStructure;
using MapleLib.Helpers;

namespace HaCreator.GUI
{
    public partial class Initialization : System.Windows.Forms.Form
    {

        public Initialization()
        {
            InitializeComponent();
            if (UserSettings.enableDebug)
            {
                debugButton.Visible = true;
            }
        }

        public static bool XNASelfCheck(ref string exceptionResult)
        {
            try
            {
                Microsoft.Xna.Framework.Point foo = new Microsoft.Xna.Framework.Point();
                foo.X = 1337; //to shut VS up;
                return true;
            }
            catch (Exception e)
            {
                exceptionResult = e.Message;
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplicationSettings.MapleVersionIndex = versionBox.SelectedIndex;
            ApplicationSettings.MapleFolderIndex = pathBox.SelectedIndex;
            string wzPath = pathBox.Text;
            if (wzPath == "Select Maple Folder")
            {
                MessageBox.Show("Please select the maple folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ApplicationSettings.MapleFolder.Contains(wzPath))
                ApplicationSettings.MapleFolder = ApplicationSettings.MapleFolder + "," + wzPath;
            WzMapleVersion fileVersion;
            short version = -1;
            if (versionBox.SelectedIndex == 3)
                fileVersion = WzTool.DetectMapleVersion(Path.Combine(wzPath, "Item.wz"), out version);
            else
                fileVersion = (WzMapleVersion)versionBox.SelectedIndex;

            InitializeWzFiles(wzPath, fileVersion);

            Hide();
            Application.DoEvents();
            new HaEditor().ShowDialog();
            Application.Exit();
        }

        private void InitializeWzFiles(string wzPath, WzMapleVersion fileVersion)
        {
            Program.WzManager = new WzFileManager(wzPath, fileVersion);
            textBox2.Text = "Initializing String.wz...";
            Application.DoEvents();
            Program.WzManager.LoadWzFile("string");
            Program.WzManager.ExtractMaps();
            Program.WzManager.ExtractItems();
            textBox2.Text = "Initializing Mob.wz...";
            Application.DoEvents();
            Program.WzManager.LoadWzFile("mob");
            Program.WzManager.ExtractMobFile();
            textBox2.Text = "Initializing Npc.wz...";
            Application.DoEvents();
            Program.WzManager.LoadWzFile("npc");
            Program.WzManager.ExtractNpcFile();
            textBox2.Text = "Initializing Reactor.wz...";
            Application.DoEvents();
            Program.WzManager.LoadWzFile("reactor");
            Program.WzManager.ExtractReactorFile();
            textBox2.Text = "Initializing Sound.wz...";
            Application.DoEvents();
            Program.WzManager.LoadWzFile("sound");
            Program.WzManager.ExtractSoundFile();
            textBox2.Text = "Initializing Map.wz...";
            Application.DoEvents();
            Program.WzManager.LoadWzFile("map");
            Program.WzManager.ExtractMapMarks();
            Program.WzManager.ExtractPortals();
            Program.WzManager.ExtractTileSets();
            Program.WzManager.ExtractObjSets();
            Program.WzManager.ExtractBackgroundSets();
            textBox2.Text = "Initializing UI.wz...";
            Application.DoEvents();
            Program.WzManager.LoadWzFile("ui");
        }


        private static readonly string[] commonMaplePaths = new string[] { @"C:\Nexon\MapleStory", @"C:\Program Files\WIZET\MapleStory", @"C:\MapleStory" };

        private void Initialization_Load(object sender, EventArgs e)
        {
            versionBox.SelectedIndex = 0;
            try
            {
                string[] paths = ApplicationSettings.MapleFolder.Split(',');
                foreach (string x in paths)
                    if (x != string.Empty)
                        pathBox.Items.Add(x);
                if (paths.Length == 1) foreach (string path in commonMaplePaths) 
                    if (Directory.Exists(path)) pathBox.Items.Add(path);
                if (pathBox.Items.Count == 0)
                    pathBox.Items.Add("Select Maple Folder");
            }
            catch
            {
            }
            versionBox.SelectedIndex = ApplicationSettings.MapleVersionIndex;
            if (pathBox.Items.Count < ApplicationSettings.MapleFolderIndex + 1)
                pathBox.SelectedIndex = pathBox.SelectedIndex = pathBox.Items.Count - 1;
            else pathBox.SelectedIndex = ApplicationSettings.MapleFolderIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog mapleSelect = new FolderBrowserDialog();
            if (mapleSelect.ShowDialog() != DialogResult.OK)
                return;
            pathBox.Items.Add(mapleSelect.SelectedPath);
            pathBox.SelectedIndex = pathBox.Items.Count - 1;
        }

        private void debugButton_Click(object sender, EventArgs e)
        {
            // This function iterates over all maps in the game and verifies that we recognize all their props
            // It is meant to use by the developer(s) to speed up the process of adjusting this program for different MapleStory versions
            string wzPath = pathBox.Text;
            short version = -1;
            WzMapleVersion fileVersion = WzTool.DetectMapleVersion(Path.Combine(wzPath, "Item.wz"), out version);
            InitializeWzFiles(wzPath, fileVersion);

            MultiBoard mb = new MultiBoard();
            Board b = new Board(new Microsoft.Xna.Framework.Point(), new Microsoft.Xna.Framework.Point(), mb, MapleLib.WzLib.WzStructure.Data.ItemTypes.None, MapleLib.WzLib.WzStructure.Data.ItemTypes.None);

            foreach (string mapid in Program.InfoManager.Maps.Keys)
            {
                string mapcat = "Map" + mapid.Substring(0, 1);
                WzImage mapImage = (WzImage)Program.WzManager["map"].GetObjectFromPath("Map.wz/Map/" + mapcat + "/" + mapid + ".img");
                if (mapImage == null)
                {
                    continue;
                }
                mapImage.ParseImage();
                if (mapImage["info"]["link"] != null)
                {
                    mapImage.UnparseImage();
                    continue;
                }
                MapLoader.VerifyMapPropsKnown(mapImage);
                MapInfo info = new MapInfo(mapImage, null, null);
                MapLoader.LoadMisc(mapImage, b);
                if (ErrorLogger.ErrorsPresent())
                {
                    ErrorLogger.SaveToFile("debug_errors.txt");
                    ErrorLogger.ClearErrors();
                }
                mapImage.UnparseImage(); // To preserve memory, since this is a very memory intensive test
            }
            MessageBox.Show("Done");
        }
    }
}