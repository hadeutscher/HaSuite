/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using HaCreator.MapEditor;
using MapleLib.WzLib.WzStructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HaCreator.CustomControls
{
    /// <summary>
    /// Interaction logic for HaRibbon.xaml
    /// </summary>
    public partial class HaRibbon : UserControl
    {
        public HaRibbon()
        {
            InitializeComponent();
        }

        public int reducedHeight = 0;
        private int actualLayerIndex = 0;
        private int changingIndexCnt = 0;

        private void Ribbon_Loaded(object sender, RoutedEventArgs e)
        {
            Grid child = VisualTreeHelper.GetChild((DependencyObject)sender, 0) as Grid;
            if (child != null)
            {
                reducedHeight = (int)child.RowDefinitions[0].ActualHeight;
                child.RowDefinitions[0].Height = new GridLength(0);
            }
        }

        public static readonly RoutedUICommand Open = new RoutedUICommand("Open", "Open", typeof(HaRibbon), 
            new InputGestureCollection() { new KeyGesture(Key.O, ModifierKeys.Control) });
        public static readonly RoutedUICommand Save = new RoutedUICommand("Save", "Save", typeof(HaRibbon),
            new InputGestureCollection() { new KeyGesture(Key.S, ModifierKeys.Control) });
        public static readonly RoutedUICommand Repack = new RoutedUICommand("Repack", "Repack", typeof(HaRibbon),
            new InputGestureCollection() {});
        public static readonly RoutedUICommand About = new RoutedUICommand("About", "About", typeof(HaRibbon),
            new InputGestureCollection() {});
        public static readonly RoutedUICommand Help = new RoutedUICommand("Help", "Help", typeof(HaRibbon),
            new InputGestureCollection() {});
        public static readonly RoutedUICommand Settings = new RoutedUICommand("Settings", "Settings", typeof(HaRibbon),
            new InputGestureCollection() {});
        public static readonly RoutedUICommand Exit = new RoutedUICommand("Exit", "Exit", typeof(HaRibbon),
            new InputGestureCollection() {});
        public static readonly RoutedUICommand ViewBoxes = new RoutedUICommand("ViewBoxes", "ViewBoxes", typeof(HaRibbon),
            new InputGestureCollection() { });
        public static readonly RoutedUICommand ShowVR = new RoutedUICommand("ShowVR", "ShowVR", typeof(HaRibbon),
            new InputGestureCollection() { });
        public static readonly RoutedUICommand Minimap = new RoutedUICommand("Minimap", "Minimap", typeof(HaRibbon),
            new InputGestureCollection() { });
        public static readonly RoutedUICommand Parallax = new RoutedUICommand("Parallax", "Parallax", typeof(HaRibbon),
            new InputGestureCollection() { });
        public static readonly RoutedUICommand Finalize = new RoutedUICommand("Finalize", "Finalize", typeof(HaRibbon),
            new InputGestureCollection() { });
        public static readonly RoutedUICommand AllLayerView = new RoutedUICommand("AllLayerView", "AllLayerView", typeof(HaRibbon),
            new InputGestureCollection() { });
        public static readonly RoutedUICommand MapSim = new RoutedUICommand("MapSim", "MapSim", typeof(HaRibbon),
            new InputGestureCollection() { });
        public static readonly RoutedUICommand RegenMinimap = new RoutedUICommand("RegenMinimap", "RegenMinimap", typeof(HaRibbon),
            new InputGestureCollection() { });
        public static readonly RoutedUICommand Snapping = new RoutedUICommand("Snapping", "Snapping", typeof(HaRibbon),
            new InputGestureCollection() { });
        public static readonly RoutedUICommand Random = new RoutedUICommand("Random", "Random", typeof(HaRibbon),
            new InputGestureCollection() { });
        public static readonly RoutedUICommand HaRepacker = new RoutedUICommand("HaRepacker", "HaRepacker", typeof(HaRibbon),
            new InputGestureCollection() { });

        private void AlwaysExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (OpenClicked != null)
                OpenClicked.Invoke();
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (SaveClicked != null)
                SaveClicked.Invoke();
        }

        private void Repack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (RepackClicked != null)
                RepackClicked.Invoke();
        }

        private void About_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (AboutClicked != null)
                AboutClicked.Invoke();
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (HelpClicked != null)
                HelpClicked.Invoke();
        }

        private void Settings_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (SettingsClicked != null)
                SettingsClicked.Invoke();
        }

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ExitClicked != null)
                ExitClicked.Invoke();
        }

        private void ViewBoxes_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CheckBox rcb = (CheckBox)e.OriginalSource;
            
            if (ViewToggled != null)
                ViewToggled.Invoke(tilesCheck.IsChecked, objsCheck.IsChecked, npcsCheck.IsChecked, mobsCheck.IsChecked, reactCheck.IsChecked, portalCheck.IsChecked, fhCheck.IsChecked, ropeCheck.IsChecked, chairCheck.IsChecked, tooltipCheck.IsChecked, bgCheck.IsChecked, miscCheck.IsChecked);
        }

        private void ShowVR_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ShowVRToggled != null)
                ShowVRToggled.Invoke(((RibbonToggleButton)e.OriginalSource).IsChecked.Value);
        }

        private void Minimap_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ShowMinimapToggled != null)
                ShowMinimapToggled.Invoke(((RibbonToggleButton)e.OriginalSource).IsChecked.Value);
        }

        private void Parallax_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ParallaxToggled != null)
                ParallaxToggled.Invoke(((RibbonToggleButton)e.OriginalSource).IsChecked.Value);
        }

        private void Finalize_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (FinalizeClicked != null)
                FinalizeClicked.Invoke();
        }

        private void AllLayerView_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            layerBox.IsEnabled = !layerCheckbox.IsChecked.Value;
            if (layerBox.SelectedIndex == -1)
            {
                layerBox.SelectedIndex = actualLayerIndex;
            }
            if (AllLayerToggled != null)
                AllLayerToggled.Invoke(layerCheckbox.IsChecked.Value ? -1 : actualLayerIndex);
        }

        private void MapSim_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MapSimulationClicked != null)
                MapSimulationClicked.Invoke();
        }

        private void RegenMinimap_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (RegenerateMinimapClicked != null)
                RegenerateMinimapClicked.Invoke();
        }

        private void Snapping_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (SnappingToggled != null)
                SnappingToggled.Invoke(((RibbonToggleButton)e.OriginalSource).IsChecked.Value);
        }

        private void Random_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (RandomTilesToggled != null)
                RandomTilesToggled.Invoke(((RibbonToggleButton)e.OriginalSource).IsChecked.Value);
        }

        private void HaRepacker_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (HaRepackerClicked != null)
                HaRepackerClicked.Invoke();
        }

        private void layerBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (changingIndexCnt > 0)
            {
                // Allows changing for internal purposes
                return;
            }
            if (layerBox.SelectedIndex != -1)
            {
                actualLayerIndex = layerBox.SelectedIndex;
            }
            if (LayerViewChanged != null)
                LayerViewChanged.Invoke(layerCheckbox.IsChecked.Value ? -1 : actualLayerIndex);
        }

        public delegate void EmptyEvent();
        public delegate void ViewToggleEvent(bool? tiles, bool? objs, bool? npcs, bool? mobs, bool? reactors, bool? portals, bool? footholds, bool? ropes, bool? chairs, bool? tooltips, bool? backgrounds, bool? misc);
        public delegate void ToggleEvent(bool pressed);
        public delegate void LayerViewChangedEvent(int layer);

        public event EmptyEvent OpenClicked;
        public event EmptyEvent SaveClicked;
        public event EmptyEvent RepackClicked;
        public event EmptyEvent AboutClicked;
        public event EmptyEvent HelpClicked;
        public event EmptyEvent SettingsClicked;
        public event EmptyEvent ExitClicked;
        public event EmptyEvent FinalizeClicked;
        public event ViewToggleEvent ViewToggled;
        public event ToggleEvent ShowVRToggled;
        public event ToggleEvent ShowMinimapToggled;
        public event ToggleEvent ParallaxToggled;
        public event LayerViewChangedEvent LayerViewChanged;
        public event LayerViewChangedEvent AllLayerToggled;
        public event EmptyEvent MapSimulationClicked;
        public event EmptyEvent RegenerateMinimapClicked;
        public event ToggleEvent SnappingToggled;
        public event ToggleEvent RandomTilesToggled;
        public event EmptyEvent HaRepackerClicked;


        public void SetSelectedLayer(int layer)
        {
            if (layer == -1)
            {
                layerCheckbox.IsChecked = true;
                layerBox.IsEnabled = false;
            }
            else
            {
                layerCheckbox.IsChecked = false;
                layerBox.IsEnabled = true;
                layerBox.SelectedIndex = layer;
            }
        }

        public void SetLayers(List<Layer> layers)
        {
            changingIndexCnt++;
            object[] arr = new object[layers.Count];
            for (int i = 0; i < layers.Count; i++) 
            {
                arr[i] = i.ToString() + (layers[i].tS != null ? (" - " + layers[i].tS) : "");
            }
            layerBox.Items.Clear();
            foreach (object o in arr)
            {
                layerBox.Items.Add(o);
            }
            resetLayerBoxIfNeeded();
            changingIndexCnt--;
        }

        public void SetLayer(Layer layer)
        {
            changingIndexCnt++;
            int i = layer.LayerNumber;
            layerBox.Items[i] = i.ToString() + (layer.tS != null ? (" - " + layer.tS) : "");
            resetLayerBoxIfNeeded();
            changingIndexCnt--;
        }

        public void SetVisibilityCheckboxes(bool? tiles, bool? objs, bool? npcs, bool? mobs, bool? reactors, bool? portals, bool? footholds, bool? ropes, bool? chairs, bool? tooltips, bool? backgrounds, bool? misc)
        {
            tilesCheck.IsChecked = tiles;
            objsCheck.IsChecked = objs;
            npcsCheck.IsChecked = npcs;
            mobsCheck.IsChecked = mobs;
            reactCheck.IsChecked = reactors;
            portalCheck.IsChecked = portals;
            fhCheck.IsChecked = footholds;
            ropeCheck.IsChecked = ropes;
            chairCheck.IsChecked = chairs;
            tooltipCheck.IsChecked = tooltips;
            bgCheck.IsChecked = backgrounds;
            miscCheck.IsChecked = misc;
        }

        private void changeLayerBoxIndexInternal(int newIndex)
        {
            changingIndexCnt++;
            layerBox.SelectedIndex = newIndex;
            changingIndexCnt--;
        }

        private void resetLayerBoxIfNeeded()
        {
            if (layerBox.SelectedIndex == -1)
            {
                changeLayerBoxIndexInternal(actualLayerIndex);
            }
        }

        public void SetEnabled(bool enabled)
        {
            viewTab.IsEnabled = enabled;
            toolsTab.IsEnabled = enabled;
            resetLayerBoxIfNeeded();
        }

        public void SetOptions(bool vr, bool minimap, bool parallax, bool snap, bool random)
        {
            showvrBtn.IsChecked = vr;
            minimapBtn.IsChecked = minimap;
            parallaxBtn.IsChecked = parallax;
            snapBtn.IsChecked = snap;
            randomBtn.IsChecked = random;
        }

        public void SetMousePos(int virtualX, int virtualY, int physicalX, int physicalY)
        {
            this.virtualPos.Text = "X: " + virtualX.ToString() + "\nY: " + virtualY.ToString();
            this.physicalPos.Text = "X: " + physicalX.ToString() + "\nY: " + physicalY.ToString();
        }

        public void SetItemDesc(string desc)
        {
            itemDesc.Text = desc;
        }
    }
}