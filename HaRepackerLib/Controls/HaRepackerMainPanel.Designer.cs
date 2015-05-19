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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HaRepackerMainPanel));
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.saveSoundButton = new System.Windows.Forms.Button();
            this.saveImageButton = new System.Windows.Forms.Button();
            this.changeSoundButton = new System.Windows.Forms.Button();
            this.changeImageButton = new System.Windows.Forms.Button();
            this.applyChangesButton = new System.Windows.Forms.Button();
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
            this.DataTree = new TreeViewMS.TreeViewMS();
            this.nameBox = new HaRepackerLib.ChangableTextbox();
            this.vectorPanel = new HaRepackerLib.XYPanel();
            this.mp3Player = new HaRepackerLib.Controls.SoundPlayer();
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
            this.MainSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.DataTree);
            // 
            // MainSplitContainer.Panel2
            // 
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
            this.MainSplitContainer.Size = new System.Drawing.Size(657, 330);
            this.MainSplitContainer.SplitterDistance = 218;
            this.MainSplitContainer.TabIndex = 0;
            this.MainSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.MainSplitContainer_SplitterMoved);
            // 
            // saveSoundButton
            // 
            this.saveSoundButton.Location = new System.Drawing.Point(45, 178);
            this.saveSoundButton.Name = "saveSoundButton";
            this.saveSoundButton.Size = new System.Drawing.Size(118, 34);
            this.saveSoundButton.TabIndex = 12;
            this.saveSoundButton.Text = "保存音樂...";
            this.saveSoundButton.UseVisualStyleBackColor = true;
            this.saveSoundButton.Visible = false;
            this.saveSoundButton.Click += new System.EventHandler(this.saveSoundButton_Click);
            // 
            // saveImageButton
            // 
            this.saveImageButton.Location = new System.Drawing.Point(17, 218);
            this.saveImageButton.Name = "saveImageButton";
            this.saveImageButton.Size = new System.Drawing.Size(118, 34);
            this.saveImageButton.TabIndex = 11;
            this.saveImageButton.Text = "保存圖片...";
            this.saveImageButton.UseVisualStyleBackColor = true;
            this.saveImageButton.Visible = false;
            this.saveImageButton.Click += new System.EventHandler(this.saveImageButton_Click);
            // 
            // changeSoundButton
            // 
            this.changeSoundButton.Location = new System.Drawing.Point(71, 240);
            this.changeSoundButton.Name = "changeSoundButton";
            this.changeSoundButton.Size = new System.Drawing.Size(118, 34);
            this.changeSoundButton.TabIndex = 10;
            this.changeSoundButton.Text = "更變音樂...";
            this.changeSoundButton.UseVisualStyleBackColor = true;
            this.changeSoundButton.Visible = false;
            this.changeSoundButton.Click += new System.EventHandler(this.changeSoundButton_Click);
            // 
            // changeImageButton
            // 
            this.changeImageButton.Location = new System.Drawing.Point(71, 138);
            this.changeImageButton.Name = "changeImageButton";
            this.changeImageButton.Size = new System.Drawing.Size(118, 34);
            this.changeImageButton.TabIndex = 9;
            this.changeImageButton.Text = "更變圖片...";
            this.changeImageButton.UseVisualStyleBackColor = true;
            this.changeImageButton.Visible = false;
            this.changeImageButton.Click += new System.EventHandler(this.changeImageButton_Click);
            // 
            // applyChangesButton
            // 
            this.applyChangesButton.Location = new System.Drawing.Point(3, 269);
            this.applyChangesButton.Name = "applyChangesButton";
            this.applyChangesButton.Size = new System.Drawing.Size(118, 34);
            this.applyChangesButton.TabIndex = 8;
            this.applyChangesButton.Text = "應用更變值";
            this.applyChangesButton.UseVisualStyleBackColor = true;
            this.applyChangesButton.Visible = false;
            this.applyChangesButton.Click += new System.EventHandler(this.applyChangesButton_Click);
            // 
            // textPropBox
            // 
            this.textPropBox.Location = new System.Drawing.Point(45, 150);
            this.textPropBox.Multiline = true;
            this.textPropBox.Name = "textPropBox";
            this.textPropBox.Size = new System.Drawing.Size(144, 124);
            this.textPropBox.TabIndex = 2;
            this.textPropBox.Visible = false;
            // 
            // pictureBoxPanel
            // 
            this.pictureBoxPanel.AutoScroll = true;
            this.pictureBoxPanel.Controls.Add(this.canvasPropBox);
            this.pictureBoxPanel.Location = new System.Drawing.Point(195, 108);
            this.pictureBoxPanel.Name = "pictureBoxPanel";
            this.pictureBoxPanel.Size = new System.Drawing.Size(199, 176);
            this.pictureBoxPanel.TabIndex = 1;
            this.pictureBoxPanel.Visible = false;
            // 
            // canvasPropBox
            // 
            this.canvasPropBox.Location = new System.Drawing.Point(7, 26);
            this.canvasPropBox.Name = "canvasPropBox";
            this.canvasPropBox.Size = new System.Drawing.Size(189, 118);
            this.canvasPropBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.canvasPropBox.TabIndex = 0;
            this.canvasPropBox.TabStop = false;
            this.canvasPropBox.Visible = false;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectionLabel,
            this.mainProgressBar,
            this.secondaryProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 434);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(663, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // selectionLabel
            // 
            this.selectionLabel.Name = "selectionLabel";
            this.selectionLabel.Size = new System.Drawing.Size(119, 17);
            this.selectionLabel.Text = "選中類型: 沒有";
            // 
            // mainProgressBar
            // 
            this.mainProgressBar.Name = "mainProgressBar";
            this.mainProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // secondaryProgressBar
            // 
            this.secondaryProgressBar.Name = "secondaryProgressBar";
            this.secondaryProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // findStrip
            // 
            this.findStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.findStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFindAll,
            this.btnFindNext,
            this.findBox,
            this.btnRestart,
            this.btnClose,
            this.btnOptions});
            this.findStrip.Location = new System.Drawing.Point(0, 409);
            this.findStrip.Name = "findStrip";
            this.findStrip.Size = new System.Drawing.Size(663, 25);
            this.findStrip.TabIndex = 2;
            this.findStrip.Text = "toolStrip1";
            this.findStrip.Visible = false;
            this.findStrip.VisibleChanged += new System.EventHandler(this.findStrip_VisibleChanged);
            // 
            // btnFindAll
            // 
            this.btnFindAll.Image = global::HaRepackerLib.Properties.Resources.find;
            this.btnFindAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFindAll.Name = "btnFindAll";
            this.btnFindAll.Size = new System.Drawing.Size(41, 22);
            this.btnFindAll.Text = "全部";
            this.btnFindAll.Click += new System.EventHandler(this.btnFindAll_Click);
            // 
            // btnFindNext
            // 
            this.btnFindNext.Image = global::HaRepackerLib.Properties.Resources.arrow_right;
            this.btnFindNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(51, 22);
            this.btnFindNext.Text = "下一個";
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // findBox
            // 
            this.findBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.findBox.Name = "findBox";
            this.findBox.Size = new System.Drawing.Size(100, 25);
            this.findBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.findBox_KeyDown);
            this.findBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.findBox_KeyPress);
            this.findBox.TextChanged += new System.EventHandler(this.findBox_TextChanged);
            // 
            // btnRestart
            // 
            this.btnRestart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRestart.Image = global::HaRepackerLib.Properties.Resources.undo;
            this.btnRestart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(23, 22);
            this.btnRestart.Text = "從新開始";
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnClose
            // 
            this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClose.Image = global::HaRepackerLib.Properties.Resources.red_x1;
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 22);
            this.btnClose.Text = "關閉";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOptions.Image")));
            this.btnOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(53, 22);
            this.btnOptions.Text = "選項";
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // MainDockPanel
            // 
            this.MainDockPanel.ActiveAutoHideContent = null;
            this.MainDockPanel.DockBackColor = System.Drawing.SystemColors.Control;
            this.MainDockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.MainDockPanel.Location = new System.Drawing.Point(135, 349);
            this.MainDockPanel.Name = "MainDockPanel";
            this.MainDockPanel.Size = new System.Drawing.Size(225, 56);
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
            this.MainDockPanel.TabIndex = 3;
            // 
            // DataTree
            // 
            this.DataTree.Location = new System.Drawing.Point(53, 59);
            this.DataTree.Name = "DataTree";
            this.DataTree.SelectedNodes = ((System.Collections.ArrayList)(resources.GetObject("DataTree.SelectedNodes")));
            this.DataTree.Size = new System.Drawing.Size(108, 193);
            this.DataTree.TabIndex = 1;
            this.DataTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DataTree_AfterSelect);
            this.DataTree.DoubleClick += new System.EventHandler(this.DataTree_DoubleClick);
            this.DataTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataTree_KeyDown);
            // 
            // nameBox
            // 
            this.nameBox.ButtonEnabled = false;
            this.nameBox.Location = new System.Drawing.Point(3, 83);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(306, 20);
            this.nameBox.TabIndex = 7;
            this.nameBox.ButtonClicked += new System.EventHandler(this.nameBox_ButtonClicked);
            // 
            // vectorPanel
            // 
            this.vectorPanel.Location = new System.Drawing.Point(71, 91);
            this.vectorPanel.MaximumSize = new System.Drawing.Size(90, 53);
            this.vectorPanel.MinimumSize = new System.Drawing.Size(90, 53);
            this.vectorPanel.Name = "vectorPanel";
            this.vectorPanel.Size = new System.Drawing.Size(90, 53);
            this.vectorPanel.TabIndex = 6;
            this.vectorPanel.Visible = false;
            this.vectorPanel.X = 0;
            this.vectorPanel.Y = 0;
            // 
            // mp3Player
            // 
            this.mp3Player.Location = new System.Drawing.Point(45, 16);
            this.mp3Player.MaximumSize = new System.Drawing.Size(310, 86);
            this.mp3Player.MinimumSize = new System.Drawing.Size(310, 86);
            this.mp3Player.Name = "mp3Player";
            this.mp3Player.Size = new System.Drawing.Size(310, 86);
            this.mp3Player.SoundProperty = null;
            this.mp3Player.TabIndex = 3;
            this.mp3Player.Visible = false;
            // 
            // HaRepackerMainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.findStrip);
            this.Controls.Add(this.MainDockPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.MainSplitContainer);
            this.Name = "HaRepackerMainPanel";
            this.Size = new System.Drawing.Size(663, 456);
            this.SizeChanged += new System.EventHandler(this.HaRepackerMainPanel_SizeChanged);
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            this.MainSplitContainer.Panel2.PerformLayout();
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
