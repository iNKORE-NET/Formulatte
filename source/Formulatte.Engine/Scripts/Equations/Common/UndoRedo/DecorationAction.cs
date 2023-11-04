using Formulatte.Engine.Scripts.Equations.Common.UndoRedo;
using Formulatte.Engine.Scripts.Equations.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formulatte
{
    public class DecorationAction : EquationAction
    {
        public CharacterDecorationInfo [] CharacterDecorations { get; set; }
        public DecorationAction(ISupportsUndo executor, CharacterDecorationInfo [] cdi)
            : base(executor)
        {
            CharacterDecorations = cdi;
        }
    }
}