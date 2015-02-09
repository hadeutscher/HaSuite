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
        private Point centerPoint;
        private BoardItemsManager boardItems = new BoardItemsManager();
        private List<Layer> layers = new List<Layer>();
        private List<BoardItem> selected = new List<BoardItem>();
        private MultiBoard parent;
        private Mouse mouse;
        private MapInfo mapInfo = new MapInfo();
        private System.Drawing.Bitmap miniMap;
        private Texture2D miniMapTexture;
        private int selectedLayerIndex = ApplicationSettings.lastDefaultLayer;
        private int _hScroll = 0;
        private int _vScroll = 0;
        private int _mag = 16;
        private UndoRedoManager undoRedoMan;
        ItemTypes visibleTypes;
        ItemTypes editedTypes;

        public ItemTypes VisibleTypes { get { return visibleTypes; } set { visibleTypes = value; } }
        public ItemTypes EditedTypes { get { return editedTypes; } set { editedTypes = value; } }

        public Board(Point mapSize, Point centerPoint, MultiBoard parent, ItemTypes visibleTypes, ItemTypes editedTypes)
        {
            this.mapSize = mapSize;
            this.centerPoint = centerPoint;
            this.parent = parent;
            this.visibleTypes = visibleTypes;
            this.editedTypes = editedTypes;
            undoRedoMan = new UndoRedoManager(this);
            parent.Boards.Add(this);
            mouse = new Mouse(this);
        }

        public void RenderList(IMapleList list, SpriteBatch sprite, int xShift, int yShift)
        {
            if (list.ListType == ItemTypes.None)
            {
                foreach (BoardItem item in list)
                    if (parent.IsItemInRange(item.X, item.Y, item.Width, item.Height, xShift - item.Origin.X, yShift - item.Origin.Y) && ((visibleTypes & item.Type) == item.Type))
                        item.Draw(sprite, item.GetColor(editedTypes, selectedLayerIndex, item.Selected), xShift, yShift);
            }
            else if ((visibleTypes & list.ListType) == list.ListType)
            {
                if (list.Selectable)
                {
                    foreach (BoardItem item in list)
                        if (parent.IsItemInRange(item.X, item.Y, item.Width, item.Height, xShift - item.Origin.X, yShift - item.Origin.Y))
                            item.Draw(sprite, item.GetColor(editedTypes, selectedLayerIndex, item.Selected), xShift, yShift);
                }
                else
                {
                    foreach (MapleLine line in list)
                        line.Draw(sprite, line.GetColor(editedTypes, selectedLayerIndex), xShift, yShift);
                }
            }
        }

/*        public void RenderBackgroundList(SpriteBatch sprite, int xShift, int yShift, bool front)
        {
            if (!((ApplicationSettings.visibleTypes & ItemTypes.Backgrounds) == ItemTypes.Backgrounds)) return;
            foreach (BackgroundInstance bg in boardItems.Backgrounds)
                if (bg.front == front && parent.IsItemInRange(bg.X, bg.Y, bg.Width, bg.Height,xShift - bg.Origin.X,yShift - bg.Origin.Y))
                    bg.Draw(sprite, bg.GetColor(ApplicationSettings.editedTypes, selectedLayerIndex, bg.Selected), xShift, yShift);
        }*/

        public System.Drawing.Bitmap ResizeImage(System.Drawing.Bitmap FullsizeImage, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
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
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(mapSize.X, mapSize.Y);
                System.Drawing.Graphics processor = System.Drawing.Graphics.FromImage(bmp);
                foreach (BoardItem item in BoardItems.TileObjs)
                    processor.DrawImage(item.Image, new System.Drawing.Point(item.X + centerPoint.X - item.Origin.X, item.Y + centerPoint.Y - item.Origin.Y));
                System.Drawing.Bitmap minimap = null;
                minimap = ResizeImage(bmp, bmp.Width / 16, bmp.Height, false);
                MiniMap = minimap;
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

            /*RenderBackgroundList(sprite, xShift, yShift, false);
            RenderList(boardItems.TileObjs, sprite, xShift, yShift);
            RenderList(boardItems.Mobs, sprite, xShift, yShift);
            RenderList(boardItems.NPCs, sprite, xShift, yShift);
            //RenderList(boardItems.Lives, sprite, xShift, yShift);
            RenderList(boardItems.Reactors, sprite, xShift, yShift);
            RenderList(boardItems.Portals, sprite, xShift, yShift);
            RenderBackgroundList(sprite, xShift, yShift, true);*/
            /*if ((ApplicationSettings.visibleTypes & ItemTypes.Footholds) == ItemTypes.Footholds) 
                foreach (FootholdLine fh in footholdLines) fh.Draw(sprite, fh.GetColor(ApplicationSettings.editedTypes, selectedLayerIndex), xShift, yShift);
            if ((ApplicationSettings.visibleTypes & ItemTypes.Ropes) == ItemTypes.Ropes) 
                foreach (RopeLine rope in ropeLines) rope.Draw(sprite, rope.GetColor(ApplicationSettings.editedTypes, selectedLayerIndex), xShift, yShift);*/
            /*RenderList(boardItems.FHAnchors, sprite, xShift, yShift);
            RenderList(boardItems.RopeAnchors, sprite, xShift, yShift);
            RenderList(boardItems.Chairs, sprite, xShift, yShift);
            RenderList(boardItems.CharacterToolTips, sprite, xShift, yShift);
            RenderList(boardItems.ToolTips, sprite, xShift, yShift);
            RenderList(boardItems.ToolTipDots, sprite, xShift, yShift);*/
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
            if (miniMap != null && UserSettings.useMiniMap)//here comes the cool part ^_^
            {
                parent.FillRectangle(sprite, new Rectangle(0, 0, miniMap.Width, miniMap.Height), Color.Gray);
                if (miniMapTexture == null) miniMapTexture = BoardItem.TextureFromBitmap(parent.Device, miniMap);
                sprite.Draw(miniMapTexture, new Rectangle(0, 0, miniMap.Width, miniMap.Height), null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0.99999f);
                int x = hScroll / 16;
                int y = vScroll / 16;
                parent.DrawRectangle(sprite, new Rectangle(x, y, parent.Width / _mag, parent.Height / _mag), Color.Blue);
                //parent.DrawRectangle(sprite, new Rectangle(0, 0, miniMap.Width, miniMap.Height), Color.Black);
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
/*        public List<Rope> Ropes
        {
            get { return ropes; }
        }

        public List<FootholdLine> FootholdLines
        {
            get { return footholdLines; }
        }

        public List<RopeLine> RopeLines
        {
            get { return ropeLines; }
        }*/

        /*public List<ToolTip> ToolTips
        {
            get { return tooltips; }
        }*/

        public UndoRedoManager UndoRedoMan
        {
            get { return undoRedoMan; }
        }

        public int mag
        {
            get { return _mag; }
            set { _mag = value; }
        }

        public MapInfo MapInfo
        {
            get { return mapInfo; }
            set { mapInfo = value; }
        }

        public System.Drawing.Bitmap MiniMap
        {
            get { return miniMap; }
            set { miniMap = value; miniMapTexture = null; }
        }

        public int hScroll
        {
            get
            {
                return _hScroll;
            }
            set
            {
                _hScroll = value;
                parent.SetHScrollbarValue(_hScroll);
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
                _vScroll = value;
                parent.SetVScrollbarValue(_vScroll);
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

/*        public ItemTypes ApplicationSettings.editedTypes
        {
            get
            {
                return ApplicationSettings.editedTypes;
            }
            set
            {
                ApplicationSettings.editedTypes = value;
            }
        }

        public ItemTypes ApplicationSettings.visibleTypes
        {
            get
            {
                return ApplicationSettings.visibleTypes;
            }
            set
            {
                ApplicationSettings.visibleTypes = value;
            }
        }*/

        public int SelectedLayerIndex
        {
            get
            {
                return selectedLayerIndex;
            }
            set
            {
                selectedLayerIndex = value;
            }
        }

        public Layer SelectedLayer
        {
            get { return Layers[SelectedLayerIndex]; }
        }
        #endregion
    }

    public abstract class BoardItem
    {
        protected Vector3 position;
        private Hashtable boundItems = new Hashtable();//key = point (distance); value = BoardItem
        private BoardItem parent = null;
        private bool selected = false;
        private Board board;
        private bool beforeAdding;

        /*temporary fields used by other functions*/
        public BoardItem tempParent = null; //for mouse drag-drop
        public Point moveStartPos = new Point(); //for undo of drag-drop

        public BoardItem(Board board, int x, int y, int z, bool beforeAdding)
        {
            position = new Vector3(x, y, z);
            this.board = board;
            this.beforeAdding = beforeAdding;
        }

        #region Methods
        public virtual void InsertItem()
        {
            Board.BoardItems.Add(this, true);
        }

        public virtual void RemoveItem(ref List<UndoRedoAction> undoPipe)
        {
            object[] keys = new object[boundItems.Keys.Count];
            boundItems.Keys.CopyTo(keys, 0);
            foreach (object key in keys) ((BoardItem)key).RemoveItem(ref undoPipe);

            undoPipe.Add(UndoRedoManager.ItemDeleted(this));
            if (parent != null)
            {
                if (!(parent is Mouse))
                    undoPipe.Add(UndoRedoManager.ItemsUnlinked(parent, this, (Microsoft.Xna.Framework.Point)parent.boundItems[this]));
                parent.ReleaseItem(this);
            }
            Selected = false;
            board.BoardItems.Remove(this);
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
            if (boundItems.Contains(item)) return;
            boundItems[item] = distance;
            item.parent = this;
        }

        public virtual void ReleaseItem(BoardItem item)
        {
            if (boundItems.Contains(item))
            {
                boundItems.Remove(item);
                item.parent = null;
            }
        }

        public virtual void Move(int x, int y)
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

        public void SnapMove(int x, int y)
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

        public abstract void Draw(SpriteBatch sprite, Color color, int xShift, int yShift);
        public abstract bool CheckIfLayerSelected(int selectedLayer);
        public virtual Color GetColor(ItemTypes editedTypes, int selectedLayer, bool selected)
        {
            if (((editedTypes & Type) == Type && (selectedLayer == -1 || CheckIfLayerSelected(selectedLayer))))
                return selected ? UserSettings.SelectedColor : Color.White;
            else return MultiBoard.InactiveColor;
        }

        public virtual bool IsPixelTransparent(int x, int y)
        {
            System.Drawing.Bitmap image = this.Image;
            if (this is IFlippable && ((IFlippable)this).Flip)
                x = image.Width - x;
            return image.GetPixel(x, y).A == 0;
        }

/*        public virtual bool IsRectrangleTransparent(int x1, int y1, int x2, int y2)
        {
            if (x2 == x1 || y2 == y1) return true;
            this.Image.Save(@"D:\rofl.png", System.Drawing.Imaging.ImageFormat.Png);
            System.Drawing.Imaging.BitmapData data = this.Image.LockBits(new System.Drawing.Rectangle(x1, y1, x2 - x1, y2 - y1), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            byte[] dataBytes = new byte[data.Height * data.Stride];
            Marshal.Copy(data.Scan0,dataBytes,0,dataBytes.Length);
            for (int iy = 0; iy < data.Height; iy++)
                for (int ix = 0; ix < data.Width; ix++)
                    if (dataBytes[iy * data.Stride + ix * 4 + 3] != 0)
                    {
                        this.Image.UnlockBits(data);
                        return true;
                    }
            this.Image.UnlockBits(data);
            return false;
        }*/

        public bool BoundToSelectedItem(Board board)
        {
            BoardItem currItem = Parent;
            while (currItem != null)
            {
                if (board.SelectedItems.Contains(currItem)) return true;
                else currItem = currItem.Parent;
            }
            return false;
        }
        #endregion

        #region Properties
        public abstract System.Drawing.Bitmap Image { get; }
        public abstract System.Drawing.Point Origin { get; }
        public abstract ItemTypes Type { get; }
        public abstract MapleDrawableInfo BaseInfo { get; }

        public virtual int Width { get { return Image.Width; } }
        public virtual int Height { get { return Image.Height; } }
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

        public virtual bool BeforeAdding
        {
            get
            {
                return beforeAdding;
            }
            set
            {
                beforeAdding = value;
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

        public virtual BoardItem Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        #endregion
    }
}
