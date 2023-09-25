using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theseus.Code.Bases;
using Theseus.Code.MVVM.Models;
using Theseus.Code.MVVM.ViewModels;

namespace Theseus.Code.Commands
{
    public class SaveMazeGridCommand : AsyncCommandBase
    {
        private readonly MazeDetailViewModel _mazeDetailViewModel;
        private readonly ModelPersistence _modelPersistence;

        public SaveMazeGridCommand(MazeDetailViewModel mazeDetailViewModel, ModelPersistence modelPersistence)
        {
            this._mazeDetailViewModel = mazeDetailViewModel;
            this._modelPersistence = modelPersistence;

            _mazeDetailViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _mazeDetailViewModel.HasUnsavedChanges && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _modelPersistence.AddMazeGrid(_mazeDetailViewModel.DisplayedMaze);

            this._mazeDetailViewModel.HasUnsavedChanges = false;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_mazeDetailViewModel.HasUnsavedChanges))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
