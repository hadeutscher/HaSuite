using MapleLib.WzLib.WzStructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.Wz
{
    public class PortalGameImageInfo
    {
        private Bitmap defaultImage;

        private MapleTable<Bitmap> imageList;

        public PortalGameImageInfo(Bitmap defaultImage, MapleTable<Bitmap> imageList)
        {
            this.defaultImage = defaultImage;
            this.imageList = imageList;
        }

        public Bitmap DefaultImage
        {
            get { return defaultImage; }
        }

        public Bitmap this[string name]
        {
            get
            {
                return imageList[name];
            }
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return imageList.GetEnumerator();
        }
    }
}
