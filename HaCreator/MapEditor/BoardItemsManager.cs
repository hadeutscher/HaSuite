/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapleLib.WzLib.WzStructure.Data;

namespace HaCreator.MapEditor
{
    public class BoardItemsEnumerator : IEnumerator
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
    }

    public interface IMapleList : IList
    {
        bool Selectable { get; }
        ItemTypes ListType { get; }
    }

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

    public class BoardItemsManager : IEnumerable
    {
        public MapleList<BackgroundInstance> BackBackgrounds = new MapleList<BackgroundInstance>(ItemTypes.Backgrounds, true);
        public MapleList<LayeredItem> TileObjs = new MapleList<LayeredItem>(ItemTypes.None, true);
        public MapleList<MobInstance> Mobs = new MapleList<MobInstance>(ItemTypes.Mobs, true);
        public MapleList<NPCInstance> NPCs = new MapleList<NPCInstance>(ItemTypes.NPCs, true);
        public MapleList<ReactorInstance> Reactors = new MapleList<ReactorInstance>(ItemTypes.Reactors, true);
        public MapleList<PortalInstance> Portals = new MapleList<PortalInstance>(ItemTypes.Portals, true);
        public MapleList<BackgroundInstance> FrontBackgrounds = new MapleList<BackgroundInstance>(ItemTypes.Backgrounds, true);
        public MapleList<FootholdLine> FootholdLines = new MapleList<FootholdLine>(ItemTypes.Footholds, false);
        public MapleList<RopeLine> RopeLines = new MapleList<RopeLine>(ItemTypes.Ropes, false);
        public MapleList<FootholdAnchor> FHAnchors = new MapleList<FootholdAnchor>(ItemTypes.Footholds, true);
        public MapleList<RopeAnchor> RopeAnchors = new MapleList<RopeAnchor>(ItemTypes.Ropes, true);
        public MapleList<Chair> Chairs = new MapleList<Chair>(ItemTypes.Chairs, true);
        public MapleList<ToolTipChar> CharacterToolTips = new MapleList<ToolTipChar>(ItemTypes.ToolTips, true);
        public MapleList<ToolTip> ToolTips = new MapleList<ToolTip>(ItemTypes.ToolTips, true);
        public MapleList<ToolTipDot> ToolTipDots = new MapleList<ToolTipDot>(ItemTypes.ToolTips, true);

        public List<Rope> Ropes = new List<Rope>();



        public IMapleList[] AllItemLists;
//        public ItemTypes[] ListFilters;

        public IEnumerator GetEnumerator()
        {
            return new BoardItemsEnumerator(this);
        }

        public BoardItemsManager()
        {
            /*Type t = this.GetType();
            System.Reflection.FieldInfo[] fields = t.GetFields();
            List<IList> lists = new List<IList>();
            for (int i = 0; i < fields.Length; i++)
                if (fields[i].Name != "AllItemLists" && fields[i].Name != "ListFilters") //making sure it's not AllItemLists inheriting from System.Array
                    lists.Add((IList)fields[i].GetValue(this));
            AllItemLists = lists.ToArray();*/
            AllItemLists = new IMapleList[] { BackBackgrounds, TileObjs, Mobs, NPCs, Reactors, Portals, FrontBackgrounds, FootholdLines, RopeLines, FHAnchors, RopeAnchors, Chairs, CharacterToolTips, ToolTips, ToolTipDots };
            //ListFilters = new ItemTypes[] { ItemTypes.Backgrounds, ItemTypes.None, ItemTypes.Mobs, ItemTypes.NPCs, ItemTypes.Reactors, ItemTypes.Portals, ItemTypes.Backgrounds, ItemTypes.Footholds, ItemTypes.Ropes, ItemTypes.Footholds, ItemTypes.Ropes, ItemTypes.Chairs, ItemTypes.ToolTips, ItemTypes.ToolTips, ItemTypes.ToolTips };
        }

        public void Clear()
        {
            foreach (IMapleList itemList in AllItemLists) itemList.Clear();
        }

        public void Remove(BoardItem item)
        {
            if (item is TileInstance || item is ObjectInstance)
                TileObjs.Remove((LayeredItem)item);
            else if (item is BackgroundInstance)
            {
                if (((BackgroundInstance)item).front)
                {
                    FrontBackgrounds.Remove((BackgroundInstance)item);
                } else 
                {
                    BackBackgrounds.Remove((BackgroundInstance)item);
                }
            }
            else
            {
                Type itemType = item.GetType();
                foreach (IMapleList itemList in AllItemLists)
                {
                    Type listType = itemList.GetType().GetGenericArguments()[0];
                    if (listType.FullName == itemType.FullName)
                    {
                        itemList.Remove(item);
                        return;
                    }
                }
                throw new Exception("unknown type at boarditems.remove");
            }
        }

        public void Add(BoardItem item, bool sort)
        {
            if (item is TileInstance || item is ObjectInstance)
            {
                TileObjs.Add((LayeredItem)item);
                if (sort) Sort();
            }
            else if (item is BackgroundInstance)
            {
                if (((BackgroundInstance)item).front)
                {
                    FrontBackgrounds.Add((BackgroundInstance)item);
                }
                else
                {
                    BackBackgrounds.Add((BackgroundInstance)item);
                }
                if (sort) Sort();
            }
            else
            {
                Type itemType = item.GetType();
                foreach (IMapleList itemList in AllItemLists)
                {
                    Type listType = itemList.GetType().GetGenericArguments()[0];
                    if (listType.FullName == itemType.FullName)
                    {
                        itemList.Add(item);
                        return;
                    }
                }
                throw new Exception("unknown type at boarditems.add");
            }
        }

        public void Sort()
        {
            SortLayers();
            SortBackBackgrounds();
            SortFrontBackgrounds();
        }

        private void SortLayers()
        {
            for (int i = 0; i < 2; i++)
                TileObjs.Sort(
                    delegate(LayeredItem a, LayeredItem b)
                    {
                        if (a.Layer.LayerNumber > b.Layer.LayerNumber)
                            return 1;
                        else if (a.Layer.LayerNumber < b.Layer.LayerNumber)
                            return -1;
                        else
                        {
                            if ((a is TileInstance && b is TileInstance) || (a is ObjectInstance && b is ObjectInstance))
                            {
                                if (a.Z > b.Z)
                                    return 1;
                                else if (a.Z < b.Z)
                                    return -1;
                                else return 0;
                            }
                            else if (a is TileInstance && b is ObjectInstance)
                                return 1;
                            else
                                return -1;
                        }
                    }
                );
        }

        public int Count
        {
            get 
            { 
                int total = 0;
                foreach (IList itemList in AllItemLists) total += itemList.Count;
                return total;
            }
        }

        public BoardItem this[int index]
        {
            get
            {
                if (index < 0) throw new Exception("invalid index");
                foreach (IList list in AllItemLists)
                {
                    if (index < list.Count) return (BoardItem)list[index];
                    index -= list.Count;
                }
                throw new Exception("invalid index");
            }
        }

        private void SortBackBackgrounds()
        {
            BackBackgrounds.Sort(
                delegate(BackgroundInstance a, BackgroundInstance b)
                {

                    if (a.Z > b.Z) return 1;
                    else if (a.Z < b.Z) return -1;
                    else return 0;
                }
            );
        }

        private void SortFrontBackgrounds()
        {
            FrontBackgrounds.Sort(
                delegate(BackgroundInstance a, BackgroundInstance b)
                {

                    if (a.Z > b.Z) return 1;
                    else if (a.Z < b.Z) return -1;
                    else return 0;
                }
            );
        }
    }
}
