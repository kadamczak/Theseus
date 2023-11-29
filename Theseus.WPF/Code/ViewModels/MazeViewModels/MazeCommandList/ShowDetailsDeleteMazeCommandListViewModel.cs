//using Theseus.Domain.CommandInterfaces.MazeCommandInterfaces;
//using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
//using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
//using Theseus.WPF.Code.Commands.MazeCommands;
//using Theseus.WPF.Code.Services;
//using Theseus.WPF.Code.Stores;
//using Theseus.WPF.Code.ViewModels.Bindings;

//namespace Theseus.WPF.Code.ViewModels
//{
//    public class ShowDetailsDeleteMazeCommandListViewModel : CommandListViewModel<MazeWithSolutionCanvasViewModel>
//    {
//        private readonly SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel> _mazeDetailsStore;
//        private readonly NavigationService<MazeDetailsViewModel> _mazeDetailsNavigationService;
//        private readonly IDeleteMazeWithSolutionCommand _removeMazeCommand;
//        private readonly IGetExamSetsWithMazeQuery _getExamSetsWithMazeQuery;

//        public ShowDetailsDeleteMazeCommandListViewModel(SelectedModelListStore<MazeWithSolutionCanvasViewModel> mazeListStore,
//                                                         SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel> mazeDetailsStore,
//                                                         NavigationService<MazeDetailsViewModel> mazeDetailsNavigationService,
//                                                         IDeleteMazeWithSolutionCommand removeMazeCommand,
//                                                         IGetExamSetsWithMazeQuery getExamSetsWithMazeQuery) : base(mazeListStore)
//        {
//            _mazeDetailsStore = mazeDetailsStore;
//            _mazeDetailsNavigationService = mazeDetailsNavigationService;
//            _removeMazeCommand = removeMazeCommand;
//            _getExamSetsWithMazeQuery = getExamSetsWithMazeQuery;
//        }

//        public override void AddModelToActionableModels(MazeWithSolutionCanvasViewModel mazeWithSolution)
//        {
//            var actionableMaze = new CommandViewModel<MazeWithSolutionCanvasViewModel>(mazeWithSolution)
//            {
//                Command1Name = "Details",
//                ShowCommand1 = true,
//                Command2Name = "Delete",
//                ShowCommand2 = true
//            };

//            actionableMaze.Button1 = new ShowMazeDetailsCommand(actionableMaze, _mazeDetailsStore, _mazeDetailsNavigationService);
//            actionableMaze.Button2 = new DeleteMazeCommand(this, actionableMaze, _removeMazeCommand, _getExamSetsWithMazeQuery);

//            this.ActionableModels.Add(actionableMaze);
//        }
//    }
//}