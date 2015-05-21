namespace HaRepackerLib.Controls
{
    partial class HaRepackerMainPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HaRepackerMainPanel));
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin5 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin5 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient13 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient29 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin5 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient5 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient30 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient14 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient31 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient5 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient32 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient33 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient15 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient34 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient35 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.DataTree = new TreeViewMS.TreeViewMS();
            this.saveSoundButton = new System.Windows.Forms.Button();
            this.saveImageButton = new System.Windows.Forms.Button();
            this.changeSoundButton = new System.Windows.Forms.Button();
            this.changeImageButton = new System.Windows.Forms.Button();
            this.applyChangesButton = new System.Windows.Forms.Button();
            this.nameBox = new HaRepackerLib.ChangableTextbox();
            this.vectorPanel = new HaRepackerLib.XYPanel();
            this.mp3Player = new HaRepackerLib.Controls.SoundPlayer();
            this.textPropBox = new System.Windows.Forms.TextBox();
            this.pictureBoxPanel = new System.Windows.Forms.Panel();
            this.canvasPropBox = new System.Windows.Forms.PictureBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.selectionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.secondaryProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.findStrip = new System.Windows.Forms.ToolStrip();
            this.btnFindAll = new System.Windows.Forms.ToolStripButton();
            this.btnFindNext = new System.Windows.Forms.ToolStripButton();
            this.findBox = new System.Windows.Forms.ToolStripTextBox();
            this.btnRestart = new System.Windows.Forms.ToolStripButton();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnOptions = new System.Windows.Forms.ToolStripButton();
            this.MainDockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.pictureBoxPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvasPropBox)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.findStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainSplitContainer
            // 
            resources.ApplyResources(this.MainSplitContainer, "MainSplitContainer");
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            resources.ApplyResources(this.MainSplitContainer.Panel1, "MainSplitContainer.Panel1");
            this.MainSplitContainer.Panel1.Controls.Add(this.DataTree);
            // 
            // MainSplitContainer.Panel2
            // 
            resources.ApplyResources(this.MainSplitContainer.Panel2, "MainSplitContainer.Panel2");
            this.MainSplitContainer.Panel2.Controls.Add(this.saveSoundButton);
            this.MainSplitContainer.Panel2.Controls.Add(this.saveImageButton);
            this.MainSplitContainer.Panel2.Controls.Add(this.changeSoundButton);
            this.MainSplitContainer.Panel2.Controls.Add(this.changeImageButton);
            this.MainSplitContainer.Panel2.Controls.Add(this.applyChangesButton);
            this.MainSplitContainer.Panel2.Controls.Add(this.nameBox);
            this.MainSplitContainer.Panel2.Controls.Add(this.vectorPanel);
            this.MainSplitContainer.Panel2.Controls.Add(this.mp3Player);
            this.MainSplitContainer.Panel2.Controls.Add(this.textPropBox);
            this.MainSplitContainer.Panel2.Controls.Add(this.pictureBoxPanel);
            this.MainSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.MainSplitContainer_SplitterMoved);
            // 
            // DataTree
            // 
            resources.ApplyResources(this.DataTree, "DataTree");
            this.DataTree.Name = "DataTree";
            this.DataTree.SelectedNodes = ((System.Collections.ArrayList)(resources.GetObject("DataTree.SelectedNodes")));
            this.DataTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DataTree_AfterSelect);
            this.DataTree.DoubleClick += new System.EventHandler(this.DataTree_DoubleClick);
            this.DataTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataTree_KeyDown);
            // 
            // saveSoundButton
            // 
            resources.ApplyResources(this.saveSoundButton, "saveSoundButton");
            this.saveSoundButton.Name = "saveSoundButton";
            this.saveSoundButton.UseVisualStyleBackColor = true;
            this.saveSoundButton.Click += new System.EventHandler(this.saveSoundButton_Click);
            // 
            // saveImageButton
            // 
            resources.ApplyResources(this.saveImageButton, "saveImageButton");
            this.saveImageButton.Name = "saveImageButton";
            this.saveImageButton.UseVisualStyleBackColor = true;
            this.saveImageButton.Click += new System.EventHandler(this.saveImageButton_Click);
            // 
            // changeSoundButton
            // 
            resources.ApplyResources(this.changeSoundButton, "changeSoundButton");
            this.changeSoundButton.Name = "changeSoundButton";
            this.changeSoundButton.UseVisualStyleBackColor = true;
            this.changeSoundButton.Click += new System.EventHandler(this.changeSoundButton_Click);
            // 
            // changeImageButton
            // 
            resources.ApplyResources(this.changeImageButton, "changeImageButton");
            this.changeImageButton.Name = "changeImageButton";
            this.changeImageButton.UseVisualStyleBackColor = true;
            this.changeImageButton.Click += new System.EventHandler(this.changeImageButton_Click);
            // 
            // applyChangesButton
            // 
            resources.ApplyResources(this.applyChangesButton, "applyChangesButton");
            this.applyChangesButton.Name = "applyChangesButton";
            this.applyChangesButton.UseVisualStyleBackColor = true;
            this.applyChangesButton.Click += new System.EventHandler(this.applyChangesButton_Click);
            // 
            // nameBox
            // 
            resources.ApplyResources(this.nameBox, "nameBox");
            this.nameBox.ButtonEnabled = false;
            this.nameBox.Name = "nameBox";
            this.nameBox.ButtonClicked += new System.EventHandler(this.nameBox_ButtonClicked);
            // 
            // vectorPanel
            // 
            resources.ApplyResources(this.vectorPanel, "vectorPanel");
            this.vectorPanel.Name = "vectorPanel";
            this.vectorPanel.X = 0;
            this.vectorPanel.Y = 0;
            // 
            // mp3Player
            // 
            resources.ApplyResources(this.mp3Player, "mp3Player");
            this.mp3Player.Name = "mp3Player";
            this.mp3Player.SoundProperty = null;
            // 
            // textPropBox
            // 
            resources.ApplyResources(this.textPropBox, "textPropBox");
            this.textPropBox.Name = "textPropBox";
            // 
            // pictureBoxPanel
            // 
            resources.ApplyResources(this.pictureBoxPanel, "pictureBoxPanel");
            this.pictureBoxPanel.Controls.Add(this.canvasPropBox);
            this.pictureBoxPanel.Name = "pictureBoxPanel";
            // 
            // canvasPropBox
            // 
            resources.ApplyResources(this.canvasPropBox, "canvasPropBox");
            this.canvasPropBox.Name = "canvasPropBox";
            this.canvasPropBox.TabStop = false;
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectionLabel,
            this.mainProgressBar,
            this.secondaryProgressBar});
            this.statusStrip.Name = "statusStrip";
            // 
            // selectionLabel
            // 
            resources.ApplyResources(this.selectionLabel, "selectionLabel");
            this.selectionLabel.Name = "selectionLabel";
            // 
            // mainProgressBar
            // 
            resources.ApplyResources(this.mainProgressBar, "mainProgressBar");
            this.mainProgressBar.Name = "mainProgressBar";
            // 
            // secondaryProgressBar
            // 
            resources.ApplyResources(this.secondaryProgressBar, "secondaryProgressBar");
            this.secondaryProgressBar.Name = "secondaryProgressBar";
            // 
            // findStrip
            // 
            resources.ApplyResources(this.findStrip, "findStrip");
            this.findStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFindAll,
            this.btnFindNext,
            this.findBox,
            this.btnRestart,
            this.btnClose,
            this.btnOptions});
            this.findStrip.Name = "findStrip";
            this.findStrip.VisibleChanged += new System.EventHandler(this.findStrip_VisibleChanged);
            // 
            // btnFindAll
            // 
            resources.ApplyResources(this.btnFindAll, "btnFindAll");
            this.btnFindAll.Image = global::HaRepackerLib.Properties.Resources.find;
            this.btnFindAll.Name = "btnFindAll";
            this.btnFindAll.Click += new System.EventHandler(this.btnFindAll_Click);
            // 
            // btnFindNext
            // 
            resources.ApplyResources(this.btnFindNext, "btnFindNext");
            this.btnFindNext.Image = global::HaRepackerLib.Properties.Resources.arrow_right;
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // findBox
            // 
            resources.ApplyResources(this.findBox, "findBox");
            this.findBox.Name = "findBox";
            this.findBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.findBox_KeyDown);
            this.findBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.findBox_KeyPress);
            this.findBox.TextChanged += new System.EventHandler(this.findBox_TextChanged);
            // 
            // btnRestart
            // 
            resources.ApplyResources(this.btnRestart, "btnRestart");
            this.btnRestart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRestart.Image = global::HaRepackerLib.Properties.Resources.undo;
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClose.Image = global::HaRepackerLib.Properties.Resources.red_x1;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOptions
            // 
            resources.ApplyResources(this.btnOptions, "btnOptions");
            this.btnOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // MainDockPanel
            // 
            resources.ApplyResources(this.MainDockPanel, "MainDockPanel");
            this.MainDockPanel.ActiveAutoHideContent = null;
            this.MainDockPanel.DockBackColor = System.Drawing.SystemColors.Control;
            this.MainDockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.MainDockPanel.Name = "MainDockPanel";
            dockPanelGradient13.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient13.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin5.DockStripGradient = dockPanelGradient13;
            tabGradient29.EndColor = System.Drawing.SystemColors.Control;
            tabGradient29.StartColor = System.Drawing.SystemColors.Control;
            tabGradient29.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin5.TabGradient = tabGradient29;
            autoHideStripSkin5.TextFont = new System.Drawing.Font("Segoe UI", 9F);
            dockPanelSkin5.AutoHideStripSkin = autoHideStripSkin5;
            tabGradient30.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient30.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient30.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient5.ActiveTabGradient = tabGradient30;
            dockPanelGradient14.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient14.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient5.DockStripGradient = dockPanelGradient14;
            tabGradient31.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient31.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient31.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient5.InactiveTabGradient = tabGradient31;
            dockPaneStripSkin5.DocumentGradient = dockPaneStripGradient5;
            dockPaneStripSkin5.TextFont = new System.Drawing.Font("Segoe UI", 9F);
            tabGradient32.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient32.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient32.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient32.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient5.ActiveCaptionGradient = tabGradient32;
            tabGradient33.EndColor = System.Drawing.SystemColors.Control;
            tabGradient33.StartColor = System.Drawing.SystemColors.Control;
            tabGradient33.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient5.ActiveTabGradient = tabGradient33;
            dockPanelGradient15.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient15.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient5.DockStripGradient = dockPanelGradient15;
            tabGradient34.EndColor = System.Drawing.SystemColors.InactiveCaption;
            tabGradient34.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient34.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient34.TextColor = System.Drawing.SystemColors.InactiveCaptionText;
            dockPaneStripToolWindowGradient5.InactiveCaptionGradient = tabGradient34;
            tabGradient35.EndColor = System.Drawing.Color.Transparent;
            tabGradient35.StartColor = System.Drawing.Color.Transparent;
            tabGradient35.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient5.InactiveTabGradient = tabGradient35;
            dockPaneStripSkin5.ToolWindowGradient = dockPaneStripToolWindowGradient5;
            dockPanelSkin5.DockPaneStripSkin = dockPaneStripSkin5;
            this.MainDockPanel.Skin = dockPanelSkin5;
            // 
            // HaRepackerMainPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.findStrip);
            this.Controls.Add(this.MainDockPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.MainSplitContainer);
            this.Name = "HaRepackerMainPanel";
            this.SizeChanged += new System.EventHandler(this.HaRepackerMainPanel_SizeChanged);
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            this.MainSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.pictureBoxPanel.ResumeLayout(false);
            this.pictureBoxPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvasPropBox)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.findStrip.ResumeLayout(false);
            this.findStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.PictureBox canvasPropBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel selectionLabel;
        private System.Windows.Forms.Panel pictureBoxPanel;
        private System.Windows.Forms.TextBox textPropBox;
        private SoundPlayer mp3Player;
        public System.Windows.Forms.ToolStripProgressBar secondaryProgressBar;
        public System.Windows.Forms.ToolStripProgressBar mainProgressBar;
        private ChangableTextbox nameBox;
        private XYPanel vectorPanel;
        private System.Windows.Forms.Button saveImageButton;
        private System.Windows.Forms.Button changeSoundButton;
        private System.Windows.Forms.Button changeImageButton;
        private System.Windows.Forms.Button applyChangesButton;
        private System.Windows.Forms.Button saveSoundButton;
        private System.Windows.Forms.ToolStripButton btnFindNext;
        private System.Windows.Forms.ToolStripTextBox findBox;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStripButton btnRestart;
        private System.Windows.Forms.ToolStripButton btnFindAll;
        private System.Windows.Forms.ToolStripButton btnOptions;
        public System.Windows.Forms.ToolStrip findStrip;
        public TreeViewMS.TreeViewMS DataTree;
        private WeifenLuo.WinFormsUI.Docking.DockPanel MainDockPanel;
    }
}
