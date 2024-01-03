using System.Timers;
using System.Windows;
using System.Windows.Input;
using Theseus.Domain.CommandInterfaces.ExamCommandInterfaces;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeCreators;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Commands.ExamCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Exams;
using Theseus.WPF.Code.ViewModels.Components;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamPracticeViewModel : ExamViewModel
    {
        public ExamMazeCanvasViewModel ExamMazeCanvasViewModel { get; }
        public IGetMazeWithSolutionByIdQuery GetMazeById { get; }

        private int _countdownValue = 3;
        public int CountdownValue
        {
            get => _countdownValue;
            set
            {
                _countdownValue = value;
                OnPropertyChanged(nameof(CountdownValue));
            }
        }

        public ICommand GoToNextPage { get; }

        private Timer _transitionTimer = new Timer() { Interval = 1000 };

        public ExamPracticeViewModel(CurrentExamStore currentExamStore,
                                 MazeCreator mazeCreator,
                                 NavigationService<ExamTransitionViewModel> examTransitionNavigationService,
                                 NavigationService<ExamEndViewModel> examEndNavigationService)
        {
            MazeWithSolution currentMaze = mazeCreator.CreateMazeWithSolution(4, 6, MazeStructureGenAlgorithm.AldousBroder, MazeSolutionGenAlgorithm.Dijkstra, true, 39);
            ExamMazeCanvasViewModel = new ExamMazeCanvasViewModel(currentMaze, currentExamStore, rememberSteps: false);

            GoToNextPage = new GoToNextPageCommand(this, currentExamStore, examTransitionNavigationService, examEndNavigationService, saveStageInfo: false);
            ExamMazeCanvasViewModel.CompletedMaze += StartCountdown;
            _transitionTimer.Elapsed += new ElapsedEventHandler(ReduceCountdownValue);

            currentExamStore.TimeSinceLastStep.Restart();
        }

        private void StartCountdown()
        {
            CanGoToNextPage = false;
            _transitionTimer.Start();
        }

        private void ReduceCountdownValue(object? sender, ElapsedEventArgs e)
        {
            CountdownValue--;

            if (CountdownValue == 0)
            {
                _transitionTimer.Stop();

                Application.Current.Dispatcher.Invoke(() => {
                    CanGoToNextPage = true;
                });
                GoToNextPage.Execute("True");
            }
        }
    }
}