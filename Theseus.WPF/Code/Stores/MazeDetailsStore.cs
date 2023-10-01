using System;
using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.WPF.Code.Stores
{
    public class MazeDetailsStore
    {
        public event Action MazeStructureChanged;
        public event Action SaveStateChanged;

        public Guid? SelectedMazeId { get; set; }

        private Maze? _selectedMaze = null;
        private bool _hasUnsavedChanges = true;

        public Maze? SelectedMaze
        {
            get => _selectedMaze;
            set
            {
                _selectedMaze = value;  
                MazeStructureChanged?.Invoke();
            }
        }

        public bool HasUnsavedChanges
        {
            get => _hasUnsavedChanges;
            set
            {
                _hasUnsavedChanges = value;
                SaveStateChanged?.Invoke();
            }
        }

        public void UpdateMazeDetails(Guid? id, Maze? maze, bool unsavedChanges)
        {
            this.SelectedMazeId = id;
            this.SelectedMaze = maze;
            this.HasUnsavedChanges = unsavedChanges;
        }
    }
}
