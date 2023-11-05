using Formulatte.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Formulatte.Engine.Dialogs
{
    /// <summary>
    /// Interaction logic for CustomZoomWindow.xaml
    /// </summary>
    public partial class MatrixInputWindow : Window
    {
        public event Action<int, int> ProcessRequest = (x, y) => { };

        public MatrixInputWindow(int rows, int columns)
        {
            InitializeComponent();
            NumberBox_Columns.Value = columns;
            NumberBox_Rows.Value = rows;
        }

        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessRequest((int)NumberBox_Rows.Value, (int)NumberBox_Columns.Value);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
        private void codeFormatComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
