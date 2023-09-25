using System;
using System.Windows.Input;
using Theseus.Code.Bases;
using Theseus.Code.Commands;
using Theseus.Code.MVVM.Models;
using Theseus.Code.MVVM.Models.Maze.Generators;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.MVVM.ViewModels
{
    public class MazeDetailViewModel : ViewModel
    {
        public MazeGrid DisplayedMaze { get; }
        public Guid? MazeId { get; }

        private bool _hasUnsavedChanges = true;

        public bool HasUnsavedChanges
        {
            get => _hasUnsavedChanges;
            set
            {
                _hasUnsavedChanges = value;
                OnPropertyChanged(nameof(HasUnsavedChanges));
            }
        }

        public ICommand SaveMaze { get; }

        public MazeDetailViewModel(ModelPersistence modelPersistence)
        {
            this.SaveMaze = new SaveMazeGridCommand(this, modelPersistence);

            MazeGenerator mazeGen = MazeGeneratorFactory.Create(Code.MVVM.Models.Maze.Enums.MazeGenAlgorithm.Sidewinder);
            this.DisplayedMaze = mazeGen.GenerateMaze(5, 5);
        }

    }
}
