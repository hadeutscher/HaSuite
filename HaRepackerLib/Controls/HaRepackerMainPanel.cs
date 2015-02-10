/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Drawing;
using System.Windows.Forms;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using System.Collections.Generic;
using System.Drawing.Imaging;
using WeifenLuo.WinFormsUI.Docking;

namespace HaRepackerLib.Controls
{
    public partial class HaRepackerMainPanel : UserControl
    {
        private const string selectionTypePrefix = "Selection Type: ";
        private List<IWzObject> clipboard = new List<IWzObject>();
        private UndoRedoManager undoRedoMan;

        public HaRepackerMainPanel()
        {
            InitializeComponent();
            MainSplitContainer.Parent = MainDockPanel;
            undoRedoMan = new UndoRedoManager(this);
        }

        #region Handlers
        private void RedockControls()
        {
            if (Width * Height == 0) return;
            //Point autoScrollPos = pictureBoxPanel.AutoScrollPosition;
            pictureBoxPanel.AutoScrollPosition = new Point();
            MainSplitContainer.Location = new Point(0, 0);
            MainSplitContainer.Size = new Size(Width, statusStrip.Location.Y - (findStrip.Visible ? findStrip.Height : 0));
            MainDockPanel.Location = new Point(0, 0);
            MainDockPanel.Size = MainSplitContainer.Size;
            DataTree.Location = new Point(0, 0);
            DataTree.Size = new Size(MainSplitContainer.Panel1.Width, MainSplitContainer.Panel1.Height);
            nameBox.Location = new Point(0, 0);
            nameBox.Size = new Size(MainSplitContainer.Panel2.Width, nameBox.Size.Height);
            pictureBoxPanel.Location = new Point(0, nameBox.Size.Height + nameBox.Margin.Bottom);
            pictureBoxPanel.Size = new Size(MainSplitContainer.Panel2.Width, MainSplitContainer.Panel2.Height - pictureBoxPanel.Location.Y - saveImageButton.Height - saveImageButton.Margin.Top);
            canvasPropBox.Location = new Point(0, 0);
            canvasPropBox.Size = canvasPropBox.Image == null ? new Size(0, 0) : canvasPropBox.Image.Size;
            textPropBox.Location = pictureBoxPanel.Location;
            textPropBox.Size = pictureBoxPanel.Size;
            mp3Player.Location = new Point(MainSplitContainer.Panel2.Width / 2 - mp3Player.Width / 2, MainSplitContainer.Height / 2 - mp3Player.Height / 2);
            vectorPanel.Location = new Point(MainSplitContainer.Panel2.Width / 2 - vectorPanel.Width / 2, MainSplitContainer.Height / 2 - vectorPanel.Height / 2);
            applyChangesButton.Location = new Point(MainSplitContainer.Panel2.Width / 2 - applyChangesButton.Width / 2, MainSplitContainer.Panel2.Height - applyChangesButton.Height);
            changeImageButton.Location = new Point(MainSplitContainer.Panel2.Width / 2 - (changeImageButton.Width + changeImageButton.Margin.Right + saveImageButton.Width) / 2, MainSplitContainer.Panel2.Height - changeImageButton.Height);
            saveImageButton.Location = new Point(changeImageButton.Location.X + changeImageButton.Width + changeImageButton.Margin.Right, changeImageButton.Location.Y);
            changeSoundButton.Location = changeImageButton.Location;
            saveSoundButton.Location = saveImageButton.Location;
        }

        private void MainSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            RedockControls();
        }

        private void HaRepackerMainPanel_SizeChanged(object sender, EventArgs e)
        {
            RedockControls();
        }

        private void DataTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (DataTree.SelectedNode == null) return;
            ShowObjectValue((IWzObject)DataTree.SelectedNode.Tag);
            selectionLabel.Text = selectionTypePrefix + ((WzNode)DataTree.SelectedNode).GetTypeName();
        }

        private void ShowObjectValue(IWzObject obj)
        {
            mp3Player.SoundProperty = null;
            nameBox.Text = obj is WzFile ? ((WzFile)obj).Header.Copyright : obj.Name;
            nameBox.ButtonEnabled = false;
            if (obj is WzFile || obj is WzDirectory || obj is WzImage || obj is WzNullProperty || obj is WzSubProperty || obj is WzConvexProperty)
            {
                nameBox.Visible = true;
                canvasPropBox.Visible = false;
                pictureBoxPanel.Visible = false;
                textPropBox.Visible = false;
                mp3Player.Visible = false;
                vectorPanel.Visible = false;
                applyChangesButton.Visible = false;
                changeImageButton.Visible = false;
                saveImageButton.Visible = false;
                changeSoundButton.Visible = false;
                saveSoundButton.Visible = false;
            }
            else if (obj is WzCanvasProperty)
            {
                nameBox.Visible = true;
                canvasPropBox.Visible = true;
                pictureBoxPanel.Visible = true;
                textPropBox.Visible = false;
                mp3Player.Visible = false;
                canvasPropBox.Image = (Bitmap)obj;
                vectorPanel.Visible = false;
                applyChangesButton.Visible = false;
                changeImageButton.Visible = true;
                saveImageButton.Visible = true;
                changeSoundButton.Visible = false;
                saveSoundButton.Visible = false;
            }
            else if (obj is WzSoundProperty)
            {
                nameBox.Visible = true;
                canvasPropBox.Visible = false;
                pictureBoxPanel.Visible = false;
                textPropBox.Visible = false;
                mp3Player.Visible = true;
                mp3Player.SoundProperty = (WzSoundProperty)obj;
                vectorPanel.Visible = false;
                applyChangesButton.Visible = false;
                changeImageButton.Visible = false;
                saveImageButton.Visible = false;
                changeSoundButton.Visible = true;
                saveSoundButton.Visible = true;
            }
            else if (obj is WzStringProperty || obj is WzCompressedIntProperty || obj is WzDoubleProperty || obj is WzByteFloatProperty || obj is WzUnsignedShortProperty || obj is WzUOLProperty)
            {
                nameBox.Visible = true;
                canvasPropBox.Visible = false;
                pictureBoxPanel.Visible = false;
                textPropBox.Visible = true;
                mp3Player.Visible = false;
                textPropBox.Text = (string)obj;
                vectorPanel.Visible = false;
                applyChangesButton.Visible = true;
                changeImageButton.Visible = false;
                saveImageButton.Visible = false;
                changeSoundButton.Visible = false;
                saveSoundButton.Visible = false;
            }
            else if (obj is WzVectorProperty)
            {
                nameBox.Visible = true;
                canvasPropBox.Visible = false;
                pictureBoxPanel.Visible = false;
                textPropBox.Visible = false;
                mp3Player.Visible = false;
                vectorPanel.Visible = true;
                vectorPanel.X = ((WzVectorProperty)obj).X.Value;
                vectorPanel.Y = ((WzVectorProperty)obj).Y.Value;
                applyChangesButton.Visible = true;
                changeImageButton.Visible = false;
                saveImageButton.Visible = false;
                changeSoundButton.Visible = false;
                saveSoundButton.Visible = false;
            }
            else
            {
            }
        }

        private void DataTree_DoubleClick(object sender, EventArgs e)
        {
            if (DataTree.SelectedNode != null && DataTree.SelectedNode.Tag is WzImage && DataTree.SelectedNode.Nodes.Count == 0)
            {
                if (!((WzImage)DataTree.SelectedNode.Tag).Parsed)
                    ((WzImage)DataTree.SelectedNode.Tag).ParseImage();
                ((WzNode)DataTree.SelectedNode).Reparse();
                DataTree.SelectedNode.Expand();
            }
        }
        #endregion

        #region Exported Fields
        private bool sort = UserSettings.Sort;

        public UndoRedoManager UndoRedoMan { get { return undoRedoMan; } }

        public bool Sort
        {
            get { return sort; }
            set
            {
                sort = value;
                UserSettings.Sort = value;
            }
        }
        #endregion

        private void saveImageButton_Click(object sender, EventArgs e)
        {
            if (!(DataTree.SelectedNode.Tag is WzCanvasProperty)) return;
            Bitmap bmp = ((WzCanvasProperty)DataTree.SelectedNode.Tag).PngProperty.GetPNG(false);
            SaveFileDialog dialog = new SaveFileDialog() { Title = "Select where to save the image...", Filter = "Portable Network Grpahics (*.png)|*.png|CompuServe Graphics Interchange Format (*.gif)|*.gif|Bitmap (*.bmp)|*.bmp|Joint Photographic Experts Group Format (*.jpg)|*.jpg|Tagged Image File Format (*.tif)|*.tif" };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            switch (dialog.FilterIndex)
            {
                case 1: //png
                    bmp.Save(dialog.FileName, ImageFormat.Png);
                    break;
                case 2: //gif
                    bmp.Save(dialog.FileName, ImageFormat.Gif);
                    break;
                case 3: //bmp
                    bmp.Save(dialog.FileName, ImageFormat.Bmp);
                    break;
                case 4: //jpg
                    bmp.Save(dialog.FileName, ImageFormat.Jpeg);
                    break;
                case 5: //tiff
                    bmp.Save(dialog.FileName, ImageFormat.Tiff);
                    break;
            }
        }

        private void saveSoundButton_Click(object sender, EventArgs e)
        {
            if (!(DataTree.SelectedNode.Tag is WzSoundProperty)) return;
            WzSoundProperty mp3 = (WzSoundProperty)DataTree.SelectedNode.Tag;
            SaveFileDialog dialog = new SaveFileDialog() { Title = "Select where to save the image...", Filter = "Moving Pictures Experts Group Format 1 Audio Layer 3 (*.mp3)|*.mp3" };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            mp3.SaveToFile(dialog.FileName);
        }

        private void nameBox_ButtonClicked(object sender, EventArgs e)
        {
            if (DataTree.SelectedNode == null) return;
            if (DataTree.SelectedNode.Tag is WzFile)
            {
                ((WzFile)DataTree.SelectedNode.Tag).Header.Copyright = nameBox.Text;
                ((WzFile)DataTree.SelectedNode.Tag).Header.RecalculateFileStart();
            }
            else if (WzNode.CanNodeBeInserted((WzNode)DataTree.SelectedNode.Parent, nameBox.Text))
            {
                string text = nameBox.Text;
                ((WzNode)DataTree.SelectedNode).ChangeName(text);
                nameBox.Text = text;
                nameBox.ButtonEnabled = false;
            }
            else
                Warning.Error("A node with that name already exists. Please choose a different name");

        }

        public void RemoveSelectedNodes()
        {
            List<UndoRedoAction> actions = new List<UndoRedoAction>();
            TreeNode[] nodeArr = new TreeNode[DataTree.SelectedNodes.Count];
            DataTree.SelectedNodes.CopyTo(nodeArr, 0);
            foreach (WzNode node in nodeArr)
                if (!(node.Tag is WzFile) && node.Parent != null)
                {
                    actions.Add(UndoRedoManager.ObjectRemoved((WzNode)node.Parent, node));
                    node.Delete();
                }
            UndoRedoMan.AddUndoBatch(actions);
        }

        private void applyChangesButton_Click(object sender, EventArgs e)
        {
            if (DataTree.SelectedNode == null) return;
            IWzObject obj = (IWzObject)DataTree.SelectedNode.Tag;
            if (obj is IWzImageProperty) ((IWzImageProperty)obj).ParentImage.Changed = true;
            if (obj is WzVectorProperty)
            {
                ((WzVectorProperty)obj).X.Value = vectorPanel.X;
                ((WzVectorProperty)obj).Y.Value = vectorPanel.Y;
            }
            else if (obj is WzStringProperty)
                ((WzStringProperty)obj).Value = textPropBox.Text;
            else if (obj is WzByteFloatProperty)
            {
                float val;
                if (!float.TryParse(textPropBox.Text,out val)) 
                { 
                    Warning.Error("Could not convert \"" + textPropBox.Text + "\" to the required type");
                    return;
                }
                ((WzByteFloatProperty)obj).Value = val;
            }
            else if (obj is WzCompressedIntProperty)
            {
                int val;
                if (!int.TryParse(textPropBox.Text, out val))
                {
                    Warning.Error("Could not convert \"" + textPropBox.Text + "\" to the required type");
                    return;
                }
                ((WzCompressedIntProperty)obj).Value = val;
            }
            else if (obj is WzDoubleProperty)
            {
                double val;
                if (!double.TryParse(textPropBox.Text, out val))
                {
                    Warning.Error("Could not convert \"" + textPropBox.Text + "\" to the required type");
                    return;
                }
                ((WzDoubleProperty)obj).Value = val;
            }
            else if (obj is WzUnsignedShortProperty)
            {
                ushort val;
                if (!ushort.TryParse(textPropBox.Text, out val))
                {
                    Warning.Error("Could not convert \"" + textPropBox.Text + "\" to the required type");
                    return;
                }
                ((WzUnsignedShortProperty)obj).Value = val;
            }
            else if (obj is WzUOLProperty)
                ((WzUOLProperty)obj).Value = textPropBox.Text;
        }

        private void changeImageButton_Click(object sender, EventArgs e)
        {
            if (DataTree.SelectedNode.Tag is WzCanvasProperty)
            {
                OpenFileDialog dialog = new OpenFileDialog() { Title = "Select the image", Filter = "Supported Image Formats (*.png;*.bmp;*.jpg;*.gif;*.jpeg;*.tif;*.tiff)|*.png;*.bmp;*.jpg;*.gif;*.jpeg;*.tif;*.tiff" };
                if (dialog.ShowDialog() != DialogResult.OK) return;
                Bitmap bmp;
                try
                {
                    bmp = (Bitmap)Image.FromFile(dialog.FileName);
                }
                catch
                {
                    Warning.Error("Could not load image");
                    return;
                }
                ((WzCanvasProperty)DataTree.SelectedNode.Tag).PngProperty.SetPNG(bmp);
                ((WzCanvasProperty)DataTree.SelectedNode.Tag).ParentImage.Changed = true;
                canvasPropBox.Image = bmp;
            }

        }

        private void changeSoundButton_Click(object sender, EventArgs e)
        {
            if (DataTree.SelectedNode.Tag is WzSoundProperty)
            {
                OpenFileDialog dialog = new OpenFileDialog() { Title = "Select the sound", Filter = "Moving Pictures Experts Group Format 1 Audio Layer 3(*.mp3)|*.mp3" };
                if (dialog.ShowDialog() != DialogResult.OK) return;
                WzSoundProperty prop;
                try
                {
                    prop = new WzSoundProperty(((WzSoundProperty)DataTree.SelectedNode.Tag).Name, dialog.FileName);
                }
                catch
                {
                    Warning.Error("Could not load image");
                    return;
                }
                IPropertyContainer parent = (IPropertyContainer)((WzSoundProperty)DataTree.SelectedNode.Tag).Parent;
                ((WzSoundProperty)DataTree.SelectedNode.Tag).ParentImage.Changed = true;
                ((WzSoundProperty)DataTree.SelectedNode.Tag).Remove();
                DataTree.SelectedNode.Tag = prop;
                parent.AddProperty(prop);
                mp3Player.SoundProperty = prop;
            }
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

        public void DoCopy()
        {
            if (!Warning.Warn("Copy WZ nodes to clipboard? (warning - can take a lot of time if many nodes are selected)")) return;
            clipboard.Clear();
            foreach (WzNode node in DataTree.SelectedNodes)
            {
                IWzObject clone = null;
                if (node.Tag is WzDirectory)
                {
                    Warning.Error("You can't copy directories because they require too much memory");
                    continue;
                }
                else if (node.Tag is WzImage)
                {
                    clone = ((WzImage)node.Tag).DeepClone();
                }
                else if (node.Tag is IWzImageProperty)
                {
                    clone = ((IWzImageProperty)node.Tag).DeepClone();
                }
                else continue;
                clipboard.Add(clone);
            }
        }

        public void DoPaste()
        {
            if (!Warning.Warn("Paste WZ nodes from clipboard? (warning - can take a lot of time if many nodes are pasted)")) return;
            yesToAll = false;
            noToAll = false;
            WzNode parent = (WzNode)DataTree.SelectedNode;
            IWzObject parentObj = (IWzObject)parent.Tag;
            if (parentObj is WzFile) parentObj = ((WzFile)parentObj).WzDirectory;

            foreach (IWzObject obj in clipboard)
            {
                if (((obj is WzDirectory || obj is WzImage) && parentObj is WzDirectory) || (obj is IWzImageProperty && parentObj is IPropertyContainer))
                {
                    WzNode node = new WzNode(obj);
                    WzNode child = WzNode.GetChildNode(parent, node.Text);
                    if (child != null)
                    {
                        if (ShowReplaceDialog(node.Text))
                            child.Delete();
                        else return;
                    }
                    parent.AddNode(node);

                }
            }
        }

        private void DataTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (!DataTree.Focused) return;
            bool ctrl = (Control.ModifierKeys & Keys.Control) == Keys.Control;
            bool alt = (Control.ModifierKeys & Keys.Alt) == Keys.Alt;
            bool shift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
            Keys filteredKeys = e.KeyData;
            if (ctrl) filteredKeys = filteredKeys ^ Keys.Control;
            if (alt) filteredKeys = filteredKeys ^ Keys.Alt;
            if (shift) filteredKeys = filteredKeys ^ Keys.Shift;

            switch (filteredKeys)
            {
                case Keys.Delete:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    if (!Warning.Warn("Are you sure you want to remove this node?")) return;
                    RemoveSelectedNodes();
                    break;
            }
            if (ctrl)
            {
                switch (filteredKeys)
                {
                    case Keys.C:
                        DoCopy();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.V:
                        DoPaste();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.F:
                        findStrip.Visible = true;
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;
                }
            }
        }

        private int searchidx = 0;
        private bool finished = false;
        private bool listSearchResults = false;
        private List<string> searchResultsList = new List<string>();
        private bool searchValues = true;
        private WzNode coloredNode = null;
        private int currentidx = 0;
        private string searchText = "";
        private bool extractImages = false;

        private void btnClose_Click(object sender, EventArgs e)
        {
            findStrip.Visible = false;
            searchidx = 0;
            if (coloredNode != null)
            {
                coloredNode.BackColor = Color.White;
                coloredNode = null;
            }
        }


        private void SearchWzProperties(IPropertyContainer parent)
        {
            foreach (IWzImageProperty prop in parent.WzProperties)
            {
                if ((0 <= prop.Name.IndexOf(searchText, StringComparison.InvariantCultureIgnoreCase)) || (searchValues && prop is WzStringProperty && (0 <= ((WzStringProperty)prop).Value.IndexOf(searchText, StringComparison.InvariantCultureIgnoreCase))))
                {
                    if (listSearchResults)
                        searchResultsList.Add(prop.FullPath.Replace(";", @"\"));
                    else if (currentidx == searchidx)
                    {
                        if (prop.HRTag == null)
                            ((WzNode)prop.ParentImage.HRTag).Reparse();
                        WzNode node = (WzNode)prop.HRTag;
                        //if (node.Style == null) node.Style = new ElementStyle();
                        node.BackColor = Color.Yellow;
                        coloredNode = node;
                        node.EnsureVisible();
                        //DataTree.Focus();
                        finished = true;
                        searchidx++;
                        return;
                    }
                    else
                        currentidx++;
                }
                if (prop is IPropertyContainer && prop.WzProperties.Count != 0)
                {
                    SearchWzProperties((IPropertyContainer)prop);
                    if (finished)
                        return;
                }
            }
        }

        private void SearchTV(WzNode node)
        {
            foreach (WzNode subnode in node.Nodes)
            {
                if (0 <= subnode.Text.IndexOf(searchText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (listSearchResults)
                        searchResultsList.Add(subnode.FullPath.Replace(";", @"\"));
                    else if (currentidx == searchidx)
                    {
                        //if (subnode.Style == null) subnode.Style = new ElementStyle();
                        subnode.BackColor = Color.Yellow;
                        coloredNode = subnode;
                        subnode.EnsureVisible();
                        //DataTree.Focus();
                        finished = true;
                        searchidx++;
                        return;
                    }
                    else
                        currentidx++;
                }
                if (subnode.Tag is WzImage)
                {
                    WzImage img = (WzImage)subnode.Tag;
                    if (img.Parsed)
                        SearchWzProperties(img);
                    else if (extractImages)
                    {
                        img.ParseImage();
                        SearchWzProperties(img);
                    }
                    if (finished) return;
                }
                else SearchTV(subnode);
            }
            /*foreach (WzDirectory subdir in dir.WzDirectories)
            {
                if (subdir.Name.Contains(searchText))
                {
                    if (currentidx == searchidx)
                    {
                        WzNode node = (WzNode)subdir.HRTag;
                        DataTree.SelectedNode = node;
                        node.EnsureVisible();
                        DataTree.Focus();
                        finished = true;
                        searchidx++;
                        return;
                    }
                    else if (listSearchResults)
                        searchResultsList.Add(subdir.FullPath);
                    else
                        currentidx++;
                }
                SearchTV(subdir, currentidx, searchText, extractImages);
                if (finished)
                    return;
            }
            foreach (WzImage img in dir.WzImages)
            {
                if (img.Name.Contains(searchText))
                {
                    if (currentidx == searchidx)
                    {
                        WzNode node = (WzNode)img.HRTag;
                        DataTree.SelectedNode = node;
                        node.EnsureVisible();
                        DataTree.Focus();
                        finished = true;
                        searchidx++;
                        return;
                    }
                    else if (listSearchResults)
                        searchResultsList.Add(img.FullPath);
                    else
                        currentidx++;
                }
                if (img.Parsed)
                    SearchWzProperties(img, currentidx, searchText);
                else if (extractImages)
                {
                    img.ParseImage();
                    SearchWzProperties(img, currentidx, searchText);
                }
                if (finished) return;
            }*/
        }

        /*private void SearchTV(Node parent, int currentidx, string searchText)
        {
            foreach (Node node in parent.Nodes)
            {
                if (node.Text.Contains(searchText))
                {
                    if (currentidx == searchidx)
                    {
                        DataTree.SelectedNode = node;
                        node.EnsureVisible();
                        DataTree.Focus();
                        finished = true;
                        searchidx++;
                        return;
                    }
                    else
                    {
                        currentidx++;
                    }
                }
                if (node.Nodes.Count != 0)
                {
                    SearchTV(node, currentidx, searchText);
                    if (finished)
                        return;
                }
            }
        }*/

        private void btnRestart_Click(object sender, EventArgs e)
        {
            searchidx = 0;
            if (coloredNode != null)
            {
                coloredNode.BackColor = Color.White;
                coloredNode = null;
            }
            findBox.Focus();
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            new SearchOptionsForm().ShowDialog();
            findBox.Focus();
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            if (coloredNode != null)
            {
                coloredNode.BackColor = Color.White;
                coloredNode = null;
            }
            if (findBox.Text == "" || DataTree.Nodes.Count == 0) return;
            if (DataTree.SelectedNode == null) DataTree.SelectedNode = DataTree.Nodes[0];
            finished = false;
            listSearchResults = false;
            searchResultsList.Clear();
            searchValues = UserSettings.SearchStringValues;
            currentidx = 0;
            searchText = findBox.Text;
            extractImages = UserSettings.ParseImagesInSearch;
            foreach (WzNode node in DataTree.SelectedNodes)
            {
                if (node.Tag is IPropertyContainer)
                    SearchWzProperties((IPropertyContainer)node.Tag);
                else if (node.Tag is IWzImageProperty) continue;
                else SearchTV(node);
                if (finished) break;
            }
            if (!finished) { MessageBox.Show("Reached the end of the tree"); searchidx = 0; DataTree.SelectedNode.EnsureVisible(); }
            findBox.Focus();
        }

        private void btnFindAll_Click(object sender, EventArgs e)
        {
            if (coloredNode != null)
            {
                coloredNode.BackColor = Color.White;
                coloredNode = null;
            }
            if (findBox.Text == "" || DataTree.Nodes.Count == 0) return;
            if (DataTree.SelectedNode == null) DataTree.SelectedNode = DataTree.Nodes[0];
            finished = false;
            listSearchResults = true;
            searchResultsList.Clear();
            //searchResultsBox.Items.Clear();
            searchValues = UserSettings.SearchStringValues;
            currentidx = 0;
            searchText = findBox.Text;
            extractImages = UserSettings.ParseImagesInSearch;
            foreach (WzNode node in DataTree.SelectedNodes)
            {
                if (node.Tag is IWzImageProperty) continue;
                else if (node.Tag is IPropertyContainer)
                    SearchWzProperties((IPropertyContainer)node.Tag);
                else SearchTV(node);
            }
            DockableSearchResult dsr = new DockableSearchResult();
            dsr.SelectedIndexChanged += new EventHandler(searchResultsBox_SelectedIndexChanged);
            foreach (string result in searchResultsList)
                dsr.searchResultsBox.Items.Add(result);
            dsr.Show(MainDockPanel);
            dsr.DockState = DockState.DockBottom;
//            searchResults.AutoHide = false;
//            searchResults.Visible = true;
//            searchResultsContainer.Visible = true;
//            dockSite8.Visible = true;
//            panelDockContainer1.Visible = true;
            findBox.Focus();
        }

        private void findBox_TextChanged(object sender, EventArgs e)
        {
            searchidx = 0;
        }

        private void findBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { btnFindNext_Click(null, null); e.Handled = true; }
        }

        private void findBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) e.Handled = true;
        }

        private void findStrip_VisibleChanged(object sender, EventArgs e)
        {
            RedockControls();
            if (findStrip.Visible) findBox.Focus();
        }

        private void searchResults_VisibleChanged(object sender, EventArgs e)
        {
            RedockControls();
        }

        private void searchResultsContainer_VisibleChanged(object sender, System.EventArgs e)
        {
            RedockControls();
        }

        private WzNode GetNodeByName(TreeNodeCollection collection, string name)
        {
            foreach (WzNode node in collection) 
                if (node.Text == name) 
                    return node; 
            return null;
        }

        private void searchResultsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox searchResultsBox = (ListBox)sender;
            try
            {
                if (searchResultsBox.SelectedItem != null)
                {
                    string[] splitPath = ((string)searchResultsBox.SelectedItem).Split(@"\".ToCharArray());
                    WzNode node = null;
                    TreeNodeCollection collection = DataTree.Nodes;
                    for (int i = 0; i < splitPath.Length; i++)
                    {
                        node = GetNodeByName(collection, splitPath[i]);
                        if (node.Tag is WzImage && !((WzImage)node.Tag).Parsed && i != splitPath.Length - 1)
                        {
                            ((WzImage)node.Tag).ParseImage();
                            node.Reparse();
                        }
                        collection = node.Nodes;
                    }
                    if (node != null)
                    {
                        DataTree.SelectedNode = node;
                        node.EnsureVisible();
                        DataTree.RefreshSelectedNodes();
                    }
                }
            }
            catch
            {
            }
        }
    }
}
