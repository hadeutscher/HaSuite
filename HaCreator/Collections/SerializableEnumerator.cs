using HaCreator.MapEditor.Instance;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.Collections
{
    class SerializableEnumerator : IEnumerable<ISerializable>, IEnumerator<ISerializable>
    {
        HashSet<ISerializable> visited;
        Queue<ISerializable> queue;
        ISerializable current = null;

        public SerializableEnumerator(IEnumerable<ISerializable> startList)
        {
            visited = new HashSet<ISerializable>(startList);
            queue = new Queue<ISerializable>(visited);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<ISerializable> GetEnumerator()
        {
            return this;
        }

        public void Dispose()
        {
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public ISerializable Current
        {
            get { return current; }
        }

        public bool MoveNext()
        {
            if (queue.Count == 0)
                return false;
            do
            {
                current = queue.Dequeue();
                if (current.ShouldSerializeChildren)
                {
                    List<ISerializable> currList = current.SelectSerialized();
                    foreach (ISerializable item in currList)
                    {
                        if (visited.Add(item))
                            queue.Enqueue(item);
                    }
                }
            }
            while (!current.ShouldSerialize);
            return true;
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }
    }
}
