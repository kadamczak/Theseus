using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeCreators;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.MazeCommands;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.Mazes;
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
                                new StructureAlgorithmViewModel("Kruskal", MazeStructureGenAlgorithm.Kruskal),
                                new StructureAlgorithmViewModel("Prim", MazeStructureGenAlgorithm.TruePrim)
                                };

        public ObservableCollection<SolutionAlgorithmViewModel> AvailableSolutionAlgorithms { get; } = new ObservableCollection<SolutionAlgorithmViewModel> {
                                new SolutionAlgorithmViewModel("RandomBorderCells".Resource(), MazeSolutionGenAlgorithm.RandomBorderCells),
                                new SolutionAlgorithmViewModel("Dijkstra".Resource(), MazeSolutionGenAlgorithm.Dijkstra),
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
                                      SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel> mazeDetailsStore,
                                      ICurrentStaffMemberStore currentStaffMemberStore,
                                      NavigationService<MazeDetailsViewModel> mazeDetailNavigationService,
                                      LastMazeGeneratorInputStore lastMazeGeneratorSettingsStore,
                                      MazeReturnServiceStore mazeReturnServiceStore,
                                      NavigationStore navigationStore,
                                      Func<MazeGeneratorViewModel> viewModelGenerator)
        {
            this.GenerateMaze = new GenerateMazeCommand(this, mazeCreator, mazeDetailsStore, currentStaffMemberStore, mazeDetailNavigationService);
            this._lastMazeGeneratorSettingsStore = lastMazeGeneratorSettingsStore;
            mazeReturnServiceStore.MazeReturnNavigationService = new NavigationService<ViewModelBase>(navigationStore, viewModelGenerator);

            PropertyChanged += HandlePropertyChange;
            GetStartValuesFromStore();
        }

        private void GetStartValuesFromStore()
        {
            this.MazeHeight = this._lastMazeGeneratorSettingsStore.Height;
            this.MazeWidth = this._lastMazeGeneratorSettingsStore.Width;
            this.SelectedStructureAlgorithm = AvailableStructureAlgorithms.Where(a => a.Value == this._lastMazeGeneratorSettingsStore.StructureAlgorithm).First();
            this.SelectedSolutionAlgorithm = AvailableSolutionAlgorithms.Where(a => a.Value == this._lastMazeGeneratorSettingsStore.SolutionAlgorithm).First();
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
                _lastMazeGeneratorSettingsStore.StructureAlgorithm = SelectedStructureAlgorithm.Value;
            }
            else if (e.PropertyName == nameof(SelectedSolutionAlgorithm))
                _lastMazeGeneratorSettingsStore.SolutionAlgorithm = SelectedSolutionAlgorithm.Value;
            else if (e.PropertyName == nameof(ShouldExcludeCellsCloseToRoot))
                _lastMazeGeneratorSettingsStore.ShouldExcludeCellsCloseToRoot = ShouldExcludeCellsCloseToRoot;
            else if (e.PropertyName == nameof(MazeHeight))
                _lastMazeGeneratorSettingsStore.Height = MazeHeight;
            else if (e.PropertyName == nameof(MazeWidth))
                _lastMazeGeneratorSettingsStore.Width = MazeWidth;
        }

        private void UpdateAlgorithmDescription()
        {
            if (SelectedStructureAlgorithm is null)
                return;

            string algorithm = SelectedStructureAlgorithm.Value.ToString();
            StructureAlgorithmDescription = (string)(App.Current.Resources[algorithm]);
        }
    }
}