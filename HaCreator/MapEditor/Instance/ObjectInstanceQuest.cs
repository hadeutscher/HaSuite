using MapleLib.WzLib.WzStructure.Data;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.MapEditor.Instance
{
    [DataContract]
    public struct ObjectInstanceQuest
    {
        [DataMember]
        public int questId;
        [DataMember]
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

        /*public dynamic Serialize()
        {
            dynamic result = new ExpandoObject();
            result.id = questId;
            result.state = state;
            return result;
        }*/

        public ObjectInstanceQuest(dynamic json)
        {
            this.questId = json.id;
            this.state = json.state;
        }
    }
}
