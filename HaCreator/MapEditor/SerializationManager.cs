using HaCreator.Collections;
using HaCreator.MapEditor.Info;
using HaCreator.MapEditor.Instance;
using MapleLib.WzLib.WzStructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XNA = Microsoft.Xna.Framework;

namespace HaCreator.MapEditor
{
    public class SerializationManager
    {
        Board board;

        public SerializationManager(Board board)
        {
            this.board = board;
        }

        public string SerializeList(IEnumerable<ISerializable> list)
        {
            // Get the list of all items to serialize, including dependencies and excluding non-serializable ISerializables
            List<ISerializable> items = new SerializableEnumerator(list).ToList();

            // Make reference IDs for every serialized object
            Dictionary<ISerializable, int> refDict =  MakeSerializationRefDict(items);

            // Loop over all items, making their dynamic objects and adding them to the serialization queue
            List<dynamic> dynamicList = new List<dynamic>(items.Count);
            foreach (ISerializable item in items)
            {
                dynamic serData = new ExpandoObject();
                serData.type = item.GetType().FullName;
                serData.data = item.Serialize();
                serData.bindings = item.SerializeBindings(refDict);
                dynamicList.Add(serData);
            }

            // Serialize into JSON
            return JsonConvert.SerializeObject(dynamicList.ToArray());
        }

        Dictionary<ISerializable, int> MakeSerializationRefDict(List<ISerializable> items)
        {
            Dictionary<ISerializable, int> result = new Dictionary<ISerializable, int>(items.Count);
            for (int i = 0; i < items.Count; i++)
            {
                result.Add(items[i], i);
            }
            return result;
        }

        Dictionary<int, ISerializable> MakeDeserializationRefDict(List<ISerializable> items)
        {
            Dictionary<int, ISerializable> result = new Dictionary<int, ISerializable>(items.Count);
            for (int i = 0; i < items.Count; i++)
            {
                result.Add(i, items[i]);
            }
            return result;
        }

        public static Dictionary<string, int> SerializePoint(XNA.Point p)
        {
            Dictionary<string, int> result = new Dictionary<string, int>(2);
            result.Add("x", p.X);
            result.Add("y", p.Y);
            return result;
        }

        private object Deserialize2(JToken obj)
        {
            if (obj is JObject)
            {
                IDictionary<string, object> result = new ExpandoObject();
                foreach (KeyValuePair<string, JToken> pair in (JObject)obj)
                {
                    result.Add(pair.Key, Deserialize2(pair.Value));
                }
                if (result.Count == 1 && result.ContainsKey("val"))
                {
                    return (MapleBool)(byte)(long)result["val"]; // Yes, you need all these casts here, otherwise VS doesn't understand how to cast that
                }
                return result;
            }
            else if (obj is JValue)
            {
                return ((JValue)obj).Value;
            }
            else if (obj is JArray)
            {
                JArray jarr = (JArray)obj;
                object[] arr = new object[jarr.Count];
                for (int i = 0; i < jarr.Count; i++)
                {
                    arr[i] = Deserialize2(jarr[i]);
                }
                return arr;
            }
            else
            {
                throw new Exception();
            }
        }

        public List<ISerializable> DeserializeList(string serialization)
        {
            dynamic[] dynamicArray = JsonConvert.DeserializeObject<dynamic[]>(serialization);
            List<ISerializable> items = new List<ISerializable>();
            List<IDictionary<string, object>> itemBindings = new List<IDictionary<string,object>>();
            foreach (dynamic serData in dynamicArray)
            {
                dynamic serData2 = Deserialize2(serData);
                string typeName = serData2.type;
                dynamic data = serData2.data;
                ISerializable item = (ISerializable)ConstructObject(typeName, new object[] { board, data }, new[] { typeof(Board), typeof(string) });
                items.Add(item);

                // Store the binding dict for later, since we can deserialize binding data untill all objects have been constructed
                itemBindings.Add(serData2.bindings);
            }

            // Make binding references and deserialize them
            Dictionary<int, ISerializable> refDict = MakeDeserializationRefDict(items);
            for (int i = 0; i < items.Count; i++)
            {
                items[i].DeserializeBindings(itemBindings[i], refDict);
            }
            return items;
        }

        public object ConstructObject(string typeName, object[] args, Type[] ctorTemplate)
        {
            Type type = Type.GetType(typeName);
            ConstructorInfo ctorInfo = type.GetConstructor(ctorTemplate);
            return ctorInfo.Invoke(args);
        }
    }
}
