using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        private string _mazeHeight;
        private string _mazeWidth;

        private string _algorithmDescription = string.Empty;

        private readonly LastMazeGeneratorInputStore _lastMazeGeneratorSettingsStore;

        public ReadOnlyCollection<AlgorithmViewModel> AvailableAlgorithms { get; } = new List<AlgorithmViewModel> {
                                new AlgorithmViewModel("Binary", MazeGenAlgorithm.Binary),
                                new AlgorithmViewModel("Sidewinder", MazeGenAlgorithm.Sidewinder),
                                new AlgorithmViewModel("Aldous-Broder", MazeGenAlgorithm.AldousBroder),
                                new AlgorithmViewModel("Hunt and Kill", MazeGenAlgorithm.HuntAndKill),
                                new AlgorithmViewModel("Kruskal's", MazeGenAlgorithm.Kruskal),
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

        public MazeGeneratorViewModel(MazeDetailsStore mazeDetailsStore,
                                      NavigationService<MazeDetailsViewModel> mazeDetailNavigationService,
                                      LastMazeGeneratorInputStore lastMazeGeneratorSettingsStore)
        {
            this.GenerateMaze = new GenerateMazeCommand(this, mazeDetailsStore, mazeDetailNavigationService);
            this._lastMazeGeneratorSettingsStore = lastMazeGeneratorSettingsStore;
            PropertyChanged += HandlePropertyChange;

            GetStartValuesFromStore();
        }

        private void GetStartValuesFromStore()
        {
            this.SelectedAlgorithm = AvailableAlgorithms.Where(a => a.Algorithm == this._lastMazeGeneratorSettingsStore.Algorithm).First();
            this.MazeHeight = this._lastMazeGeneratorSettingsStore.Height;
            this.MazeWidth = this._lastMazeGeneratorSettingsStore.Width;
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
                _lastMazeGeneratorSettingsStore.Algorithm = SelectedAlgorithm.Algorithm;
            }
            else if (e.PropertyName == nameof(MazeHeight))
            {
                _lastMazeGeneratorSettingsStore.Height = MazeHeight;
            }
            else if (e.PropertyName == nameof(MazeWidth))
            {
                _lastMazeGeneratorSettingsStore.Width = MazeWidth;
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