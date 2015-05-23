using HaCreator.MapEditor.UndoRedo;
using MapleLib.WzLib.WzStructure.Data;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNA = Microsoft.Xna.Framework;

namespace HaCreator.MapEditor.Instance.Shapes
{
    public class ToolTipChar : MapleRectangle
    {
        private ToolTipInstance boundTooltip;

        public ToolTipChar(Board board, XNA.Rectangle rect, ToolTipInstance boundTooltip)
            : base(board, rect)
        {
            lock (board.ParentControl)
            {
                PointA = new ToolTipDot(this, board, rect.Left, rect.Top);
                PointB = new ToolTipDot(this, board, rect.Right, rect.Top);
                PointC = new ToolTipDot(this, board, rect.Right, rect.Bottom);
                PointD = new ToolTipDot(this, board, rect.Left, rect.Bottom);
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
        }

        public ToolTipInstance BoundTooltip
        {
            get { return boundTooltip; }
            set { boundTooltip = value; if (value != null) value.CharacterToolTip = this; }
        }

        public override XNA.Color Color
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

        public override void Draw(SpriteBatch sprite, XNA.Color dotColor, int xShift, int yShift)
        {
            base.Draw(sprite, dotColor, xShift, yShift);
            if (boundTooltip != null) Board.ParentControl.DrawLine(sprite, new XNA.Vector2(X + Width / 2 + xShift, Y + Height / 2 + yShift), new XNA.Vector2(boundTooltip.X + boundTooltip.Width / 2 + xShift, boundTooltip.Y + boundTooltip.Height / 2 + yShift), UserSettings.ToolTipBindingLine);
        }

        public override void OnItemPlaced(List<UndoRedoAction> undoPipe)
        {
            lock (board.ParentControl)
            {
                base.OnItemPlaced(undoPipe);
                undoPipe.Add(UndoRedoManager.ToolTipLinked(BoundTooltip, this));
            }
        }

        public override void RemoveItem(List<UndoRedoAction> undoPipe)
        {
            lock (board.ParentControl)
            {
                if (boundTooltip == null) return; //already removed via the parent tooltip
                base.RemoveItem(undoPipe);
                if (undoPipe != null)
                {
                    undoPipe.Add(UndoRedoManager.ToolTipUnlinked(boundTooltip, this));
                }
                boundTooltip.CharacterToolTip = null;
                boundTooltip = null;
            }
        }
    }
}
