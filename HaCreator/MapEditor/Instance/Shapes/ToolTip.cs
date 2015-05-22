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
    public class ToolTipInstance : MapleRectangle // Renamed to ToolTipInstance to avoid ambiguity with System.Windows.Forms.ToolTip
    {
        private string title;
        private string desc;
        private ToolTipChar ttc;
        private int originalNum;

        public ToolTipInstance(Board board, XNA.Rectangle rect, string title, string desc, int originalNum = -1)
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
                this.title = title;
                this.desc = desc;
                this.ttc = null;
                this.originalNum = originalNum;
            }
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

        public int OriginalNumber
        {
            get { return originalNum; }
        }

        public override XNA.Color Color
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

        public override void Draw(SpriteBatch sprite, XNA.Color dotColor, int xShift, int yShift)
        {
            base.Draw(sprite, dotColor, xShift, yShift);
            if (title != null)
            {
                Board.ParentControl.FontEngine.DrawString(sprite, new System.Drawing.Point(X + xShift + 2, Y + yShift + 2), Microsoft.Xna.Framework.Color.Black, title, Width);
            }
            if (desc != null)
            {
                int titleHeight = (int)Math.Ceiling(Board.ParentControl.FontEngine.MeasureString(title).Height);
                Board.ParentControl.FontEngine.DrawString(sprite, new System.Drawing.Point(X + xShift + 2, Y + yShift + 2 + titleHeight), Microsoft.Xna.Framework.Color.Black, desc, Width);
            }
        }

        public override void RemoveItem(List<UndoRedoAction> undoPipe)
        {
            lock (board.ParentControl)
            {
                base.RemoveItem(undoPipe);
                if (ttc != null)
                    ttc.RemoveItem(undoPipe);
            }
        }
    }
}
