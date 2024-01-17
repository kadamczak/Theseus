using System.ComponentModel;
using Theseus.Domain.Models.MazeRelated.MazeCreators;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.MazeCommands
{
    /// <summary>
    /// The <c>GenerateMazeCommand</c> class uses <c>MazeCreator</c> to generate an <c>MazeWithSolution</c> following
    /// settings saved in <c>MazeGeneratorViewModel</c>.
    /// </summary>
    public class GenerateMazeCommand : CommandBase
    {
        private readonly MazeGeneratorViewModel _mazeGenViewModel;
        private readonly MazeCreator _mazeCreator;
        private readonly SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel> _mazeDetailsStore;
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;
        private readonly NavigationService<MazeDetailsViewModel> _mazeDetailNavigationService;

        private const int MaxMazeDimension = 50;
        private const int MinMazeDimension = 3;

        public GenerateMazeCommand(MazeGeneratorViewModel mazeGenViewModel,
                                   MazeCreator mazeCreator,
                                   SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel> mazeDetailsStore,
                                   ICurrentStaffMemberStore currentStaffMemberStore,
                                   NavigationService<MazeDetailsViewModel> mazeDetailNavigationService)
        {
            _mazeGenViewModel = mazeGenViewModel;
            _mazeCreator = mazeCreator;
            _mazeDetailsStore = mazeDetailsStore;
            _currentStaffMemberStore = currentStaffMemberStore;
            _mazeDetailNavigationService = mazeDetailNavigationService;

            _mazeGenViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        protected override void Dispose()
        {
            _mazeGenViewModel.PropertyChanged -= OnViewModelPropertyChanged;
            base.Dispose();
        }

        public override void Execute(object? parameter)
        {
            int height = int.Parse(_mazeGenViewModel.MazeHeight);
            int width = int.Parse(_mazeGenViewModel.MazeWidth);

            var structureAlgorithm = _mazeGenViewModel.SelectedStructureAlgorithm.Value;
            var solutionAlgorithm = _mazeGenViewModel.SelectedSolutionAlgorithm.Value;
            bool shouldExcludeCellsCloseToRoot = _mazeGenViewModel.ShouldExcludeCellsCloseToRoot;

            var mazeWithSolution = _mazeCreator.CreateMazeWithSolution(height,
                                                                       width,
                                                                       structureAlgorithm,
                                                                       solutionAlgorithm,
                                                                       shouldExcludeCellsCloseToRoot);

            mazeWithSolution.StaffMember = _currentStaffMemberStore.StaffMember ?? throw new StaffMemberNotLoggedInException();
            _mazeDetailsStore.SelectedModel = new MazeWithSolutionCanvasViewModel(mazeWithSolution);
            _mazeDetailNavigationService.Navigate();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string property = e.PropertyName;

            if (property == nameof(_mazeGenViewModel.MazeWidth) || property == nameof(_mazeGenViewModel.MazeHeight))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            if (!IsMazeDimensionValid(_mazeGenViewModel.MazeHeight))
                return false;

            if (!IsMazeDimensionValid(_mazeGenViewModel.MazeWidth))
                return false;

            return base.CanExecute(parameter);
        }

        private bool IsMazeDimensionValid(string userInput)
        {
            if (!int.TryParse(userInput, out int userInputValue))
            {
                return false;
            }

            return userInputValue >= MinMazeDimension && userInputValue <= MaxMazeDimension;
        }
    }
}
