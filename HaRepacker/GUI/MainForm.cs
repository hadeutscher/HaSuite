/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Footholds;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using HaRepackerLib;
using MapleLib.WzLib.Serialization;
using System.Threading;
using HaRepacker.GUI.Interaction;
using MapleLib.WzLib.Util;
using System.Runtime.InteropServices;
using MapleLib.WzLib.WzStructure;
using System.Net;
using System.Text;
using System.Diagnostics;
using HaRepackerLib.Controls;
using System.IO.Pipes;

namespace HaRepacker.GUI
{
    public partial class MainForm : Form
    {
        FHMapper.FHMapper mapper;

        public MainForm(string wzToLoad, bool usingPipes, bool firstrun)
        {
            InitializeComponent();
            /*if (firstrun)
                new AboutForm().ShowDialog();*/
#if DEBUG
            debugToolStripMenuItem.Visible = true;
#endif
            WindowState = ApplicationSettings.Maximized ? FormWindowState.Maximized : FormWindowState.Normal;
            Size = ApplicationSettings.WindowSize;

            if (usingPipes)
            {
                try
                {
                    Program.pipe = new NamedPipeServerStream(Program.pipeName, PipeDirection.In);
                    Program.pipeThread = new Thread(new ThreadStart(PipeServer));
                    Program.pipeThread.IsBackground = true;
                    Program.pipeThread.Start();
                }
                catch (IOException)
                {
                    if (wzToLoad != null)
                    {
                        try
                        {
                            NamedPipeClientStream clientPipe = new NamedPipeClientStream(".", Program.pipeName, PipeDirection.Out);
                            clientPipe.Connect(0);
                            StreamWriter sw = new StreamWriter(clientPipe);
                            sw.WriteLine(wzToLoad);
                            clientPipe.WaitForPipeDrain();
                            sw.Close();
                            Environment.Exit(0);
                        }
                        catch (TimeoutException)
                        {
                        }
                    }
                }
            }
            if (wzToLoad != null && File.Exists(wzToLoad))
            {
                short version;
                WzMapleVersion encVersion = WzTool.DetectMapleVersion(wzToLoad, out version);
                encryptionBox.SelectedIndex = (int)encVersion;
                LoadWzFileThreadSafe(wzToLoad, MainPanel, false);
            }

            mapper = new FHMapper.FHMapper(MainPanel);
        }

        public void Interop_AddLoadedWzFileToManager(WzFile f)
        {
            Program.WzMan.InsertWzFileUnsafe(f, MainPanel);
        }

        private delegate void LoadWzFileDelegate(string path, HaRepackerMainPanel panel, bool detectMapleVersion);
        private void LoadWzFileCallback(string path, HaRepackerMainPanel panel, bool detectMapleVersion)
        {
            try
            {
                if (detectMapleVersion)
                    Program.WzMan.LoadWzFile(path, panel);
                else
                    Program.WzMan.LoadWzFile(path, (WzMapleVersion)encryptionBox.SelectedIndex, MainPanel);
            }
            catch
            {
                Warning.Error("Could not open WZ file \"" + path + "\"");
            }
        }

        private void LoadWzFileThreadSafe(string path, HaRepackerMainPanel panel, bool detectMapleVersion)
        {
            if (panel.InvokeRequired)
                panel.Invoke(new LoadWzFileDelegate(LoadWzFileCallback), path, panel, detectMapleVersion);
            else
                LoadWzFileCallback(path, panel, detectMapleVersion);
        }

        private delegate void SetWindowStateDelegate(FormWindowState state);
        private void SetWindowStateCallback(FormWindowState state)
        {
            WindowState = state;
            SetWindowPos(Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            SetWindowPos(Handle, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
        }

        private void SetWindowStateThreadSafe(FormWindowState state)
        {
            if (InvokeRequired)
                Invoke(new SetWindowStateDelegate(SetWindowStateCallback), state);
            else
                SetWindowStateCallback(state);
        }

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X,
            int Y, int cx, int cy, uint uFlags);

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);

        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOZORDER = 0x0004;
        const UInt32 SWP_NOREDRAW = 0x0008;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        const UInt32 SWP_FRAMECHANGED = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
        const UInt32 SWP_SHOWWINDOW = 0x0040;
        const UInt32 SWP_HIDEWINDOW = 0x0080;
        const UInt32 SWP_NOCOPYBITS = 0x0100;
        const UInt32 SWP_NOOWNERZORDER = 0x0200;  /* Don't do owner Z ordering */
        const UInt32 SWP_NOSENDCHANGING = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */

        private string OnPipeRequest(string request)
        {
            if (File.Exists(request)) LoadWzFileThreadSafe(request, MainPanel, true);
            SetWindowStateThreadSafe(FormWindowState.Normal);
            return "OK";
        }

        private void PipeServer()
        {
            try
            {
                while (true)
                {
                    Program.pipe.WaitForConnection();
                    StreamReader sr = new StreamReader(Program.pipe);
                    OnPipeRequest(sr.ReadLine());
                    Program.pipe.Disconnect();
                }
            }
            catch { }
        }


        public static Thread updater = null;

        //a thread used by the updating feature
        private void UpdaterThread()
        {
            Thread.Sleep(60000);
            WebClient client = new WebClient();
            try
            {
                int version = int.Parse(
                    Encoding.ASCII.GetString(
                    client.DownloadData(
                    ApplicationSettings.UpdateServer + "version.txt"
                    )));
                string notice = Encoding.ASCII.GetString(
                    client.DownloadData(
                    ApplicationSettings.UpdateServer + "notice.txt"
                    ));
                string url = Encoding.ASCII.GetString(
                    client.DownloadData(
                    ApplicationSettings.UpdateServer + "url.txt"
                    ));
                if (version <= Constants.Version)
                    return;
                if (MessageBox.Show(notice.Replace("%URL%", url) + "\r\n\r\nClick \"Yes\" to download the new version.", "New Version", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Process.Start(url);
            }
            catch { }
        }

        #region Handlers
        private void MainForm_Load(object sender, EventArgs e)
        {
            encryptionBox.SelectedIndex = (int)ApplicationSettings.MapleVersion;
            if (UserSettings.AutoUpdate && ApplicationSettings.UpdateServer != "")
            {
                updater = new Thread(new ThreadStart(UpdaterThread));
                updater.IsBackground = true;
                updater.Start();
            }
        }

        private void RedockControls()
        {
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            RedockControls();
            if (this.Size.Width * this.Size.Height != 0)
            {
                ApplicationSettings.WindowSize = this.Size;
                ApplicationSettings.Maximized = WindowState == FormWindowState.Maximized;
            }
        }

        private void encryptionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplicationSettings.MapleVersion = (WzMapleVersion)encryptionBox.SelectedIndex;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog() { Title = "Select the WZ File", Filter = "WZ File(*.wz)|*.wz", Multiselect = true };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            foreach (string name in dialog.FileNames)
            {
                if (WzTool.IsListFile(name))
                    new ListEditor(name, (WzMapleVersion)encryptionBox.SelectedIndex).Show();
                else
                {
                    WzFile f = Program.WzMan.LoadWzFile(name, (WzMapleVersion)encryptionBox.SelectedIndex, MainPanel);
                }
            }
        }

        private void unloadAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Warning.Warn("Are you sure you want to unload all files?"))
                Program.WzMan.UnloadAll();
        }

        private void reloadAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Warning.Warn("Are you sure you want to reload all files?"))
                Program.WzMan.ReloadAll(MainPanel);
        }

        private void renderMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapper.ParseSettings();
            if (MainPanel.DataTree.SelectedNode.Tag is WzImage)
            {
                WzImage img = (WzImage)MainPanel.DataTree.SelectedNode.Tag;
                if (!Directory.Exists("Renders\\" + img.Name.Substring(0, img.Name.Length - 4)))
                    Directory.CreateDirectory("Renders\\" + img.Name.Substring(0, img.Name.Length - 4));
                new FHMapper.FHMapper(MainPanel).SaveMap(img, double.Parse(zoomTextBox.TextBox.Text));
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapper.ParseSettings();
            Settings settingsDialog = new Settings();
            settingsDialog.settings = mapper.settings;
            settingsDialog.main = mapper;
            settingsDialog.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new OptionsForm(MainPanel).ShowDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new NewForm(MainPanel).ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WzNode node;
            if (MainPanel.DataTree.SelectedNode == null)
            {
                if (MainPanel.DataTree.Nodes.Count == 1)
                    node = (WzNode)MainPanel.DataTree.Nodes[0];
                else
                {
                    MessageBox.Show("Please select the WZ file to save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (MainPanel.DataTree.SelectedNode.Tag is WzFile)
                    node = (WzNode)MainPanel.DataTree.SelectedNode;
                else
                    node = ((WzNode)MainPanel.DataTree.SelectedNode).TopLevelNode;
            }
            if (node.Tag is WzFile)
                new SaveForm(MainPanel, node).ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ApplicationSettings.Maximized = WindowState == FormWindowState.Maximized;
            e.Cancel = !Warning.Warn("Are you sure you want to exit?");
        }
        #endregion

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveSelectedNodes();
        }

        private void RemoveSelectedNodes()
        {
            if (!Warning.Warn("Are you sure you want to remove this node?")) return;
            MainPanel.RemoveSelectedNodes();
        }

        private void RunWzFilesExtraction(object param)
        {
            ChangeApplicationState(false);
            string[] wzFilesToDump = (string[])((object[])param)[0];
            string baseDir = (string)((object[])param)[1];
            WzMapleVersion version = (WzMapleVersion)((object[])param)[2];
            IWzFileSerializer serializer = (IWzFileSerializer)((object[])param)[3];
            UpdateProgressBar(MainPanel.mainProgressBar, 0, false, true);
            UpdateProgressBar(MainPanel.mainProgressBar, wzFilesToDump.Length, true, true);
            foreach (string wzpath in wzFilesToDump)
            {
                if (WzTool.IsListFile(wzpath))
                {
                    Warning.Error("The file at " + wzpath + " is a List.wz file and will be skipped.");
                    continue;
                }
                WzFile f = new WzFile(wzpath, version);
                f.ParseWzFile();
                serializer.SerializeFile(f, Path.Combine(baseDir, f.Name));
                f.Dispose();
                UpdateProgressBar(MainPanel.mainProgressBar, 1, false, false);
            }
            threadDone = true;
        }

        private void RunWzImgDirsExtraction(object param)
        {
            ChangeApplicationState(false);
            List<WzDirectory> dirsToDump = (List<WzDirectory>)((object[])param)[0];
            List<WzImage> imgsToDump = (List<WzImage>)((object[])param)[1];
            string baseDir = (string)((object[])param)[2];
            IWzImageSerializer serializer = (IWzImageSerializer)((object[])param)[3];
            UpdateProgressBar(MainPanel.mainProgressBar, 0, false, true);
            UpdateProgressBar(MainPanel.mainProgressBar, dirsToDump.Count + imgsToDump.Count, true, true);
            foreach (WzImage img in imgsToDump)
            {
                serializer.SerializeImage(img, Path.Combine(baseDir, img.Name));
                UpdateProgressBar(MainPanel.mainProgressBar, 1, false, false);
            }
            foreach (WzDirectory dir in dirsToDump)
            {
                serializer.SerializeDirectory(dir, Path.Combine(baseDir, dir.Name));
                UpdateProgressBar(MainPanel.mainProgressBar, 1, false, false);
            }
            threadDone = true;
        }

        private void RunWzObjExtraction(object param)
        {
            ChangeApplicationState(false);
            List<WzObject> objsToDump = (List<WzObject>)((object[])param)[0];
            string path = (string)((object[])param)[1];
            ProgressingWzSerializer serializer = (ProgressingWzSerializer)((object[])param)[2];
            UpdateProgressBar(MainPanel.mainProgressBar, 0, false, true);
            if (serializer is IWzObjectSerializer)
            {
                UpdateProgressBar(MainPanel.mainProgressBar, objsToDump.Count, true, true);
                foreach (WzObject obj in objsToDump)
                {
                    ((IWzObjectSerializer)serializer).SerializeObject(obj, path);
                    UpdateProgressBar(MainPanel.mainProgressBar, 1, false, false);
                }
            }
            else if (serializer is WzNewXmlSerializer)
            {
                UpdateProgressBar(MainPanel.mainProgressBar, 1, true, true);
                ((WzNewXmlSerializer)serializer).ExportCombinedXml(objsToDump, path);
                UpdateProgressBar(MainPanel.mainProgressBar, 1, false, false);

            }
            threadDone = true;
        }

        //yes I know this is a stupid way to synchronize threads, I'm just too lazy to use events or locks
        private bool threadDone = false;
        private Thread runningThread = null;


        private delegate void ChangeAppStateDelegate(bool enabled);
        private void ChangeApplicationStateCallback(bool enabled)
        {
            mainMenu.Enabled = enabled;
            MainPanel.Enabled = enabled;
            AbortButton.Visible = !enabled;
        }
        private void ChangeApplicationState(bool enabled)
        {
            Invoke(new ChangeAppStateDelegate(ChangeApplicationStateCallback), new object[] { enabled });
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog() { Title = "Choose the files you wish to export", Filter = "WZ Files(*.wz)|*.wz", Multiselect = true };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            FolderBrowserDialog folderDialog = new FolderBrowserDialog() { Description = "Select where to output the files to" };
            if (folderDialog.ShowDialog() != DialogResult.OK) return;
            WzClassicXmlSerializer serializer = new WzClassicXmlSerializer(UserSettings.Indentation, UserSettings.LineBreakType, false);
            threadDone = false;
            new Thread(new ParameterizedThreadStart(RunWzFilesExtraction)).Start((object)new object[] { dialog.FileNames, folderDialog.SelectedPath, encryptionBox.SelectedIndex, serializer });
            new Thread(new ParameterizedThreadStart(ProgressBarThread)).Start(serializer);
        }

        private delegate void UpdateProgressBarDelegate(ToolStripProgressBar pbar, int value, bool max, bool absolute); //max for .Maximum, !max for .Value
        private void UpdateProgressBarCallback(ToolStripProgressBar pbar, int value, bool max, bool absolute)
        {
            if (max)
            {
                if (absolute)
                    pbar.Maximum = value;
                else pbar.Maximum += value;
            }
            else
            {
                if (absolute)
                    pbar.Value = value;
                else pbar.Value += value;
            }
        }
        private void UpdateProgressBar(ToolStripProgressBar pbar, int value, bool max, bool absolute)
        {
            if (pbar.ProgressBar.InvokeRequired) pbar.ProgressBar.Invoke(new UpdateProgressBarDelegate(UpdateProgressBarCallback), new object[] { pbar, value, max, absolute });
            else UpdateProgressBarCallback(pbar, value, max, absolute);
        }


        private void ProgressBarThread(object param)
        {
            ProgressingWzSerializer serializer = (ProgressingWzSerializer)param;
            while (!threadDone)
            {
                int total = serializer.Total;
                UpdateProgressBar(MainPanel.secondaryProgressBar, total, true, true);
                UpdateProgressBar(MainPanel.secondaryProgressBar, Math.Min(total, serializer.Current), false, true);
                Thread.Sleep(500);
            }
            UpdateProgressBar(MainPanel.mainProgressBar, 0, true, true);
            UpdateProgressBar(MainPanel.secondaryProgressBar, 0, false, true);
            ChangeApplicationState(true);
            threadDone = false;
        }

        private string GetOutputDirectory()
        {
            return UserSettings.DefaultXmlFolder == "" ?
                SavedFolderBrowser.Show("Select the output directory")
                : UserSettings.DefaultXmlFolder;
        }

        private void rawDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog() { Title = "Choose the files you wish to export", Filter = "WZ Files(*.wz)|*.wz", Multiselect = true };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            string outPath = GetOutputDirectory();
            if (outPath == "") return;
            WzPngMp3Serializer serializer = new WzPngMp3Serializer();
            threadDone = false;
            runningThread = new Thread(new ParameterizedThreadStart(RunWzFilesExtraction));
            runningThread.Start((object)new object[] { dialog.FileNames, outPath, encryptionBox.SelectedIndex, serializer });
            new Thread(new ParameterizedThreadStart(ProgressBarThread)).Start(serializer);
        }

        private void imgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog() { Title = "Choose the files you wish to export", Filter = "WZ Files(*.wz)|*.wz", Multiselect = true };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            string outPath = GetOutputDirectory();
            if (outPath == "") return;
            WzImgSerializer serializer = new WzImgSerializer();
            threadDone = false;
            runningThread = new Thread(new ParameterizedThreadStart(RunWzFilesExtraction));
            runningThread.Start((object)new object[] { dialog.FileNames, outPath, encryptionBox.SelectedIndex, serializer });
            new Thread(new ParameterizedThreadStart(ProgressBarThread)).Start(serializer);
        }

        private void imgToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string outPath = GetOutputDirectory();
            if (outPath == "") return;
            List<WzDirectory> dirs = new List<WzDirectory>();
            List<WzImage> imgs = new List<WzImage>();
            foreach (WzNode node in MainPanel.DataTree.SelectedNodes)
            {
                if (node.Tag is WzDirectory) dirs.Add((WzDirectory)node.Tag);
                else if (node.Tag is WzImage) imgs.Add((WzImage)node.Tag);
            }
            WzImgSerializer serializer = new WzImgSerializer();
            threadDone = false;
            runningThread = new Thread(new ParameterizedThreadStart(RunWzImgDirsExtraction));
            runningThread.Start((object)new object[] { dirs, imgs, outPath, serializer });
            new Thread(new ParameterizedThreadStart(ProgressBarThread)).Start(serializer);
        }

        private void pNGsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string outPath = GetOutputDirectory();
            if (outPath == "") return;
            List<WzObject> objs = new List<WzObject>();
            foreach (WzNode node in MainPanel.DataTree.SelectedNodes)
                if (node.Tag is WzObject)
                    objs.Add((WzObject)node.Tag);
            WzPngMp3Serializer serializer = new WzPngMp3Serializer();
            threadDone = false;
            runningThread = new Thread(new ParameterizedThreadStart(RunWzObjExtraction));
            runningThread.Start((object)new object[] { objs, outPath, serializer });
            new Thread(new ParameterizedThreadStart(ProgressBarThread)).Start(serializer);
        }

        private void privateServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string outPath = GetOutputDirectory();
            if (outPath == "") return;
            List<WzDirectory> dirs = new List<WzDirectory>();
            List<WzImage> imgs = new List<WzImage>();
            foreach (WzNode node in MainPanel.DataTree.SelectedNodes)
            {
                if (node.Tag is WzDirectory) dirs.Add((WzDirectory)node.Tag);
                else if (node.Tag is WzImage) imgs.Add((WzImage)node.Tag);
            }
            WzClassicXmlSerializer serializer = new WzClassicXmlSerializer(UserSettings.Indentation, UserSettings.LineBreakType, false);
            threadDone = false;
            runningThread = new Thread(new ParameterizedThreadStart(RunWzImgDirsExtraction));
            runningThread.Start((object)new object[] { dirs, imgs, outPath, serializer });
            new Thread(new ParameterizedThreadStart(ProgressBarThread)).Start(serializer);
        }

        private void classicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string outPath = GetOutputDirectory();
            if (outPath == "") return;
            List<WzDirectory> dirs = new List<WzDirectory>();
            List<WzImage> imgs = new List<WzImage>();
            foreach (WzNode node in MainPanel.DataTree.SelectedNodes)
            {
                if (node.Tag is WzDirectory) dirs.Add((WzDirectory)node.Tag);
                else if (node.Tag is WzImage) imgs.Add((WzImage)node.Tag);
            }
            WzClassicXmlSerializer serializer = new WzClassicXmlSerializer(UserSettings.Indentation, UserSettings.LineBreakType, true);
            threadDone = false;
            runningThread = new Thread(new ParameterizedThreadStart(RunWzImgDirsExtraction));
            runningThread.Start((object)new object[] { dirs, imgs, outPath, serializer });
            new Thread(new ParameterizedThreadStart(ProgressBarThread)).Start(serializer);
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog() { Title = "Select where to save the XML", Filter = "eXtended Markup Language (*.xml)|*.xml" };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            List<WzObject> objs = new List<WzObject>();
            foreach (WzNode node in MainPanel.DataTree.SelectedNodes)
                if (node.Tag is WzObject)
                    objs.Add((WzObject)node.Tag);
            WzNewXmlSerializer serializer = new WzNewXmlSerializer(UserSettings.Indentation, UserSettings.LineBreakType);
            threadDone = false;
            runningThread = new Thread(new ParameterizedThreadStart(RunWzObjExtraction));
            runningThread.Start((object)new object[] { objs, dialog.FileName, serializer });
            new Thread(new ParameterizedThreadStart(ProgressBarThread)).Start(serializer);
        }

        private void AbortButton_Click(object sender, EventArgs e)
        {
            if (Warning.Warn("Are you sure you want to abort the current operation?"))
            {
                threadDone = true;
                runningThread.Abort();
            }
        }

        private bool ValidAnimation(WzObject prop)
        {
            if (!(prop is WzSubProperty)) return false;
            WzSubProperty castedProp = (WzSubProperty)prop;
            List<WzCanvasProperty> props = new List<WzCanvasProperty>(castedProp.WzProperties.Count);
            int foo;
            foreach (WzImageProperty subprop in castedProp.WzProperties)
            {
                if (!(subprop is WzCanvasProperty)) continue;
                if (!int.TryParse(subprop.Name, out foo)) return false;
                props.Add((WzCanvasProperty)subprop);
            }
            if (props.Count < 2) return false;
            props.Sort(new Comparison<WzCanvasProperty>(AnimationBuilder.PropertySorter));
            for (int i = 0; i < props.Count; i++) if (i.ToString() != props[i].Name) return false;
            return true;
        }

        private void aPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainPanel.DataTree.SelectedNode == null) return;
            if (!ValidAnimation((WzObject)MainPanel.DataTree.SelectedNode.Tag))
                Warning.Error("This item is not an animation.");
            else
            {
                SaveFileDialog dialog = new SaveFileDialog() { Title = "Select where to save the animation", Filter = "Animated Portable Network Graphics (*.png)|*.png" };
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                AnimationBuilder.ExtractAnimation((WzSubProperty)MainPanel.DataTree.SelectedNode.Tag, dialog.FileName, UserSettings.UseApngIncompatibilityFrame);
            }
        }

        private void wzDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            if (!(MainPanel.DataTree.SelectedNode.Tag is WzDirectory) && !(MainPanel.DataTree.SelectedNode.Tag is WzFile))
            {
                Warning.Error("Cannot insert selected object into this type of wz node.");
                return;
            }
            else if (!NameInputBox.Show("Add directory", out name))
                return;
            ((WzNode)MainPanel.DataTree.SelectedNode).AddObject(new WzDirectory(name), MainPanel.UndoRedoMan);
        }

        private void wzImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            if (!(MainPanel.DataTree.SelectedNode.Tag is WzDirectory) && !(MainPanel.DataTree.SelectedNode.Tag is WzFile))
            {
                Warning.Error("Cannot insert selected object into this type of wz node.");
                return;
            }
            else if (!NameInputBox.Show("Add image", out name))
                return;
            ((WzNode)MainPanel.DataTree.SelectedNode).AddObject(new WzImage(name) { Changed = true }, MainPanel.UndoRedoMan);
        }

        private void wzByteFloatPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            double? d;
            if (!(MainPanel.DataTree.SelectedNode.Tag is IPropertyContainer))
            {
                Warning.Error("Cannot insert selected object into this type of wz node.");
                return;
            }
            else if (!FloatingPointInputBox.Show("Add float", out name, out d))
                return;
            ((WzNode)MainPanel.DataTree.SelectedNode).AddObject(new WzFloatProperty(name, (float)d), MainPanel.UndoRedoMan);
        }

        private void wzCanvasPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            Bitmap bmp;
            if (!(MainPanel.DataTree.SelectedNode.Tag is IPropertyContainer))
            {
                Warning.Error("Cannot insert selected object into this type of wz node.");
                return;
            }
            else if (!BitmapInputBox.Show("Add canvas", out name, out bmp))
                return;
            ((WzNode)MainPanel.DataTree.SelectedNode).AddObject(new WzCanvasProperty(name) { PngProperty = new WzPngProperty() { PNG = bmp } }, MainPanel.UndoRedoMan);
        }

        private void wzCompressedIntPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            int? value;
            if (!(MainPanel.DataTree.SelectedNode.Tag is IPropertyContainer))
            {
                Warning.Error("Cannot insert selected object into this type of wz node.");
                return;
            }
            else if (!IntInputBox.Show("Add int", out name, out value))
                return;
            ((WzNode)MainPanel.DataTree.SelectedNode).AddObject(new WzIntProperty(name, (int)value), MainPanel.UndoRedoMan);
        }

        private void wzConvexPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            if (!(MainPanel.DataTree.SelectedNode.Tag is IPropertyContainer))
            {
                Warning.Error("Cannot insert selected object into this type of wz node.");
                return;
            }
            else if (!NameInputBox.Show("Add convex", out name))
                return;
            ((WzNode)MainPanel.DataTree.SelectedNode).AddObject(new WzConvexProperty(name), MainPanel.UndoRedoMan);
        }

        private void wzDoublePropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            double? d;
            if (!(MainPanel.DataTree.SelectedNode.Tag is IPropertyContainer))
            {
                Warning.Error("Cannot insert selected object into this type of wz node.");
                return;
            }
            else if (!FloatingPointInputBox.Show("Add double", out name, out d))
                return;
            ((WzNode)MainPanel.DataTree.SelectedNode).AddObject(new WzDoubleProperty(name, (double)d), MainPanel.UndoRedoMan);
         }

        private void wzNullPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            if (!(MainPanel.DataTree.SelectedNode.Tag is IPropertyContainer))
            {
                Warning.Error("Cannot insert selected object into this type of wz node.");
                return;
            }
            else if (!NameInputBox.Show("Add null", out name))
                return;
            ((WzNode)MainPanel.DataTree.SelectedNode).AddObject(new WzNullProperty(name), MainPanel.UndoRedoMan);
        }

        private void wzSoundPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            string path;
            if (!(MainPanel.DataTree.SelectedNode.Tag is IPropertyContainer))
            {
                Warning.Error("Cannot insert selected object into this type of wz node.");
                return;
            }
            else if (!SoundInputBox.Show("Add sound", out name, out path))
                return;
            ((WzNode)MainPanel.DataTree.SelectedNode).AddObject(new WzSoundProperty(name, path), MainPanel.UndoRedoMan);
        }

        private void wzStringPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            string value;
            if (!(MainPanel.DataTree.SelectedNode.Tag is IPropertyContainer))
            {
                Warning.Error("Cannot insert selected object into this type of wz node.");
                return;
            }
            else if (!NameValueInputBox.Show("Add string", out name, out value))
                return;
            ((WzNode)MainPanel.DataTree.SelectedNode).AddObject(new WzStringProperty(name, value), MainPanel.UndoRedoMan);
        }

        private void wzSubPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            if (!(MainPanel.DataTree.SelectedNode.Tag is IPropertyContainer))
            {
                Warning.Error("Cannot insert selected object into this type of wz node.");
                return;
            }
            else if (!NameInputBox.Show("Add sub", out name))
                return;
            ((WzNode)MainPanel.DataTree.SelectedNode).AddObject(new WzSubProperty(name), MainPanel.UndoRedoMan);
        }

        private void wzUnsignedShortPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            int? value;
            if (!(MainPanel.DataTree.SelectedNode.Tag is IPropertyContainer))
            {
                Warning.Error("Cannot insert selected object into this type of wz node.");
                return;
            }
            else if (!IntInputBox.Show("Add ushort", out name, out value))
                return;
            ((WzNode)MainPanel.DataTree.SelectedNode).AddObject(new WzShortProperty(name, (short)value), MainPanel.UndoRedoMan);
        }

        private void wzUolPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            string value;
            if (!(MainPanel.DataTree.SelectedNode.Tag is IPropertyContainer))
            {
                Warning.Error("Cannot insert selected object into this type of wz node.");
                return;
            }
            else if (!NameValueInputBox.Show("Add uol", out name, out value))
                return;
            ((WzNode)MainPanel.DataTree.SelectedNode).AddObject(new WzUOLProperty(name, value), MainPanel.UndoRedoMan);
        }

        private void wzVectorPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            Point? pt;
            if (!(MainPanel.DataTree.SelectedNode.Tag is IPropertyContainer))
            {
                Warning.Error("Cannot insert selected object into this type of wz node.");
                return;
            }
            else if (!VectorInputBox.Show("Add vector", out name, out pt))
                return;
            ((WzNode)MainPanel.DataTree.SelectedNode).AddObject(new WzVectorProperty(name, new WzIntProperty("X",((Point)pt).X),new WzIntProperty("Y",((Point)pt).Y)), MainPanel.UndoRedoMan);
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainPanel.DataTree.ExpandAll();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainPanel.DataTree.CollapseAll();
        }

        private void xMLToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (MainPanel.DataTree.SelectedNode == null ||(!(MainPanel.DataTree.SelectedNode.Tag is WzDirectory) && !(MainPanel.DataTree.SelectedNode.Tag is WzFile) && !(MainPanel.DataTree.SelectedNode.Tag is IPropertyContainer))) return;
             WzFile wzFile = ((WzObject)MainPanel.DataTree.SelectedNode.Tag).WzFileParent;
            if (!(wzFile is WzFile)) return;
            OpenFileDialog dialog = new OpenFileDialog() { Title = "Select the XML files", Filter = "eXtended Markup Language (*.xml)|*.xml", Multiselect = true };
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            WzXmlDeserializer deserializer = new WzXmlDeserializer(true, WzTool.GetIvByMapleVersion(wzFile.MapleVersion));
            yesToAll = false;
            noToAll = false;
            threadDone = false;

            runningThread = new Thread(new ParameterizedThreadStart(WzImporterThread));
            runningThread.Start(new object[] { deserializer, dialog.FileNames, MainPanel.DataTree.SelectedNode, null });
            new Thread(new ParameterizedThreadStart(ProgressBarThread)).Start(deserializer);
        }


        private delegate void InsertWzNode(WzNode node, WzNode parent);
        private void InsertWzNodeCallback(WzNode node, WzNode parent)
        {
            WzNode child = WzNode.GetChildNode(parent, node.Text);
            if (child != null)
            {
                if (ShowReplaceDialog(node.Text))
                    child.Delete();
                else return;
            }
            parent.AddNode(node);
        }

        private void InsertWzNodeThreadSafe(WzNode node, WzNode parent)
        {
            if (MainPanel.InvokeRequired) MainPanel.Invoke(new InsertWzNode(InsertWzNodeCallback), node, parent);
            else InsertWzNodeCallback(node, parent);
        }

        private bool yesToAll = false;
        private bool noToAll = false;

        private bool ShowReplaceDialog(string name)
        {
            if (yesToAll) return true;
            else if (noToAll) return false;
            else
            {
                ReplaceBox dialog = new ReplaceBox(name);
                dialog.ShowDialog();
                switch (dialog.result)
                {
                    case ReplaceResult.NoToAll:
                        noToAll = true;
                        return false;
                    case ReplaceResult.No:
                        return false;
                    case ReplaceResult.YesToAll:
                        yesToAll = true;
                        return true;
                    case ReplaceResult.Yes:
                        return true;
                }
            }
            throw new Exception("cant get here anyway");
        }

        private void WzImporterThread(object param)
        {
            ChangeApplicationState(false);
            object[] arr = (object[])param;
            ProgressingWzSerializer deserializer = (ProgressingWzSerializer)arr[0];
            string[] files = (string[])arr[1];
            WzNode parent = (WzNode)arr[2];
            byte[] iv = (byte[])arr[3];
            WzObject parentObj = (WzObject)parent.Tag;
            if (parentObj is WzFile) parentObj = ((WzFile)parentObj).WzDirectory;
            UpdateProgressBar(MainPanel.mainProgressBar, files.Length, true, true);
            foreach (string file in files)
            {
                List<WzObject> objs;
                try
                {
                    if (deserializer is WzXmlDeserializer)
                        objs = ((WzXmlDeserializer)deserializer).ParseXML(file);
                    else
                        objs = new List<WzObject> { ((WzImgDeserializer)deserializer).WzImageFromIMGFile(file, iv, Path.GetFileName(file)) };
                }
                catch (ThreadAbortException)
                {
                    return;
                }
                catch (Exception e)
                {
                    Warning.Error("The file \"" + file + "\" is invalid and will be skipped. Error: " + e.Message);
                    UpdateProgressBar(MainPanel.mainProgressBar, 1, false, false);
                    continue;
                }
                foreach (WzObject obj in objs)
                {
                    if (((obj is WzDirectory || obj is WzImage) && parentObj is WzDirectory) || (obj is WzImageProperty && parentObj is IPropertyContainer))
                    {
                        WzNode node = new WzNode(obj);
                        InsertWzNodeThreadSafe(node, parent);
                    }
                }
                UpdateProgressBar(MainPanel.mainProgressBar, 1, false, false);
            }
            threadDone = true;
        }

        private void iMGToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (MainPanel.DataTree.SelectedNode == null || (!(MainPanel.DataTree.SelectedNode.Tag is WzDirectory) && !(MainPanel.DataTree.SelectedNode.Tag is WzFile) && !(MainPanel.DataTree.SelectedNode.Tag is IPropertyContainer))) return;
            WzFile wzFile = ((WzObject)MainPanel.DataTree.SelectedNode.Tag).WzFileParent;
            if (!(wzFile is WzFile)) return;
            OpenFileDialog dialog = new OpenFileDialog() { Title = "Select the IMG files", Filter = "WZ Image Files (*.img)|*.img", Multiselect = true };
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            byte[] iv = WzTool.GetIvByMapleVersion(wzFile.MapleVersion);
            WzImgDeserializer deserializer = new WzImgDeserializer(false);
            yesToAll = false;
            noToAll = false;
            threadDone = false;

            runningThread = new Thread(new ParameterizedThreadStart(WzImporterThread));
            runningThread.Start(new object[] { deserializer, dialog.FileNames, MainPanel.DataTree.SelectedNode, iv });
            new Thread(new ParameterizedThreadStart(ProgressBarThread)).Start(deserializer);
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainPanel.findStrip.Visible = true;
        }

        private static readonly string HelpFile = "Help.htm";
        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string helpPath = Path.Combine(Application.StartupPath, HelpFile);
            if (File.Exists(helpPath))
                Help.ShowHelp(this, HelpFile);
            else
                Warning.Error("Help could not be shown because the help file (" + HelpFile + ") was not found");
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainPanel.DoCopy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainPanel.DoPaste();
        }
    }
}