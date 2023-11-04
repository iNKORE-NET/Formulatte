using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

/* 项目“Formulatte.Engine (net6.0-windows)”的未合并的更改
在此之前:
using System.Collections.Generic;

namespace Formulatte
在此之后:
using System.Collections.Generic;
using Formulatte.Engine.Common;
using Formulatte;

namespace Formulatte
*/
using System.Collections.Generic;
using Formulatte.Engine.Scripts.Equations.Common;

namespace Formulatte.Engine.Common
{
    public class Caret : FrameworkElement
    {
        Point location;
        public double CaretLength { get; set; }
        bool isHorizontal = false;

        public static readonly DependencyProperty VisibleProperty = DependencyProperty.Register("Visible", typeof(bool), typeof(Caret), new FrameworkPropertyMetadata(false /* defaultValue */, FrameworkPropertyMetadataOptions.AffectsRender));

        public Caret(bool isHorizontal)
        {
            this.isHorizontal = isHorizontal;
            CaretLength = 18;
            Visible = true;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (Visible)
            {
                dc.DrawLine(PenManager.GetPen(Math.Max(1, EditorControl.RootFontSize * .8 / EditorControl.rootFontBaseSize)), location, OtherPoint);
            }
            else if (isHorizontal)
            {
                dc.DrawLine(PenManager.GetWhitePen(Math.Max(1, EditorControl.RootFontSize * .8 / EditorControl.rootFontBaseSize)), location, OtherPoint);
            }
        }

        Point OtherPoint
        {
            get
            {
                if (isHorizontal)
                {
                    return new Point(Left + CaretLength, Top);
                }
                else
                {
                    return new Point(Left, VerticalCaretBottom);
                }
            }
        }

        public void ToggleVisibility()
        {
            Dispatcher.Invoke(new Action(() => { Visible = !Visible; }));
        }

        bool Visible
        {
            get
            {
                return (bool)GetValue(VisibleProperty);
            }
            set
            {
                SetValue(VisibleProperty, value);
            }
        }

        public Point Location
        {
            get { return location; }
            set
            {
                location.X = Math.Floor(value.X) + .5;
                location.Y = Math.Floor(value.Y) + .5;
                if (Visible)
                {
                    Visible = false;
                }
            }
        }

        public double Left
        {
            get { return location.X; }
            set
            {
                location.X = Math.Floor(value) + .5;
                if (Visible)
                {
                    Visible = false;
                }
            }
        }

        public double Top
        {
            get { return location.Y; }
            set
            {
                location.Y = Math.Floor(value) + .5;
                if (Visible)
                {
                    Visible = false;
                }
            }
        }

        public double VerticalCaretBottom
        {
            get { return location.Y + CaretLength; }
        }
    }
}
