using System.Windows;
using System.Windows.Controls;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for ExamTransitionView.xaml
    /// </summary>
    public partial class ExamTransitionView : UserControl
    {
        public ExamTransitionView()
        {
            InitializeComponent();
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            Button nextPageButton = this.FindName("NextPageButton") as Button;
            nextPageButton.Command.Execute(null);
        }
    }
}