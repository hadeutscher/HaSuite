using HaCreator.MapEditor.Info;
using HaCreator.MapEditor.Input;
using HaCreator.MapEditor.UndoRedo;
using MapleLib.WzLib.WzStructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNA = Microsoft.Xna.Framework;

namespace HaCreator.MapEditor.Instance.Shapes
{
    public class RopeAnchor : MapleDot, IContainsLayerInfo, ISnappable
    {
        private Rope parentRope;

        public RopeAnchor(Board board, int x, int y, Rope parentRope)
            : base(board, x, y)
        {
            this.parentRope = parentRope;
        }

        public override bool CheckIfLayerSelected(SelectionInfo sel)
        {
            // Ropes have no zM
            return (sel.selectedLayer == -1 || sel.selectedLayer == parentRope.LayerNumber);
        }

        public override XNA.Color Color
        {
            get
            {
                return UserSettings.RopeColor;
            }
        }

        public override XNA.Color InactiveColor
        {
            get { return MultiBoard.RopeInactiveColor; }
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.Ropes; }
        }

        public int LayerNumber
        {
            get { return parentRope.LayerNumber; }
            set { parentRope.LayerNumber = value; }
        }
        public int PlatformNumber { get { return -1; } set { return; } }

        protected override bool RemoveConnectedLines
        {
            // This should never happen because RemoveItem is overridden to remove through parentRope
            get { throw new NotImplementedException(); }
        }

        public override void RemoveItem(List<UndoRedoAction> undoPipe)
        {
            parentRope.Remove(undoPipe);
        }

        public override void DoSnap()
        {
            FootholdLine closestLine = null;
            double closestDistanceLine = double.MaxValue;
            foreach (FootholdLine fh in Board.BoardItems.FootholdLines)
            {
                if (!fh.IsWall && BetweenOrEquals(X, fh.FirstDot.X, fh.SecondDot.X, (int)UserSettings.SnapDistance) && BetweenOrEquals(Y, fh.FirstDot.Y, fh.SecondDot.Y, (int)UserSettings.SnapDistance))
                {
                    double targetY = fh.CalculateY(X) + 2;
                    double distance = Math.Abs(targetY - Y);
                    if (closestDistanceLine > distance)
                    {
                        closestDistanceLine = distance;
                        closestLine = fh;
                    }
                }
            }
            XNA.Point? closestRopeHint = null;
            double closestDistanceRope = double.MaxValue;
            foreach (LayeredItem li in Board.BoardItems.TileObjs)
            {
                if (!(li is ObjectInstance))
                    continue;
                ObjectInstance objInst = (ObjectInstance)li;
                ObjectInfo objInfo = (ObjectInfo)objInst.BaseInfo;
                if (objInfo.RopeOffsets == null)
                    continue;
                foreach (XNA.Point rope in objInfo.RopeOffsets)
                {
                    int dx = objInst.X + rope.X - X;
                    int dy = objInst.Y + rope.Y - Y;
                    if (Math.Abs(dx) > UserSettings.SnapDistance || Math.Abs(dy) > UserSettings.SnapDistance)
                        continue;
                    double distance = InputHandler.Distance(dx, dy);
                    if (distance > UserSettings.SnapDistance)
                        continue;
                    if (closestDistanceRope > distance)
                    {
                        closestDistanceRope = distance;
                        closestRopeHint = new XNA.Point(objInst.X + rope.X, objInst.Y + rope.Y);
                    }
                }
            }
            if (closestDistanceRope >= closestDistanceLine && closestLine != null)
                this.Y = (int)closestLine.CalculateY(X) + 2;
            else if (closestDistanceRope <= closestDistanceLine && closestRopeHint.HasValue)
            {
                this.X = closestRopeHint.Value.X;
                this.Y = closestRopeHint.Value.Y;
            }
        }

        public Rope ParentRope { get { return parentRope; } }
    }
}
