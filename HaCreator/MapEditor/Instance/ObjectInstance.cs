using HaCreator.MapEditor.Info;
using HaCreator.MapEditor.Input;
using MapleLib.WzLib.WzStructure;
using MapleLib.WzLib.WzStructure.Data;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNA = Microsoft.Xna.Framework;

namespace HaCreator.MapEditor.Instance
{
    public class ObjectInstance : LayeredItem, IFlippable, ISnappable
    {
        private ObjectInfo baseInfo;
        private bool flip;
        private MapleBool _r;
        private string name;
        private MapleBool _hide;
        private MapleBool _reactor;
        private MapleBool _flow;
        private int? _rx, _ry, _cx, _cy;
        private string _tags;
        private List<ObjectInstanceQuest> questInfo;

        public ObjectInstance(ObjectInfo baseInfo, Layer layer, Board board, int x, int y, int z, int zM, MapleBool r, MapleBool hide, MapleBool reactor, MapleBool flow, int? rx, int? ry, int? cx, int? cy, string name, string tags, List<ObjectInstanceQuest> questInfo, bool flip)
            : base(board, layer, zM, x, y, z)
        {
            this.baseInfo = baseInfo;
            this.flip = flip;
            this._r = r;
            this.name = name;
            this._hide = hide;
            this._reactor = reactor;
            this._flow = flow;
            this._rx = rx;
            this._ry = ry;
            this._cx = cx;
            this._cy = cy;
            this._tags = tags;
            this.questInfo = questInfo;
            if (flip)
                X -= Width - 2 * Origin.X;
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.Objects; }
        }

        public override MapleDrawableInfo BaseInfo
        {
            get { return baseInfo; }
        }

        public override XNA.Color GetColor(SelectionInfo sel, bool selected)
        {
            XNA.Color c = base.GetColor(sel, selected);
            if (_hide) c.R = (byte)UserSettings.HiddenLifeR;
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
                if (flip == value) return;
                flip = value;
                int xFlipShift = Width - 2 * Origin.X;
                if (flip) X -= xFlipShift;
                else X += xFlipShift;
            }
        }

        public int UnflippedX
        {
            get
            {
                return flip ? (X + Width - 2 * Origin.X) : X;
            }
        }

        public override void Draw(SpriteBatch sprite, XNA.Color color, int xShift, int yShift)
        {
            XNA.Rectangle destinationRectangle = new XNA.Rectangle((int)X + xShift - Origin.X, (int)Y + yShift - Origin.Y, Width, Height);
            sprite.Draw(baseInfo.GetTexture(sprite), destinationRectangle, null, color, 0f, new XNA.Vector2(0, 0), Flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0 /*Layer.LayerNumber / 10f + Z / 1000f*/);
            base.Draw(sprite, color, xShift, yShift);
        }

        public override System.Drawing.Bitmap Image
        {
            get
            {
                return baseInfo.Image;
            }
        }

        public override int Width
        {
            get { return baseInfo.Width; }
        }

        public override int Height
        {
            get { return baseInfo.Height; }
        }

        public override System.Drawing.Point Origin
        {
            get
            {
                return baseInfo.Origin;
            }
        }

        public void DoSnap()
        {
            if (!baseInfo.Connect)
                return;
            XNA.Point? closestDestPoint = null;
            double closestDistance = double.MaxValue;
            foreach (LayeredItem li in Board.BoardItems.TileObjs)
            {
                if (!(li is ObjectInstance))
                    continue;
                ObjectInstance objInst = (ObjectInstance)li;
                ObjectInfo objInfo = (ObjectInfo)objInst.BaseInfo;
                if (!objInfo.Connect)
                    continue;
                XNA.Point snapPoint = new XNA.Point(objInst.X, objInst.Y - objInst.Origin.Y + objInst.Height + this.Origin.Y);
                double dx = snapPoint.X - X;
                double dy = snapPoint.Y - Y;
                if (dx > UserSettings.SnapDistance || dy > UserSettings.SnapDistance)
                    continue;
                double distance = InputHandler.Distance(dx, dy);
                if (distance > UserSettings.SnapDistance)
                    continue;
                if (closestDistance > distance)
                {
                    closestDistance = distance;
                    closestDestPoint = snapPoint;
                }
            }

            if (closestDestPoint.HasValue)
            {
                X = closestDestPoint.Value.X;
                Y = closestDestPoint.Value.Y;
            }
        }

        public string Name { get { return name; } set { name = value; } }
        public string tags { get { return _tags; } set { _tags = value; } }
        public MapleBool r { get { return _r; } set { _r = value; } }
        public MapleBool hide { get { return _hide; } set { _hide = value; } }
        public MapleBool flow { get { return _flow; } set { _flow = value; } }
        public MapleBool reactor { get { return _reactor; } set { _reactor = value; } }
        public int? rx { get { return _rx; } set { _rx = value; } }
        public int? ry { get { return _ry; } set { _ry = value; } }
        public int? cx { get { return _cx; } set { _cx = value; } }
        public int? cy { get { return _cy; } set { _cy = value; } }
        public List<ObjectInstanceQuest> QuestInfo { get { return questInfo; } set { questInfo = value; } }
    }
}
