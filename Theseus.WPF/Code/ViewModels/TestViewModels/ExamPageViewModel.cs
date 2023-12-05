using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using Theseus.Domain.CommandInterfaces.ExamCommandInterfaces;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.ExamCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Exams;
using Theseus.WPF.Code.ViewModels.Components;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamPageViewModel : ViewModelBase
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
        private bool _canGoToNextPage = true;
        public bool CanGoToNextPage
        {
            get => _canGoToNextPage;
            set
            {
                _canGoToNextPage = value;
                OnPropertyChanged(nameof(CanGoToNextPage));
            }
        }

        private Timer _transitionTimer = new Timer() { Interval = 1000 };

        public ExamPageViewModel(CurrentExamStore currentExamStore,
                                 ICreateExamCommand createExamCommand,
                                 NavigationService<ExamTransitionViewModel> examTransitionNavigationService,
                                 NavigationService<ExamEndViewModel> examEndNavigationService)
        {      
            MazeWithSolution currentMaze = currentExamStore.Mazes.ElementAt(currentExamStore.CurrentIndex);
            ExamMazeCanvasViewModel = new ExamMazeCanvasViewModel(currentMaze, currentExamStore);

            GoToNextPage = new GoToNextPageCommand(this, currentExamStore, examTransitionNavigationService, examEndNavigationService, createExamCommand);
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