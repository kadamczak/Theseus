﻿using System;
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
        private readonly MazeGeneratorViewModel _viewModel;
        private readonly MazeDetailsStore _mazeDetailsStore;
        private readonly NavigationService<MazeDetailsViewModel> _mazeDetailNavigationService;

        private const int MaxMazeDimension = 99;
        private const int MinMazeDimension = 2;

        public GenerateMazeCommand(MazeGeneratorViewModel viewModel,
                                   MazeDetailsStore mazeDetailsStore,
                                   NavigationService<MazeDetailsViewModel> mazeDetailNavigationService)
        {
            this._viewModel = viewModel;
            this._mazeDetailsStore = mazeDetailsStore;
            this._mazeDetailNavigationService = mazeDetailNavigationService;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            var generator = MazeGeneratorFactory.Create(_viewModel.SelectedAlgorithm.Algorithm);

            int height = Int32.Parse(_viewModel.MazeHeight);
            int width = Int32.Parse(_viewModel.MazeWidth);

            Maze maze = generator.GenerateMaze(height, width);

            _mazeDetailsStore.UpdateMazeDetails(id: null, maze, unsavedChanges: true);

            _mazeDetailNavigationService.Navigate();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string property = e.PropertyName;

            if (property == nameof(_viewModel.MazeWidth) || property == nameof(_viewModel.MazeHeight))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            if (!IsMazeDimensionValid(_viewModel.MazeHeight))
                return false;

            if (!IsMazeDimensionValid(_viewModel.MazeWidth))
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