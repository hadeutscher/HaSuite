using HaCreator.MapEditor.UndoRedo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNA = Microsoft.Xna.Framework;

namespace HaCreator.MapEditor.Instance.Shapes
{
    public class VRRectangle : MapleEmptyRectangle
    {
        public VRRectangle(Board board, XNA.Rectangle rect)
            : base(board)
        {
            lock (board.ParentControl)
            {
                PointA = new VRDot(this, board, rect.Left, rect.Top);
                PointB = new VRDot(this, board, rect.Right, rect.Top);
                PointC = new VRDot(this, board, rect.Right, rect.Bottom);
                PointD = new VRDot(this, board, rect.Left, rect.Bottom);
                board.BoardItems.SpecialDots.Add((VRDot)PointA);
                board.BoardItems.SpecialDots.Add((VRDot)PointB);
                board.BoardItems.SpecialDots.Add((VRDot)PointC);
                board.BoardItems.SpecialDots.Add((VRDot)PointD);
                LineAB = new VRLine(board, PointA, PointB);
                LineBC = new VRLine(board, PointB, PointC);
                LineCD = new VRLine(board, PointC, PointD);
                LineDA = new VRLine(board, PointD, PointA);
                LineAB.yBind = true;
                LineBC.xBind = true;
                LineCD.yBind = true;
                LineDA.xBind = true;
            }
        }

        public override void RemoveItem(List<UndoRedoAction> undoPipe)
        {
            lock (board.ParentControl)
            {
                base.RemoveItem(null);
                board.VRRectangle = null;
            }
        }
    }
}
