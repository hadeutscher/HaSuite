/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapleLib.WzLib.WzStructure.Data;
using MapleLib.WzLib.WzStructure;

namespace HaCreator.MapEditor
{
    public abstract class MapleDot : BoardItem, ISnappable
    {
        public MapleDot(Board board, int x, int y, bool beforeAdding)
            : base(board, x, y, -1, beforeAdding)
        {
        }

        public List<MapleLine> connectedLines = new List<MapleLine>();

        public abstract Color Color { get; }
        public abstract Color InactiveColor { get; }

        private static System.Drawing.Point origin = new System.Drawing.Point(UserSettings.DotWidth, UserSettings.DotWidth);

        public override bool IsPixelTransparent(int x, int y)
        {
            return false;
        }

        public override MapleDrawableInfo BaseInfo
        {
            get { return null; }
        }

        public override void RemoveItem(ref List<UndoRedoAction> undoPipe)
        {
            base.RemoveItem(ref undoPipe);
            if (!(this is ToolTipDot))
                while (connectedLines.Count > 0)
                {
                    connectedLines[0].Remove(false, ref undoPipe);
                }
        }

        public static System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(UserSettings.DotWidth * 2, UserSettings.DotWidth * 2);

        public override System.Drawing.Bitmap Image //yes I know that this is kind of lame to do it like that
        {
            get { return bmp; }
        }

        public override Color GetColor(ItemTypes EditedTypes, int selectedLayer, bool selected)
        {
            if (((EditedTypes & Type) == Type && (selectedLayer == -1 || CheckIfLayerSelected(selectedLayer))))
                return selected ? UserSettings.SelectedColor : Color;
            else return InactiveColor;
        }

        public override System.Drawing.Point Origin
        {
            get 
            {
                return origin;
            }
        }

        public override void Draw(SpriteBatch sprite, Color color, int xShift, int yShift)
        {
            Board.ParentControl.FillRectangle(sprite, new Rectangle(this.X - UserSettings.DotWidth + xShift, this.Y - UserSettings.DotWidth + yShift, UserSettings.DotWidth * 2, UserSettings.DotWidth * 2), color);
        }

        public void DisconnectLine(MapleLine line)
        {
            connectedLines.Remove(line);
        }

        public bool IsMoveHandled { get { return PointMoved != null; } }

        public override int X
        {
            get
            {
                return base.X;
            }
            set
            {
                base.X = value;
                if (PointMoved != null) PointMoved.Invoke();
            }
        }

        public override int Y
        {
            get
            {
                return base.Y;
            }
            set
            {
                base.Y = value;
                if (PointMoved != null) PointMoved.Invoke();
            }
        }

        public override void Move(int x, int y)
        {
            base.Move(x, y);
            if (PointMoved != null) PointMoved.Invoke();
        }

        public void MoveSilent(int x, int y)
        {
            base.Move(x, y);
        }

        public virtual void DoSnap()
        {
            if (!InputHandler.IsKeyPushedDown(System.Windows.Forms.Keys.ShiftKey) || connectedLines.Count == 0 || !(connectedLines[0] is FootholdLine)) return;
            FootholdAnchor closestAnchor = null;
            double closestAngle = double.MaxValue;
            bool xClosest = true;
            foreach (FootholdLine line in connectedLines)
            {
                FootholdAnchor otherAnchor = (FootholdAnchor)(line.FirstDot == this ? line.SecondDot : line.FirstDot);
                double xAngle = Math.Abs(Math.Atan((double)(Y - otherAnchor.Y) / (double)(X - otherAnchor.X)));
                double yAngle = Math.Abs(Math.Atan((double)(X - otherAnchor.X) / (double)(Y - otherAnchor.Y)));
                double minAngle;
                bool xSmaller = false;
                if (xAngle < yAngle) { xSmaller = true; minAngle = xAngle; }
                else { xSmaller = false; minAngle = yAngle; }
                if (minAngle < closestAngle) { xClosest = xSmaller; closestAnchor = otherAnchor; closestAngle = minAngle; }
            }
            if (closestAnchor != null)
            {
                if (xClosest)
                    Y = closestAnchor.Y;
                else
                    X = closestAnchor.X;
            }
        }

        public bool BetweenOrEquals(int value, int bounda, int boundb, int tolerance)
        {
            if (bounda < boundb)
                return (bounda - tolerance) <= value && value <= (boundb + tolerance);
            else
                return (boundb - tolerance) <= value && value <= (bounda + tolerance);
        }

        public delegate void OnPointMovedDelegate();
        public event OnPointMovedDelegate PointMoved;
    }

    public class FootholdAnchor : MapleDot, IContainsLayerInfo
    {
        private int layer;

        public bool removed;

        public FootholdAnchor(Board board, int x, int y, int layer, bool beforeAdding)
            : base(board, x, y, beforeAdding)
        {
            this.layer = layer;
        }

        public override bool CheckIfLayerSelected(int selectedLayer)
        {
            return selectedLayer == layer;
        }

        public override Color Color
        {
            get
            {
                return UserSettings.FootholdColor;
            }
        }

        public override Color InactiveColor
        {
            get { return MultiBoard.FootholdInactiveColor; }
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.Footholds; }
        }

/*        public override void RemoveItem(ref List<UndoRedoAction> undoPipe)
        {
            if (connectedLines.Count > 0)
                foreach (MapleLine line in connectedLines) 
                    undoPipe.Add(UndoRedoManager.LineRemoved(line, line.FirstDot, line.SecondDot));
            base.RemoveItem(ref undoPipe);
        }*/

        public static void MergeAnchors(ref List<FootholdAnchor> anchors)
        {
        }

        public static int FHAnchorSorter(FootholdAnchor c, FootholdAnchor d)
        {
            if (c.X > d.X)
                return 1;
            else if (c.X < d.X)
                return -1;
            else
            {
                if (c.Y > d.Y)
                    return 1;
                else if (c.Y < d.Y)
                    return -1;
                else
                {
                    if (c.LayerNumber > d.LayerNumber)
                        return 1;
                    else if (c.LayerNumber < d.LayerNumber)
                        return -1;
                    else return 0;
                }
            }
        }

        public int LayerNumber
        {
            get { return layer; }
            set { layer = value; }
        }

        //public bool keyAnchor = false; //for foothold parsing
    }


    public class RopeAnchor : MapleDot, IContainsLayerInfo, ISnappable
    {
        private Rope parentRope;

        public RopeAnchor(Board board, int x, int y, Rope parentRope, bool beforeAdding)
            : base(board, x, y, beforeAdding)
        {
            this.parentRope = parentRope;
        }

        public override bool CheckIfLayerSelected(int selectedLayer)
        {
            return selectedLayer == parentRope.page;
        }

        public override Color Color
        {
            get
            {
                return UserSettings.RopeColor;
            }
        }

        public override Color InactiveColor
        {
            get { return MultiBoard.RopeInactiveColor; }
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.Ropes; }
        }

        public int LayerNumber
        {
            get { return parentRope.page; }
            set { parentRope.page = value; }
        }

        public override void RemoveItem(ref List<UndoRedoAction> undoPipe)
        {
            parentRope.Remove(ref undoPipe);
        }

        public override void DoSnap()
        {
            FootholdLine closestLine = null;
            int closestDistance = int.MaxValue;
            foreach (FootholdLine fh in Board.BoardItems.FootholdLines)
            {
                if (!fh.IsWall && BetweenOrEquals(X, fh.FirstDot.X, fh.SecondDot.X, (int)UserSettings.SnapDistance) && BetweenOrEquals(Y, fh.FirstDot.Y, fh.SecondDot.Y, (int)UserSettings.SnapDistance))
                {
                    int targetY = fh.CalculateY(X) + 2;
                    int distance = Math.Abs(targetY - Y);
                    if (closestDistance > distance) { closestDistance = distance; closestLine = fh; }
                }
            }
            if (closestLine != null) this.Y = closestLine.CalculateY(X) + 2;
        }

        public Rope ParentRope { get { return parentRope; } }
    }

    public class Chair : MapleDot, ISnappable
    {
        public Chair(Board board, int x, int y, bool beforeAdding)
            : base(board, x, y, beforeAdding)
        {
        }

        public override bool CheckIfLayerSelected(int selectedLayer)
        {
            return true;
        }

        public override void DoSnap()
        {
            FootholdLine closestLine = null;
            int closestDistance = int.MaxValue;
            foreach (FootholdLine fh in Board.BoardItems.FootholdLines)
            {
                if (!fh.IsWall && BetweenOrEquals(X, fh.FirstDot.X, fh.SecondDot.X, (int)UserSettings.SnapDistance) && BetweenOrEquals(Y, fh.FirstDot.Y, fh.SecondDot.Y, (int)UserSettings.SnapDistance))
                {
                    int targetY = fh.CalculateY(X) - 1;
                    int distance = Math.Abs(targetY - Y);
                    if (closestDistance > distance) { closestDistance = distance; closestLine = fh; }
                }
            }
            if (closestLine != null) this.Y = closestLine.CalculateY(X) - 1;
        }

        public override Color Color
        {
            get
            {
                return UserSettings.ChairColor;
            }
        }

        public override Color InactiveColor
        {
            get { return MultiBoard.ChairInactiveColor; }
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.Chairs; }
        }
    }

    //it is important to remember that if the line is connecting mouse and a MapleDot, the mouse is ALWAYS the second dot.
    public abstract class MapleLine
    {
        private Board board;
        private MapleDot firstDot;
        private MapleDot secondDot;
        private bool beforeConnecting;
        private bool _xBind = false;
        private bool _yBind = false;

        public MapleLine(Board board, MapleDot firstDot)
        {
            this.board = board;
            this.firstDot = firstDot;
            this.firstDot.connectedLines.Add(this);
            this.secondDot = board.Mouse;
            this.secondDot.connectedLines.Add(this);
            this.beforeConnecting = true;
            firstDot.PointMoved += new MapleDot.OnPointMovedDelegate(OnFirstDotMoved);
        }

            protected MapleLine()
        {
        }

        public MapleLine(Board board, MapleDot firstDot, MapleDot secondDot)
        {
            this.board = board;
            this.firstDot = firstDot;
            this.firstDot.connectedLines.Add(this);
            this.secondDot = secondDot;
            this.secondDot.connectedLines.Add(this);
            this.beforeConnecting = false;
            firstDot.PointMoved += new MapleDot.OnPointMovedDelegate(OnFirstDotMoved);
            secondDot.PointMoved += new MapleDot.OnPointMovedDelegate(OnSecondDotMoved);
        }

        public void ConnectSecondDot(MapleDot secondDot)
        {
            if (!beforeConnecting) return;
            this.secondDot.connectedLines.Clear();
            this.secondDot = secondDot;
            this.secondDot.connectedLines.Add(this);
            secondDot.PointMoved += new MapleDot.OnPointMovedDelegate(OnSecondDotMoved);
        }

        /*public void Dispose()
        {
            this.firstDot.connectedLines.Remove(this);
            this.secondDot.connectedLines.Remove(this);
        }*/

        public virtual void Remove(bool removeDots, ref List<UndoRedoAction> undoPipe)
        {
            firstDot.DisconnectLine(this);
            secondDot.DisconnectLine(this);
            if (this is FootholdLine) board.BoardItems.FootholdLines.Remove((FootholdLine)this);
            else if (this is RopeLine) board.BoardItems.RopeLines.Remove((RopeLine)this);
            if (!(secondDot is Mouse))
                undoPipe.Add(UndoRedoManager.LineRemoved(this, firstDot, secondDot));
            if (removeDots)
            {
                firstDot.RemoveItem(ref undoPipe);
                if (secondDot != null)
                    secondDot.RemoveItem(ref undoPipe);
            }
        }

        public int CalculateY(int x)
        {
            return ((FirstDot.Y - SecondDot.Y) / (FirstDot.X - SecondDot.X)) * (x - FirstDot.X) + FirstDot.Y; // y-y1=m(x-x1) => y=(d/dx)(x-x1)+y1
        }

        public bool Selected { get { return firstDot != null && firstDot.Selected && secondDot != null && secondDot.Selected; } }

        public Board Board { get { return board; } set { board = value; } }

        public abstract Color Color { get; }
        public abstract Color InactiveColor { get; }
        public abstract ItemTypes Type { get; }

        public bool xBind
        {
            get { return _xBind; }
            set { _xBind = value; }
        }

        public bool yBind
        {
            get { return _yBind; }
            set { _yBind = value; }
        }

        public MapleDot FirstDot
        {
            get { return firstDot; }
            set { firstDot = value; }
        }

        public MapleDot SecondDot
        {
            get { return secondDot; }
            set { secondDot = value; }
        }

        public void OnFirstDotMoved()
        {
            if (secondDot.Selected) return;
            if (xBind)
                secondDot.MoveSilent(firstDot.X, secondDot.Y);
            if (yBind)
                secondDot.MoveSilent(secondDot.X, firstDot.Y);
        }

        public void OnSecondDotMoved()
        {
            if (firstDot.Selected) return;
            if (xBind)
                firstDot.MoveSilent(secondDot.X, firstDot.Y);
            if (yBind)
                firstDot.MoveSilent(firstDot.X, secondDot.Y);
        }

//        private static System.Drawing.Point origin = new System.Drawing.Point(MultiBoard.DotWidth, MultiBoard.DotWidth);
       
//        public static System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(MultiBoard.DotWidth * 2, MultiBoard.DotWidth * 2);

        public Color GetColor(ItemTypes EditedTypes, int selectedLayer)
        {
            if (((EditedTypes & Type) == Type && (selectedLayer == -1 || firstDot.CheckIfLayerSelected(selectedLayer))))
                return Color;
            else return InactiveColor;
        }

        public void Draw(SpriteBatch sprite, Color color, int xShift, int yShift)
        {
            board.ParentControl.DrawLine(sprite, new Vector2(firstDot.X + xShift, firstDot.Y + yShift), new Vector2(secondDot.X + xShift, secondDot.Y + yShift), color);
        }
    }

    public class FootholdLine : MapleLine
    {
        private MapleBool _cantThrough;
        private MapleBool _forbidFallDown;
        private int? _piece;
        private int? _force;

        //temporary variables:
        public FootholdLine cloneLine = null;
        //faster to remove all lines at once by flagging them instead of using .RemoveAt or .Remove
        public bool remove = false; 
        public int prev = 0;
        public int next = 0;
        public Foothold fh;

        internal FootholdLine() : base() { }
        public static FootholdLine CreateCustomFootholdLine(Board board, MapleDot firstDot, MapleDot secondDot)
        {
            FootholdLine result = new FootholdLine();
            result.Board = board;
            result.FirstDot = firstDot;
            result.SecondDot = secondDot;
            return result;
        }

        public FootholdLine(Board board, MapleDot firstDot, MapleDot secondDot)
            : base(board, firstDot, secondDot)
        {
            _cantThrough = null;
            _forbidFallDown = null;
            _piece = null;
            _force = null;
        }

        public FootholdLine(Board board, MapleDot firstDot)
            : base(board, firstDot)
        {
            _cantThrough = null;
            _forbidFallDown = null;
            _piece = null;
            _force = null;
        }

        public FootholdLine(Board board, MapleDot firstDot, MapleDot secondDot, MapleBool forbidFallDown, MapleBool cantThrough, int? piece, int? force)
            : base(board, firstDot, secondDot)
        {
            this._cantThrough = cantThrough;
            this._forbidFallDown = forbidFallDown;
            this._piece = piece;
            this._force = force;
        }

        public FootholdLine(Board board, MapleDot firstDot, MapleBool forbidFallDown, MapleBool cantThrough, int? piece, int? force)
            : base(board, firstDot)
        {
            this._cantThrough = cantThrough;
            this._forbidFallDown = forbidFallDown;
            this._piece = piece;
            this._force = force;
        }

        public override Color Color
        {
            get { return Selected ? UserSettings.SelectedColor : UserSettings.FootholdColor; }
        }

        public override Color InactiveColor
        {
            get { return MultiBoard.FootholdInactiveColor; }
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.Footholds; }
        }

        public bool FhEquals(FootholdLine obj)
        {
            return ((((FootholdLine)obj).FirstDot.X == FirstDot.X && ((FootholdLine)obj).SecondDot.X == SecondDot.X)
                && (((FootholdLine)obj).FirstDot.Y == FirstDot.Y && ((FootholdLine)obj).SecondDot.Y == SecondDot.Y))
                || ((((FootholdLine)obj).FirstDot.X == SecondDot.X && ((FootholdLine)obj).SecondDot.X == FirstDot.X)
                && (((FootholdLine)obj).FirstDot.Y == SecondDot.Y && ((FootholdLine)obj).SecondDot.Y == FirstDot.Y));
        }

        public static bool Exists(int x1, int y1, int x2, int y2, Board board)
        {
            foreach (FootholdLine fh in board.BoardItems.FootholdLines)
            {
                if (((fh.FirstDot.X == x1 && fh.FirstDot.Y == y1) &&
                    (fh.SecondDot.X == x2 && fh.SecondDot.Y == y2)) ||
                    ((fh.FirstDot.X == x2 && fh.FirstDot.Y == y2) &&
                    (fh.SecondDot.X == x1 && fh.SecondDot.Y == y1))) return true;
            }
            return false;
        }

        public static FootholdLine[] GetSelectedFootholds(Board board)
        {
            int length = 0;
            foreach (FootholdLine line in board.BoardItems.FootholdLines) 
                if (line.Selected) length++;
            FootholdLine[] result = new FootholdLine[length];
            int index = 0;
            foreach (FootholdLine line in board.BoardItems.FootholdLines) 
                if (line.Selected) { result[index] = line; index++; }
            return result;
        }

        public bool IsWall { get { return FirstDot.X == SecondDot.X; } }

        public int? Force { get { return _force; } set { _force = value; } }
        public int? Piece { get { return _piece; } set { _piece = value; } }
        public MapleBool ForbidFallDown { get { return _forbidFallDown; } set { _forbidFallDown = value; } }
        public MapleBool CantThrough { get { return _cantThrough; } set { _cantThrough = value; } }

        public int num; //temporary, for saving.
    }

    public class RopeLine : MapleLine
    {
        public RopeLine(Board board, MapleDot firstDot, MapleDot secondDot)
            : base(board, firstDot, secondDot)
        {
            xBind = true;
        }

        public RopeLine(Board board, MapleDot firstDot)
            : base(board, firstDot)
        {
            xBind = true;
        }

        public override Color Color
        {
            get { return UserSettings.RopeColor; }
        }

        public override Color InactiveColor
        {
            get { return MultiBoard.RopeInactiveColor; }
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.Ropes; }
        }
    }

    public struct Line
    {
        int x1, x2, y1, y2;

        public Line(int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }
    }

    public class Rope
    {
        private Board board;
        private RopeAnchor firstAnchor;
        private RopeAnchor secondAnchor;
        private RopeLine line;

        private int _page; //aka layer
        private bool _ladder;
        private bool _uf; //deciding if you can climb over the end of the rope (usually true)
                         //according to koolk it stands for "Upper Foothold"
        public Rope(Board board, int x, int y1, int y2, bool ladder, int page, bool uf)
        {
            this.board = board;
            this._page = page;
            this._ladder = ladder;
            this._uf = uf;
            this.firstAnchor = new RopeAnchor(board, x, y1, this, false);
            this.secondAnchor = new RopeAnchor(board, x, y2, this, false);
            this.line = new RopeLine(board, firstAnchor, secondAnchor);
            Create();
        }

        public void Remove(ref List<UndoRedoAction> undoPipe)
        {
            firstAnchor.Selected = false;
            secondAnchor.Selected = false;
            board.BoardItems.RopeAnchors.Remove(firstAnchor);
            board.BoardItems.RopeAnchors.Remove(secondAnchor);
            board.BoardItems.RopeLines.Remove(line);
            undoPipe.Add(UndoRedoManager.RopeRemoved(this));
        }

        public void Create()
        {
            board.BoardItems.RopeAnchors.Add(firstAnchor);
            board.BoardItems.RopeAnchors.Add(secondAnchor);
            board.BoardItems.RopeLines.Add(line);
        }

        public int page { get { return _page; } set { _page = value; } }
        public bool ladder { get { return _ladder; } set { _ladder = value; } }
        public bool uf { get { return _uf; } set { _uf = value; } }

        public RopeAnchor FirstAnchor { get { return firstAnchor; } }
        public RopeAnchor SecondAnchor { get { return secondAnchor; } }
    }

    public class ToolTipDot : MapleDot
    {
        private MapleRectangle parentTooltip;

        public ToolTipDot(MapleRectangle parentTooltip, Board board, int x, int y, bool beforeAdding)
            : base(board, x, y, beforeAdding)
        {
            this.parentTooltip = parentTooltip;
        }

        public override bool CheckIfLayerSelected(int selectedLayer)
        {
            return true;
        }

        public override Color Color
        {
            get
            {
                return UserSettings.ToolTipColor;
            }
        }

        public override Color InactiveColor
        {
            get { return MultiBoard.ToolTipInactiveColor; }
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.ToolTips; }
        }

        public MapleRectangle ParentTooltip
        {
            get { return parentTooltip; }
            set { parentTooltip = value; }
        }
    }

    public class ToolTipLine : MapleLine
    {
        public ToolTipLine(Board board, MapleDot firstDot, MapleDot secondDot)
            : base(board, firstDot, secondDot)
        {
        }

        public override Color Color
        {
            get { return UserSettings.ToolTipColor; }
        }

        public override Color InactiveColor
        {
            get { return MultiBoard.ToolTipInactiveColor; }
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.ToolTips; }
        }

        public override void Remove(bool removeDots, ref List<UndoRedoAction> undoPipe)
        {
            
        }
    }

    public abstract class MapleRectangle : BoardItem
    {
        //private Board board;
        //private Rectangle rect;

        //clockwise, beginning in upper-left
        private MapleDot a;
        private MapleDot b;
        private MapleDot c;
        private MapleDot d;

        private MapleLine ab;
        private MapleLine bc;
        private MapleLine cd;
        private MapleLine da;

        /*public MapleRectangle(Board board, Rectangle rect)
        {
            this.board = board;
            //this.rect = rect;
        }*/

        public MapleRectangle(Board board, Rectangle rect)
            : base(board, rect.X, rect.Y, -1, false)
        {
        }

        public abstract Color Color { get; }

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

        public override bool CheckIfLayerSelected(int selectedLayer)
        {
            return true;
        }

        public override void Draw(SpriteBatch sprite, Color dotColor, int xShift, int yShift)
        {
            Color lineColor = ab.Color;
            if (Selected)
                lineColor = dotColor;
            int x, y;
            if (a.X < b.X) x = a.X + xShift;
            else x = b.X + xShift;
            if (b.Y < c.Y) y = b.Y + yShift;
            else y = c.Y + yShift;
            Board.ParentControl.FillRectangle(sprite, new Rectangle(x, y, Math.Abs(b.X - a.X), Math.Abs(c.Y - a.Y)), Color);
            ab.Draw(sprite, lineColor, xShift, yShift);
            bc.Draw(sprite, lineColor, xShift, yShift);
            cd.Draw(sprite, lineColor, xShift, yShift);
            da.Draw(sprite, lineColor, xShift, yShift);
        }

        public override MapleDrawableInfo BaseInfo
        {
            get { return null; }
        }

/*        public void Draw(SpriteBatch sprite, ItemTypes EditedTypes, int xShift, int yShift)
        {
            Color dotColor = a.GetColor(EditedTypes, -1, a.Selected);
            Color lineColor = ab.Color;
            if (a.Selected && b.Selected && c.Selected && d.Selected)
                lineColor = dotColor;
            int x, y;
            if (a.X < b.X) x = a.X + xShift;
            else x = b.X + xShift;
            if (b.Y < c.Y) y = b.Y + yShift;
            else y = c.Y + yShift;
            Board.ParentControl.FillRectangle(sprite, new Rectangle(x, y, Math.Abs(b.X - a.X),Math.Abs(c.Y - a.Y)), Color);
            ab.Draw(sprite, lineColor, xShift, yShift);
            bc.Draw(sprite, lineColor, xShift, yShift);
            cd.Draw(sprite, lineColor, xShift, yShift);
            da.Draw(sprite, lineColor, xShift, yShift);
        }*/

        public override System.Drawing.Bitmap Image
        {
            get { throw new NotImplementedException(); }
        }

        public override System.Drawing.Point Origin
        {
            get { return System.Drawing.Point.Empty; }
        }

        public override bool IsPixelTransparent(int x, int y)
        {
            return false;
        }

        public override int Width
        {
            get
            {
                return a.X < b.X ? b.X - a.X : a.X - b.X;
            }
        }

        public override int Height
        {
            get
            {
                return b.Y < c.Y ? c.Y - b.Y : b.Y - c.Y;
            }
        }

        public override int X
        {
            get
            {
                return Math.Min(a.X, b.X);
            }
            set
            {
                /*int diff = value - a.X;
                if (!a.Selected) a.X += diff;
                if (!c.Selected) c.X += diff;*/
            }
        }

        public override int Y
        {
            get
            {
                return Math.Min(b.Y, c.Y);
            }
            set
            {
                /*int diff = value - a.Y;
                if (!a.Selected) a.Y += diff;
                if (!c.Selected) c.Y += diff;*/
            }
        }

        public override void Move(int x, int y)
        {
            //X = x;
            //Y = y;
        }

        public override int Left
        {
            get
            {
                return Math.Min(a.X, b.X);
            }
        }

        public override int Top
        {
            get
            {
                return Math.Min(b.Y, c.Y);
            }
        }

        public override int Bottom
        {
            get
            {
                return Math.Max(b.Y, c.Y);
            }
        }

        public override int Right
        {
            get
            {
                return Math.Max(a.X, b.X);
            }
        }

        public override bool Selected
        {
            get
            {
                return base.Selected;
            }
            set
            {
                base.Selected = value;
                a.Selected = value;
                b.Selected = value;
                c.Selected = value;
                d.Selected = value;
            }
        }

        public override void RemoveItem(ref List<UndoRedoAction> undoPipe)
        {
            base.RemoveItem(ref undoPipe);
            PointA.RemoveItem(ref undoPipe);
            PointB.RemoveItem(ref undoPipe);
            PointC.RemoveItem(ref undoPipe);
            PointD.RemoveItem(ref undoPipe);
        }
    }

    public class ToolTip : MapleRectangle
    {
        private string title;
        private string desc;
        private ToolTipChar ttc;

        public ToolTip(Board board, Rectangle rect, string title, string desc)
            : base(board, rect)
        {
            PointA = new ToolTipDot(this, board, rect.Left, rect.Top, false);
            PointB = new ToolTipDot(this, board, rect.Right, rect.Top, false);
            PointC = new ToolTipDot(this, board, rect.Right, rect.Bottom, false);
            PointD = new ToolTipDot(this, board, rect.Left, rect.Bottom, false);
            board.BoardItems.ToolTipDots.Add((ToolTipDot)PointA);
            board.BoardItems.ToolTipDots.Add((ToolTipDot)PointB);
            board.BoardItems.ToolTipDots.Add((ToolTipDot)PointC);
            board.BoardItems.ToolTipDots.Add((ToolTipDot)PointD);
            LineAB = new ToolTipLine(board, PointA, PointB);
            LineBC = new ToolTipLine(board, PointB, PointC);
            LineCD = new ToolTipLine(board, PointC, PointD);
            LineDA = new ToolTipLine(board, PointD, PointA);
            LineAB.yBind = true;
            LineBC.xBind = true;
            LineCD.yBind = true;
            LineDA.xBind = true;
            this.title = title;
            this.desc = desc;
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        public ToolTipChar CharacterToolTip
        {
            get { return ttc; }
            set { ttc = value; }
        }

        public override Color Color
        {
            get 
            {
                return Selected ? UserSettings.ToolTipSelectedFill : UserSettings.ToolTipFill; 
            }
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.ToolTips; }
        }

        public override void Draw(SpriteBatch sprite, Color dotColor, int xShift, int yShift)
        {
            base.Draw(sprite, dotColor, xShift, yShift);
            if (title != null)
            {
                //sprite.DrawString(Board.ParentControl.ArialFont, title, new Vector2(X + xShift + 2, Y + yShift + 2), Color.Black);
                Board.ParentControl.FontEngine.DrawString(sprite, new System.Drawing.Point(X + xShift + 2, Y + yShift + 2), Microsoft.Xna.Framework.Color.Black, title, Width);
            }
            if (desc != null)
            {
                //sprite.DrawString(Board.ParentControl.ArialFont, desc, new Vector2(X + xShift + 2, Y + yShift + 2 + Board.ParentControl.ArialFont.MeasureString(title).Y), Color.Black);
                int titleHeight = (int)Math.Ceiling(Board.ParentControl.FontEngine.MeasureString(title).Height);
                Board.ParentControl.FontEngine.DrawString(sprite, new System.Drawing.Point(X + xShift + 2, Y + yShift + 2 + titleHeight), Microsoft.Xna.Framework.Color.Black, desc, Width);
            }
        }

        public override void RemoveItem(ref List<UndoRedoAction> undoPipe)
        {
            base.RemoveItem(ref undoPipe);
            if (ttc != null)
                ttc.RemoveItem(ref undoPipe);
        }
    }

    public class ToolTipChar : MapleRectangle
    {
        private ToolTip boundTooltip;

        public ToolTipChar(Board board, Rectangle rect, ToolTip boundTooltip)
            : base(board, rect)
        {
            PointA = new ToolTipDot(this, board, rect.Left, rect.Top, false);
            PointB = new ToolTipDot(this, board, rect.Right, rect.Top, false);
            PointC = new ToolTipDot(this, board, rect.Right, rect.Bottom, false);
            PointD = new ToolTipDot(this, board, rect.Left, rect.Bottom, false);
            board.BoardItems.ToolTipDots.Add((ToolTipDot)PointA);
            board.BoardItems.ToolTipDots.Add((ToolTipDot)PointB);
            board.BoardItems.ToolTipDots.Add((ToolTipDot)PointC);
            board.BoardItems.ToolTipDots.Add((ToolTipDot)PointD);
            LineAB = new ToolTipLine(board, PointA, PointB);
            LineBC = new ToolTipLine(board, PointB, PointC);
            LineCD = new ToolTipLine(board, PointC, PointD);
            LineDA = new ToolTipLine(board, PointD, PointA);
            LineAB.yBind = true;
            LineBC.xBind = true;
            LineCD.yBind = true;
            LineDA.xBind = true;
            BoundTooltip = boundTooltip;
        }

        public ToolTip BoundTooltip
        {
            get { return boundTooltip; }
            set { boundTooltip = value; if (value != null) value.CharacterToolTip = this; }
        }

        public override Color Color
        {
            get
            {
                return Selected ? UserSettings.ToolTipCharSelectedFill : UserSettings.ToolTipCharFill;
            }
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.ToolTips; }
        }

        public override void Draw(SpriteBatch sprite, Color dotColor, int xShift, int yShift)
        {
            base.Draw(sprite, dotColor, xShift, yShift);
            if (boundTooltip != null) Board.ParentControl.DrawLine(sprite, new Vector2(X + Width / 2 + xShift, Y + Height / 2 + yShift), new Vector2(boundTooltip.X + boundTooltip.Width / 2 + xShift, boundTooltip.Y + boundTooltip.Height / 2 + yShift), UserSettings.ToolTipBindingLine);
        }

        public override void RemoveItem(ref List<UndoRedoAction> undoPipe)
        {
            if (boundTooltip == null) return; //already removed via the parent tooltip
            base.RemoveItem(ref undoPipe);
            undoPipe.Add(UndoRedoManager.ToolTipUnlinked(boundTooltip, this));
            boundTooltip.CharacterToolTip = null;
            boundTooltip = null;
        }
    }
}
