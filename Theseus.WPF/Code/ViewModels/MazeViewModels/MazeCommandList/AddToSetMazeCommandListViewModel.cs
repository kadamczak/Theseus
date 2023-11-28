using System.Collections.ObjectModel;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class AddToSetMazeCommandListViewModel : CommandListViewModel<MazeWithSolutionCanvasViewModel>
    {
        public ObservableCollection<MazeWithSolution> SelectedMazes = new ObservableCollection<MazeWithSolution>();

        public AddToSetMazeCommandListViewModel(SelectedModelListStore<MazeWithSolutionCanvasViewModel> mazeListStore) : base(mazeListStore)
        {
        }

        public override void AddModelToActionableModels(MazeWithSolutionCanvasViewModel mazeWithSolution)
        {
            var actionableMaze = new CommandViewModel<MazeWithSolutionCanvasViewModel>(mazeWithSolution)
            {
               Command1Name = "Add",
               ShowCommand1 = true
            };

            actionableMaze.Command1 = new AddToExamSetCommand(actionableMaze, this);
            this.ActionableModels.Add(actionableMaze);
        }
    }
}