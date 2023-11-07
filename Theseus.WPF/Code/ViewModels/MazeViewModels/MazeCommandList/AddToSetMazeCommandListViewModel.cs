using System.Collections.ObjectModel;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class AddToSetMazeCommandListViewModel : MazeCommandListViewModel
    {
        public ObservableCollection<MazeWithSolution> SelectedMazes = new ObservableCollection<MazeWithSolution>();

        public AddToSetMazeCommandListViewModel(SelectedMazeListStore mazeListStore) : base(mazeListStore)
        {
        }

        protected override void AddMazeWithCommandToActionableMazes(MazeWithSolution mazeWithSolution)
        {
            MazeWithSolutionCommandViewModel actionableMaze = new MazeWithSolutionCommandViewModel(mazeWithSolution);
            actionableMaze.Command = new AddToSetCommand(actionableMaze, this);
            actionableMaze.CommandName = "Add";
            this.ActionableMazes.Add(actionableMaze);
        }
    }
}