using System;
using System.ComponentModel;
using Theseus.Domain.Models.MazeRelated.Generators;
using Theseus.Domain.Models.MazeRelated.MazeStructure;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands
{
    public class GenerateMazeCommand : CommandBase
    {
        private readonly MazeGeneratorViewModel _mazeGenViewModel;
        private readonly MazeDetailsStore _mazeDetailsStore;
        private readonly NavigationService<MazeDetailsViewModel> _mazeDetailNavigationService;

        private const int MaxMazeDimension = 99;
        private const int MinMazeDimension = 2;

        public GenerateMazeCommand(MazeGeneratorViewModel viewModel,
                                   MazeDetailsStore mazeDetailsStore,
                                   NavigationService<MazeDetailsViewModel> mazeDetailNavigationService)
        {
            this._mazeGenViewModel = viewModel;
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
            var generator = MazeGeneratorFactory.Create(_mazeGenViewModel.SelectedAlgorithm.Algorithm);

            int height = Int32.Parse(_mazeGenViewModel.MazeHeight);
            int width = Int32.Parse(_mazeGenViewModel.MazeWidth);

            Maze maze = generator.GenerateMaze(height, width);

            _mazeDetailsStore.UpdateMazeDetails(maze, unsavedChanges: true);

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
            int userInputValue;

            if (!Int32.TryParse(userInput, out userInputValue))
            {
                return false;
            }

            return (userInputValue >= MinMazeDimension && userInputValue <= MaxMazeDimension);
        }
    }
}
