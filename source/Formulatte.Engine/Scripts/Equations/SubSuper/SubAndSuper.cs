using System;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using Formulatte.Engine.Common;

namespace Formulatte.Engine.Scripts.Equations.SubSuper
{
    public class SubAndSuper : SubSuperBase
    {
        RowContainer superEquation;
        RowContainer subEquation;

        public SubAndSuper(EquationRow parent, Position position)
            : base(parent, position)
        {
            ActiveChild = superEquation = new RowContainer(this);
            subEquation = new RowContainer(this);
            childEquations.Add(superEquation);
            childEquations.Add(subEquation);
            if (SubLevel == 1)
            {
                superEquation.FontFactor = SubFontFactor;
                subEquation.FontFactor = SubFontFactor;
            }
            else if (SubLevel >= 2)
            {
                superEquation.FontFactor = SubSubFontFactor;
                subEquation.FontFactor = SubSubFontFactor;
            }
        }

        public override XElement Serialize()
        {
            XElement thisElement = new XElement(GetType().Name);
            XElement parameters = new XElement("parameters");
            parameters.Add(new XElement(Position.GetType().Name, Position));
            thisElement.Add(parameters);
            thisElement.Add(superEquation.Serialize());
            thisElement.Add(subEquation.Serialize());
            return thisElement;
        }

        public override void DeSerialize(XElement xElement)
        {
            XElement[] elements = xElement.Elements(typeof(RowContainer).Name).ToArray();
            superEquation.DeSerialize(elements[0]);
            subEquation.DeSerialize(elements[1]);
            CalculateSize();
        }

        protected override void CalculateWidth()
        {
            Width = Math.Max(subEquation.Width, superEquation.Width) + Padding * 2;
        }

        protected override void CalculateHeight()
        {
            if (Buddy.GetType() == typeof(TextEquation))
            {
                double superHeight = superEquation.Height + Buddy.RefY - SuperOverlap; ;
                double subHeight = subEquation.Height - SubOverlap;
                Height = subHeight + superHeight;
            }
            else
            {
                Height = Buddy.Height - SuperOverlap - SubOverlap * 2 + subEquation.Height + superEquation.Height;
            }
        }

        public override double Top
        {
            get { return base.Top; }
            set
            {
                base.Top = value;
                superEquation.Top = value;
                subEquation.Bottom = Bottom;
            }
        }

        public override double RefY
        {
            get
            {
                return superEquation.Height + Buddy.RefY - SuperOverlap;
            }
        }

        public override bool ConsumeKey(Key key)
        {
            if (ActiveChild.ConsumeKey(key))
            {
                CalculateSize();
                return true;
            }
            if (key == Key.Down)
            {
                if (ActiveChild == superEquation)
                {
                    ActiveChild = subEquation;
                    return true;
                }
            }
            else if (key == Key.Up)
            {
                if (ActiveChild == subEquation)
                {
                    ActiveChild = superEquation;
                    return true;
                }
            }
            return false;
        }

        public override double Left
        {
            get { return base.Left; }
            set
            {
                base.Left = value;
                if (Position == Position.Right)
                {
                    subEquation.Left = Left + Padding;
                    superEquation.Left = Left + Padding;
                }
                else
                {
                    subEquation.Right = Right - Padding;
                    superEquation.Right = Right - Padding;
                }
            }
        }
    }
}
