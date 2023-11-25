using System.Collections.ObjectModel;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class AddToSetMazeCommandListViewModel : MazeCommandListViewModel
    {
        public ObservableCollection<MazeWithSolution> SelectedMazes = new ObservableCollection<MazeWithSolution>();

        public AddToSetMazeCommandListViewModel(SelectedModelListStore<MazeWithSolution> mazeListStore) : base(mazeListStore)
        {
        }

        protected override void AddMazeWithCommandToActionableMazes(MazeWithSolution mazeWithSolution)
        {
            var actionableMaze = new CommandViewModel<MazeWithSolutionCanvasViewModel>(new MazeWithSolutionCanvasViewModel(mazeWithSolution));
            actionableMaze.Command1 = new AddToExamSetCommand(actionableMaze, this);
            actionableMaze.Command1Name = "Add";
            actionableMaze.ShowCommand1 = true;

            this.ActionableMazes.Add(actionableMaze);
        }
    }
}