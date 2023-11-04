using Formulatte.Engine.Common;
using Formulatte.Engine.Scripts.Equations.Common.UndoRedo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formulatte.Engine.Scripts.Equations.Common.UndoRedo.Text
{
    public class ModeChangeAction : EquationAction
    {
        public int Index { get; set; }
        public EditorMode[] OldModes { get; set; }
        public EditorMode[] NewModes { get; set; }

        public ModeChangeAction(ISupportsUndo executor, int index, EditorMode[] oldModes, EditorMode[] newModes)
            : base(executor)
        {
            Index = index;
            OldModes = oldModes;
            NewModes = newModes;
        }
    }
}
