using System;
using System.ComponentModel;
using Theseus.Domain.Models.MazeRelated.MazeCreators;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands
{
    public class GenerateMazeCommand : CommandBase
    {
        private readonly MazeGeneratorViewModel _mazeGenViewModel;
        private readonly MazeCreator _mazeCreator;
        private readonly SelectedMazeDetailsStore _mazeDetailsStore;
        private readonly NavigationService<MazeDetailsViewModel> _mazeDetailNavigationService;

        private const int MaxMazeDimension = 50;
        private const int MinMazeDimension = 2;

        public GenerateMazeCommand(MazeGeneratorViewModel mazeGenViewModel,
                                   MazeCreator mazeCreator,  
                                   SelectedMazeDetailsStore mazeDetailsStore,
                                   NavigationService<MazeDetailsViewModel> mazeDetailNavigationService)
        {
            this._mazeGenViewModel = mazeGenViewModel;
            this._mazeCreator = mazeCreator;
            this._mazeDetailsStore = mazeDetailsStore;
            this._mazeDetailNavigationService = mazeDetailNavigationService;

            _mazeGenViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        protected override void Dispose()
        {
            _mazeGenViewModel.PropertyChanged -= OnViewModelPropertyChanged;
            base.Dispose();
        }

        public override void Execute(object? parameter)
        {
            int height = Int32.Parse(_mazeGenViewModel.MazeHeight);
            int width = Int32.Parse(_mazeGenViewModel.MazeWidth);

            var structureAlgorithm = _mazeGenViewModel.SelectedStructureAlgorithm.Algorithm;
            var solutionAlgorithm = _mazeGenViewModel.SelectedSolutionAlgorithm.Algorithm;
            bool shouldExcludeCellsCloseToRoot = _mazeGenViewModel.ShouldExcludeCellsCloseToRoot;

            var mazeWithSolution = this._mazeCreator.CreateMazeWithSolution(height,
                                                                            width,
                                                                            structureAlgorithm,
                                                                            solutionAlgorithm,
                                                                            shouldExcludeCellsCloseToRoot);

            _mazeDetailsStore.UpdateMazeDetails(mazeWithSolution, unsavedChanges: true);
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
            if (!Int32.TryParse(userInput, out int userInputValue))
            {
                return false;
            }

            return (userInputValue >= MinMazeDimension && userInputValue <= MaxMazeDimension);
        }
    }
}
