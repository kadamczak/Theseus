using System;
using System.ComponentModel;
using Theseus.Code.Bases;
using Theseus.Code.MVVM.Models.Maze.Generators;
using Theseus.Code.MVVM.Models.Maze.GridStructure;
using Theseus.Code.MVVM.ViewModels;
using Theseus.Code.Services;

namespace Theseus.Code.Commands
{
    public class GenerateMazeCommand : CommandBase
    {
        private readonly MazeGeneratorSettingsViewModel _viewModel;
        private readonly NavigationService _mazeDetailNavigationService;

        public GenerateMazeCommand(MazeGeneratorSettingsViewModel viewModel, NavigationService mazeDetailNavigationService)
        {
            this._viewModel = viewModel;
            this._mazeDetailNavigationService = mazeDetailNavigationService;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            MazeGenerator generator = MazeGeneratorFactory.Create(_viewModel.SelectedAlgorithm.Algorithm);

            int height = Int32.Parse(_viewModel.MazeHeight);
            int width = Int32.Parse(_viewModel.MazeWidth);

            MazeGrid maze = generator.GenerateMaze(height, width);

            _mazeDetailNavigationService.Navigate(); //TODO
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string property = e.PropertyName;

            if(property == nameof(_viewModel.MazeWidth) || property == nameof(_viewModel.MazeHeight))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            int height, width;

            if (!Int32.TryParse(_viewModel.MazeHeight, out height))
            {
                return false;
            }

            if (!Int32.TryParse(_viewModel.MazeWidth, out width))
            {
                return false;
            }

            if (height < 2 || height > 99)
                return false;

            if (width < 2 || width > 99)
                return false;

            return base.CanExecute(parameter);
        }
    }
}
