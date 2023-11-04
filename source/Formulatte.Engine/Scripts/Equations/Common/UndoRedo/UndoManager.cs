using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formulatte.Engine.Scripts.Equations.Common.UndoRedo
{
    public class UndoManager
    {
        public bool DisableAddingActions { get; set; }
        Stack<EquationAction> undoStack = new Stack<EquationAction>();
        Stack<EquationAction> redoStack = new Stack<EquationAction>();

        public event EventHandler<UndoEventArgs> CanUndo = (a, b) => { };
        public event EventHandler<UndoEventArgs> CanRedo = (a, b) => { };

        public void AddUndoAction(EquationAction equationAction)
        {
            if (!DisableAddingActions)
            {
                undoStack.Push(equationAction);
                redoStack.Clear();
                CanUndo(null, new UndoEventArgs(true));
                CanRedo(null, new UndoEventArgs(false));
            }
        }

        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                EquationAction temp = undoStack.Peek();
                for (int i = 0; i <= temp.FurtherUndoCount; i++)
                {
                    EquationAction action = undoStack.Pop();
                    action.Executor.ProcessUndo(action);
                    action.UndoFlag = !action.UndoFlag;
                    redoStack.Push(action);
                }
                if (undoStack.Count == 0)
                {
                    CanUndo(null, new UndoEventArgs(false));
                }
                CanRedo(null, new UndoEventArgs(true));
            }
        }

        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                EquationAction temp = redoStack.Peek();
                for (int i = 0; i <= temp.FurtherUndoCount; i++)
                {
                    EquationAction action = redoStack.Pop();
                    action.Executor.ProcessUndo(action);
                    action.UndoFlag = !action.UndoFlag;
                    undoStack.Push(action);
                }
                if (redoStack.Count == 0)
                {
                    CanRedo(null, new UndoEventArgs(false));
                }
                CanUndo(null, new UndoEventArgs(true));
            }
        }

        public void ClearAll()
        {
            undoStack.Clear();
            redoStack.Clear();
            CanUndo(null, new UndoEventArgs(false));
            CanRedo(null, new UndoEventArgs(false));
        }

        public int UndoCount
        {
            get { return undoStack.Count; }
        }

        public void ChangeUndoCountOfLastAction(int newCount)
        {
            undoStack.Peek().FurtherUndoCount = newCount;
            for (int i = 0; i < newCount; i++)
            {
                redoStack.Push(undoStack.Pop());
            }
            undoStack.Peek().FurtherUndoCount = newCount;
            for (int i = 0; i < newCount; i++)
            {
                undoStack.Push(redoStack.Pop());
            }
        }
    }
}

