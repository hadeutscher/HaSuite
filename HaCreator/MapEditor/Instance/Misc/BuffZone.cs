using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNA = Microsoft.Xna.Framework;

namespace HaCreator.MapEditor.Instance.Misc
{
    public class BuffZone : MiscRectangle
    {
        private int itemID;
        private int interval;
        private int duration;
        private string zoneName;

        public BuffZone(Board board, XNA.Rectangle rect, int itemID, int interval, int duration, string zoneName)
            : base(board, rect)
        {
            this.itemID = itemID;
            this.interval = interval;
            this.duration = duration;
            this.zoneName = zoneName;
        }

        public override string Name
        {
            get { return "BuffZone " + this.zoneName; }
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
        public string ZoneName
        {
            get { return zoneName; }
            set { zoneName = value; }
        }
    }
}
