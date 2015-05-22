using MapleLib.WzLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.MapEditor.Info
{
    public abstract class MapleExtractableInfo : MapleDrawableInfo
    {
        public MapleExtractableInfo(Bitmap image, System.Drawing.Point origin, WzObject parentObject)
            : base(image, origin, parentObject)
        {
        }

        public override Bitmap Image
        {
            get
            {
                if (base.Image == null)
                    ParseImage();
                return base.Image;
            }
            set
            {
                base.Image = value;
            }
        }

        public abstract void ParseImage();
    }
}
