using System.Collections.ObjectModel;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class MazeCommandListViewModel : ViewModelBase
    {
        public ObservableCollection<MazeWithSolutionCommandViewModel> ActionableMazes { get; } = new ObservableCollection<MazeWithSolutionCommandViewModel>();

        public MazeCommandListViewModel()
        {
        }
    }
}