using Formulatte.Engine.Controls;
using Formulatte.Engine.Scripts.Equations.Common.UndoRedo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulatte.Engine.Common
{
    public class EditorHandler : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  
        }

        public EditorControl Control_Editor { get; private set; }
        public CharacterToolBar Control_Characters { get; private set; }
        public EquationToolBar Control_Equations { get; private set; }
        public HistoryToolBar Control_History { get; private set; }
        public UndoManager UndoManager { get; private set; }

        public EditorHandler(EditorControl editor, CharacterToolBar characterToolBar, EquationToolBar equationToolBar, HistoryToolBar historyToolBar)
        {
            Control_Editor = editor;
            Control_Characters = characterToolBar;
            Control_Equations = equationToolBar;
            Control_History = historyToolBar;

            Initialize();
        }

        public EditorHandler()
        {
            Control_Editor = new EditorControl();
            Control_Characters = new CharacterToolBar();
            Control_Equations = new EquationToolBar();
            Control_History = new HistoryToolBar();

            Initialize();
        }



        private void Initialize()
        {
            UndoManager = new UndoManager();

            Control_Editor.EditorHandler = this;
            Control_Characters.EditorHandler = this;
            Control_History.EditorHandler = this;
            Control_Equations.EditorHandler = this;
        }

        public void HandleToolBarCommand(CommandDetails commandDetails)
        {
            if (commandDetails.CommandType == CommandType.CustomMatrix)
            {
                MatrixInputForm inputForm = new MatrixInputForm(((int[])commandDetails.CommandParam)[0], ((int[])commandDetails.CommandParam)[1]);
                inputForm.ProcessRequest += (x, y) =>
                {
                    CommandDetails newCommand = new CommandDetails { CommandType = CommandType.Matrix };
                    newCommand.CommandParam = new int[] { x, y };
                    Control_Editor.HandleUserCommand(newCommand);
                };
                inputForm.ShowDialog();
            }
            else
            {
                Control_Editor.HandleUserCommand(commandDetails);
                if (commandDetails.CommandType == CommandType.Text)
                {
                    Control_History.AddItem(commandDetails.UnicodeString);
                }
            }
        }


    }
}
