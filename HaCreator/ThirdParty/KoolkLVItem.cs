/* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

namespace HaCreator.ThirdParty
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class KoolkLVItem : UserControl
    {
        private Image image;
        private string name;
        private bool Field_02 = false;
        private bool selected = false;
        private object tag;
        public int Field_04;
        public int Field_05;
        public bool viewName;
        public IContainer container;

        public KoolkLVItem()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.container != null))
            {
                this.container.Dispose();
            }
            base.Dispose(disposing);
        }

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        public new string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Selected
        {
            get { return selected; }
            set { selected = value; base.Invalidate(); }
        }

        public bool Field02
        {
            get { return Field_02; }
            set { Field_02 = value; }
        }

        public new object Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public static Image ResizeImage(Image source, int maxWidth, int maxHeight)
        {
            int width = source.Width;
            int height = source.Height;
            double xProportion = ((double)maxWidth) / ((double)width);
            double yProportion = ((double)maxHeight) / ((double)height);
            double proportion = Math.Min(xProportion, yProportion);
            width = Math.Max((int)(width * proportion), 1);
            height = Math.Max((int)(height * proportion), 1);
            Image result = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(source, 0, 0, width, height);
            }
            return result;
        }

        public void LoadResizedImage(string path, int maxWidth, int maxHeight)
        {
            Image image = Image.FromFile(path);
            this.name = path;
            int width = image.Width;
            int height = image.Height;
            int num3 = maxWidth;
            int num4 = maxHeight;
            double num5 = ((double) num3) / ((double) width);
            double num6 = ((double) num4) / ((double) height);
            double num7 = num5;
            width = (int) (width * num7);
            height = (int) (height * num7);
            this.image = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(this.image);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(image, 0, 0, width, height);
            graphics.Dispose();
            image.Dispose();
        }

        private void Method_09(object A_0, EventArgs A_1)
        {
            base.Invalidate();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            this.BackColor = SystemColors.ControlLightLight;
            this.DoubleBuffered = true;
            base.Name = "ImageViewer";
            base.Size = new Size(0xe3, 210);
            base.ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            if ((graphics != null) && (this.image != null))
            {
                float num = 1f;
                float num2 = 0f;
                float num3 = 0f;
                if ((this.Field_04 > 0) && (this.image.Width > this.Field_04))
                {
                    num2 = ((float) this.Field_04) / ((float) this.image.Width);
                }
                if ((this.Field_05 > 0) && (this.image.Height > this.Field_05))
                {
                    num3 = ((float) this.Field_05) / ((float) this.image.Height);
                }
                if ((num2 != 0f) && (num3 == 0f))
                {
                    num = num2;
                }
                if ((num3 != 0f) && (num2 == 0f))
                {
                    num = num3;
                }
                if ((num2 != 0f) && (num2 != 0f))
                {
                    num = Math.Min(num2, num3);
                }
                graphics.Clear(Color.White);
                graphics.DrawImage(this.image, (float) 4f, (float) 4f, (float) (this.image.Width * num), (float) (this.image.Height * num));
                if (this.selected)
                {
                    graphics.DrawRectangle(new Pen(Color.White, 1f), (float) 3f, (float) 3f, (float) ((this.image.Width * num) + 2f), (float) ((this.image.Height * num) + 2f));
                    graphics.DrawRectangle(new Pen(Color.Blue, 2f), (float) 1f, (float) 1f, (float) ((this.image.Width * num) + 6f), (float) ((this.image.Height * num) + 6f));
                }
                if (this.viewName)
                {
                    graphics.DrawString(Name, new Font("Microsoft Sans Serif", 8.5f, FontStyle.Regular, GraphicsUnit.Point, 0xb1), Brushes.Black, (float) 0f, (float) ((this.image.Height * num) + 7f));
                }
            }
        }

        public static void item_MouseUp(object sender, MouseEventArgs e)
        {
            ((KoolkLVItem)sender).Selected = false;
        }
    }
}

