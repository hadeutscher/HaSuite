using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.MapEditor.Instance
{
    public interface ISerializable
    {
        // Lets the object decide whether it needs to be serialized or not, and possibly add other objects to the serialization queue
        bool SelectSerialized(HashSet<ISerializable> serList);

        // Serializes the object
        dynamic Serialize();

        IDictionary<string, object> SerializeBindings(Dictionary<ISerializable, int> refDict);

        List<ISerializable> DeserializBindings(IDictionary<string, object> bindSer, Dictionary<int, ISerializable> refDict);
    }
}
