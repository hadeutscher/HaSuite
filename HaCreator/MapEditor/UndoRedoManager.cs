/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HaCreator.MapEditor
{
    public class UndoRedoManager
    {
        public List<UndoRedoBatch> UndoList = new List<UndoRedoBatch>();
        public List<UndoRedoBatch> RedoList = new List<UndoRedoBatch>();
        private Board parentBoard;

        public UndoRedoManager(Board parentBoard)
        {
            this.parentBoard = parentBoard;
        }

        public void AddUndoBatch(List<UndoRedoAction> actions)
        {
            if (actions.Count == 0)
            {
                return;
            }
            UndoRedoBatch batch = new UndoRedoBatch() { Actions = actions };
            UndoList.Add(batch);
            RedoList.Clear();
            parentBoard.ParentControl.UndoListChanged();
            parentBoard.ParentControl.RedoListChanged();
        }

        #region Undo Actions Creation
        public static UndoRedoAction ItemAdded(BoardItem item)
        {
            return new UndoRedoAction(item, UndoRedoType.ItemAdded, null, null);
        }

        public static UndoRedoAction ItemDeleted(BoardItem item)
        {
            return new UndoRedoAction(item, UndoRedoType.ItemDeleted, null, null);
        }

        public static UndoRedoAction ItemMoved(BoardItem item, Point oldPos, Point newPos)
        {
            return new UndoRedoAction(item, UndoRedoType.ItemMoved, oldPos, newPos);
        }

        public static UndoRedoAction VRChanged(Rectangle oldVR, Rectangle newVR)
        {
            return new UndoRedoAction(null, UndoRedoType.VRChanged, oldVR, newVR);
        }

        public static UndoRedoAction MapCenterChanged(Point oldCenter, Point newCenter)
        {
            return new UndoRedoAction(null, UndoRedoType.MapCenterChanged, oldCenter, newCenter);
        }

        public static UndoRedoAction ItemFlipped(IFlippable item)
        {
            return new UndoRedoAction((BoardItem)item, UndoRedoType.ItemFlipped, null, null);
        }

        public static UndoRedoAction LineRemoved(MapleLine line, MapleDot a, MapleDot b)
        {
            return new UndoRedoAction(null, UndoRedoType.LineRemoved, a, b, line);
        }

        public static UndoRedoAction LineAdded(MapleLine line, MapleDot a, MapleDot b)
        {
            return new UndoRedoAction(null, UndoRedoType.LineAdded, a, b, line);
        }

        public static UndoRedoAction ToolTipLinked(ToolTip tt, ToolTipChar ttc)
        {
            return new UndoRedoAction(tt, UndoRedoType.ToolTipLinked, ttc, null);
        }

        public static UndoRedoAction ToolTipUnlinked(ToolTip tt, ToolTipChar ttc)
        {
            return new UndoRedoAction(tt, UndoRedoType.ToolTipUnlinked, ttc, null);
        }

        public static UndoRedoAction BackgroundMoved(BackgroundInstance item, Point oldPos, Point newPos)
        {
            return new UndoRedoAction(item, UndoRedoType.BackgroundMoved, oldPos, newPos);
        }

        public static UndoRedoAction ItemsLinked(BoardItem parent, BoardItem child, Point distance)
        {
            return new UndoRedoAction(parent, UndoRedoType.ItemsLinked, child, distance);
        }

        public static UndoRedoAction ItemsUnlinked(BoardItem parent, BoardItem child, Point distance)
        {
            return new UndoRedoAction(parent, UndoRedoType.ItemsUnlinked, child, distance);
        }

        public static UndoRedoAction ItemsLayerChanged(List<IContainsLayerInfo> items, int oldLayerIndex, int newLayerIndex)
        {
            return new UndoRedoAction(null, UndoRedoType.ItemsLayerChanged, oldLayerIndex, newLayerIndex, items);
        }

        public static UndoRedoAction ItemLayerPlatChanged(IContainsLayerInfo item, Tuple<int, int> oldLayerPlat, Tuple<int, int> newLayerPlat)
        {
            return new UndoRedoAction(null, UndoRedoType.ItemLayerPlatChanged, oldLayerPlat, newLayerPlat, item);
        }

        public static UndoRedoAction RopeRemoved(Rope rope)
        {
            return new UndoRedoAction(null, UndoRedoType.RopeRemoved, rope, null);
        }

        public static UndoRedoAction RopeAdded(Rope rope)
        {
            return new UndoRedoAction(null, UndoRedoType.RopeAdded, rope, null);
        }

        public static UndoRedoAction ItemZChanged(BoardItem item, int oldZ, int newZ)
        {
            return new UndoRedoAction(item, UndoRedoType.ItemZChanged, oldZ, newZ);
        }

        public static UndoRedoAction LayerTSChanged(Layer layer, string oldTS, string newTS)
        {
            return new UndoRedoAction(null, UndoRedoType.LayerTSChanged, oldTS, newTS, layer);
        }

        public static UndoRedoAction zMChanged(IContainsLayerInfo target, int oldZM, int newZM)
        {
            return new UndoRedoAction(null, UndoRedoType.zMChanged, oldZM, newZM, target);
        }
        #endregion

        public void Undo()
        {
            lock (parentBoard.ParentControl)
            {
                UndoRedoBatch action = UndoList[UndoList.Count - 1];
                action.UndoRedo(parentBoard);
                action.SwitchActions();
                UndoList.RemoveAt(UndoList.Count - 1);
                RedoList.Add(action);
                parentBoard.ParentControl.UndoListChanged();
                parentBoard.ParentControl.RedoListChanged();
            }
        }

        public void Redo()
        {
            lock (parentBoard.ParentControl)
            {
                UndoRedoBatch action = RedoList[RedoList.Count - 1];
                action.UndoRedo(parentBoard);
                action.SwitchActions();
                RedoList.RemoveAt(RedoList.Count - 1);
                UndoList.Add(action);
                parentBoard.ParentControl.UndoListChanged();
                parentBoard.ParentControl.RedoListChanged();
            }
        }
    }

    public class UndoRedoBatch
    {
        public List<UndoRedoAction> Actions = new List<UndoRedoAction>();

        public void UndoRedo(Board board)
        {
            HashSet<int> layersToRecheck = new HashSet<int>();
            foreach (UndoRedoAction action in Actions) 
                action.UndoRedo(layersToRecheck);
            layersToRecheck.ToList().ForEach(x => board.Layers[x].RecheckTileSet());
        }

        public void SwitchActions()
        {
            foreach (UndoRedoAction action in Actions) action.SwitchAction();
        }
    }

    public class UndoRedoAction
    {
        private BoardItem item;
        private UndoRedoType type;
        private object ParamA;
        private object ParamB;
        private object ParamC;

        public UndoRedoAction(BoardItem item, UndoRedoType type, object ParamA, object ParamB)
        {
            this.item = item;
            this.type = type;
            this.ParamA = ParamA;
            this.ParamB = ParamB;
        }

        public UndoRedoAction(BoardItem item, UndoRedoType type, object ParamA, object ParamB, object ParamC)
            : this(item, type, ParamA, ParamB)
        {
            this.ParamC = ParamC;
        }

        public void UndoRedo(HashSet<int> layersToRecheck)
        {
            Board board;
            switch (type)
            {
                case UndoRedoType.ItemDeleted:
                    //item.Board.BoardItems.Add(item, true);
                    item.InsertItem();
                    break;
                case UndoRedoType.ItemAdded:
                    item.RemoveItem(null);
                    break;
                case UndoRedoType.ItemMoved:
                    Point oldPos = (Point)ParamA;
                    item.Move(oldPos.X, oldPos.Y);
                    break;
                case UndoRedoType.ItemFlipped:
                    ((IFlippable)item).Flip = !((IFlippable)item).Flip;
                    break;
                case UndoRedoType.LineRemoved:
                    board = ((MapleDot)ParamB).Board;
                    if (ParamC is FootholdLine)
                        board.BoardItems.FootholdLines.Add((FootholdLine)ParamC);
                    else if (ParamC is RopeLine)
                        board.BoardItems.RopeLines.Add((RopeLine)ParamC);
                    else throw new Exception("wrong type at undoredo, lineremoved");
                    ((MapleLine)ParamC).FirstDot = (MapleDot)ParamA;
                    ((MapleLine)ParamC).SecondDot = (MapleDot)ParamB;
                    ((MapleDot)ParamA).connectedLines.Add((MapleLine)ParamC);
                    ((MapleDot)ParamB).connectedLines.Add((MapleLine)ParamC);
                    break;
                case UndoRedoType.LineAdded:
                    board = ((MapleDot)ParamB).Board;
                    if (ParamC is FootholdLine)
                        board.BoardItems.FootholdLines.Remove((FootholdLine)ParamC);
                    else if (ParamC is RopeLine)
                        board.BoardItems.RopeLines.Remove((RopeLine)ParamC);
                    else throw new Exception("wrong type at undoredo, lineadded");
                    ((MapleLine)ParamC).Remove(false, null);
                    break;
                case UndoRedoType.ToolTipLinked:
                    ((ToolTip)item).CharacterToolTip = null;
                    ((ToolTipChar)ParamA).BoundTooltip = null;
                    break;
                case UndoRedoType.ToolTipUnlinked:
                    ((ToolTipChar)ParamA).BoundTooltip = (ToolTip)item;
                    break;
                case UndoRedoType.BackgroundMoved:
                    ((BackgroundInstance)item).BaseX = ((Point)ParamA).X;
                    ((BackgroundInstance)item).BaseY = ((Point)ParamA).Y;
                    break;
                case UndoRedoType.ItemsLinked:
                    item.ReleaseItem((BoardItem)ParamA);
                    break;
                case UndoRedoType.ItemsUnlinked:
                    item.BindItem((BoardItem)ParamA, (Microsoft.Xna.Framework.Point)ParamB);
                    break;
                case UndoRedoType.ItemsLayerChanged:
                    InputHandler.ClearSelectedItems(((BoardItem)((List<IContainsLayerInfo>)ParamC)[0]).Board);
                    foreach (IContainsLayerInfo layerInfoItem in (List<IContainsLayerInfo>)ParamC)
                        layerInfoItem.LayerNumber = (int)ParamA;
                    ((BoardItem)((List<IContainsLayerInfo>)ParamC)[0]).Board.Layers[(int)ParamA].RecheckTileSet();
                    ((BoardItem)((List<IContainsLayerInfo>)ParamC)[0]).Board.Layers[(int)ParamB].RecheckTileSet();
                    break;
                case UndoRedoType.ItemLayerPlatChanged:
                    Tuple<int, int> oldLayerPlat = (Tuple<int, int>)ParamA;
                    Tuple<int, int> newLayerPlat = (Tuple<int, int>)ParamB;
                    IContainsLayerInfo li = (IContainsLayerInfo)ParamC;
                    li.LayerNumber = oldLayerPlat.Item1;
                    li.PlatformNumber = oldLayerPlat.Item2;
                    layersToRecheck.Add(oldLayerPlat.Item1);
                    layersToRecheck.Add(newLayerPlat.Item1);
                    break;
                case UndoRedoType.RopeAdded:
                    ((Rope)ParamA).Remove(null);
                    break;
                case UndoRedoType.RopeRemoved:
                    ((Rope)ParamA).Create();
                    break;
                case UndoRedoType.ItemZChanged:
                    item.Z = (int)ParamA;
                    item.Board.BoardItems.Sort();
                    break;
                case UndoRedoType.VRChanged:
                    //TODO
                    break;
                case UndoRedoType.MapCenterChanged:
                    //TODO
                    break;
                case UndoRedoType.LayerTSChanged:
                    string ts_old = (string)ParamA;
                    string ts_new = (string)ParamB;
                    Layer l = (Layer)ParamC;
                    l.ReplaceTS(ts_old);
                    break;
                case UndoRedoType.zMChanged:
                    int zm_old = (int)ParamA;
                    int zm_new = (int)ParamB;
                    IContainsLayerInfo target = (IContainsLayerInfo)ParamC;
                    target.PlatformNumber = zm_old;
                    break;
            }
        }


        public void SwitchAction()
        {
            switch (type)
            {
                case UndoRedoType.ItemAdded:
                    type = UndoRedoType.ItemDeleted;
                    break;
                case UndoRedoType.ItemDeleted:
                    type = UndoRedoType.ItemAdded;
                    break;
                case UndoRedoType.LineAdded:
                    type = UndoRedoType.LineRemoved;
                    break;
                case UndoRedoType.LineRemoved:
                    type = UndoRedoType.LineAdded;
                    break;
                case UndoRedoType.ToolTipLinked:
                    type = UndoRedoType.ToolTipUnlinked;
                    break;
                case UndoRedoType.ToolTipUnlinked:
                    type = UndoRedoType.ToolTipLinked;
                    break;
                case UndoRedoType.ItemsLinked:
                    type = UndoRedoType.ItemsUnlinked;
                    break;
                case UndoRedoType.ItemsUnlinked:
                    type = UndoRedoType.ItemsLinked;
                    break;
                case UndoRedoType.RopeAdded:
                    type = UndoRedoType.RopeRemoved;
                    break;
                case UndoRedoType.RopeRemoved:
                    type = UndoRedoType.RopeAdded;
                    break;
                case UndoRedoType.ItemsLayerChanged:
                case UndoRedoType.ItemLayerPlatChanged:
                case UndoRedoType.BackgroundMoved:
                case UndoRedoType.ItemMoved:
                case UndoRedoType.MapCenterChanged:
                case UndoRedoType.ItemZChanged:
                case UndoRedoType.VRChanged:
                case UndoRedoType.LayerTSChanged:
                case UndoRedoType.zMChanged:
                    object ParamBTemp = ParamB;
                    object ParamATemp = ParamA;
                    ParamA = ParamBTemp;
                    ParamB = ParamATemp;
                    break;
                case UndoRedoType.ItemFlipped:
                    break;
            }
        }
    }

    public enum UndoRedoType
    {
        ItemDeleted,
        ItemAdded,
        ItemMoved,
        ItemFlipped,
        LineRemoved,
        LineAdded,
        ToolTipLinked,
        ToolTipUnlinked,
        BackgroundMoved,
        ItemsUnlinked,
        ItemsLinked,
        ItemsLayerChanged,
        ItemLayerPlatChanged,
        RopeRemoved,
        RopeAdded,
        ItemZChanged,
        VRChanged,
        MapCenterChanged,
        LayerTSChanged,
        zMChanged
    }
}
