using MapleLib.WzLib.WzStructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.MapEditor.Instance
{
    public struct ObjectInstanceQuest
    {
        public int questId;
        public QuestState state;

        public ObjectInstanceQuest(int questId, QuestState state)
        {
            this.questId = questId;
            this.state = state;
        }

        public override string ToString()
        {
            return questId.ToString() + " - " + Enum.GetName(typeof(QuestState), state);
        }
    }
}
