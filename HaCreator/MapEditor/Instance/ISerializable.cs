/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using HaCreator.MapEditor.UndoRedo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.MapEditor.Instance
{
    public interface ISerializable
    {
        // Lets the object decide whether it needs to be serialized itself
        bool ShouldSerialize { get; }

        // Decide whether the object has children to serialize (to avoid calling SelectSerialized for all objects)
        bool ShouldSerializeChildren { get; }

        // Lets the object add other objects to the serialization queue
        List<ISerializable> SelectSerialized();

        // Serializes the object
        dynamic Serialize();

        // Serialize the binding data of the object
        IDictionary<string, object> SerializeBindings(Dictionary<ISerializable, int> refDict);

        // Deserialize the binding data and bind the objects, returning a list of the bound objects
        void DeserializeBindings(IDictionary<string, object> bindSer, Dictionary<int, ISerializable> refDict);

        // Adds the object to the board's BoardItemManager
        void AddToBoard(List<UndoRedoAction> undoPipe);
    }
}
