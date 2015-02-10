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

namespace HaCreator.GUI
{
    public partial class Initialization : System.Windows.Forms.Form
    {

        public Initialization()
        {
            InitializeComponent();
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

            // Init WZ files
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

            Hide();
            Application.DoEvents();
            new HaEditor().ShowDialog();
            Application.Exit();
        }

        string[] commonMaplePaths = new string[] { @"C:\Nexon\MapleStory", @"C:\Program Files\WIZET\MapleStory", @"C:\MapleStory" };

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
    }
}