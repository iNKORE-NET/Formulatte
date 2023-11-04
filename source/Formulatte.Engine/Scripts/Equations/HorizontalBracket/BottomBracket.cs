using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formulatte
{
    public class BottomBracket : HorizontalBracket
    {
        public BottomBracket(EquationContainer parent, HorizontalBracketSignType signType)
             :base (parent, signType)
        {
            bottomEquation.FontFactor = SubFontFactor;
            ActiveChild = topEquation;
        }
    }
}
