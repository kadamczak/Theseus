using System.Collections.ObjectModel;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public abstract class MazeCommandListViewModel : ViewModelBase
    {
        private SelectedMazeListStore _mazeListStore;
        public ObservableCollection<MazeWithSolutionCommandViewModel> ActionableMazes { get; } = new ObservableCollection<MazeWithSolutionCommandViewModel>();

        public MazeCommandListViewModel(SelectedMazeListStore mazeListStore)
        {
            this._mazeListStore = mazeListStore;
            //CreateMazeCommandViewModels();
        }

        public void CreateMazeCommandViewModels()
        {
            this.ActionableMazes.Clear();

            foreach (var mazeWithSolution in _mazeListStore.Mazes)
            {
                AddMazeWithCommandToActionableMazes(mazeWithSolution);
            }
        }

        protected abstract void AddMazeWithCommandToActionableMazes(MazeWithSolution mazeWithSolution);
    }
}