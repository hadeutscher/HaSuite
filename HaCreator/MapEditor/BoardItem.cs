using HaCreator.MapEditor.Info;
using HaCreator.MapEditor.Input;
using HaCreator.MapEditor.Instance;
using HaCreator.MapEditor.UndoRedo;
using MapleLib.WzLib.WzStructure.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.MapEditor
{
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

        public virtual void OnItemPlaced(List<UndoRedoAction> undoPipe)
        {
            lock (Board.ParentControl)
            {
                object[] keys = new object[boundItems.Keys.Count];
                boundItems.Keys.CopyTo(keys, 0);
                foreach (object key in keys)
                    ((BoardItem)key).OnItemPlaced(undoPipe);

                if (undoPipe != null)
                {
                    undoPipe.Add(UndoRedoManager.ItemAdded(this));
                }
                if (parent != null)
                {
                    if (!(parent is Mouse) && undoPipe != null)
                    {
                        undoPipe.Add(UndoRedoManager.ItemsLinked(parent, this, (Microsoft.Xna.Framework.Point)parent.boundItems[this]));
                    }
                }
            }
        }

        public virtual void RemoveItem(List<UndoRedoAction> undoPipe)
        {
            lock (Board.ParentControl)
            {
                object[] keys = new object[boundItems.Keys.Count];
                boundItems.Keys.CopyTo(keys, 0);
                foreach (object key in keys)
                    ((BoardItem)key).RemoveItem(undoPipe);

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

        public virtual void Draw(SpriteBatch sprite, Color color, int xShift, int yShift)
        {
            if (ApplicationSettings.InfoMode)
            {
                Board.ParentControl.FillRectangle(sprite, new Rectangle(this.X - UserSettings.DotWidth + xShift, this.Y - UserSettings.DotWidth + yShift, UserSettings.DotWidth * 2, UserSettings.DotWidth * 2), UserSettings.OriginColor);
            }
        }

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
