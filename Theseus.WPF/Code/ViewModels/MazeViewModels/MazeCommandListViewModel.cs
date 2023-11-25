using System.Collections.ObjectModel;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public abstract class MazeCommandListViewModel : ViewModelBase
    {
        private SelectedModelListStore<MazeWithSolution> _mazeListStore;
        public ObservableCollection<CommandViewModel<MazeWithSolutionCanvasViewModel>> ActionableMazes { get; } = new ObservableCollection<CommandViewModel<MazeWithSolutionCanvasViewModel>>();

        public MazeCommandListViewModel(SelectedModelListStore<MazeWithSolution> mazeListStore)
        {
            this._mazeListStore = mazeListStore;
        }

        public void CreateMazeCommandViewModels()
        {
            this.ActionableMazes.Clear();

            foreach (var mazeWithSolution in _mazeListStore.ModelList)
            {
                AddMazeWithCommandToActionableMazes(mazeWithSolution);
            }
        }

        protected abstract void AddMazeWithCommandToActionableMazes(MazeWithSolution mazeWithSolution);
    }
}