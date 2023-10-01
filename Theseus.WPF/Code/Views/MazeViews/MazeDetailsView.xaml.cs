using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.Generators;
using Theseus.Domain.Models.MazeRelated.MazeStructure;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for MazeDetailsView.xaml
    /// </summary>
    public partial class MazeDetailsView : UserControl
    {
        public MazeDetailsView()
        {
            InitializeComponent();
        }
    }
}
