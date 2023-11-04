using Formulatte.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formulatte.Engine.Scripts.Equations.HorizontalBracket
{
    public class BottomBracket : HorizontalBracket
    {
        public BottomBracket(EquationContainer parent, HorizontalBracketSignType signType)
             : base(parent, signType)
        {
            bottomEquation.FontFactor = SubFontFactor;
            ActiveChild = topEquation;
        }
    }
}
