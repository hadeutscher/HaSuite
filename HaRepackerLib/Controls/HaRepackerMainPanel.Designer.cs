﻿namespace HaRepackerLib.Controls
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
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
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
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            autoHideStripSkin1.TextFont = new System.Drawing.Font("Segoe UI", 9F);
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            dockPaneStripSkin1.TextFont = new System.Drawing.Font("Segoe UI", 9F);
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.InactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.InactiveCaptionText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.MainDockPanel.Skin = dockPanelSkin1;
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
