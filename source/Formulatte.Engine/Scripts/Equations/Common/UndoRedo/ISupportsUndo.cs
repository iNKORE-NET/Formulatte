using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formulatte
{
    public interface ISupportsUndo
    {
        void ProcessUndo(EquationAction action);
    }
}

