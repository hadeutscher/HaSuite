using MapleLib.WzLib.WzStructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.Collections
{
    public class MapleList<T> : List<T>, IMapleList
    {
        private ItemTypes listType;
        private bool selectable;

        public MapleList(ItemTypes listType, bool selectable)
            : base()
        {
            this.listType = listType;
            this.selectable = selectable;
        }

        public bool Selectable
        {
            get { return selectable; }
        }

        public ItemTypes ListType
        {
            get { return listType; }
        }
    }
}
