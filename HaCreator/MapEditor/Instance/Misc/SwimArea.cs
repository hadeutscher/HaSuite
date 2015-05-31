/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNA = Microsoft.Xna.Framework;

namespace HaCreator.MapEditor.Instance.Misc
{
    public class SwimArea : MiscRectangle
    {
        string id;

        public SwimArea(Board board, XNA.Rectangle rect, string id)
            : base(board, rect)
        {
            this.id = id;
        }

        public string Identifier
        {
            get { return id; }
            set { id = value; }
        }

        public override string Name
        {
            get { return "SwimArea " + id; }
        }
    }
}
