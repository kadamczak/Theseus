using System.Collections.ObjectModel;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public abstract class MazeCommandListViewModel : ViewModelBase
    {
        private MazeListStore _mazeListStore;
        public ObservableCollection<MazeWithSolutionCommandViewModel> ActionableMazes { get; } = new ObservableCollection<MazeWithSolutionCommandViewModel>();

        public MazeCommandListViewModel(MazeListStore mazeListStore)
        {
            this._mazeListStore = mazeListStore;
            LoadMazesFromMazeListStore();
        }

        public void LoadMazesFromMazeListStore()
        {
            this.ActionableMazes.Clear();

            foreach (var mazeWithSolution in _mazeListStore.MazesInList)
            {
                AddMazeWithCommandToActionableMazes(mazeWithSolution);
            }
        }

        protected abstract void AddMazeWithCommandToActionableMazes(MazeWithSolution mazeWithSolution);
    }
}