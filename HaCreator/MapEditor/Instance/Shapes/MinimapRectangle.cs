/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using HaCreator.MapEditor.UndoRedo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNA = Microsoft.Xna.Framework;

namespace HaCreator.MapEditor.Instance.Shapes
{
    public class MinimapRectangle : MapleEmptyRectangle
    {
        public MinimapRectangle(Board board, XNA.Rectangle rect)
            : base(board)
        {
            lock (board.ParentControl)
            {
                PointA = new MinimapDot(this, board, rect.Left, rect.Top);
                PointB = new MinimapDot(this, board, rect.Right, rect.Top);
                PointC = new MinimapDot(this, board, rect.Right, rect.Bottom);
                PointD = new MinimapDot(this, board, rect.Left, rect.Bottom);
                board.BoardItems.SpecialDots.Add((MinimapDot)PointA);
                board.BoardItems.SpecialDots.Add((MinimapDot)PointB);
                board.BoardItems.SpecialDots.Add((MinimapDot)PointC);
                board.BoardItems.SpecialDots.Add((MinimapDot)PointD);
                LineAB = new MinimapLine(board, PointA, PointB);
                LineBC = new MinimapLine(board, PointB, PointC);
                LineCD = new MinimapLine(board, PointC, PointD);
                LineDA = new MinimapLine(board, PointD, PointA);
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
                board.MinimapRectangle = null;
                board.RegenerateMinimap();
            }
        }
    }
}
