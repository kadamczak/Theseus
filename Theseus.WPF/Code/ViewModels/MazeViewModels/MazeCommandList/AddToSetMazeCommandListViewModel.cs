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
            var actionableMaze = new CommandViewModel<MazeWithSolutionCanvasViewModel>(new MazeWithSolutionCanvasViewModel(mazeWithSolution))
            {
               Command1Name = "Add",
               ShowCommand1 = true
            };

            actionableMaze.Command1 = new AddToExamSetCommand(actionableMaze, this);
            this.ActionableMazes.Add(actionableMaze);
        }
    }
}