using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Xml.Linq;

/* 项目“Formulatte.Engine (net6.0-windows)”的未合并的更改
在此之前:
using Formulatte.Engine.Common;
在此之后:
using Formulatte.Engine.Common;
using Formulatte;
using Formulatte.Engine.Scripts.Equations.Text;
*/
using Formulatte.Engine.Common;
using iNKORE.Coreworks.Windows.Helpers;

namespace Formulatte.Engine.Scripts.Equations.Text
{
    public class TextFormat
    {
        public double FontSize { get; private set; }
        public FontType FontType { get; private set; }
        public FontFamily FontFamily { get; private set; }
        public FontStyle FontStyle { get; private set; }
        public FontWeight FontWeight { get; private set; }
        public SolidColorBrush TextBrush { get; private set; }
        public Typeface TypeFace { get; private set; }
        public string TextBrushString { get; private set; }
        public bool UseUnderline { get; set; }
        public int Index { get; set; }

        public TextFormat(double size, FontType ft, FontStyle fs, FontWeight fw, SolidColorBrush brush, bool useUnderline)
        {
            FontSize = Math.Round(size, 1);
            FontType = ft;
            FontFamily = FontFactory.GetFontFamily(ft);
            FontStyle = fs;
            UseUnderline = useUnderline;
            FontWeight = fw;
            TextBrush = brush;
            TypeFace = new Typeface(FontFamily, fs, fw, FontStretches.Normal, FontFactory.GetFontFamily(FontType.STIXGeneral));
            //BrushConverter bc = new BrushConverter();
            TextBrushString = brush.Color.ColorToHex();
        }

        public XElement Serialize()
        {
            XElement thisElement = new XElement(GetType().Name);
            thisElement.Add(new XAttribute("FontSize", FontSize),
                             new XAttribute("FontType", FontType),
                             new XAttribute("FontStyle", FontStyle),
                             new XAttribute("Underline", UseUnderline),
                             new XAttribute("FontWeight", FontWeight),
                             new XAttribute("Brush", TextBrushString));
            return thisElement;
        }

        public static TextFormat DeSerialize(XElement xe)
        {
            try
            {
                double fontSize = double.Parse(xe.Attribute("FontSize").Value);
                FontType fontType = (FontType)Enum.Parse(typeof(FontType), xe.Attribute("FontType").Value);
                FontStyle fontStyle = xe.Attribute("FontStyle").Value == "Italic" ? FontStyles.Italic : FontStyles.Normal;
                FontWeight fontWeight = xe.Attribute("FontWeight").Value == "Bold" ? FontWeights.Bold : FontWeights.Normal;
                BrushConverter bc = new BrushConverter();
                SolidColorBrush brush = new SolidColorBrush(TypeHelper.HexToWpfColor(xe.Attribute("Brush").Value));
                bool useUnderline = Convert.ToBoolean(xe.Attribute("Underline").Value);
                return new TextFormat(fontSize, fontType, fontStyle, fontWeight, brush, useUnderline);
            }
            catch
            {
                return new TextFormat(12, FontType.STIXGeneral, FontStyles.Normal, FontWeights.Normal, Brushes.Black, false);

            }
        }
    }
}
