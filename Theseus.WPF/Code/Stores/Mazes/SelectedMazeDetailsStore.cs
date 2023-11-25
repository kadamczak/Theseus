using System;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.WPF.Code.Stores.Mazes
{
    public class SelectedMazeDetailsStore
    {
        public event Action MazeStructureChanged;
        public event Action SaveStateChanged;

        private MazeWithSolution? _selectedMazeWithSolution = null;
        private bool _hasUnsavedChanges = true;

        public MazeWithSolution? SelectedMazeWithSolution
        {
            get => _selectedMazeWithSolution;
            set
            {
                _selectedMazeWithSolution = value;
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

        public void UpdateMazeDetails(MazeWithSolution? mazeWithSolution, bool unsavedChanges)
        {
            SelectedMazeWithSolution = mazeWithSolution;
            HasUnsavedChanges = unsavedChanges;
        }
    }
}