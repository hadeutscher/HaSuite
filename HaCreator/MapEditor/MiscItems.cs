/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapleLib.WzLib.WzStructure.Data;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MapleLib.WzLib.WzStructure;

namespace HaCreator.MapEditor
{
    public class MiscDot : MapleDot
    {
        private MiscRectangle parentItem;

        public MiscDot(MiscRectangle parentItem, Board board, int x, int y, bool beforeAdding)
            : base(board, x, y, beforeAdding)
        {
            this.parentItem = parentItem;
        }

        public override bool CheckIfLayerSelected(int selectedLayer)
        {
            return true;
        }

        public override Color Color
        {
            get { return UserSettings.MiscColor; }
        }

        public override Color InactiveColor
        {
            get { return MultiBoard.MiscInactiveColor; }
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.Misc; }
        }

        public MiscRectangle ParentRectangle { get { return parentItem; } set { parentItem = value; } }
    }

    public class MiscLine : MapleLine
    {
        public MiscLine(Board board, MapleDot firstDot, MapleDot secondDot)
            : base(board, firstDot, secondDot)
        {
        }

        public override Color Color
        {
            get { return UserSettings.MiscColor; }
        }

        public override Color InactiveColor
        {
            get { return MultiBoard.MiscInactiveColor; }
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.Misc; }
        }

        public override void Remove(bool removeDots, ref List<UndoRedoAction> undoPipe)
        {
            
        }
    }

    public abstract class MiscRectangle : MapleRectangle
    {
        public abstract string Name { get; }

        public MiscRectangle(Board board, Rectangle rect)
            : base(board, rect)
        {
            PointA = new MiscDot(this, board, rect.Left, rect.Top, false);
            PointB = new MiscDot(this, board, rect.Right, rect.Top, false);
            PointC = new MiscDot(this, board, rect.Right, rect.Bottom, false);
            PointD = new MiscDot(this, board, rect.Left, rect.Bottom, false);
            board.BoardItems.ToolTipDots.Add((ToolTipDot)PointA);
            board.BoardItems.ToolTipDots.Add((ToolTipDot)PointB);
            board.BoardItems.ToolTipDots.Add((ToolTipDot)PointC);
            board.BoardItems.ToolTipDots.Add((ToolTipDot)PointD);
            LineAB = new MiscLine(board, PointA, PointB);
            LineBC = new MiscLine(board, PointB, PointC);
            LineCD = new MiscLine(board, PointC, PointD);
            LineDA = new MiscLine(board, PointD, PointA);
            LineAB.yBind = true;
            LineBC.xBind = true;
            LineCD.yBind = true;
            LineDA.xBind = true;
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
            //sprite.DrawString(Board.ParentControl.ArialFont, Name, new Vector2(X + xShift + 2, Y + yShift + 2), Color.Black);
        }
    }

    public class BuffZone : MiscRectangle
    {
        private int itemID;
        private int interval;
        private int duration;

        public BuffZone(Board board, Rectangle rect, int itemID, int interval, int duration)
            : base(board, rect)
        {
            this.itemID = itemID;
            this.interval = interval;
            this.duration = duration;
        }

        public override string  Name
        {
	        get { return "BuffZone"; }
        }

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        public int Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }
    }

    public struct UserInfo //array of these
    {
        public Condition? cond;
        public Stat? stat;
        public Look? look;
        public int[] noitem; //can be null

        public struct Condition //cond
        {
            public int? battleFieldTeam;
            public int? jobCategory;
            public int? job;
            public int? level;
            public MapleBool compare;
        }

        public struct Stat
        {
            public int? str;
            public int? dex;
            public int? inte; //int
            public int? luk;
            public int? pad;
            public int? mad;
            public int? acc;
            public int? eva;
            public int? jump;
            public int? speed;
            public int? speedmax;
        }

        public struct Look
        {
            public int? cap;
            public int? clothes;
            public int? pants;
            public int? gloves;
            public int? shoes;
            public int? cape;
        }
    }

    public struct BattleField
    {
        public string effectWin;
        public string effectLose;
        public int? rewardMapWinWolf;
        public int? rewardMapWinSheep;
        public int? rewardMapLoseWolf;
        public int? rewardMapLoseSheep;
        public int timeDefault;
        public int timeFinish;
    }

    //noSkill is an... int array? inside the class property

    public struct MobMassacre
    {
        public int mapDistance;
        public Gauge gauge;
        public CountEffect[] countEffect;
        public MapleBool disableSkill;

        public struct Gauge
        {
            public int total;
            public int decrease;
            public int coolAdd;
            public int missSub;
            public int hitAdd;
        }

        public struct CountEffect
        {
            public int count;
            public int? buff;
            public MapleBool skillUse; 
        }
        
    }

    public class ShipObject : BoardItem, IFlippable
    {
        private ObjectInfo baseInfo; //shipObj
        private bool flip;
        private int x0;
        private int tMove;
        private int shipKind;

        public ShipObject(ObjectInfo baseInfo, Board board, int x, int y, int z, int x0, int tMove, int shipKind, bool flip, bool beforeAdding)
            : base(board, x, y, z, beforeAdding)
        {
            this.baseInfo = baseInfo;
            this.flip = flip;
            this.x0 = x0;
            this.tMove = tMove;
            this.shipKind = shipKind;
            if (flip)
                X -= Width - 2 * Origin.X;
        }

        public int X0
        {
            get { return x0; }
            set { x0 = value; }
        }

        public int TimeMove
        {
            get { return tMove; }
            set { tMove = value; }
        }

        public int ShipKind
        {
            get { return shipKind; }
            set { shipKind = value; }
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.Misc; }
        }

        public override MapleDrawableInfo BaseInfo
        {
            get { return baseInfo; }
        }

        public override Color GetColor(ItemTypes editedTypes, int selectedLayer, bool selected)
        {
            Color c = base.GetColor(editedTypes, selectedLayer, selected);
            return c;
        }

        public bool Flip
        {
            get
            {
                return flip;
            }
            set
            {
                flip = value;
                int xFlipShift = Width - 2 * Origin.X;
                if (flip) X -= xFlipShift;
                else X += xFlipShift;
            }
        }

        public override void Draw(SpriteBatch sprite, Color color, int xShift, int yShift)
        {
            Rectangle destinationRectangle = new Rectangle((int)X + xShift - Origin.X, (int)Y + yShift - Origin.Y, Width, Height);
            //if (baseInfo.Texture == null) baseInfo.CreateTexture(sprite.GraphicsDevice);
            sprite.Draw(baseInfo.GetTexture(sprite), destinationRectangle, null, color, 0f, new Vector2(0, 0), Flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0 /*Layer.LayerNumber / 10f + Z / 1000f*/);
        }

        public override bool CheckIfLayerSelected(int selectedLayer)
        {
            return true;
        }

        public override System.Drawing.Bitmap Image
        {
            get
            {
                return baseInfo.Image;
            }
        }

        public override System.Drawing.Point Origin
        {
            get
            {
                return baseInfo.Origin;
            }
        }
    }

    public class Clock : MiscRectangle
    {
        public Clock(Board board, Rectangle rect)
            : base(board, rect)
        {
        }

        public override string Name
        {
            get { return "Clock"; }
        }
    }
}
