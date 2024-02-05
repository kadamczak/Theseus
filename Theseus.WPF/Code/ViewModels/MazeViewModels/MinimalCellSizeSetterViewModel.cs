using System.Windows.Input;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeCreators;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.SettingsCommands;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.ViewModels
{
    public class MinimalCellSizeSetterViewModel : ViewModelBase
    {
        public MazeWithSolutionCanvasViewModel MazeWithSolutionCanvasViewModel { get; }

        private string _minimalCellSize = Properties.Settings.Default.MinimalCellSize.ToString();
        public string MinimalCellSize
        {
            get => _minimalCellSize;
            set
            {
                _minimalCellSize = value;
                OnPropertyChanged(nameof(MinimalCellSize));
            }
        }

        public ICommand ChangeMinimalCellSize { get; }

        public MinimalCellSizeSetterViewModel(MazeCreator mazeCreator, NavigationService<SettingsViewModel> navigationService)
        {
            MazeWithSolution exampleMaze = mazeCreator.CreateMazeWithSolution(height: 8,
                                                                              width: 12,
                                                                              MazeStructureGenAlgorithm.HuntAndKill,
                                                                              MazeSolutionGenAlgorithm.Dijkstra,
                                                                              shouldExcludeCellsCloseToRoot: true,
                                                                              rndSeed: 66);

            MazeWithSolutionCanvasViewModel = new MazeWithSolutionCanvasViewModel(exampleMaze);
            ChangeMinimalCellSize = new ChangeMinimalCellSizeCommand(this, navigationService);
        }
    }
}