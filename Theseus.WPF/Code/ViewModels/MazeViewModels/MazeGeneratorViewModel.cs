using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class MazeGeneratorViewModel : ViewModelBase
    {
        private AlgorithmViewModel _selectedAlgorithm;
        private string _algorithmDescription = string.Empty;
        private string _mazeHeight = "10";
        private string _mazeWidth = "10";

        public ReadOnlyCollection<AlgorithmViewModel> AvailableAlgorithms { get; } = new List<AlgorithmViewModel> {
                                new AlgorithmViewModel("Binary", MazeGenAlgorithm.Binary),
                                new AlgorithmViewModel("Sidewinder", MazeGenAlgorithm.Sidewinder),
                                new AlgorithmViewModel("Kruskal", MazeGenAlgorithm.Kruskal),
                                new AlgorithmViewModel("Hunt and Kill", MazeGenAlgorithm.HuntAndKill),
                                new AlgorithmViewModel("Recursive Backtracker", MazeGenAlgorithm.RecursiveBacktracker)
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

        public MazeGeneratorViewModel(MazeDetailsStore mazeDetailsStore, NavigationService<MazeDetailsViewModel> mazeDetailNavigationService)
        {
            GenerateMaze = new GenerateMazeCommand(this, mazeDetailsStore, mazeDetailNavigationService);

            PropertyChanged += HandlePropertyChange;
        }

        protected override void Dispose()
        {
            PropertyChanged -= HandlePropertyChange;
            base.Dispose();
        }

        public void HandlePropertyChange(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedAlgorithm))
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
