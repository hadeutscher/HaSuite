using HaCreator.MapEditor.Info;
using MapleLib.WzLib.WzStructure;
using MapleLib.WzLib.WzStructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.MapEditor.Instance
{
    public class MobInstance : LifeInstance
    {
        public MobInstance(MapleDrawableInfo baseInfo, Board board, int x, int y, int rx0Shift, int rx1Shift, int yShift, string limitedname, int? mobTime, MapleBool flip, MapleBool hide, int? info, int? team)
            : base(baseInfo, board, x, y, rx0Shift, rx1Shift, yShift, limitedname, mobTime, flip, hide, info, team) { }

        public override ItemTypes Type
        {
            get { return ItemTypes.Mobs; }
        }
    }
}
