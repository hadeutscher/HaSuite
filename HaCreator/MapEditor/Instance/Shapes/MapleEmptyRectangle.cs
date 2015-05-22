using HaCreator.MapEditor.UndoRedo;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNA = Microsoft.Xna.Framework;

namespace HaCreator.MapEditor.Instance.Shapes
{
    public abstract class MapleEmptyRectangle
    {
        //clockwise, beginning in upper-left
        private MapleDot a;
        private MapleDot b;
        private MapleDot c;
        private MapleDot d;

        private MapleLine ab;
        private MapleLine bc;
        private MapleLine cd;
        private MapleLine da;

        protected Board board;

        public MapleEmptyRectangle(Board board)
        {
            this.board = board;
        }

        public MapleDot PointA
        {
            get { return a; }
            set { a = value; }
        }

        public MapleDot PointB
        {
            get { return b; }
            set { b = value; }
        }

        public MapleDot PointC
        {
            get { return c; }
            set { c = value; }
        }

        public MapleDot PointD
        {
            get { return d; }
            set { d = value; }
        }

        public MapleLine LineAB
        {
            get { return ab; }
            set { ab = value; }
        }

        public MapleLine LineBC
        {
            get { return bc; }
            set { bc = value; }
        }

        public MapleLine LineCD
        {
            get { return cd; }
            set { cd = value; }
        }

        public MapleLine LineDA
        {
            get { return da; }
            set { da = value; }
        }

        public int Width
        {
            get
            {
                return a.X < b.X ? b.X - a.X : a.X - b.X;
            }
        }

        public int Height
        {
            get
            {
                return b.Y < c.Y ? c.Y - b.Y : b.Y - c.Y;
            }
        }

        public int X
        {
            get
            {
                return Math.Min(a.X, b.X);
            }
        }

        public int Y
        {
            get
            {
                return Math.Min(b.Y, c.Y);
            }
        }

        public int Left
        {
            get
            {
                return Math.Min(a.X, b.X);
            }
        }

        public int Top
        {
            get
            {
                return Math.Min(b.Y, c.Y);
            }
        }

        public int Bottom
        {
            get
            {
                return Math.Max(b.Y, c.Y);
            }
        }

        public int Right
        {
            get
            {
                return Math.Max(a.X, b.X);
            }
        }

        public virtual void RemoveItem(List<UndoRedoAction> undoPipe)
        {
            lock (board.ParentControl)
            {
                PointA.RemoveItem(undoPipe);
                PointB.RemoveItem(undoPipe);
                PointC.RemoveItem(undoPipe);
                PointD.RemoveItem(undoPipe);
            }
        }

        public virtual void Draw(SpriteBatch sprite, int xShift, int yShift)
        {
            XNA.Color lineColor = ab.Color;
            int x, y;
            if (a.X < b.X) x = a.X + xShift;
            else x = b.X + xShift;
            if (b.Y < c.Y) y = b.Y + yShift;
            else y = c.Y + yShift;
            ab.Draw(sprite, lineColor, xShift, yShift);
            bc.Draw(sprite, lineColor, xShift, yShift);
            cd.Draw(sprite, lineColor, xShift, yShift);
            da.Draw(sprite, lineColor, xShift, yShift);
        }
    }
}
