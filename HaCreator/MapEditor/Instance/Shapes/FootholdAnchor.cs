using MapleLib.WzLib.WzStructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNA = Microsoft.Xna.Framework;

namespace HaCreator.MapEditor.Instance.Shapes
{
    public class FootholdAnchor : MapleDot, IContainsLayerInfo
    {
        private int layer;
        private int zm;

        public bool user;
        public bool removed;

        public FootholdAnchor(Board board, int x, int y, int layer, int zm, bool user)
            : base(board, x, y)
        {
            this.layer = layer;
            this.zm = zm;
            this.user = user;
        }

        public override bool CheckIfLayerSelected(SelectionInfo sel)
        {
            return (sel.selectedLayer == -1 || sel.selectedLayer == layer) && (sel.selectedPlatform == -1 || sel.selectedPlatform == zm);
        }

        public override XNA.Color Color
        {
            get
            {
                return UserSettings.FootholdColor;
            }
        }

        public override XNA.Color InactiveColor
        {
            get { return MultiBoard.FootholdInactiveColor; }
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.Footholds; }
        }

        protected override bool RemoveConnectedLines
        {
            get { return true; }
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
                    else
                    {
                        if (c.PlatformNumber > d.PlatformNumber)
                            return 1;
                        else if (c.PlatformNumber < d.PlatformNumber)
                            return -1;
                        else
                            return 0;
                    }
                }
            }
        }

        public static void MergeAnchors(FootholdAnchor a, FootholdAnchor b)
        {
            foreach (FootholdLine line in b.connectedLines)
            {
                if (line.FirstDot == b)
                    line.FirstDot = a;
                else if (line.SecondDot == b)
                    line.SecondDot = a;
                else
                    throw new Exception("No anchor matches foothold");

                a.connectedLines.Add(line);
            }
            b.connectedLines.Clear();
        }

        public bool AllConnectedLinesVertical()
        {
            foreach (MapleLine line in connectedLines)
            {
                if (line.FirstDot.X != line.SecondDot.X)
                {
                    return false;
                }
            }
            return true;
        }
        public bool AllConnectedLinesHorizontal()
        {
            foreach (MapleLine line in connectedLines)
            {
                if (line.FirstDot.Y != line.SecondDot.Y)
                {
                    return false;
                }
            }
            return true;
        }

        public int LayerNumber
        {
            get { return layer; }
            set { layer = value; }
        }

        public int PlatformNumber
        {
            get { return zm; }
            set { zm = value; }
        }

        public FootholdLine GetOtherLine(FootholdLine line)
        {
            foreach (FootholdLine currLine in connectedLines)
            {
                if (line != currLine)
                {
                    return currLine;
                }
            }
            return null;
        }
    }
}
