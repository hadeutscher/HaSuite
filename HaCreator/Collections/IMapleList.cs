using MapleLib.WzLib.WzStructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.Collections
{
    public interface IMapleList : IList
    {
        bool Selectable { get; }
        ItemTypes ListType { get; }
    }
}
