using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.Collections
{
    public class BoardItemsEnumerator : IEnumerator<object>
    {
        private BoardItemsManager parent;
        private int currListIndex;
        private IMapleList currList;
        private int listIndex;

        public BoardItemsEnumerator(BoardItemsManager parent)
        {
            this.parent = parent;
            currList = parent.BackBackgrounds;
            listIndex = -1;
            currListIndex = 0;
        }

        public bool MoveNext()
        {
            listIndex++;
            if (listIndex == currList.Count)
            {
                listIndex = 0;
                do
                {
                replaceList:
                    currListIndex++;
                    if (currListIndex == parent.AllItemLists.Length) return false;
                    currList = parent.AllItemLists[currListIndex];
                    if (!currList.Selectable) goto replaceList;
                }
                while (currList.Count == 0);
            }
            return true;
        }

        public void Reset()
        {
            listIndex = -1;
            currListIndex = 0;
            currList = parent.BackBackgrounds;
        }

        public object Current
        {
            get
            {
                return currList[listIndex];
            }
        }

        public void Dispose()
        {
        }
    }
}
