using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DevComponents.AdvTree;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;

namespace HaRepackerLib
{
    public  class ContextMenuManager
    {
        //private HaRepackerMainPanel parent;

        public  ContextMenuStrip WzFileMenu;
        public  ContextMenuStrip WzListFileMenu;
        public  ContextMenuStrip WzDirectoryMenu;
        public  ContextMenuStrip PropertyContainerMenu;
        public  ContextMenuStrip SubPropertyMenu;
        public  ContextMenuStrip PropertyMenu;

        private  ToolStripMenuItem SaveFile;
        private  ToolStripMenuItem Remove;
        private  ToolStripMenuItem Unload;
        private  ToolStripMenuItem Reload;

        private   ToolStripMenuItem AddPropsSubMenu;
        private   ToolStripMenuItem AddDirsSubMenu;
        private   ToolStripMenuItem AddConvexSubMenu;
        private   ToolStripMenuItem AddListSubMenu;
        private   ToolStripMenuItem AddList;
        private   ToolStripMenuItem AddImage;
        private   ToolStripMenuItem AddDirectory;
        private   ToolStripMenuItem AddByteFloat;
        private   ToolStripMenuItem AddCanvas;
        private   ToolStripMenuItem AddInt;
        private   ToolStripMenuItem AddConvex;
        private   ToolStripMenuItem AddDouble;
        private   ToolStripMenuItem AddNull;
        private   ToolStripMenuItem AddSound;
        private   ToolStripMenuItem AddString;
        private   ToolStripMenuItem AddSub;
        private   ToolStripMenuItem AddUshort;
        private   ToolStripMenuItem AddUOL;
        private   ToolStripMenuItem AddVector;

        private   ToolStripMenuItem ExportPropertySubMenu;
        private   ToolStripMenuItem ExportAnimationSubMenu;
        private   ToolStripMenuItem ExportDirectorySubMenu;
        private   ToolStripMenuItem ExportPServerXML;
        private   ToolStripMenuItem ExportDataXML;
        private   ToolStripMenuItem ExportImgData;
        private   ToolStripMenuItem ExportRawData;
        private   ToolStripMenuItem ExportGIF;
        private   ToolStripMenuItem ExportAPNG;

        private   ToolStripMenuItem ImportSubMenu;
        private   ToolStripMenuItem ImportXML;
        private   ToolStripMenuItem ImportImgData;

        public    ContextMenuManager(/*HaRepackerMainPanel parent*/)
        {
            //this.parent = parent;
            SaveFile = new ToolStripMenuItem("Save...", Properties.Resources.disk, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    foreach (WzNode node in GetNodes(sender))
                    {
                        HaRepackerMainPanel parent = ((HaRepackerMainPanel)node.TreeControl.Parent.Parent.Parent);
                        parent.CallSaveForm(node);
                    }
                }));
            Remove = new ToolStripMenuItem("Remove", Properties.Resources.delete, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    if (!Warning.Warn("Are you sure you want to remove this node?")) return;
                    foreach (WzNode node in GetNodes(sender))
                        if (!(node.Tag is IWzFile))
                            node.Delete();
                }));
            Unload = new ToolStripMenuItem("Unload", Properties.Resources.delete, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    if (!Warning.Warn("Are you sure you want to unload this file?")) return;
                    foreach (WzNode node in GetNodes(sender))
                    {
                        HaRepackerMainPanel parent = ((HaRepackerMainPanel)node.TreeControl.Parent.Parent.Parent);
                        parent.CallUnloadFile(node);
                    }
                }));
            Reload = new ToolStripMenuItem("Reload", Properties.Resources.arrow_refresh, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    if (!Warning.Warn("Are you sure you want to reload this file?")) return;
                    foreach (WzNode node in GetNodes(sender))
                    {
                        HaRepackerMainPanel parent = ((HaRepackerMainPanel)node.TreeControl.Parent.Parent.Parent);
                        parent.CallReloadFile(node);
                    }
                }));

            AddList = new ToolStripMenuItem("List Entry", null, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    WzNode[] nodes = GetNodes(sender);
                    if (nodes.Length != 1) { MessageBox.Show("Please select only ONE node"); return; }
                    string name;
                    if (Interaction.NameInputBox.Show("Add List Entry", out name))
                        nodes[0].AddObject(new WzListEntry(name));
                }));
            AddImage = new ToolStripMenuItem("Image", null, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    WzNode[] nodes = GetNodes(sender);
                    if (nodes.Length != 1) { MessageBox.Show("Please select only ONE node"); return; }
                    string name;
                    if (Interaction.NameInputBox.Show("Add Image", out name))
                        nodes[0].AddObject(new WzImage(name));
                }));
            AddDirectory = new ToolStripMenuItem("Directory", null, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    WzNode[] nodes = GetNodes(sender);
                    if (nodes.Length != 1) { MessageBox.Show("Please select only ONE node"); return; }
                    string name;
                    if (Interaction.NameInputBox.Show("Add Directory", out name))
                        nodes[0].AddObject(new WzDirectory(name));
                }));
            AddByteFloat = new ToolStripMenuItem("Float", null, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    WzNode[] nodes = GetNodes(sender);
                    if (nodes.Length != 1) { MessageBox.Show("Please select only ONE node"); return; }
                    string name; double? val;
                    if (Interaction.FloatingPointInputBox.Show("Add Float", out name, out val))
                        nodes[0].AddObject(new WzByteFloatProperty(name, (float)val));
                }));
            AddCanvas = new ToolStripMenuItem("Canvas", null, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    WzNode[] nodes = GetNodes(sender);
                    if (nodes.Length != 1) { MessageBox.Show("Please select only ONE node"); return; }
                    string name; string path;
                    if (Interaction.BitmapInputBox.Show("Add Canvas", out name, out path))
                    {
                        Bitmap bmp = new Bitmap(path);
                        WzCanvasProperty prop = new WzCanvasProperty(name);
                        prop.PngProperty = new WzPngProperty();
                        prop.PngProperty.SetPNG(bmp);
                        nodes[0].AddObject(new WzCanvasProperty(name));
                    }
                }));
            AddInt = new ToolStripMenuItem("Int", null, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    WzNode[] nodes = GetNodes(sender);
                    if (nodes.Length != 1) { MessageBox.Show("Please select only ONE node"); return; }
                    string name; int? val;
                    if (Interaction.IntInputBox.Show("Add Int", out name, out val))
                        nodes[0].AddObject(new WzCompressedIntProperty(name, (int)val));
                }));
            AddConvex = new ToolStripMenuItem("Convex", null, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    WzNode[] nodes = GetNodes(sender);
                    if (nodes.Length != 1) { MessageBox.Show("Please select only ONE node"); return; }
                    string name;
                    if (Interaction.NameInputBox.Show("Add Convex", out name))
                        nodes[0].AddObject(new WzConvexProperty(name));
                }));
            AddDouble = new ToolStripMenuItem("Double", null, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    WzNode[] nodes = GetNodes(sender);
                    if (nodes.Length != 1) { MessageBox.Show("Please select only ONE node"); return; }
                    string name; double? val;
                    if (Interaction.FloatingPointInputBox.Show("Add Double", out name, out val))
                        nodes[0].AddObject(new WzDoubleProperty(name, (double)val));
                }));
            AddNull = new ToolStripMenuItem("Null", null, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    WzNode[] nodes = GetNodes(sender);
                    if (nodes.Length != 1) { MessageBox.Show("Please select only ONE node"); return; }
                    string name;
                    if (Interaction.NameInputBox.Show("Add Null", out name))
                        nodes[0].AddObject(new WzNullProperty(name));
                }));
            AddSound = new ToolStripMenuItem("Sound", null, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    WzNode[] nodes = GetNodes(sender);
                    if (nodes.Length != 1) { MessageBox.Show("Please select only ONE node"); return; }
                    string name, path;
                    if (Interaction.SoundInputBox.Show("Add Sound", out name, out path))
                    {
                        try { nodes[0].AddObject(WzSoundProperty.CreateCustomProperty(name, path)); }
                        catch (Exception ex) { MessageBox.Show("Exception caught while adding property: \"" + ex.Message + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }));
            AddString = new ToolStripMenuItem("String", null, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    WzNode[] nodes = GetNodes(sender);
                    if (nodes.Length != 1) { MessageBox.Show("Please select only ONE node"); return; }
                    string name, value;
                    if (Interaction.NameValueInputBox.Show("Add String", out name, out value))
                        nodes[0].AddObject(new WzStringProperty(name, value));
                }));
            AddSub = new ToolStripMenuItem("Sub", null, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    WzNode[] nodes = GetNodes(sender);
                    if (nodes.Length != 1) { MessageBox.Show("Please select only ONE node"); return; }
                    string name;
                    if (Interaction.NameInputBox.Show("Add Sub", out name))
                        nodes[0].AddObject(new WzSubProperty(name));
                }));
            AddUshort = new ToolStripMenuItem("Short", null, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    WzNode[] nodes = GetNodes(sender);
                    if (nodes.Length != 1) { MessageBox.Show("Please select only ONE node"); return; }
                    string name; int? val;
                    if (Interaction.IntInputBox.Show("Add Unsigned Short", out name, out val))
                        nodes[0].AddObject(new WzUnsignedShortProperty(name, (ushort)val));
                }));
            AddUOL = new ToolStripMenuItem("UOL", null, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    WzNode[] nodes = GetNodes(sender);
                    if (nodes.Length != 1) { MessageBox.Show("Please select only ONE node"); return; }
                    string name, value;
                    if (Interaction.NameValueInputBox.Show("Add UOL", out name, out value))
                        nodes[0].AddObject(new WzUOLProperty(name, value));
                }));
            AddVector = new ToolStripMenuItem("Vector", null, new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    WzNode[] nodes = GetNodes(sender);
                    if (nodes.Length != 1) { MessageBox.Show("Please select only ONE node"); return; }
                    string name; Point? pt;
                    if (Interaction.VectorInputBox.Show("Add Vector", out name, out pt))
                        nodes[0].AddObject(new WzVectorProperty(name, new WzCompressedIntProperty("X", pt.Value.X), new WzCompressedIntProperty("Y", pt.Value.Y)));
                }));

            AddConvexSubMenu = new ToolStripMenuItem("Add", Properties.Resources.add, AddVector);
            AddDirsSubMenu = new ToolStripMenuItem("Add", Properties.Resources.add, AddDirectory, AddImage);
            AddPropsSubMenu = new ToolStripMenuItem("Add", Properties.Resources.add, AddDirectory, AddCanvas, AddConvex, AddDouble, AddByteFloat, AddInt, AddNull, AddUshort, AddSound, AddString, AddSub, AddUOL, AddVector);
            AddListSubMenu = new ToolStripMenuItem("Add", Properties.Resources.add, AddList);

            WzFileMenu = new ContextMenuStrip();
            WzFileMenu.Items.AddRange(new ToolStripItem[] { AddDirsSubMenu, SaveFile, Unload, Reload });
            WzListFileMenu = new ContextMenuStrip();
            WzListFileMenu.Items.AddRange(new ToolStripItem[] { AddListSubMenu, SaveFile, Unload, Reload });
            WzDirectoryMenu = new ContextMenuStrip();
            WzDirectoryMenu.Items.AddRange(new ToolStripItem[] { AddDirsSubMenu, /*export, import,*/Remove });
            PropertyContainerMenu = new ContextMenuStrip();
            PropertyContainerMenu.Items.AddRange(new ToolStripItem[] { AddPropsSubMenu, /*export, import,*/Remove });
            MessageBox.Show(PropertyContainerMenu.Items.Count.ToString());
            PropertyMenu = new ContextMenuStrip();
            PropertyMenu.Items.AddRange(new ToolStripItem[] { /*export, import,*/Remove });
            SubPropertyMenu = new ContextMenuStrip();
            SubPropertyMenu.Items.AddRange(new ToolStripItem[] { AddPropsSubMenu, /*export, import,*/Remove });
        }

        public   ContextMenuStrip CreateMenu(IWzObject Tag)
        {
            ContextMenuStrip menu = null;
            if (Tag is WzImage || Tag is IPropertyContainer)
            {
                if (Tag is WzSubProperty)
                {
                                menu = new ContextMenuStrip();
            menu.Items.AddRange(new ToolStripItem[] { AddPropsSubMenu, /*export, import,*/Remove });

                }
                else
                {
                    menu = new ContextMenuStrip();
                    menu.Items.AddRange(new ToolStripItem[] { AddPropsSubMenu, /*export, import,*/Remove });
                }
            }
            else if (Tag is IWzImageProperty)
            {
                menu = new ContextMenuStrip();
                menu.Items.AddRange(new ToolStripItem[] { /*export, import,*/Remove });
            }
            else if (Tag is WzDirectory)
            {
                menu = new ContextMenuStrip();
                menu.Items.AddRange(new ToolStripItem[] { AddDirsSubMenu, /*export, import,*/Remove });
            }
            else if (Tag is WzFile)
            {
                menu = new ContextMenuStrip();
                menu.Items.AddRange(new ToolStripItem[] { AddDirsSubMenu, SaveFile, Unload, Reload });
            }
            else if (Tag is WzListFile)
            {
                menu = new ContextMenuStrip();
                menu.Items.AddRange(new ToolStripItem[] { AddListSubMenu, SaveFile, Unload, Reload });
            }
            return menu;

            
        }

        private   WzNode[] GetNodes(object sender)
        {
            NodeCollection selectedNodes = ((AdvTree)((ContextMenuStrip)sender).SourceControl).SelectedNodes;
            Node[] selectedNodesArr = new Node[selectedNodes.Count];
            selectedNodes.CopyTo(selectedNodesArr);
            return (WzNode[])selectedNodesArr.Cast<WzNode>();
        }
    }
}