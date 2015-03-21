/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapleLib.WzLib.WzStructure.Data;
using MapleLib.WzLib.WzStructure;

namespace HaCreator.MapEditor
{
    public class Board
    {
        private Point mapSize;
        //private Point maxMapSize;
        private Point centerPoint;
        private BoardItemsManager boardItems;
        private List<Layer> layers = new List<Layer>();
        private List<BoardItem> selected = new List<BoardItem>();
        private MultiBoard parent;
        private Mouse mouse;
        private MapInfo mapInfo = new MapInfo();
        private System.Drawing.Bitmap miniMap;
        private Texture2D miniMapTexture;
        private int selectedLayerIndex = ApplicationSettings.lastDefaultLayer;
        private int selectedPlatform = -1;
        private int _hScroll = 0;
        private int _vScroll = 0;
        private int _mag = 16;
        private UndoRedoManager undoRedoMan;
        ItemTypes visibleTypes;
        ItemTypes editedTypes;
        private bool loading = false;

        public ItemTypes VisibleTypes { get { return visibleTypes; } set { visibleTypes = value; } }
        public ItemTypes EditedTypes { get { return editedTypes; } set { editedTypes = value; } }

        public Board(Point mapSize, Point centerPoint, MultiBoard parent, ItemTypes visibleTypes, ItemTypes editedTypes)
        {
            this.mapSize = mapSize;
            this.centerPoint = centerPoint;
            this.parent = parent;
            this.visibleTypes = visibleTypes;
            this.editedTypes = editedTypes;

            //this.maxMapSize = mapSize;
            
            boardItems = new BoardItemsManager(this);
            undoRedoMan = new UndoRedoManager(this);
            parent.Boards.Add(this);
            mouse = new Mouse(this);
        }

        public void RenderList(IMapleList list, SpriteBatch sprite, int xShift, int yShift)
        {
            SelectionInfo sel = GetUserSelectionInfo();
            if (list.ListType == ItemTypes.None)
            {
                foreach (BoardItem item in list)
                {
                    if (parent.IsItemInRange(item.X, item.Y, item.Width, item.Height, xShift - item.Origin.X, yShift - item.Origin.Y) && ((visibleTypes & item.Type) == item.Type))
                        item.Draw(sprite, item.GetColor(sel, item.Selected), xShift, yShift);
                }
            }
            else if ((visibleTypes & list.ListType) == list.ListType)
            {
                if (list.Selectable)
                {
                    foreach (BoardItem item in list)
                    {
                        if (parent.IsItemInRange(item.X, item.Y, item.Width, item.Height, xShift - item.Origin.X, yShift - item.Origin.Y))
                            item.Draw(sprite, item.GetColor(sel, item.Selected), xShift, yShift);
                    }
                }
                else
                {
                    foreach (MapleLine line in list)
                    {
                        if (parent.IsItemInRange(line.FirstDot.X, line.FirstDot.Y, Math.Abs(line.FirstDot.X - line.SecondDot.X), Math.Abs(line.FirstDot.Y - line.SecondDot.Y), xShift, yShift))
                            line.Draw(sprite, line.GetColor(sel), xShift, yShift);
                    }
                }
            }
        }

        public static System.Drawing.Bitmap ResizeImage(System.Drawing.Bitmap FullsizeImage, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
        {
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (OnlyResizeIfWider)
            {
                if (FullsizeImage.Width <= NewWidth)
                {
                    NewWidth = FullsizeImage.Width;
                }
            }

            int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
            if (NewHeight > MaxHeight)
            {
                NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
                NewHeight = MaxHeight;
            }
            System.Drawing.Bitmap NewImage = (System.Drawing.Bitmap)FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);
            return NewImage;
        }


        public bool RegenerateMinimap()
        {
            try
            {
                lock (parent)
                {
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(mapSize.X, mapSize.Y);
                    System.Drawing.Graphics processor = System.Drawing.Graphics.FromImage(bmp);
                    foreach (BoardItem item in BoardItems.TileObjs)
                        processor.DrawImage(item.Image, new System.Drawing.Point(item.X + centerPoint.X - item.Origin.X, item.Y + centerPoint.Y - item.Origin.Y));
                    System.Drawing.Bitmap minimap = null;
                    minimap = ResizeImage(bmp, bmp.Width / 16, bmp.Height, false);
                    MiniMap = minimap;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void RenderBoard(SpriteBatch sprite)
        {
            if (mapInfo == null) return;
            int xShift = centerPoint.X - hScroll;
            int yShift = centerPoint.Y - vScroll;

            for (int i = 0; i < boardItems.AllItemLists.Length; i++)
                RenderList(boardItems.AllItemLists[i], sprite, xShift, yShift);

            if (mouse.MultiSelectOngoing)
            {
                Rectangle selectionRect = InputHandler.CreateRectangle(
                    new Point(MultiBoard.VirtualToPhysical(mouse.MultiSelectStart.X, centerPoint.X, hScroll, 0), MultiBoard.VirtualToPhysical(mouse.MultiSelectStart.Y, centerPoint.Y, vScroll, 0)),
                    new Point(MultiBoard.VirtualToPhysical(mouse.X, centerPoint.X, hScroll, 0), MultiBoard.VirtualToPhysical(mouse.Y, centerPoint.Y, vScroll, 0)));
                parent.DrawRectangle(sprite, selectionRect, UserSettings.SelectSquare);
                selectionRect.X++;
                selectionRect.Y++;
                selectionRect.Width--;
                selectionRect.Height--;
                parent.FillRectangle(sprite, selectionRect, UserSettings.SelectSquareFill);
            }
            if (UserSettings.showVR && mapInfo.VR != null)
                parent.DrawRectangle(sprite, new Rectangle(
                    MultiBoard.VirtualToPhysical(mapInfo.VR.Value.X, centerPoint.X, hScroll, 0),
                    MultiBoard.VirtualToPhysical(mapInfo.VR.Value.Y, centerPoint.Y, vScroll, 0),
                    mapInfo.VR.Value.Width, mapInfo.VR.Value.Height), UserSettings.VRColor);
            if (miniMap != null && UserSettings.useMiniMap) //here comes the cool part ^_^
            {
                parent.FillRectangle(sprite, new Rectangle(0, 0, miniMap.Width, miniMap.Height), Color.Gray);
                if (miniMapTexture == null) miniMapTexture = BoardItem.TextureFromBitmap(parent.Device, miniMap);
                sprite.Draw(miniMapTexture, new Rectangle(0, 0, miniMap.Width, miniMap.Height), null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0.99999f);
                int x = hScroll / 16;
                int y = vScroll / 16;
                parent.DrawRectangle(sprite, new Rectangle(x, y, parent.Width / _mag, parent.Height / _mag), Color.Blue);
                parent.DrawLine(sprite, new Vector2(miniMap.Width + 1, 0), new Vector2(miniMap.Width + 1, miniMap.Height), Color.Black);
                parent.DrawLine(sprite, new Vector2(0, miniMap.Height), new Vector2(miniMap.Width + 1, miniMap.Height), Color.Black);
            }
        }

        public void CreateLayers(int num)
        {
            for (int i = 0; i < num; i++)
                new Layer(this);
        }

        public void Dispose()
        {
            parent.Boards.Remove(this);
            boardItems.Clear();
            selected.Clear();
            layers.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #region Properties

        public UndoRedoManager UndoRedoMan
        {
            get { return undoRedoMan; }
        }

        public int mag
        {
            get { return _mag; }
            set { lock (parent) { _mag = value; } }
        }

        public MapInfo MapInfo
        {
            get { return mapInfo; }
            set 
            { 
                lock (parent) 
                { 
                    mapInfo = value; 
                    /*if (mapInfo.VR.HasValue) 
                    {
                        int offsX = (mapInfo.VR.Value.Left - 100 + centerPoint.X) > 0 ? mapInfo.VR.Value.Left - 100 + centerPoint.X : 0;
                        int offsY = (mapInfo.VR.Value.Top - 100 + centerPoint.Y) > 0 ? mapInfo.VR.Value.Top - 100 + centerPoint.Y : 0;
                        maxMapSize = new Point(Math.Max(mapSize.X, mapInfo.VR.Value.Width + 100 - offsX), Math.Max(mapSize.Y, mapInfo.VR.Value.Height + 100 - offsY));
                    }*/
                } 
            }
        }

        public System.Drawing.Bitmap MiniMap
        {
            get { return miniMap; }
            set { lock (parent) { miniMap = value; miniMapTexture = null; } }
        }

        public int hScroll
        {
            get
            {
                return _hScroll;
            }
            set
            {
                lock (parent)
                {
                    _hScroll = value;
                    parent.SetHScrollbarValue(_hScroll);
                }
            }
        }

        public Point CenterPoint
        {
            get { return centerPoint; }
        }

        public int vScroll
        {
            get
            {
                return _vScroll;
            }
            set
            {
                lock (parent)
                {
                    _vScroll = value;
                    parent.SetVScrollbarValue(_vScroll);
                }
            }
        }

        public MultiBoard ParentControl
        {
            get
            {
                return parent;
            }
        }

        public Mouse Mouse
        {
            get { return mouse; }
        }

        public Point MapSize
        {
            get
            {
                //return maxMapSize;
                return mapSize;
            }
        }

        public Point MiniMapSize
        {
            get
            {
                return mapSize;
            }
        }

        public BoardItemsManager BoardItems
        {
            get
            {
                return boardItems;
            }
        }

        public List<BoardItem> SelectedItems
        {
            get
            {
                return selected;
            }
        }

        public List<Layer> Layers
        {
            get
            {
                return layers;
            }
        }

        public int SelectedLayerIndex
        {
            get
            {
                return selectedLayerIndex;
            }
            set
            {
                lock (parent)
                {
                    selectedLayerIndex = value;
                }
            }
        }

        public Layer SelectedLayer
        {
            get { return SelectedLayerIndex == -1 ? null : Layers[SelectedLayerIndex]; }
        }

        public int SelectedPlatform
        {
            get { return selectedPlatform; }
            set { selectedPlatform = value; }
        }

        public SelectionInfo GetUserSelectionInfo()
        {
            return new SelectionInfo(selectedLayerIndex, selectedPlatform, visibleTypes, editedTypes);
        }
        #endregion

        public bool Loading { get { return loading; } set { loading = value; } }
    }

    public abstract class BoardItem
    {
        protected Vector3 position;
        private Hashtable boundItems = new Hashtable();//key = BoardItem; value = point (distance)
        private List<BoardItem> boundItemsList = new List<BoardItem>();
        private BoardItem parent = null;
        private bool selected = false;
        protected Board board;

        /*temporary fields used by other functions*/
        public BoardItem tempParent = null; //for mouse drag-drop
        public Point moveStartPos = new Point(); //for undo of drag-drop

        public BoardItem(Board board, int x, int y, int z)
        {
            position = new Vector3(x, y, z);
            this.board = board;
        }

        #region Methods
        public virtual void InsertItem()
        {
            lock (Board.ParentControl)
            {
                Board.BoardItems.Add(this, true);
            }
        }

        public virtual void RemoveItem(List<UndoRedoAction> undoPipe)
        {
            lock (Board.ParentControl)
            {
                object[] keys = new object[boundItems.Keys.Count];
                boundItems.Keys.CopyTo(keys, 0);
                foreach (object key in keys) ((BoardItem)key).RemoveItem(undoPipe);

                if (undoPipe != null)
                {
                    undoPipe.Add(UndoRedoManager.ItemDeleted(this));
                }
                if (parent != null)
                {
                    if (!(parent is Mouse) && undoPipe != null)
                    {
                        undoPipe.Add(UndoRedoManager.ItemsUnlinked(parent, this, (Microsoft.Xna.Framework.Point)parent.boundItems[this]));
                    }
                    parent.ReleaseItem(this);
                }
                Selected = false;
                board.BoardItems.Remove(this);
            }
        }

        public static Texture2D TextureFromBitmap(GraphicsDevice device, System.Drawing.Bitmap bitmap)
        {
            Texture2D texture;
            using (System.IO.MemoryStream s = new System.IO.MemoryStream())
            {
                bitmap.Save(s, System.Drawing.Imaging.ImageFormat.Png);
                s.Seek(0, System.IO.SeekOrigin.Begin);
                texture = Texture2D.FromStream(device, s);
                //texture = Texture2D.FromFile(device, s);
            }
            return texture;
        }

        public virtual void BindItem(BoardItem item, Point distance)
        {
            lock (Board.ParentControl)
            {
                if (boundItems.Contains(item)) return;
                boundItems[item] = distance;
                boundItemsList.Add(item);
                item.parent = this;
            }
        }

        public virtual void ReleaseItem(BoardItem item)
        {
            lock (Board.ParentControl)
            {
                if (boundItems.Contains(item))
                {
                    boundItems.Remove(item);
                    boundItemsList.Remove(item);
                    item.parent = null;
                }
            }
        }

        public virtual void Move(int x, int y)
        {
            lock (Board.ParentControl)
            {
                position.X = x;
                position.Y = y;
                object[] keys = new object[boundItems.Keys.Count];
                boundItems.Keys.CopyTo(keys, 0);
                foreach (object key in keys)
                {
                    object value = boundItems[key];
                    ((BoardItem)key).Move(((Point)value).X + x, ((Point)value).Y + y);
                }
                if (this.parent != null && !(parent is Mouse))
                {
                    parent.boundItems[this] = new Point(this.X - parent.X, this.Y - parent.Y);
                }
                if (this.tempParent != null && !tempParent.Selected) //to fix a certain mouse selection bug
                {
                    tempParent.boundItems[this] = new Point(this.X - tempParent.X, this.Y - tempParent.Y);
                }
            }
        }

        public void SnapMove(int x, int y)
        {
            lock (Board.ParentControl)
            {
                position.X = x;
                position.Y = y;
                object[] keys = new object[boundItems.Keys.Count];
                boundItems.Keys.CopyTo(keys, 0);
                foreach (object key in keys)
                {
                    object value = boundItems[key];
                    ((BoardItem)key).Move(((Point)value).X + x, ((Point)value).Y + y);
                }
                if (this.tempParent != null && !tempParent.Selected) //to fix a certain mouse selection bug
                {
                    tempParent.boundItems[this] = new Point(this.X - tempParent.X, this.Y - tempParent.Y);
                }
            }
        }

        public abstract void Draw(SpriteBatch sprite, Color color, int xShift, int yShift);
        public virtual bool CheckIfLayerSelected(SelectionInfo sel)
        {
            // By default, item is nonlayered
            return true;
        }
        public virtual Color GetColor(SelectionInfo sel, bool selected)
        {
            if ((sel.editedTypes & Type) == Type && CheckIfLayerSelected(sel))
                return selected ? UserSettings.SelectedColor : Color.White;
            else return MultiBoard.InactiveColor;
        }

        public virtual bool IsPixelTransparent(int x, int y)
        {
            lock (Board.ParentControl)
            {
                System.Drawing.Bitmap image = this.Image;
                if (this is IFlippable && ((IFlippable)this).Flip)
                    x = image.Width - x;
                return image.GetPixel(x, y).A == 0;
            }
        }

        public bool BoundToSelectedItem(Board board)
        {
            lock (Board.ParentControl)
            {
                BoardItem currItem = Parent;
                while (currItem != null)
                {
                    if (board.SelectedItems.Contains(currItem)) return true;
                    else currItem = currItem.Parent;
                }
            }
            return false;
        }
        #endregion

        #region Properties
        public abstract System.Drawing.Bitmap Image { get; }
        public abstract System.Drawing.Point Origin { get; }
        public abstract ItemTypes Type { get; }
        public abstract MapleDrawableInfo BaseInfo { get; }

        public abstract int Width { get; }
        public abstract int Height { get; }
        public virtual int X
        {
            get
            {
                return (int)position.X;
            }
            set
            {
                Move(value, (int)position.Y);
            }
        }

        public virtual int Y
        {
            get
            {
                return (int)position.Y;
            }
            set
            {
                Move((int)position.X, value);
            }
        }

        public virtual int Z
        {
            get
            {
                return (int)position.Z;
            }
            set
            {
                position.Z = Math.Max(0, value);
                /*if (this is LayeredItem || this is BackgroundInstance)
                    board.BoardItems.Sort();*/
            }
        }

        public virtual int Left
        {
            get { return (int)X - Origin.X; }
        }

        public virtual int Top
        {
            get { return (int)Y - Origin.Y; }
        }

        public virtual int Right
        {
            get { return (int)X - Origin.X + Width; }
        }

        public virtual int Bottom
        {
            get { return (int)Y - Origin.Y + Height; }
        }

        public virtual bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                lock (Board.ParentControl)
                {
                    if (selected == value) return;
                    selected = value;
                    if (value && !board.SelectedItems.Contains(this))
                        board.SelectedItems.Add(this);
                    else if (!value && board.SelectedItems.Contains(this))
                        board.SelectedItems.Remove(this);
                    if (board.SelectedItems.Count == 1)
                        board.ParentControl.OnSelectedItemChanged(board.SelectedItems[0]);
                    else if (board.SelectedItems.Count == 0)
                        board.ParentControl.OnSelectedItemChanged(null);
                }
            }
        }

        public Board Board
        {
            get
            {
                return board;
            }
        }

        public virtual Hashtable BoundItems
        {
            get
            {
                return boundItems;
            }
        }

        public virtual List<BoardItem> BoundItemsList
        {
            get
            {
                return boundItemsList;
            }
        }

        public virtual BoardItem Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        #endregion
    }
}
