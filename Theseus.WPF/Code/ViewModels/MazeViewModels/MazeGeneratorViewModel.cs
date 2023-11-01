using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeCreators;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class MazeGeneratorViewModel : ViewModelBase
    {
        private string _mazeHeight;
        private string _mazeWidth;
        private StructureAlgorithmViewModel _selectedStructureAlgorithm;
        private SolutionAlgorithmViewModel _selectedSolutionAlgorithm;
        private bool _shouldExcludeCellsCloseToRoot = true;

        private string _structureAlgorithmDescription = string.Empty;

        private readonly LastMazeGeneratorInputStore _lastMazeGeneratorSettingsStore;

        public ObservableCollection<StructureAlgorithmViewModel> AvailableStructureAlgorithms { get; } = new ObservableCollection<StructureAlgorithmViewModel> {
                                new StructureAlgorithmViewModel("Binary", MazeStructureGenAlgorithm.Binary),
                                new StructureAlgorithmViewModel("Sidewinder", MazeStructureGenAlgorithm.Sidewinder),
                                new StructureAlgorithmViewModel("Aldous-Broder", MazeStructureGenAlgorithm.AldousBroder),
                                new StructureAlgorithmViewModel("Hunt and Kill", MazeStructureGenAlgorithm.HuntAndKill),
                                new StructureAlgorithmViewModel("Kruskal's", MazeStructureGenAlgorithm.Kruskal),
                                };

        public ObservableCollection<SolutionAlgorithmViewModel> AvailableSolutionAlgorithms { get; } = new ObservableCollection<SolutionAlgorithmViewModel> {
                                new SolutionAlgorithmViewModel("Random border cells", MazeSolutionGenAlgorithm.RandomBorderCells),
                                new SolutionAlgorithmViewModel("Dijkstra (longest path)", MazeSolutionGenAlgorithm.Dijkstra),
                                };

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

        public StructureAlgorithmViewModel SelectedStructureAlgorithm
        {
            get => _selectedStructureAlgorithm;
            set
            {
                _selectedStructureAlgorithm = value;
                OnPropertyChanged(nameof(SelectedStructureAlgorithm));
            }
        }

        public SolutionAlgorithmViewModel SelectedSolutionAlgorithm
        {
            get => _selectedSolutionAlgorithm;
            set
            {
                _selectedSolutionAlgorithm = value;
                OnPropertyChanged(nameof(SelectedSolutionAlgorithm));
            }
        }

        public bool ShouldExcludeCellsCloseToRoot
        {
            get => _shouldExcludeCellsCloseToRoot;
            set
            {
                _shouldExcludeCellsCloseToRoot = value;
                OnPropertyChanged(nameof(ShouldExcludeCellsCloseToRoot));
            }
        }

        public string StructureAlgorithmDescription
        {
            get => _structureAlgorithmDescription;
            set
            {
                _structureAlgorithmDescription = value;
                OnPropertyChanged(nameof(StructureAlgorithmDescription));
            }
        }

        public ICommand GenerateMaze { get; }

        public MazeGeneratorViewModel(MazeCreator mazeCreator,
                                      SelectedMazeDetailsStore mazeDetailsStore,
                                      NavigationService<MazeDetailsViewModel> mazeDetailNavigationService,
                                      LastMazeGeneratorInputStore lastMazeGeneratorSettingsStore)
        {
            this.GenerateMaze = new GenerateMazeCommand(this, mazeCreator, mazeDetailsStore, mazeDetailNavigationService);
            this._lastMazeGeneratorSettingsStore = lastMazeGeneratorSettingsStore;
            PropertyChanged += HandlePropertyChange;

            GetStartValuesFromStore();
        }

        private void GetStartValuesFromStore()
        {
            this.MazeHeight = this._lastMazeGeneratorSettingsStore.Height;
            this.MazeWidth = this._lastMazeGeneratorSettingsStore.Width;
            this.SelectedStructureAlgorithm = AvailableStructureAlgorithms.Where(a => a.Algorithm == this._lastMazeGeneratorSettingsStore.StructureAlgorithm).First();
            this.SelectedSolutionAlgorithm = AvailableSolutionAlgorithms.Where(a => a.Algorithm == this._lastMazeGeneratorSettingsStore.SolutionAlgorithm).First();
            this.ShouldExcludeCellsCloseToRoot = this._lastMazeGeneratorSettingsStore.ShouldExcludeCellsCloseToRoot;
        }

        protected override void Dispose()
        {
            PropertyChanged -= HandlePropertyChange;
            base.Dispose();
        }

        public void HandlePropertyChange(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedStructureAlgorithm))
            {
                UpdateAlgorithmDescription();
                _lastMazeGeneratorSettingsStore.StructureAlgorithm = SelectedStructureAlgorithm.Algorithm;
            }
            else if (e.PropertyName == nameof(SelectedSolutionAlgorithm))
            {
                _lastMazeGeneratorSettingsStore.SolutionAlgorithm = SelectedSolutionAlgorithm.Algorithm;
            }
            else if (e.PropertyName == nameof(ShouldExcludeCellsCloseToRoot))
            {
                _lastMazeGeneratorSettingsStore.ShouldExcludeCellsCloseToRoot = ShouldExcludeCellsCloseToRoot;
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
            if (SelectedStructureAlgorithm is null)
                return;

            string algorithm = SelectedStructureAlgorithm.Algorithm.ToString();
            StructureAlgorithmDescription = (string)(App.Current.Resources[algorithm]);
        }
    }
}