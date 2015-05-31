/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using HaCreator.MapEditor.Instance.Shapes;
using MapleLib.WzLib.WzStructure.Data;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNA = Microsoft.Xna.Framework;

namespace HaCreator.MapEditor.Instance.Misc
{
    public abstract class MiscRectangle : MapleRectangle, INamedMisc
    {
        public abstract string Name { get; }

        public MiscRectangle(Board board, XNA.Rectangle rect)
            : base(board, rect)
        {
            lock (board.ParentControl)
            {
                PointA = new MiscDot(this, board, rect.Left, rect.Top);
                PointB = new MiscDot(this, board, rect.Right, rect.Top);
                PointC = new MiscDot(this, board, rect.Right, rect.Bottom);
                PointD = new MiscDot(this, board, rect.Left, rect.Bottom);
                board.BoardItems.MiscItems.Add((MiscDot)PointA);
                board.BoardItems.MiscItems.Add((MiscDot)PointB);
                board.BoardItems.MiscItems.Add((MiscDot)PointC);
                board.BoardItems.MiscItems.Add((MiscDot)PointD);
                LineAB = new MiscLine(board, PointA, PointB);
                LineBC = new MiscLine(board, PointB, PointC);
                LineCD = new MiscLine(board, PointC, PointD);
                LineDA = new MiscLine(board, PointD, PointA);
                LineAB.yBind = true;
                LineBC.xBind = true;
                LineCD.yBind = true;
                LineDA.xBind = true;
            }
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
            get { return ItemTypes.Misc; }
        }

        public override void Draw(SpriteBatch sprite, XNA.Color dotColor, int xShift, int yShift)
        {
            base.Draw(sprite, dotColor, xShift, yShift);
            board.ParentControl.FontEngine.DrawString(sprite, new System.Drawing.Point(X + xShift + 2, Y + yShift + 2), XNA.Color.Black, Name, Width);
        }
    }
}
