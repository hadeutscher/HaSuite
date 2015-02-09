/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace HaCreator.MapEditor
{
    internal class CharTexture
    {
        internal Texture2D texture;
        internal int w;
        internal int h;

        internal CharTexture(Texture2D texture, int w, int h)
        {
            this.texture = texture;
            this.w = w;
            this.h = h;
        }
    }

    public class FontEngine
    {
        private Bitmap globalBitmap;
        private Graphics globalGraphics;
        private Font font;
        private GraphicsDevice device;

        private CharTexture[] characters = new CharTexture[0x60];

        public FontEngine(string fontName, FontStyle fontStyle, float size, GraphicsDevice device)
        {
            this.device = device;
            font = new Font(fontName, size, fontStyle);
            globalBitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            globalGraphics = Graphics.FromImage(globalBitmap);
            globalGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            //format.Alignment = StringAlignment.Near;
            //format.LineAlignment = StringAlignment.Near;

            for (char ch = (char)0x20; ch < 0x80; ch++)
            {
                characters[(int)ch - 0x20] = RasterizeCharacter(ch);
            }
        }

        Brush brush = new SolidBrush(Color.White);
        StringFormat format = new StringFormat();

        private CharTexture RasterizeCharacter(char ch)
        {
            string text = ch.ToString();

            StringFormat format = StringFormat.GenericTypographic;
            SizeF size = globalGraphics.MeasureString(text, font, new PointF(0, 0), format);

            if (size.Width < 1)
            {
                format = StringFormat.GenericDefault;
                size = globalGraphics.MeasureString(text, font); //fallback for spaces
            }

            int width = (int)Math.Ceiling(size.Width);
            int height = (int)Math.Ceiling(size.Height);

            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                /*if (antialias)
                {
                    graphics.TextRenderingHint =
                        TextRenderingHint.AntiAliasGridFit;
                }
                else
                {
                    graphics.TextRenderingHint =
                        TextRenderingHint.SingleBitPerPixelGridFit;
                }*/

                graphics.DrawString(text, font, brush, 0, 0, format);
            }

            return new CharTexture(BoardItem.TextureFromBitmap(device, bitmap), width, height);
        }


        //draws a string to the global device using the font textures.
        //if the string exceedds maxWidth, it will be truncated with dots
        public void DrawString(SpriteBatch sprite, Point position, Microsoft.Xna.Framework.Color color, string str, int maxWidth)
        {
            //if the string is too long, truncate it and place "..."
            if (UserSettings.clipText && globalGraphics.MeasureString(str, font).Width > maxWidth)
            {
                int dotsWidth = (int)globalGraphics.MeasureString("...", font, new PointF(0, 0), StringFormat.GenericTypographic).Width;
                do
                {
                    str = str.Substring(0, str.Length - 1);
                }
                while (globalGraphics.MeasureString(str, font).Width + dotsWidth > maxWidth);
                str += "...";
            }
            int xOffs = 0;
            foreach (char c in str.ToCharArray())
            {
                //SizeF cSize = globalGraphics.MeasureString(new string(new char[] { c }), font, new PointF(0, 0), StringFormat.GenericTypographic);
                int w = characters[c - 0x20].w;
                int h = characters[c - 0x20].h;
                //SizeF cSize = characters[c - 0x20].size;
                sprite.Draw(characters[c - 0x20].texture, new Microsoft.Xna.Framework.Rectangle(position.X + xOffs, position.Y, w, h), color);
                xOffs += w;
            }
        }

        public SizeF MeasureString(string s)
        {
            return globalGraphics.MeasureString(s, font);
        }
    }
}
