﻿using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Commands.ExamCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Exams;
using Theseus.WPF.Code.ViewModels.Components;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamPageViewModel : ExamViewModel
    {
        public ExamMazeCanvasViewModel ExamMazeCanvasViewModel { get; }
        private readonly CurrentExamStore _currentExamStore;

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

        public ExamPageViewModel(CurrentExamStore currentExamStore,
                                 NavigationService<ExamTransitionViewModel> examTransitionNavigationService,
                                 NavigationService<ExamEndViewModel> examEndNavigationService)
        {      
            MazeWithSolution currentMaze = currentExamStore.Mazes.ElementAt(currentExamStore.CurrentIndex);
            ExamMazeCanvasViewModel = new ExamMazeCanvasViewModel(currentMaze, currentExamStore, rememberSteps: true);

            GoToNextPage = new GoToNextPageCommand(this, currentExamStore, examTransitionNavigationService, examEndNavigationService, saveStageInfo: true);
            ExamMazeCanvasViewModel.CompletedMaze += StartCountdown;
            _transitionTimer.Elapsed += new ElapsedEventHandler(ReduceCountdownValue);

            _currentExamStore = currentExamStore;
            currentExamStore.TimeSinceBeginningOfStage.Restart();
            currentExamStore.TimeSinceLastStep.Restart();
        }

        private void StartCountdown()
        {
            CanGoToNextPage = false;

            _currentExamStore.TimeSinceBeginningOfStage.Stop();
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