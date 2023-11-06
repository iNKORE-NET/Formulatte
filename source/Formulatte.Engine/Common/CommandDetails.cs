using iNKORE.UI.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Formulatte.Engine.Common
{
    public class CommandDetails
    {
        public OpacityMaskedImage Image { get; set; }
        public string UnicodeString { get; set; }
        public CommandType CommandType { get; set; }
        public object CommandParam { get; set; }
    }
}
