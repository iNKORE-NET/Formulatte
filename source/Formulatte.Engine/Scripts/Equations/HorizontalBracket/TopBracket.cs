using Formulatte.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formulatte.Engine.Scripts.Equations.HorizontalBracket
{
    public class TopBracket : HorizontalBracket
    {
        public TopBracket(EquationContainer parent, HorizontalBracketSignType signType)
             : base(parent, signType)
        {
            topEquation.FontFactor = SubFontFactor;
            ActiveChild = bottomEquation;
        }
    }
}
