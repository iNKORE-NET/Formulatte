using Formulatte.Engine.Scripts.Equations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Formulatte.Engine.Scripts.Equations.Division
{
    public class DivRegularSmall : DivRegular
    {
        public DivRegularSmall(EquationContainer parent)
            : base(parent, true)
        {
        }
    }

    public class DivDoubleBar : DivRegular
    {
        public DivDoubleBar(EquationContainer parent)
            : base(parent)
        {
            barCount = 2;
        }
    }

    public class DivTripleBar : DivRegular
    {
        public DivTripleBar(EquationContainer parent)
            : base(parent)
        {
            barCount = 3;
        }
    }

    public class DivSlantedSmall : DivSlanted
    {
        public DivSlantedSmall(EquationContainer parent)
            : base(parent, true)
        {
        }
    }

    public class DivHorizSmall : DivHorizontal
    {
        public DivHorizSmall(EquationContainer parent)
            : base(parent, true)
        {
        }
    }

}
