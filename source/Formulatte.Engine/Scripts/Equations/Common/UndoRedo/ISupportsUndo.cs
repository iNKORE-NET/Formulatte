using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formulatte.Engine.Scripts.Equations.Common.UndoRedo
{
    public interface ISupportsUndo
    {
        void ProcessUndo(EquationAction action);
    }
}

