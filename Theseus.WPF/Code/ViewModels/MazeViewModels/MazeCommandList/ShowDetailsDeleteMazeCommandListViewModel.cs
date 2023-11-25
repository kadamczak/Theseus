using Theseus.Domain.CommandInterfaces.MazeCommandInterfaces;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.WPF.Code.Commands.MazeCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class ShowDetailsDeleteMazeCommandListViewModel : MazeCommandListViewModel
    {
        private readonly SelectedMazeDetailsStore _mazeDetailsStore;
        private readonly NavigationService<MazeDetailsViewModel> _mazeDetailsNavigationService;
        private readonly IRemoveMazeWithSolutionCommand _removeMazeCommand;
        private readonly IGetExamSetsWithMazeQuery _getExamSetsWithMazeQuery;

        public ShowDetailsDeleteMazeCommandListViewModel(SelectedModelListStore<MazeWithSolution> mazeListStore,
                                                         SelectedMazeDetailsStore mazeDetailsStore,
                                                         NavigationService<MazeDetailsViewModel> mazeDetailsNavigationService,
                                                         IRemoveMazeWithSolutionCommand removeMazeCommand,
                                                         IGetExamSetsWithMazeQuery getExamSetsWithMazeQuery) : base(mazeListStore)
        {
            _mazeDetailsStore = mazeDetailsStore;
            _mazeDetailsNavigationService = mazeDetailsNavigationService;
            _removeMazeCommand = removeMazeCommand;
            _getExamSetsWithMazeQuery = getExamSetsWithMazeQuery;
        }

        protected override void AddMazeWithCommandToActionableMazes(MazeWithSolution mazeWithSolution)
        {
            var actionableMaze = new CommandViewModel<MazeWithSolutionCanvasViewModel>(new MazeWithSolutionCanvasViewModel(mazeWithSolution))
            {
                Command1Name = "Details",
                ShowCommand1 = true,
                Command2Name = "Delete",
                ShowCommand2 = true
            };

            actionableMaze.Command1 = new ShowMazeDetailsCommand(actionableMaze, _mazeDetailsStore, _mazeDetailsNavigationService);
            actionableMaze.Command2 = new DeleteMazeCommand(this, actionableMaze, _removeMazeCommand, _getExamSetsWithMazeQuery);

            this.ActionableMazes.Add(actionableMaze);
        }
    }
}