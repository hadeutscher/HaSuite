/* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

namespace HaCreator.ThirdParty
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class KoolkLVContainer : FlowLayoutPanel
    {
        public KoolkLVItem createItem(Bitmap image, string name, bool viewName)
        {
            // This item is obfuscated and can not be translated.
            KoolkLVItem item = new KoolkLVItem {
                Dock = DockStyle.Bottom
            };
            if (image.Width > 100 || image.Height > 100) 
                image = (Bitmap)KoolkLVItem.ResizeImage(image, 100, 100);
            item.Image = image;
            item.viewName = viewName;
            item.Width = image.Width + 8;
            if (viewName)
                item.Height = image.Height + 20;
            else
                item.Height = image.Height + 8;
            item.Name = name;
            item.Field02 = false;
            base.Controls.Add(item);
            return item;
        }

        protected override Point ScrollToControl(Control activeControl)
        {
            return base.AutoScrollPosition;
        }

        public void ClearSelectedItems()
        {
            foreach (KoolkLVItem item in Controls)
                if (item.Selected) item.Selected = false;
        }
    }
}

