using Formulatte.Engine.Common;
using Formulatte.Engine.Scripts.Equations.Common.UndoRedo;
using Formulatte.Engine.Scripts.Equations.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formulatte
{
    public class TextAction : EquationAction
    {
        public int Index { get; set; }
        public string Text { get; set; }
        public int[] Formats { get; set; }
        public EditorMode[] Modes { get; set; }
        public CharacterDecorationInfo[] Decorations { get; set; }

        public TextAction(ISupportsUndo executor, int index, string text, int[] formats, EditorMode[] modes, CharacterDecorationInfo[] decorations)
            : base(executor)
        {
            Index = index;
            Text = text;
            Formats = formats;
            Modes = modes;
            Decorations = decorations;
        }
    }
}