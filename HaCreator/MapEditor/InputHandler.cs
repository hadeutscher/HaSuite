/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;

namespace HaCreator.MapEditor
{
    public class InputHandler
    {
        private MultiBoard parentBoard;

        public InputHandler(MultiBoard parentBoard)
        {
            this.parentBoard = parentBoard;
            parentBoard.LeftMouseDown += new MultiBoard.LeftMouseDownDelegate(parentBoard_LeftMouseDown);
            parentBoard.LeftMouseUp += new MultiBoard.LeftMouseUpDelegate(parentBoard_LeftMouseUp);
            parentBoard.RightMouseClick += new MultiBoard.RightMouseClickDelegate(parentBoard_RightMouseClick);
            parentBoard.MouseDoubleClick += new MultiBoard.MouseDoubleClickDelegate(parentBoard_MouseDoubleClick);
            parentBoard.ShortcutKeyPressed += new MultiBoard.ShortcutKeyPressedDelegate(parentBoard_ShortcutKeyPressed);
            parentBoard.MouseMoved += new MultiBoard.MouseMovedDelegate(parentBoard_MouseMoved);
        }

        public static Rectangle CreateRectangle(Point a, Point b)
        {
            int left, right, top, bottom;
            if (a.X < b.X) { left = a.X; right = b.X; }
            else { left = b.X; right = a.X; }
            if (a.Y < b.Y) { top = a.Y; bottom = b.Y; }
            else { top = b.Y; bottom = a.Y; }
            return new Rectangle(left, top, right - left, bottom - top);
        }

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        public static bool IsKeyPushedDown(Keys vKey)
        {
            return 0 != (GetAsyncKeyState(vKey) & 0x8000);
        }
        
        private void parentBoard_MouseMoved(Board selectedBoard, Point oldPos, Point newPos, Point currPhysicalPos)
        {
            if (selectedBoard.Mouse.MinimapBrowseOngoing && selectedBoard.Mouse.State == MouseState.Selection)
            {
                HandleMinimapBrowse(selectedBoard, currPhysicalPos);
            }
            else if (selectedBoard.Mouse.MultiSelectOngoing && (Math.Abs(selectedBoard.Mouse.X - selectedBoard.Mouse.MultiSelectStart.X) > 1 || Math.Abs(selectedBoard.Mouse.Y - selectedBoard.Mouse.MultiSelectStart.Y) > 1))
            {
                Rectangle oldRect = CreateRectangle(oldPos, selectedBoard.Mouse.MultiSelectStart);
                Rectangle newRect = CreateRectangle(newPos, selectedBoard.Mouse.MultiSelectStart);
                List<BoardItem> toRemove = new List<BoardItem>();
                foreach (BoardItem item in selectedBoard.BoardItems)
                {
                    /*bool itemUnderNewRect = MultiBoard.IsItemUnderRectangle(item, newRect);
                    bool newPosUnderRectangle = MultiBoard.IsItemUnderRectangle(item, newRect);
                    if (itemUnderNewRect && (ApplicationSettings.editedTypes & item.Type) == item.Type && (selectedBoard.SelectedLayerIndex == -1 || item.CheckIfLayerSelected(selectedBoard.SelectedLayerIndex)))
                    {
                        //if (item.IsRectrangleTransparent(Math.Max(item.Left, newRect.Left) - item.Left, Math.Max(item.Top, newRect.Top) - item.Top, Math.Min(item.Right, newRect.Right) - item.Left, Math.Min(item.Bottom, newRect.Bottom) - item.Top))
                            item.Selected = true;
                    }
                    else if (item.Selected && !itemUnderNewRect && (MultiBoard.IsItemUnderRectangle(item, oldRect) || !MultiBoard.IsPointInsideRectangle(newPos, item.Left, item.Top, item.Right, item.Bottom) || item.IsPixelTransparent(newRect.Left - item.Left, newRect.Top - item.Top)))
                        toRemove.Add(item);*/
                    if (MultiBoard.IsItemUnderRectangle(item, newRect) && (selectedBoard.EditedTypes & item.Type) == item.Type && (selectedBoard.SelectedLayerIndex == -1 || item.CheckIfLayerSelected(selectedBoard.SelectedLayerIndex)))
                        item.Selected = true;
                    else if (item.Selected && MultiBoard.IsItemUnderRectangle(item, oldRect))
                        toRemove.Add(item);
                }
                foreach (BoardItem item in toRemove)
                    item.Selected = false;
                toRemove.Clear();
            }
            else if (selectedBoard.Mouse.BoundItems.Count > 0)
            {
                //snapping
                if (UserSettings.useSnapping && selectedBoard.Mouse.BoundItems.Count != 0 && !IsKeyPushedDown(Keys.Menu))
                {
                    MouseState state = selectedBoard.Mouse.State;
                    if (state == MouseState.Selection || state == MouseState.StaticObjectAdding || state == MouseState.RandomTiles || state == MouseState.Ropes || state == MouseState.Footholds || state == MouseState.Chairs)
                    {
                        object[] keys = new object[selectedBoard.Mouse.BoundItems.Count];
                        selectedBoard.Mouse.BoundItems.Keys.CopyTo(keys, 0);
                        foreach (object item in keys)
                            if (item is ISnappable)
                                ((ISnappable)item).DoSnap();
                    }
                }
            }
            else if (selectedBoard.Mouse.State == MouseState.Footholds)
                selectedBoard.Mouse.DoSnap();

            if ((selectedBoard.Mouse.BoundItems.Count > 0 || selectedBoard.Mouse.MultiSelectOngoing) && selectedBoard.Mouse.State == MouseState.Selection)
            {
                //auto scrolling
                if (currPhysicalPos.X - UserSettings.ScrollDistance < 0 && oldPos.X > newPos.X) //move to left
                    selectedBoard.hScroll = (int)Math.Max(0, selectedBoard.hScroll - Math.Pow(UserSettings.ScrollBase, (UserSettings.ScrollDistance - currPhysicalPos.X) * UserSettings.ScrollExponentFactor) * UserSettings.ScrollFactor);
                else if (currPhysicalPos.X + UserSettings.ScrollDistance > selectedBoard.ParentControl.Width && oldPos.X < newPos.X) //move to right
                    selectedBoard.hScroll = (int)Math.Min(selectedBoard.hScroll + Math.Pow(UserSettings.ScrollBase, (currPhysicalPos.X - selectedBoard.ParentControl.Width + UserSettings.ScrollDistance) * UserSettings.ScrollExponentFactor) * UserSettings.ScrollFactor, selectedBoard.ParentControl.maxHScroll);
                if (currPhysicalPos.Y - UserSettings.ScrollDistance < 0 && oldPos.Y > newPos.Y) //move to top
                    selectedBoard.vScroll = (int)Math.Max(0, selectedBoard.vScroll - Math.Pow(UserSettings.ScrollBase, (UserSettings.ScrollDistance - currPhysicalPos.Y) * UserSettings.ScrollExponentFactor) * UserSettings.ScrollFactor);
                else if (currPhysicalPos.Y + UserSettings.ScrollDistance > selectedBoard.ParentControl.Height && oldPos.Y < newPos.Y) //move to bottom
                    selectedBoard.vScroll = (int)Math.Min(selectedBoard.vScroll + Math.Pow(UserSettings.ScrollBase, (currPhysicalPos.Y - selectedBoard.ParentControl.Height + UserSettings.ScrollDistance) * UserSettings.ScrollExponentFactor) * UserSettings.ScrollFactor, selectedBoard.ParentControl.maxVScroll);
            }
        }

        private UndoRedoAction CreateItemUndoMoveAction(BoardItem item, Point posChange)
        {
            if (item is BackgroundInstance)
                return UndoRedoManager.BackgroundMoved((BackgroundInstance)item, new Point(((BackgroundInstance)item).BaseX + posChange.X, ((BackgroundInstance)item).BaseY + posChange.Y), new Point(((BackgroundInstance)item).BaseX, ((BackgroundInstance)item).BaseY));
            else
                return UndoRedoManager.ItemMoved(item, new Point(item.X + posChange.X, item.Y + posChange.Y), new Point(item.X, item.Y));
        }

        private void parentBoard_ShortcutKeyPressed(Board selectedBoard, bool ctrl, bool shift, bool alt, Keys key)
        {
            List<UndoRedoAction> actions = new List<UndoRedoAction>();
            if (key == Keys.ControlKey || key == Keys.ShiftKey || key == Keys.Menu /*ALT key*/)
                return;
            bool clearRedo = true;
            switch (key)
            {
                case Keys.Left:
                    foreach (BoardItem item in selectedBoard.SelectedItems)
                        if (!item.BoundToSelectedItem(selectedBoard))
                        {
                            item.X--;
                            actions.Add(CreateItemUndoMoveAction(item, new Point(1, 0)));
                        }
                    break;
                case Keys.Right:
                    foreach (BoardItem item in selectedBoard.SelectedItems)
                        if (!item.BoundToSelectedItem(selectedBoard))
                        {
                            item.X++;
                            actions.Add(CreateItemUndoMoveAction(item, new Point(-1, 0)));
                        }
                    break;
                case Keys.Up:
                    foreach (BoardItem item in selectedBoard.SelectedItems)
                        if (!item.BoundToSelectedItem(selectedBoard))
                        {
                            item.Y--;
                            actions.Add(CreateItemUndoMoveAction(item, new Point(0, 1)));
                        }
                    break;
                case Keys.Down:
                    foreach (BoardItem item in selectedBoard.SelectedItems)
                        if (!item.BoundToSelectedItem(selectedBoard))
                        {
                            item.Y++;
                            actions.Add(CreateItemUndoMoveAction(item, new Point(0, -1)));
                        }
                    break;
                case Keys.Delete:
                    switch (selectedBoard.Mouse.State)
                    {
                        case MouseState.Selection:
                            int selectedItemIndex = 0;
                            while (selectedBoard.SelectedItems.Count > selectedItemIndex)
                            {
                                BoardItem item = selectedBoard.SelectedItems[selectedItemIndex];
                                if (item is ToolTipDot) selectedItemIndex++;
                                else item.RemoveItem(ref actions);
                            }
                            break;
                        case MouseState.RandomTiles:
                        case MouseState.StaticObjectAdding:
                        case MouseState.Chairs:
                        case MouseState.Ropes:
                            parentBoard.InvokeReturnToSelectionState();
                            break;
                        case MouseState.Footholds:
                            while (selectedBoard.Mouse.connectedLines.Count > 0 && selectedBoard.Mouse.connectedLines[0].FirstDot.connectedLines.Count > 0)
                                selectedBoard.Mouse.connectedLines[0].FirstDot.connectedLines[0].Remove(false, ref actions);
                            break;
                    }
                    break;
                case Keys.F:
                    if (ctrl)
                    {
                        foreach (BoardItem item in selectedBoard.SelectedItems)
                            if (item is IFlippable)
                            {
                                ((IFlippable)item).Flip = !((IFlippable)item).Flip;
                                actions.Add(UndoRedoManager.ItemFlipped((IFlippable)item));
                            }
                    }
                    break;
                case Keys.Add:
                    foreach (BoardItem item in selectedBoard.SelectedItems)
                    {
                        item.Z += UserSettings.zShift;
                        actions.Add(UndoRedoManager.ItemZChanged(item, item.Z - UserSettings.zShift, item.Z));
                    }
                    selectedBoard.BoardItems.Sort();
                    break;
                case Keys.Subtract:
                    foreach (BoardItem item in selectedBoard.SelectedItems)
                    {
                        item.Z -= UserSettings.zShift;
                        actions.Add(UndoRedoManager.ItemZChanged(item, item.Z + UserSettings.zShift, item.Z));
                    }
                    selectedBoard.BoardItems.Sort();
                    break;
                case Keys.A:
                    if (ctrl) foreach (BoardItem item in selectedBoard.BoardItems)
                        if ((selectedBoard.EditedTypes & item.Type) == item.Type) 
                            item.Selected = true;
                    clearRedo = false;
                    break;
                case Keys.Z:
                    if (ctrl && selectedBoard.UndoRedoMan.UndoList.Count > 0) selectedBoard.UndoRedoMan.Undo();
                    clearRedo = false;
                    break;
                case Keys.Y:
                    if (ctrl && selectedBoard.UndoRedoMan.RedoList.Count > 0) selectedBoard.UndoRedoMan.Redo();
                    clearRedo = false;
                    break;
                case Keys.Escape:
                    if (selectedBoard.Mouse.State == MouseState.Selection)
                    {
                        ClearBoundItems(selectedBoard);
                        ClearSelectedItems(selectedBoard);
                        clearRedo = false;
                    }
                    else if (selectedBoard.Mouse.State == MouseState.Footholds)
                        selectedBoard.Mouse.Clear();
                    else
                        parentBoard.InvokeReturnToSelectionState();
                    break;
                default:
                    clearRedo = false;
                    break;
            }
            if (actions.Count > 0)
                selectedBoard.UndoRedoMan.AddUndoBatch(actions);
            if (clearRedo)
                selectedBoard.UndoRedoMan.RedoList.Clear();
                
        }
        private void ShiftAllSelectedItems(Board selectedBoard, Point offset)
        {
            foreach (BoardItem item in selectedBoard.SelectedItems)
            {
                item.Move(item.X + offset.X, item.Y + offset.Y);
            }
        }

        private bool ClickOnMinimap(Board selectedBoard, Point position)
        {
            if (selectedBoard.MiniMap == null || !UserSettings.useMiniMap) return false;
            return position.X > 0 && position.X < selectedBoard.MiniMap.Width && position.Y > 0 && position.Y < selectedBoard.MiniMap.Height;

        }

        private void parentBoard_MouseDoubleClick(Board selectedBoard, BoardItem target, Point realPosition, Point virtualPosition)
        {
            if (ClickOnMinimap(selectedBoard, realPosition)) return;
            if (target != null)
            {
                ClearSelectedItems(selectedBoard);
                target.Selected = true;
                parentBoard.EditInstanceClicked(target);
            }
            else if (selectedBoard.Mouse.State == MouseState.Footholds)
            {
                selectedBoard.Mouse.CreateFhAnchor();
            }
        }

        private void parentBoard_RightMouseClick(Board selectedBoard, BoardItem rightClickTarget, Point realPosition, Point virtualPosition, MouseState mouseState)
        {
            if (mouseState == MouseState.Selection)
            {
               ClearBoundItems(selectedBoard);
//                    ClearSelectedItems(selectedBoard);
                if (ClickOnMinimap(selectedBoard, realPosition)) return;
                if (rightClickTarget == null)
                    return;
                ContextMenuStrip menuStrip = new ContextMenuStrip();
                menuStrip.Tag = rightClickTarget;
                rightClickTarget.Selected = true;
                ToolStripMenuItem editInstance = new ToolStripMenuItem("Edit this instance...");
                editInstance.Tag = rightClickTarget;
                editInstance.Click += new EventHandler(editInstance_Click);
                editInstance.Font = new System.Drawing.Font(editInstance.Font, System.Drawing.FontStyle.Bold);
                menuStrip.Items.Add(editInstance);
                ToolStripMenuItem baseInfo = new ToolStripMenuItem("Edit base info...");
                baseInfo.Tag = rightClickTarget;
                baseInfo.Click += new EventHandler(baseInfo_Click);
                menuStrip.Items.Add(baseInfo);
                if (rightClickTarget is BackgroundInstance || rightClickTarget is LayeredItem)
                {
                    ToolStripMenuItem bringToFront = new ToolStripMenuItem("Bring to Front");
                    bringToFront.Tag = rightClickTarget;
                    bringToFront.Click += new EventHandler(bringToFront_Click);
                    menuStrip.Items.Add(bringToFront);
                    ToolStripMenuItem sendToBack = new ToolStripMenuItem("Send to Back");
                    sendToBack.Tag = rightClickTarget;
                    sendToBack.Click += new EventHandler(sendToBack_Click);
                    menuStrip.Items.Add(sendToBack);
                }
                menuStrip.Show(parentBoard.PointToScreen(new System.Drawing.Point(realPosition.X, realPosition.Y)));
            }
            else parentBoard.InvokeReturnToSelectionState();
        }

        void sendToBack_Click(object sender, EventArgs e)
        {
            parentBoard.SendToBackClicked((BoardItem)((ToolStripMenuItem)sender).Tag);
        }

        void bringToFront_Click(object sender, EventArgs e)
        {
            parentBoard.BringToFrontClicked((BoardItem)((ToolStripMenuItem)sender).Tag);
        }

        private void baseInfo_Click(object sender, EventArgs e)
        {
            parentBoard.EditBaseClicked((BoardItem)((ToolStripMenuItem)sender).Tag);
        }

        private void editInstance_Click(object sender, EventArgs e)
        {
            parentBoard.EditInstanceClicked((BoardItem)((ToolStripMenuItem)sender).Tag);
        }

        private void parentBoard_LeftMouseUp(Board selectedBoard, BoardItem target, BoardItem selectedTarget, Point realPosition, Point virtualPosition, bool selectedItemHigher)
        {
            if (selectedBoard.Mouse.State == MouseState.Selection)//handle drag-drop selection end
                ClearBoundItems(selectedBoard);
            else if (selectedBoard.Mouse.State == MouseState.StaticObjectAdding ||
                selectedBoard.Mouse.State == MouseState.RandomTiles ||
                selectedBoard.Mouse.State == MouseState.Chairs ||
                selectedBoard.Mouse.State == MouseState.Ropes ||
                selectedBoard.Mouse.State == MouseState.Tooltip) //handle clicks that are meant to add an item to the board
                selectedBoard.Mouse.PlaceObject();
            else if (selectedBoard.Mouse.State == MouseState.Footholds)
                selectedBoard.Mouse.TryConnectFoothold();
        }
        
        private void HandleMinimapBrowse(Board selectedBoard, Point realPosition)
        {
            int h = realPosition.X * selectedBoard.mag - selectedBoard.ParentControl.Width / 2;
            int v = realPosition.Y * selectedBoard.mag - selectedBoard.ParentControl.Height / 2;
            if (h < 0) selectedBoard.hScroll = 0;
            else if (h > selectedBoard.ParentControl.maxHScroll) selectedBoard.hScroll = selectedBoard.ParentControl.maxHScroll;
            else selectedBoard.hScroll = h;
            if (v < 0) selectedBoard.vScroll = 0;
            else if (v > selectedBoard.ParentControl.maxVScroll) selectedBoard.vScroll = selectedBoard.ParentControl.maxVScroll;
            else selectedBoard.vScroll = v;
        }

        private void parentBoard_LeftMouseDown(Board selectedBoard, BoardItem item, BoardItem selectedItem, Point realPosition, Point virtualPosition, bool selectedItemHigher)
        {
            if (ClickOnMinimap(selectedBoard, realPosition) && selectedBoard.Mouse.State == MouseState.Selection)
            {
                ClearSelectedItems(selectedBoard);
                selectedBoard.Mouse.MinimapBrowseOngoing = true;
                HandleMinimapBrowse(selectedBoard, realPosition);
            }
            else if (selectedBoard.Mouse.State == MouseState.Selection)//handle drag-drop, multiple selection and all that
            {
                BoardItem itemToSelect = null;
                bool ctrlDown = (Control.ModifierKeys & Keys.Control) == Keys.Control;
                if (item == null && selectedItem == null) //multi-drag
                {
                    if (!ctrlDown) ClearSelectedItems(selectedBoard);
                    selectedBoard.Mouse.MultiSelectOngoing = true;
                    selectedBoard.Mouse.MultiSelectStart = virtualPosition;
                    return;
                }
                bool itemAlreadySelected = false; //do not attempt to understand this algorithm, I know I can't
                if (item == null)
                {
                    itemToSelect = selectedItem;
                    itemAlreadySelected = true;
                }
                else if (selectedItem == null) itemToSelect = item;
                else if (!selectedItemHigher) itemToSelect = item;
                else
                {
                    itemToSelect = selectedItem;
                    itemAlreadySelected = true;
                }
                if (!itemAlreadySelected && !ctrlDown)
                {
                    ClearSelectedItems(selectedBoard);
                }
                if (itemAlreadySelected && ctrlDown)
                    itemToSelect.Selected = false;
                else
                {
                    itemToSelect.Selected = true;
                    BindAllSelectedItems(selectedBoard);
                }
            }
        }

        private void BindAllSelectedItems(Board selectedBoard)
        {
            foreach (BoardItem itemToSelect in selectedBoard.SelectedItems)
            {
                selectedBoard.Mouse.BindItem(itemToSelect, new Point((itemToSelect.X/* - itemToSelect.Origin.X*/) - selectedBoard.Mouse.X, (itemToSelect.Y/* - itemToSelect.Origin.Y*/) - selectedBoard.Mouse.Y));
                if (itemToSelect is BackgroundInstance)
                    itemToSelect.moveStartPos = new Point(((BackgroundInstance)itemToSelect).BaseX, ((BackgroundInstance)itemToSelect).BaseY);
                else
                    itemToSelect.moveStartPos = new Point(itemToSelect.X, itemToSelect.Y);
            }
        }

        public static void ClearSelectedItems(Board board)
        {
            while (board.SelectedItems.Count > 0)
            {
                board.SelectedItems[0].Selected = false;
            }
        }

        public static void ClearBoundItems(Board board)
        {
            object[] keys = new object[board.Mouse.BoundItems.Count];
            //int i = 0;
            board.Mouse.BoundItems.Keys.CopyTo(keys, 0);
            /*foreach (DictionaryEntry entry in board.Mouse.BoundItems)
            {
                keys[i] = entry.Key;
                i++;
            }*/
            List<UndoRedoAction> undoActions = new List<UndoRedoAction>();
            bool addUndo;
            foreach (object key in keys)
            {
                BoardItem item = (BoardItem)key;
                addUndo = item.tempParent == null || !(item.tempParent.Parent is Mouse);
                board.Mouse.ReleaseItem(item);
                if (addUndo)
                {
                    if ((item is BackgroundInstance) && (((BackgroundInstance)item).BaseX != item.moveStartPos.X || ((BackgroundInstance)item).BaseY != item.moveStartPos.Y))
                        undoActions.Add(UndoRedoManager.BackgroundMoved((BackgroundInstance)item, new Point(item.moveStartPos.X, item.moveStartPos.Y), new Point(((BackgroundInstance)item).BaseX, ((BackgroundInstance)item).BaseY)));
                    else if (!(item is BackgroundInstance) && (item.X != item.moveStartPos.X || item.Y != item.moveStartPos.Y))
                        undoActions.Add(UndoRedoManager.ItemMoved(item, new Point(item.moveStartPos.X, item.moveStartPos.Y), new Point(item.X, item.Y)));
                }
            }
            if (undoActions.Count > 0)
                board.UndoRedoMan.AddUndoBatch(undoActions);
        }
    }
}
