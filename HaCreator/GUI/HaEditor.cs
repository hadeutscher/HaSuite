/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using HaCreator.CustomControls;
using HaCreator.CustomControls.EditorPanels;
using HaCreator.MapEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI;
using WeifenLuo.WinFormsUI.Docking;

namespace HaCreator.GUI
{
    public partial class HaEditor : Form
    {
        private InputHandler handler;
        private HaCreatorStateManager hcsm;
        private DockInflateHelper dih;

        private TilePanel tilePanel;
        private ObjPanel objPanel;
        private LifePanel lifePanel;
        private PortalPanel portalPanel;
        private BackgroundPanel bgPanel;
        private CommonPanel commonPanel;

        public HaEditor()
        {
            InitializeComponent();
            InitializeComponentCustom();
            RedockControls();
        }

        private void InitializeComponentCustom()
        {
            // make dockPanel everyone's parent
            wpfHost.Parent = dockPanel;
            tabs.Parent = dockPanel;
            dockPanel.Parent = this;

            // helper classes
            dih = new DockInflateHelper(this.dockPanel);
            dih.RedockRequired += RedockControls;
            handler = new InputHandler(multiBoard);
            hcsm = new HaCreatorStateManager(multiBoard, ribbon, tabs);
            hcsm.SelectedItemChanged += hcsm_SelectedItemChanged;
            hcsm.CloseRequested += hcsm_CloseRequested;
            hcsm.FirstMapLoaded += hcsm_FirstMapLoaded;

        }

        void hcsm_FirstMapLoaded()
        {
            tilePanel.Enabled = true;
            objPanel.Enabled = true;
            lifePanel.Enabled = true;
            portalPanel.Enabled = true;
            bgPanel.Enabled = true;
            commonPanel.Enabled = true;
            WindowState = FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HaEditor_FormClosing);
        }

        void hcsm_CloseRequested()
        {
            Close();
        }

        void hcsm_SelectedItemChanged(string desc)
        {
            
        }

        private void HaEditor_Load(object sender, EventArgs e)
        {
            // This has to be here and not in .ctor for some reason, otherwise subwindows are not locating properly
            tilePanel = new TilePanel(hcsm) { Enabled = false };
            objPanel = new ObjPanel(hcsm) { Enabled = false };
            lifePanel = new LifePanel(hcsm) { Enabled = false };
            portalPanel = new PortalPanel(hcsm) { Enabled = false };
            bgPanel = new BackgroundPanel(hcsm) { Enabled = false };
            commonPanel = new CommonPanel(hcsm) { Enabled = false };
            dih.AddContent(tilePanel, DockState.DockRight);
            dih.AddContent(objPanel, DockState.DockRight);
            dih.AddContent(lifePanel, DockState.DockRight);
            dih.AddContent(portalPanel, DockState.DockRight);
            dih.AddContent(bgPanel, DockState.DockRight);
            dih.AddContent(commonPanel, DockState.DockRight);

            commonPanel.Pane = bgPanel.Pane = portalPanel.Pane = lifePanel.Pane = objPanel.Pane = tilePanel.Pane;

            hcsm.LoadMap();
        }

        private void RedockControls()
        {
            Rectangle baseArea = dih.GetActiveRectangle();
            int ribbonHeight = (int)ribbon.ribbon.ActualHeight - ribbon.reducedHeight;

            wpfHost.Location = new Point(baseArea.Left, baseArea.Top);
            wpfHost.Size = new Size(baseArea.Width, ribbonHeight);
            tabs.Location = new Point(baseArea.Left + tabs.Margin.Left, ribbonHeight + tabs.Margin.Top);
            tabs.Size = new Size(baseArea.Width - tabs.Margin.Left - tabs.Margin.Right, baseArea.Height - ribbonHeight - tabs.Margin.Top - tabs.Margin.Bottom);
        }

        private void HaEditor_SizeChanged(object sender, EventArgs e)
        {
            RedockControls();
        }

        private void tabs_PageClosing(HaCreator.ThirdParty.TabPages.TabPage page, ref bool cancel)
        {
            if (MessageBox.Show("Are you sure you want to close this map?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                cancel = true;
        }

        private void HaEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void HaEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            multiBoard.Stop();
        }
    }
}
