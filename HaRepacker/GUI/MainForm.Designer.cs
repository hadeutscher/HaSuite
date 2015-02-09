namespace HaRepacker.GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unloadAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wzDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wzImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.wzByteFloatPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wzCanvasPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wzCompressedIntPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wzConvexPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wzDoublePropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wzNullPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wzSoundPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wzStringPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wzSubPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wzUnsignedShortPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wzUolPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wzVectorPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportFilesToXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.privateServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pNGsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.iMGToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fHMappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renderMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.animateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encryptionBox = new System.Windows.Forms.ToolStripComboBox();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AbortButton = new System.Windows.Forms.Button();
            this.MainPanel = new HaRepackerLib.Controls.HaRepackerMainPanel();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.extrasToolStripMenuItem,
            this.encryptionBox,
            this.helpToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(683, 25);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.unloadAllToolStripMenuItem,
            this.reloadAllToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.page_white;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.newToolStripMenuItem.Text = "New...";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.folder;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.disk;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.saveToolStripMenuItem.Text = "Save...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // unloadAllToolStripMenuItem
            // 
            this.unloadAllToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.delete;
            this.unloadAllToolStripMenuItem.Name = "unloadAllToolStripMenuItem";
            this.unloadAllToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.unloadAllToolStripMenuItem.Text = "Unload All";
            this.unloadAllToolStripMenuItem.Click += new System.EventHandler(this.unloadAllToolStripMenuItem_Click);
            // 
            // reloadAllToolStripMenuItem
            // 
            this.reloadAllToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.arrow_refresh;
            this.reloadAllToolStripMenuItem.Name = "reloadAllToolStripMenuItem";
            this.reloadAllToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.reloadAllToolStripMenuItem.Text = "Reload All";
            this.reloadAllToolStripMenuItem.Click += new System.EventHandler(this.reloadAllToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.expandAllToolStripMenuItem,
            this.collapseAllToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wzDirectoryToolStripMenuItem,
            this.wzImageToolStripMenuItem,
            this.toolStripSeparator1,
            this.wzByteFloatPropertyToolStripMenuItem,
            this.wzCanvasPropertyToolStripMenuItem,
            this.wzCompressedIntPropertyToolStripMenuItem,
            this.wzConvexPropertyToolStripMenuItem,
            this.wzDoublePropertyToolStripMenuItem,
            this.wzNullPropertyToolStripMenuItem,
            this.wzSoundPropertyToolStripMenuItem,
            this.wzStringPropertyToolStripMenuItem,
            this.wzSubPropertyToolStripMenuItem,
            this.wzUnsignedShortPropertyToolStripMenuItem,
            this.wzUolPropertyToolStripMenuItem,
            this.wzVectorPropertyToolStripMenuItem});
            this.addToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.add;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // wzDirectoryToolStripMenuItem
            // 
            this.wzDirectoryToolStripMenuItem.Name = "wzDirectoryToolStripMenuItem";
            this.wzDirectoryToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.wzDirectoryToolStripMenuItem.Text = "WzDirectory";
            this.wzDirectoryToolStripMenuItem.Click += new System.EventHandler(this.wzDirectoryToolStripMenuItem_Click);
            // 
            // wzImageToolStripMenuItem
            // 
            this.wzImageToolStripMenuItem.Name = "wzImageToolStripMenuItem";
            this.wzImageToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.wzImageToolStripMenuItem.Text = "WzImage";
            this.wzImageToolStripMenuItem.Click += new System.EventHandler(this.wzImageToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
            // 
            // wzByteFloatPropertyToolStripMenuItem
            // 
            this.wzByteFloatPropertyToolStripMenuItem.Name = "wzByteFloatPropertyToolStripMenuItem";
            this.wzByteFloatPropertyToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.wzByteFloatPropertyToolStripMenuItem.Text = "WzByteFloatProperty";
            this.wzByteFloatPropertyToolStripMenuItem.Click += new System.EventHandler(this.wzByteFloatPropertyToolStripMenuItem_Click);
            // 
            // wzCanvasPropertyToolStripMenuItem
            // 
            this.wzCanvasPropertyToolStripMenuItem.Name = "wzCanvasPropertyToolStripMenuItem";
            this.wzCanvasPropertyToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.wzCanvasPropertyToolStripMenuItem.Text = "WzCanvasProperty";
            this.wzCanvasPropertyToolStripMenuItem.Click += new System.EventHandler(this.wzCanvasPropertyToolStripMenuItem_Click);
            // 
            // wzCompressedIntPropertyToolStripMenuItem
            // 
            this.wzCompressedIntPropertyToolStripMenuItem.Name = "wzCompressedIntPropertyToolStripMenuItem";
            this.wzCompressedIntPropertyToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.wzCompressedIntPropertyToolStripMenuItem.Text = "WzCompressedIntProperty";
            this.wzCompressedIntPropertyToolStripMenuItem.Click += new System.EventHandler(this.wzCompressedIntPropertyToolStripMenuItem_Click);
            // 
            // wzConvexPropertyToolStripMenuItem
            // 
            this.wzConvexPropertyToolStripMenuItem.Name = "wzConvexPropertyToolStripMenuItem";
            this.wzConvexPropertyToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.wzConvexPropertyToolStripMenuItem.Text = "WzConvexProperty";
            this.wzConvexPropertyToolStripMenuItem.Click += new System.EventHandler(this.wzConvexPropertyToolStripMenuItem_Click);
            // 
            // wzDoublePropertyToolStripMenuItem
            // 
            this.wzDoublePropertyToolStripMenuItem.Name = "wzDoublePropertyToolStripMenuItem";
            this.wzDoublePropertyToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.wzDoublePropertyToolStripMenuItem.Text = "WzDoubleProperty";
            this.wzDoublePropertyToolStripMenuItem.Click += new System.EventHandler(this.wzDoublePropertyToolStripMenuItem_Click);
            // 
            // wzNullPropertyToolStripMenuItem
            // 
            this.wzNullPropertyToolStripMenuItem.Name = "wzNullPropertyToolStripMenuItem";
            this.wzNullPropertyToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.wzNullPropertyToolStripMenuItem.Text = "WzNullProperty";
            this.wzNullPropertyToolStripMenuItem.Click += new System.EventHandler(this.wzNullPropertyToolStripMenuItem_Click);
            // 
            // wzSoundPropertyToolStripMenuItem
            // 
            this.wzSoundPropertyToolStripMenuItem.Name = "wzSoundPropertyToolStripMenuItem";
            this.wzSoundPropertyToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.wzSoundPropertyToolStripMenuItem.Text = "WzSoundProperty";
            this.wzSoundPropertyToolStripMenuItem.Click += new System.EventHandler(this.wzSoundPropertyToolStripMenuItem_Click);
            // 
            // wzStringPropertyToolStripMenuItem
            // 
            this.wzStringPropertyToolStripMenuItem.Name = "wzStringPropertyToolStripMenuItem";
            this.wzStringPropertyToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.wzStringPropertyToolStripMenuItem.Text = "WzStringProperty";
            this.wzStringPropertyToolStripMenuItem.Click += new System.EventHandler(this.wzStringPropertyToolStripMenuItem_Click);
            // 
            // wzSubPropertyToolStripMenuItem
            // 
            this.wzSubPropertyToolStripMenuItem.Name = "wzSubPropertyToolStripMenuItem";
            this.wzSubPropertyToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.wzSubPropertyToolStripMenuItem.Text = "WzSubProperty";
            this.wzSubPropertyToolStripMenuItem.Click += new System.EventHandler(this.wzSubPropertyToolStripMenuItem_Click);
            // 
            // wzUnsignedShortPropertyToolStripMenuItem
            // 
            this.wzUnsignedShortPropertyToolStripMenuItem.Name = "wzUnsignedShortPropertyToolStripMenuItem";
            this.wzUnsignedShortPropertyToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.wzUnsignedShortPropertyToolStripMenuItem.Text = "WzUnsignedShortProperty";
            this.wzUnsignedShortPropertyToolStripMenuItem.Click += new System.EventHandler(this.wzUnsignedShortPropertyToolStripMenuItem_Click);
            // 
            // wzUolPropertyToolStripMenuItem
            // 
            this.wzUolPropertyToolStripMenuItem.Name = "wzUolPropertyToolStripMenuItem";
            this.wzUolPropertyToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.wzUolPropertyToolStripMenuItem.Text = "WzUolProperty";
            this.wzUolPropertyToolStripMenuItem.Click += new System.EventHandler(this.wzUolPropertyToolStripMenuItem_Click);
            // 
            // wzVectorPropertyToolStripMenuItem
            // 
            this.wzVectorPropertyToolStripMenuItem.Name = "wzVectorPropertyToolStripMenuItem";
            this.wzVectorPropertyToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.wzVectorPropertyToolStripMenuItem.Text = "WzVectorProperty";
            this.wzVectorPropertyToolStripMenuItem.Click += new System.EventHandler(this.wzVectorPropertyToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.delete;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.removeToolStripMenuItem.Text = "Remove (Del)";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Visible = false;
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Visible = false;
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.expandAllToolStripMenuItem.Text = "Expand All";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.collapseAllToolStripMenuItem.Text = "Collapse All";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportFilesToXMLToolStripMenuItem,
            this.exportDataToolStripMenuItem,
            this.importToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 21);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // exportFilesToXMLToolStripMenuItem
            // 
            this.exportFilesToXMLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xMLToolStripMenuItem,
            this.rawDataToolStripMenuItem,
            this.imgToolStripMenuItem});
            this.exportFilesToXMLToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.folder_go;
            this.exportFilesToXMLToolStripMenuItem.Name = "exportFilesToXMLToolStripMenuItem";
            this.exportFilesToXMLToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exportFilesToXMLToolStripMenuItem.Text = "Export File(s) to";
            // 
            // xMLToolStripMenuItem
            // 
            this.xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            this.xMLToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.xMLToolStripMenuItem.Text = "Private Server XML";
            this.xMLToolStripMenuItem.Click += new System.EventHandler(this.xMLToolStripMenuItem_Click);
            // 
            // rawDataToolStripMenuItem
            // 
            this.rawDataToolStripMenuItem.Name = "rawDataToolStripMenuItem";
            this.rawDataToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.rawDataToolStripMenuItem.Text = "PNG\\MP3";
            this.rawDataToolStripMenuItem.Click += new System.EventHandler(this.rawDataToolStripMenuItem_Click);
            // 
            // imgToolStripMenuItem
            // 
            this.imgToolStripMenuItem.Name = "imgToolStripMenuItem";
            this.imgToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.imgToolStripMenuItem.Text = "IMG";
            this.imgToolStripMenuItem.Click += new System.EventHandler(this.imgToolStripMenuItem_Click);
            // 
            // exportDataToolStripMenuItem
            // 
            this.exportDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xMLToolStripMenuItem1,
            this.pNGsToolStripMenuItem,
            this.imgToolStripMenuItem1});
            this.exportDataToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.page_go;
            this.exportDataToolStripMenuItem.Name = "exportDataToolStripMenuItem";
            this.exportDataToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exportDataToolStripMenuItem.Text = "Export Selection";
            // 
            // xMLToolStripMenuItem1
            // 
            this.xMLToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.privateServerToolStripMenuItem,
            this.classicToolStripMenuItem,
            this.newToolStripMenuItem1});
            this.xMLToolStripMenuItem1.Name = "xMLToolStripMenuItem1";
            this.xMLToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.xMLToolStripMenuItem1.Text = "XML";
            // 
            // privateServerToolStripMenuItem
            // 
            this.privateServerToolStripMenuItem.Name = "privateServerToolStripMenuItem";
            this.privateServerToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.privateServerToolStripMenuItem.Text = "Private server";
            this.privateServerToolStripMenuItem.Click += new System.EventHandler(this.privateServerToolStripMenuItem_Click);
            // 
            // classicToolStripMenuItem
            // 
            this.classicToolStripMenuItem.Name = "classicToolStripMenuItem";
            this.classicToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.classicToolStripMenuItem.Text = "Classic";
            this.classicToolStripMenuItem.Click += new System.EventHandler(this.classicToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem1
            // 
            this.newToolStripMenuItem1.Name = "newToolStripMenuItem1";
            this.newToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.newToolStripMenuItem1.Text = "New";
            this.newToolStripMenuItem1.Click += new System.EventHandler(this.newToolStripMenuItem1_Click);
            // 
            // pNGsToolStripMenuItem
            // 
            this.pNGsToolStripMenuItem.Name = "pNGsToolStripMenuItem";
            this.pNGsToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.pNGsToolStripMenuItem.Text = "PNG\\MP3";
            this.pNGsToolStripMenuItem.Click += new System.EventHandler(this.pNGsToolStripMenuItem_Click);
            // 
            // imgToolStripMenuItem1
            // 
            this.imgToolStripMenuItem1.Name = "imgToolStripMenuItem1";
            this.imgToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.imgToolStripMenuItem1.Text = "IMG";
            this.imgToolStripMenuItem1.Click += new System.EventHandler(this.imgToolStripMenuItem1_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xMLToolStripMenuItem2,
            this.iMGToolStripMenuItem2});
            this.importToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.page_add;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // xMLToolStripMenuItem2
            // 
            this.xMLToolStripMenuItem2.Name = "xMLToolStripMenuItem2";
            this.xMLToolStripMenuItem2.Size = new System.Drawing.Size(98, 22);
            this.xMLToolStripMenuItem2.Text = "XML";
            this.xMLToolStripMenuItem2.Click += new System.EventHandler(this.xMLToolStripMenuItem2_Click);
            // 
            // iMGToolStripMenuItem2
            // 
            this.iMGToolStripMenuItem2.Name = "iMGToolStripMenuItem2";
            this.iMGToolStripMenuItem2.Size = new System.Drawing.Size(98, 22);
            this.iMGToolStripMenuItem2.Text = "IMG";
            this.iMGToolStripMenuItem2.Click += new System.EventHandler(this.iMGToolStripMenuItem2_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.cog;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.find;
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.searchToolStripMenuItem.Text = "Search (Ctrl+F)";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.copyToolStripMenuItem.Text = "Copy (Ctrl+C)";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.pasteToolStripMenuItem.Text = "Paste (Ctrl+V)";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // extrasToolStripMenuItem
            // 
            this.extrasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fHMappingToolStripMenuItem,
            this.animateToolStripMenuItem});
            this.extrasToolStripMenuItem.Name = "extrasToolStripMenuItem";
            this.extrasToolStripMenuItem.Size = new System.Drawing.Size(49, 21);
            this.extrasToolStripMenuItem.Text = "Extras";
            // 
            // fHMappingToolStripMenuItem
            // 
            this.fHMappingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renderMapToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.zoomTextBox});
            this.fHMappingToolStripMenuItem.Name = "fHMappingToolStripMenuItem";
            this.fHMappingToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.fHMappingToolStripMenuItem.Text = "FH Mapping";
            // 
            // renderMapToolStripMenuItem
            // 
            this.renderMapToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.map;
            this.renderMapToolStripMenuItem.Name = "renderMapToolStripMenuItem";
            this.renderMapToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.renderMapToolStripMenuItem.Text = "Render Map";
            this.renderMapToolStripMenuItem.Click += new System.EventHandler(this.renderMapToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.cog;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.settingsToolStripMenuItem.Text = "Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // zoomTextBox
            // 
            this.zoomTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.zoomTextBox.Name = "zoomTextBox";
            this.zoomTextBox.Size = new System.Drawing.Size(100, 21);
            this.zoomTextBox.Text = "1";
            // 
            // animateToolStripMenuItem
            // 
            this.animateToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.lightning;
            this.animateToolStripMenuItem.Name = "animateToolStripMenuItem";
            this.animateToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.animateToolStripMenuItem.Text = "Animate";
            this.animateToolStripMenuItem.Click += new System.EventHandler(this.aPNGToolStripMenuItem_Click);
            // 
            // encryptionBox
            // 
            this.encryptionBox.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.encryptionBox.Items.AddRange(new object[] {
            "GMS (old)",
            "MSEA (old)",
            "BMS\\GMS\\MSEA"});
            this.encryptionBox.Name = "encryptionBox";
            this.encryptionBox.Size = new System.Drawing.Size(155, 21);
            this.encryptionBox.SelectedIndexChanged += new System.EventHandler(this.encryptionBox_SelectedIndexChanged);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.help;
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.viewHelpToolStripMenuItem.Text = "View Help";
            this.viewHelpToolStripMenuItem.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::HaRepacker.Properties.Resources.information;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.aboutToolStripMenuItem.Text = "About HaRepacker";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 21);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Visible = false;
            // 
            // AbortButton
            // 
            this.AbortButton.Location = new System.Drawing.Point(274, 61);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(113, 75);
            this.AbortButton.TabIndex = 2;
            this.AbortButton.Text = "Abort";
            this.AbortButton.UseVisualStyleBackColor = true;
            this.AbortButton.Visible = false;
            this.AbortButton.Click += new System.EventHandler(this.AbortButton_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 25);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(683, 344);
            this.MainPanel.Sort = true;
            this.MainPanel.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 369);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "HaRepacker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HaRepackerLib.Controls.HaRepackerMainPanel MainPanel;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportFilesToXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox encryptionBox;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rawDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imgToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pNGsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imgToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wzDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wzImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem wzByteFloatPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wzCanvasPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wzCompressedIntPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wzConvexPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wzDoublePropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wzNullPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wzSoundPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wzStringPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wzSubPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wzUnsignedShortPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wzUolPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wzVectorPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem iMGToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem privateServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem classicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem1;
        private System.Windows.Forms.Button AbortButton;
        private System.Windows.Forms.ToolStripMenuItem extrasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem animateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fHMappingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renderMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox zoomTextBox;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem unloadAllToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem reloadAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    }
}

