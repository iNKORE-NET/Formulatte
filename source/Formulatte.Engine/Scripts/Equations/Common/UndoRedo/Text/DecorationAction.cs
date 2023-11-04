using Formulatte.Engine.Scripts.Equations.Common.UndoRedo;
using Formulatte.Engine.Scripts.Equations.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formulatte.Engine.Scripts.Equations.Common.UndoRedo.Text
{
    public class DecorationAction : EquationAction
    {
        public CharacterDecorationInfo[] CharacterDecorations { get; set; }
        public bool Added { get; set; }

        public DecorationAction(ISupportsUndo executor, CharacterDecorationInfo[] cdi, bool added)
            : base(executor)
        {
            Added = added;
            CharacterDecorations = cdi;
        }

        public DecorationAction(ISupportsUndo executor, CharacterDecorationInfo[] cdi) : base(executor)
        {
            CharacterDecorations = cdi;
        }
    }
}