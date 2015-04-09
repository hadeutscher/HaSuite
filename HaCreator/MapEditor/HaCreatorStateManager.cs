/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using HaCreator.CustomControls;
using HaCreator.CustomControls.EditorPanels;
using HaCreator.GUI;
using HaCreator.GUI.InstanceEditor;
using MapleLib.Helpers;
using MapleLib.WzLib.WzStructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HaCreator.ThirdParty.TabPages;
using MapleLib.WzLib;
using HaCreator.WzStructure;

namespace HaCreator.MapEditor
{
    public class HaCreatorStateManager
    {
        MultiBoard multiBoard;
        HaRibbon ribbon;
        PageCollection tabs;
        TilePanel tilePanel;

        public HaCreatorStateManager(MultiBoard multiBoard, HaRibbon ribbon, PageCollection tabs)
        {
            this.multiBoard = multiBoard;
            this.ribbon = ribbon;
            this.tabs = tabs;

            this.ribbon.OpenClicked += ribbon_OpenClicked;
            this.ribbon.SaveClicked += ribbon_SaveClicked;
            this.ribbon.RepackClicked += ribbon_RepackClicked;
            this.ribbon.AboutClicked += ribbon_AboutClicked;
            this.ribbon.HelpClicked += ribbon_HelpClicked;
            this.ribbon.SettingsClicked += ribbon_SettingsClicked;
            this.ribbon.ExitClicked += ribbon_ExitClicked;
            this.ribbon.ViewToggled += ribbon_ViewToggled;
            this.ribbon.ShowVRToggled += ribbon_ShowVRToggled;
            this.ribbon.ShowMinimapToggled += ribbon_ShowMinimapToggled;
            this.ribbon.ParallaxToggled += ribbon_ParallaxToggled;
            this.ribbon.LayerViewChanged += ribbon_LayerViewChanged;
            this.ribbon.MapSimulationClicked += ribbon_MapSimulationClicked;
            this.ribbon.RegenerateMinimapClicked += ribbon_RegenerateMinimapClicked;
            this.ribbon.SnappingToggled += ribbon_SnappingToggled;
            this.ribbon.RandomTilesToggled += ribbon_RandomTilesToggled;
            this.ribbon.InfoModeToggled += ribbon_InfoModeToggled;
            this.ribbon.HaRepackerClicked += ribbon_HaRepackerClicked;
            this.ribbon.FinalizeClicked += ribbon_FinalizeClicked;
            this.ribbon.NewPlatformClicked += ribbon_NewPlatformClicked;

            this.tabs.CurrentPageChanged += tabs_CurrentPageChanged;

            this.multiBoard.OnBringToFrontClicked += multiBoard_OnBringToFrontClicked;
            this.multiBoard.OnEditBaseClicked += multiBoard_OnEditBaseClicked;
            this.multiBoard.OnEditInstanceClicked += multiBoard_OnEditInstanceClicked;
            this.multiBoard.OnLayerTSChanged += multiBoard_OnLayerTSChanged;
            this.multiBoard.OnSendToBackClicked += multiBoard_OnSendToBackClicked;
            this.multiBoard.ReturnToSelectionState += multiBoard_ReturnToSelectionState;
            this.multiBoard.SelectedItemChanged += multiBoard_SelectedItemChanged;
            this.multiBoard.MouseMoved += multiBoard_MouseMoved;
            this.multiBoard.ImageDropped += multiBoard_ImageDropped;

            multiBoard.Visible = false;
            ribbon.SetEnabled(false);
        }


        #region MultiBoard Events
        void multiBoard_ImageDropped(Board selectedBoard, System.Drawing.Bitmap bmp, Microsoft.Xna.Framework.Point pos)
        {
            ObjectInfo oi = new ObjectInfo(bmp, new System.Drawing.Point(bmp.Width / 2, bmp.Height / 2), "", "", "", "", null);
            selectedBoard.BoardItems.Add(oi.CreateInstance(selectedBoard.SelectedLayer, selectedBoard, pos.X, pos.Y, 0, true), true);
        }

        void multiBoard_MouseMoved(Board selectedBoard, Microsoft.Xna.Framework.Point oldPos, Microsoft.Xna.Framework.Point newPos, Microsoft.Xna.Framework.Point currPhysicalPos)
        {
            ribbon.SetMousePos(newPos.X, newPos.Y, currPhysicalPos.X, currPhysicalPos.Y);
        }

        void multiBoard_SelectedItemChanged(BoardItem selectedItem)
        {
            if (selectedItem != null)
            {
                ribbon.SetItemDesc(CreateItemDescription(selectedItem, "\n"));
            }
            else
            {
                ribbon.SetItemDesc("");
            }
        }

        void multiBoard_ReturnToSelectionState()
        {
            // No need to lock because SelectionMode() and ExitEditMode() are both thread-safe
            multiBoard.SelectedBoard.Mouse.SelectionMode();
            ExitEditMode();
            multiBoard.Focus();
        }

        void multiBoard_OnSendToBackClicked(BoardItem boardRefItem)
        {
            lock (multiBoard)
            {
                foreach (BoardItem item in boardRefItem.Board.SelectedItems)
                {
                    if (item.Z > 0)
                    {
                        item.Board.UndoRedoMan.AddUndoBatch(new List<UndoRedoAction> { UndoRedoManager.ItemZChanged(item, item.Z, 0) });
                        item.Z = 0;
                    }
                }
                boardRefItem.Board.BoardItems.Sort();
            }
            multiBoard.Focus();
        }

        void multiBoard_OnLayerTSChanged(Layer layer)
        {
            ribbon.SetLayer(layer);
        }

        void multiBoard_OnEditInstanceClicked(BoardItem item)
        {
            InputHandler.ClearBoundItems(multiBoard.SelectedBoard);
            switch (item.GetType().Name)
            {
                case "ObjectInstance":
                    new ObjectInstanceEditor((ObjectInstance)item).ShowDialog();
                    break;
                case "TileInstance":
                    new TileInstanceEditor((TileInstance)item).ShowDialog();
                    break;
                case "Chair":
                    new GeneralInstanceEditor(item).ShowDialog();
                    break;
                case "FootholdAnchor":
                    FootholdLine[] selectedFootholds = FootholdLine.GetSelectedFootholds(item.Board);
                    if (selectedFootholds.Length > 0)
                    {
                        new FootholdEditor(selectedFootholds).ShowDialog();
                    }
                    else
                    {
                        new GeneralInstanceEditor(item).ShowDialog();
                    }
                    break;
                case "RopeAnchor":
                    new RopeInstanceEditor((RopeAnchor)item).ShowDialog();
                    break;
                case "NPCInstance":
                case "MobInstance":
                    new LifeInstanceEditor((LifeInstance)item).ShowDialog();
                    break;
                case "ReactorInstance":
                    new ReactorInstanceEditor((ReactorInstance)item).ShowDialog();
                    break;
                case "BackgroundInstance":
                    new BackgroundInstanceEditor((BackgroundInstance)item).ShowDialog();
                    break;
                case "PortalInstance":
                    new PortalInstanceEditor((PortalInstance)item).ShowDialog();
                    break;
                case "ToolTip":
                    new TooltipInstanceEditor((ToolTip)item).ShowDialog();
                    break;
                default:
                    break;
            }
        }

        void multiBoard_OnEditBaseClicked(BoardItem item)
        {
            //TODO
        }

        void multiBoard_OnBringToFrontClicked(BoardItem boardRefItem)
        {
            lock (multiBoard)
            {
                foreach (BoardItem item in boardRefItem.Board.SelectedItems)
                {
                    int oldZ = item.Z;
                    if (item is BackgroundInstance)
                    {
                        IList list = ((BackgroundInstance)item).front ? multiBoard.SelectedBoard.BoardItems.FrontBackgrounds : multiBoard.SelectedBoard.BoardItems.BackBackgrounds;
                        int highestZ = 0;
                        foreach (BackgroundInstance bg in list)
                            if (bg.Z > highestZ)
                                highestZ = bg.Z;
                        item.Z = highestZ + 1;
                    }
                    else
                    {
                        int highestZ = 0;
                        foreach (LayeredItem layeredItem in multiBoard.SelectedBoard.BoardItems.TileObjs)
                            if (layeredItem.Z > highestZ) highestZ = layeredItem.Z;
                        item.Z = highestZ + 1;
                    }
                    if (item.Z != oldZ)
                        item.Board.UndoRedoMan.AddUndoBatch(new List<UndoRedoAction> { UndoRedoManager.ItemZChanged(item, oldZ, item.Z) });
                }
            }
            boardRefItem.Board.BoardItems.Sort();
        }
        #endregion

        #region Tab Events
        private void mapEditInfo(object sender, EventArgs e)
        {
            Board selectedBoard = (Board)((ToolStripMenuItem)sender).Tag;
            new InfoEditor(selectedBoard.MapInfo, multiBoard).ShowDialog();
        }

        void tabs_CurrentPageChanged(HaCreator.ThirdParty.TabPages.TabPage currentPage, HaCreator.ThirdParty.TabPages.TabPage previousPage)
        {
            if (previousPage == null)
            {
                return;
            }
            lock (multiBoard)
            {
                multiBoard_ReturnToSelectionState();
                multiBoard.SelectedBoard = (Board)currentPage.Tag;
                ApplicationSettings.lastDefaultLayer = multiBoard.SelectedBoard.SelectedLayerIndex;
                ribbon.SetLayers(multiBoard.SelectedBoard.Layers);
                ribbon.SetSelectedLayer(multiBoard.SelectedBoard.SelectedLayerIndex, multiBoard.SelectedBoard.SelectedPlatform);
                ParseVisibleEditedTypes();
                multiBoard.Focus();
            }
        }
        #endregion

        #region Ribbon Handlers
        void ribbon_FinalizeClicked()
        {
            if (MessageBox.Show("This will finalize all footholds, removing their Tile bindings and clearing the Undo/Redo list in the process.\r\nContinue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                lock (multiBoard)
                {
                    new MapSaver(multiBoard.SelectedBoard).ActualizeFootholds();
                }
            }
        }

        void ribbon_HaRepackerClicked()
        {
            HaRepacker.Program.WzMan = new HaRepackerLib.WzFileManager();
            bool firstRun = HaRepacker.Program.PrepareApplication(false);
            HaRepacker.GUI.MainForm mf = new HaRepacker.GUI.MainForm(null, false, firstRun);
            mf.unloadAllToolStripMenuItem.Visible = false;
            mf.reloadAllToolStripMenuItem.Visible = false;
            foreach (DictionaryEntry entry in Program.WzManager.wzFiles)
                mf.Interop_AddLoadedWzFileToManager((WzFile)entry.Value);
            lock (multiBoard)
            {

                mf.ShowDialog();
            }
            HaRepacker.Program.EndApplication(false, false);
        }
        
        bool? getTypes(ItemTypes visibleTypes, ItemTypes editedTypes, ItemTypes type)
        {
            if ((editedTypes & type) == type)
            {
                return true;
            }
            else if ((visibleTypes & type) == type)
            {
                return (bool?)null;
            }
            else
            {
                return false;
            }
        }

        private void ParseVisibleEditedTypes()
        {
            ItemTypes visibleTypes = ApplicationSettings.theoreticalVisibleTypes = multiBoard.SelectedBoard.VisibleTypes;
            ItemTypes editedTypes = ApplicationSettings.theoreticalEditedTypes = multiBoard.SelectedBoard.EditedTypes;
            ribbon.SetVisibilityCheckboxes(getTypes(visibleTypes, editedTypes, ItemTypes.Tiles),
                                            getTypes(visibleTypes, editedTypes, ItemTypes.Objects),
                                            getTypes(visibleTypes, editedTypes, ItemTypes.NPCs),
                                            getTypes(visibleTypes, editedTypes, ItemTypes.Mobs),
                                            getTypes(visibleTypes, editedTypes, ItemTypes.Reactors),
                                            getTypes(visibleTypes, editedTypes, ItemTypes.Portals),
                                            getTypes(visibleTypes, editedTypes, ItemTypes.Footholds),
                                            getTypes(visibleTypes, editedTypes, ItemTypes.Ropes),
                                            getTypes(visibleTypes, editedTypes, ItemTypes.Chairs),
                                            getTypes(visibleTypes, editedTypes, ItemTypes.ToolTips),
                                            getTypes(visibleTypes, editedTypes, ItemTypes.Backgrounds),
                                            getTypes(visibleTypes, editedTypes, ItemTypes.Misc));
        }
        
        void ribbon_RandomTilesToggled(bool pressed)
        {
            ApplicationSettings.randomTiles = pressed;
            if (tilePanel != null)
                tilePanel.LoadTileSetList();
        }

        void ribbon_SnappingToggled(bool pressed)
        {
            UserSettings.useSnapping = pressed;
        }

        void ribbon_InfoModeToggled(bool pressed)
        {
            ApplicationSettings.InfoMode = pressed;
        }

        void ribbon_RegenerateMinimapClicked()
        {
            if (multiBoard.SelectedBoard.RegenerateMinimap())
                MessageBox.Show("Minimap regenerated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                MessageBox.Show("An error occured during minimap regeneration. The error has been logged. If possible, save the map and send it to" + ApplicationSettings.AuthorEmail, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLogger.Log(ErrorLevel.Critical, "error regenning minimap for map " + multiBoard.SelectedBoard.MapInfo.id.ToString());
            }
        }

        void ribbon_MapSimulationClicked()
        {
            multiBoard.DeviceReady = false;
            MapSimulator.MapSimulator.CreateMapSimulator(multiBoard.SelectedBoard).ShowDialog();
            multiBoard.DeviceReady = true;
        }

        void ribbon_ParallaxToggled(bool pressed)
        {
            UserSettings.emulateParallax = pressed;
        }

        void ribbon_ShowMinimapToggled(bool pressed)
        {
            UserSettings.useMiniMap = pressed;
        }

        void ribbon_ShowVRToggled(bool pressed)
        {
            UserSettings.showVR = pressed;
        }

        void setTypes(ref ItemTypes newVisibleTypes, ref ItemTypes newEditedTypes, bool? x, ItemTypes type)
        {
            if (x.HasValue)
            {
                if (x.Value)
                {
                    newVisibleTypes ^= type;
                    newEditedTypes ^= type;
                }
            }
            else
            {
                newVisibleTypes ^= type;
            }
        }

        void ribbon_ViewToggled(bool? tiles, bool? objs, bool? npcs, bool? mobs, bool? reactors, bool? portals, bool? footholds, bool? ropes, bool? chairs, bool? tooltips, bool? backgrounds, bool? misc)
        {
            lock (multiBoard)
            {
                ItemTypes newVisibleTypes = 0;
                ItemTypes newEditedTypes = 0;
                setTypes(ref newVisibleTypes, ref newEditedTypes, tiles, ItemTypes.Tiles);
                setTypes(ref newVisibleTypes, ref newEditedTypes, objs, ItemTypes.Objects);
                setTypes(ref newVisibleTypes, ref newEditedTypes, npcs, ItemTypes.NPCs);
                setTypes(ref newVisibleTypes, ref newEditedTypes, mobs, ItemTypes.Mobs);
                setTypes(ref newVisibleTypes, ref newEditedTypes, reactors, ItemTypes.Reactors);
                setTypes(ref newVisibleTypes, ref newEditedTypes, portals, ItemTypes.Portals);
                setTypes(ref newVisibleTypes, ref newEditedTypes, footholds, ItemTypes.Footholds);
                setTypes(ref newVisibleTypes, ref newEditedTypes, ropes, ItemTypes.Ropes);
                setTypes(ref newVisibleTypes, ref newEditedTypes, chairs, ItemTypes.Chairs);
                setTypes(ref newVisibleTypes, ref newEditedTypes, tooltips, ItemTypes.ToolTips);
                setTypes(ref newVisibleTypes, ref newEditedTypes, backgrounds, ItemTypes.Backgrounds);
                setTypes(ref newVisibleTypes, ref newEditedTypes, misc, ItemTypes.Misc);
                ApplicationSettings.theoreticalVisibleTypes = newVisibleTypes;
                ApplicationSettings.theoreticalEditedTypes = newEditedTypes;
                if (multiBoard.SelectedBoard != null)
                {
                    InputHandler.ClearSelectedItems(multiBoard.SelectedBoard);
                    multiBoard.SelectedBoard.VisibleTypes = newVisibleTypes;
                    multiBoard.SelectedBoard.EditedTypes = newEditedTypes;
                }
            }
        }

        void ribbon_ExitClicked()
        {
            if (CloseRequested != null)
            {
                CloseRequested.Invoke();
            }
        }

        void ribbon_SettingsClicked()
        {
            new UserSettingsForm().ShowDialog();
        }

        void ribbon_HelpClicked()
        {
            string helpPath = Path.Combine(Application.StartupPath, "Help.htm");
            if (File.Exists(helpPath))
                Process.Start(helpPath);
            else
                HaRepackerLib.Warning.Error("Help could not be shown because the help file (HRHelp.htm) was not found");
        }

        void ribbon_AboutClicked()
        {
            new About().ShowDialog();
        }

        void ribbon_RepackClicked()
        {
            lock (multiBoard)
            {
                Repack r = new Repack();
                r.ShowDialog();
                if (Program.Restarting)
                {
                    multiBoard.Stop();
                }
            }
            if (Program.Restarting && CloseRequested != null)
            {
                CloseRequested.Invoke();
            }
        }

        void ribbon_SaveClicked()
        {
            lock(multiBoard)
            {
                new Save(multiBoard.SelectedBoard).ShowDialog();
            }
        }

        void ribbon_OpenClicked()
        {
            LoadMap();
        }

        public void LoadMap()
        {
            if (new Load(multiBoard, tabs, new EventHandler(mapEditInfo)).ShowDialog() == DialogResult.OK)
            {
                if (!multiBoard.DeviceReady)
                {
                    ribbon.SetEnabled(true);
                    ribbon.SetOptions(UserSettings.showVR, UserSettings.useMiniMap, UserSettings.emulateParallax, UserSettings.useSnapping, ApplicationSettings.randomTiles, ApplicationSettings.InfoMode);
                    if (FirstMapLoaded != null)
                        FirstMapLoaded.Invoke();
                    multiBoard.Start();
                }
                multiBoard.SelectedBoard.SelectedPlatform = multiBoard.SelectedBoard.SelectedLayerIndex == -1 ? -1 : multiBoard.SelectedBoard.Layers[multiBoard.SelectedBoard.SelectedLayerIndex].zMList.ElementAt(0);
                ribbon.SetLayers(multiBoard.SelectedBoard.Layers);
                ribbon.SetSelectedLayer(multiBoard.SelectedBoard.SelectedLayerIndex, multiBoard.SelectedBoard.SelectedPlatform);
                multiBoard.SelectedBoard.VisibleTypes = ApplicationSettings.theoreticalVisibleTypes;
                multiBoard.SelectedBoard.EditedTypes = ApplicationSettings.theoreticalEditedTypes;
                ParseVisibleEditedTypes();
                multiBoard.Focus();
            }
        }

        void ribbon_NewPlatformClicked()
        {
            lock (multiBoard)
            {
                NewPlatform dlg = new NewPlatform(multiBoard.SelectedBoard.SelectedLayer.zMList);
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;
                int zm = dlg.result;
                multiBoard.SelectedBoard.SelectedLayer.zMList.Add(zm);
                multiBoard.SelectedBoard.SelectedPlatform = zm;
                ribbon.SetLayers(multiBoard.SelectedBoard.Layers);
                ribbon.SetSelectedLayer(multiBoard.SelectedBoard.SelectedLayerIndex, multiBoard.SelectedBoard.SelectedPlatform);
            }
        }
        #endregion

        #region Ribbon Layer Boxes
        private void SetLayer(int currentLayer, int currentPlatform)
        {
            multiBoard.SelectedBoard.SelectedLayerIndex = currentLayer;
            multiBoard.SelectedBoard.SelectedPlatform = currentPlatform;
            ApplicationSettings.lastDefaultLayer = currentLayer;
        }

        void ribbon_LayerViewChanged(int layer, int platform)
        {
            if (multiBoard.SelectedBoard == null)
                return;
            SetLayer(layer, platform);
            InputHandler.ClearSelectedItems(multiBoard.SelectedBoard);

        }
        #endregion

        public delegate void EmptyDelegate();

        public event EmptyDelegate CloseRequested;
        public event EmptyDelegate FirstMapLoaded;

        public static string CreateItemDescription(BoardItem item, string lineBreak)
        {
            switch (item.GetType().Name)
            {
                case "TileInstance":
                    return "Tile:" + lineBreak + ((TileInfo)item.BaseInfo).tS + @"\" + ((TileInfo)item.BaseInfo).u + @"\" + ((TileInfo)item.BaseInfo).no;
                case "ObjectInstance":
                    return "Object:" + lineBreak + ((ObjectInfo)item.BaseInfo).oS + @"\" + ((ObjectInfo)item.BaseInfo).l0 + @"\" + ((ObjectInfo)item.BaseInfo).l1 + @"\" + ((ObjectInfo)item.BaseInfo).l2;
                case "BackgroundInstance":
                    return "Background:" + lineBreak + ((BackgroundInfo)item.BaseInfo).bS + @"\" + (((BackgroundInfo)item.BaseInfo).ani ? "ani" : "back") + @"\" + ((BackgroundInfo)item.BaseInfo).no;
                case "PortalInstance":
                    return "Portal:" + lineBreak + "Name: " + ((PortalInstance)item).pn + lineBreak + "Type: " + Tables.PortalTypeNames[((PortalInstance)item).pt];
                case "MobInstance":
                    return "Mob:" + lineBreak + "Name: " + ((MobInfo)item.BaseInfo).Name + lineBreak + "ID: " + ((MobInfo)item.BaseInfo).ID;
                case "NPCInstance":
                    return "Npc:" + lineBreak + "Name: " + ((NpcInfo)item.BaseInfo).Name + lineBreak + "ID: " + ((NpcInfo)item.BaseInfo).ID;
                case "ReactorInstance":
                    return "Reactor:" + lineBreak + "ID: " + ((ReactorInfo)item.BaseInfo).ID;
                case "FootholdAnchor":
                    return "Foothold";
                case "RopeAnchor":
                    return ((RopeAnchor)item).ParentRope.ladder ? "Ladder" : "Rope";
                case "Chair":
                    return "Chair";
                case "ToolTipDot":
                case "ToolTip":
                case "ToolTipChar":
                    return "Tooltip";
                default:
                    if (item is INamedMisc)
                    {
                        return ((INamedMisc)item).Name;
                    }
                    else
                    {
                        return "";
                    }
            }
        }

        public void SetTilePanel(TilePanel tp)
        {
            this.tilePanel = tp;
        }

        public void EnterEditMode(ItemTypes type)
        {
            multiBoard.SelectedBoard.EditedTypes = type;
            ribbon.SetEnabled(false);
        }

        public void ExitEditMode()
        {
            multiBoard.SelectedBoard.EditedTypes = ApplicationSettings.theoreticalEditedTypes;
            ribbon.SetEnabled(true);
        }

        public bool AssertLayerSelected()
        {
            if (multiBoard.SelectedBoard.SelectedLayerIndex == -1)
            {
                MessageBox.Show("Select a real layer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public MultiBoard MultiBoard
        {
            get
            {
                return multiBoard;
            }
        }

        public HaRibbon Ribbon
        {
            get
            {
                return ribbon;
            }
        }
    }
}
