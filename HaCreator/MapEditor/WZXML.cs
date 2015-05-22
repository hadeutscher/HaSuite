/*  HaRepacker - MapleStory Repacker
 * Copyright (C) 2009, 2010  haha01haha01
   
 * This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

 * This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Xml;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;

namespace HaCreator.MapEditor
{
    public static class WZXML
    {
        private static NumberFormatInfo formattingInfo = initFormat();
        private static string indent = "    ";

        private static NumberFormatInfo initFormat()
        {
            NumberFormatInfo format = new System.Globalization.NumberFormatInfo();
            format.NumberDecimalSeparator = ".";
            format.NumberGroupSeparator = ",";
            return format;
        }

        public static void DumpMap(WzImage img, string path,string mapname, string streetname)
        {

            TextWriter tw = new StreamWriter(path);
            tw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
            tw.WriteLine("<imgdir name=\"" + img.Name + "\" mapname=\"" + mapname + "\" streetname=\"" + streetname + "\">");
            DumpXML(tw, "    ", img.WzProperties);
            tw.WriteLine("</imgdir>");
            tw.Close();
        }

        private static void DumpXML(TextWriter tw, string depth, List<IWzImageProperty> props)
        {
            foreach (IWzImageProperty property in props)
            {
                if (property != null)
                {
                    if (property is WzCanvasProperty)
                    {
                        WzCanvasProperty canvas = (WzCanvasProperty)property;
                        MemoryStream stream = new MemoryStream();
                        canvas.PngProperty.GetPNG(false).Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] pngbytes = stream.ToArray();
                        stream.Close();
                        tw.WriteLine(string.Concat(new object[] { depth, "<canvas name=\"", canvas.Name, "\" width=\"", canvas.PngProperty.Width, "\" height=\"", canvas.PngProperty.Height, "\" basedata=\"", Convert.ToBase64String(pngbytes), "\">" }));
                        DumpXML(tw, depth + indent, canvas.WzProperties);
                        tw.WriteLine(depth + "</canvas>");
                    }
                    else if (property is WzCompressedIntProperty)
                    {
                        WzCompressedIntProperty compressedInt = (WzCompressedIntProperty)property;
                        tw.WriteLine(string.Concat(new object[] { depth, "<int name=\"", compressedInt.Name, "\" value=\"", compressedInt.Value, "\"/>" }));
                    }
                    else if (property is WzDoubleProperty)
                    {
                        WzDoubleProperty doubleProp = (WzDoubleProperty)property;
                        tw.WriteLine(string.Concat(new object[] { depth, "<double name=\"", doubleProp.Name, "\" value=\"", doubleProp.Value.ToString(formattingInfo), "\"/>" }));
                    }
                    else if (property is WzNullProperty)
                    {
                        WzNullProperty nullProp = (WzNullProperty)property;
                        tw.WriteLine(depth + "<null name=\"" + nullProp.Name + "\"/>");
                    }
                    else if (property is WzSoundProperty)
                    {
                        WzSoundProperty sound = (WzSoundProperty)property;
                        tw.WriteLine(string.Concat(new object[] { depth, "<sound name=\"", sound.Name, "\" basedata=\"", Convert.ToBase64String(sound.GetBytes(false)), "\"/>" }));
                    }
                    else if (property is WzStringProperty)
                    {
                        WzStringProperty stringProp = (WzStringProperty)property;
                        string str = stringProp.Value.Replace("<", "&lt;").Replace("&", "&amp;").Replace(">", "&gt;").Replace("'", "&apos;").Replace("\"", "&quot;");
                        tw.WriteLine(depth + "<string name=\"" + stringProp.Name + "\" value=\"" + str + "\"/>");
                    }
                    else if (property is WzSubProperty)
                    {
                        WzSubProperty sub = (WzSubProperty)property;
                        tw.WriteLine(depth + "<imgdir name=\"" + sub.Name + "\">");
                        DumpXML(tw, depth + indent, sub.WzProperties);
                        tw.WriteLine(depth + "</imgdir>");
                    }
                    else if (property is WzUnsignedShortProperty)
                    {
                        WzUnsignedShortProperty ushortProp = (WzUnsignedShortProperty)property;
                        tw.WriteLine(string.Concat(new object[] { depth, "<short name=\"", ushortProp.Name, "\" value=\"", ushortProp.Value.ToString(formattingInfo), "\"/>" }));
                    }
                    else if (property is WzUOLProperty)
                    {
                        WzUOLProperty uol = (WzUOLProperty)property;
                        tw.WriteLine(depth + "<uol name=\"" + uol.Name + "\" value=\"" + uol.Value + "\"/>");
                    }
                    else if (property is WzVectorProperty)
                    {
                        WzVectorProperty vector = (WzVectorProperty)property;
                        tw.WriteLine(string.Concat(new object[] { depth, "<vector name=\"", vector.Name, "\" x=\"", vector.X.Value, "\" y=\"", vector.Y.Value, "\"/>" }));
                    }
                    else if (property is WzByteFloatProperty)
                    {
                        WzByteFloatProperty floatProp = (WzByteFloatProperty)property;
                        string str2 = floatProp.Value.ToString(formattingInfo);
                        if (!str2.Contains("."))
                        {
                            str2 = str2 + ".0";
                        }
                        tw.WriteLine(depth + "<float name=\"" + floatProp.Name + "\" value=\"" + str2 + "\"/>");
                    }
                    else if (property is WzConvexProperty)
                    {
                        tw.WriteLine(depth + "<extended name=\"" + property.Name + "\">");
                        DumpXML(tw, depth + indent, ((WzConvexProperty)property).WzProperties);
                        tw.WriteLine(depth + "</extended>");
                    }
                }
            }
        }

        public static WzImage LoadMap(string xmlpath, ref string mapname, ref string streetname) //yes I know this is a shitty workaround
        {
            XmlDocument document = new XmlDocument();
            document.Load(xmlpath);
            WzImage wo = null;
            foreach (XmlNode node in document)
            {
                if (node is XmlElement)
                {
                    XmlElement element = (XmlElement)node;
                    wo = new WzImage(element.GetAttribute("name"));
                    mapname = element.GetAttribute("mapname");
                    streetname = element.GetAttribute("streetname");
                    ParseXML(element, wo);
                }
            }
            wo.Changed = true;
            return wo;
        }

        private static void ParseXML(XmlElement element, IPropertyContainer wo)
        {
            foreach (XmlNode node in element)
            {
                if (!(node is XmlElement)) continue;
                XmlElement childElement = (XmlElement)node;
                if (childElement.Name == "imgdir")
                {
                    WzSubProperty sub = new WzSubProperty(childElement.GetAttribute("name"));
                    wo.AddProperty(sub);
                    ParseXML(childElement, (IPropertyContainer)sub);
                }
                else if (childElement.Name == "canvas")
                {
                    WzCanvasProperty canvas = new WzCanvasProperty(childElement.GetAttribute("name"));
                    canvas.PngProperty = new WzPngProperty();
                    MemoryStream pngstream = new MemoryStream(Convert.FromBase64String(childElement.GetAttribute("basedata")));
                    canvas.PngProperty.SetPNG((Bitmap)Image.FromStream(pngstream, true, true));
                    wo.AddProperty(canvas);
                    ParseXML(childElement, (IPropertyContainer)canvas);
                }
                if (childElement.Name == "int")
                {
                    WzCompressedIntProperty compressedInt = new WzCompressedIntProperty(childElement.GetAttribute("name"), int.Parse(childElement.GetAttribute("value"), formattingInfo));
                    wo.AddProperty(compressedInt);
                }
                if (childElement.Name == "double")
                {
                    WzDoubleProperty doubleProp = new WzDoubleProperty(childElement.GetAttribute("name"), double.Parse(childElement.GetAttribute("value"), formattingInfo));
                    wo.AddProperty(doubleProp);
                }
                if (childElement.Name == "null")
                {
                    WzNullProperty nullProp = new WzNullProperty(childElement.GetAttribute("name"));
                    wo.AddProperty(nullProp);
                }
                if (childElement.Name == "sound")
                {
                    WzSoundProperty sound = new WzSoundProperty(childElement.GetAttribute("name"));
                    sound.SetDataUnsafe(Convert.FromBase64String(childElement.GetAttribute("basedata")));
                    wo.AddProperty(sound);
                }
                if (childElement.Name == "string")
                {
                    string str = childElement.GetAttribute("value").Replace("&lt;", "<").Replace("&amp;", "&").Replace("&gt;", ">").Replace("&apos;", "'").Replace("&quot;", "\"");
                    WzStringProperty stringProp = new WzStringProperty(childElement.GetAttribute("name"), str);
                    wo.AddProperty(stringProp);
                }
                if (childElement.Name == "short")
                {
                    WzUnsignedShortProperty shortProp = new WzUnsignedShortProperty(childElement.GetAttribute("name"), ushort.Parse(childElement.GetAttribute("value"), formattingInfo));
                    wo.AddProperty(shortProp);
                }
                if (childElement.Name == "uol")
                {
                    WzUOLProperty uol = new WzUOLProperty(childElement.GetAttribute("name"), childElement.GetAttribute("value"));
                    wo.AddProperty(uol);
                }
                if (childElement.Name == "vector")
                {
                    WzVectorProperty vector = new WzVectorProperty(childElement.GetAttribute("name"), new WzCompressedIntProperty("x", Convert.ToInt32(childElement.GetAttribute("x"))), new WzCompressedIntProperty("y", Convert.ToInt32(childElement.GetAttribute("y"))));
                    wo.AddProperty(vector);
                }
                if (childElement.Name == "float")
                {
                    WzByteFloatProperty floatProp = new WzByteFloatProperty(childElement.GetAttribute("name"), float.Parse(childElement.GetAttribute("value"), formattingInfo));
                    wo.AddProperty(floatProp);
                }
                if (childElement.Name == "extended")
                {
                    WzConvexProperty convex = new WzConvexProperty(childElement.GetAttribute("name"));
                    wo.AddProperty(convex);
                    ParseXML(childElement, (IPropertyContainer)convex);
                }
            }
        }
    }
}