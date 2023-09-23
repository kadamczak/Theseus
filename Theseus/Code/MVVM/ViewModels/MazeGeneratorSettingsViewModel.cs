using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Theseus.Code.Commands;
using Theseus.Code.MVVM.Models.Maze.Enums;
using Theseus.Code.MVVM.ViewModels.Bindings;
using Theseus.Code.Services;

namespace Theseus.Code.MVVM.ViewModels
{
    public class MazeGeneratorSettingsViewModel : Bases.ViewModel
    {
        private AlgorithmViewModel _selectedAlgorithm;
        private string _algorithmDescription = string.Empty;
        private string _mazeHeight = "10";
        private string _mazeWidth = "10";

        public ReadOnlyCollection<AlgorithmViewModel> AvailableAlgorithms { get; } = new List<AlgorithmViewModel> {
                                new AlgorithmViewModel("Binary", MazeGenAlgorithm.Binary),
                                new AlgorithmViewModel("Sidewinder", MazeGenAlgorithm.Sidewinder),
                                new AlgorithmViewModel("Kruskal", MazeGenAlgorithm.Kruskal),
                                new AlgorithmViewModel("Prim's", MazeGenAlgorithm.Prim)
                                }.AsReadOnly();

        public AlgorithmViewModel SelectedAlgorithm
        {
            get => _selectedAlgorithm;
            set
            {
                _selectedAlgorithm = value;
                OnPropertyChanged(nameof(SelectedAlgorithm));
            }
        }

        public string AlgorithmDescription
        {
            get => _algorithmDescription;
            set
            {
                _algorithmDescription = value;
                OnPropertyChanged(nameof(AlgorithmDescription));
            }
        }

        public string MazeHeight
        {
            get => _mazeHeight;
            set
            {
                _mazeHeight = value;
                OnPropertyChanged(nameof(MazeHeight));
            }
        }

        public string MazeWidth
        {
            get => _mazeWidth;
            set
            {
                _mazeWidth = value;
                OnPropertyChanged(nameof(MazeWidth));
            }
        }

        public ICommand GenerateMaze { get; }

        public MazeGeneratorSettingsViewModel(NavigationService mazeDetailNavigationService)
        {
            GenerateMaze = new GenerateMazeCommand(this, mazeDetailNavigationService);

            PropertyChanged += HandlePropertyChange;
        }


        public void HandlePropertyChange(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SelectedAlgorithm))
            {
                UpdateAlgorithmDescription();
            }

        }

        private void UpdateAlgorithmDescription()
        {
            if (SelectedAlgorithm is null)
                return;

            string algorithm = SelectedAlgorithm.Algorithm.ToString();
            AlgorithmDescription = (string)(App.Current.Resources[algorithm]);
        }

    }
}
