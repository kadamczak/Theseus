using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.ViewModels.Components
{
    public class ReplayMazeCanvasViewModel : ViewModelBase
    {
        private readonly List<TimedCell> _userSolutionWithTimes;
        private int _nextCellIndex = 1;

        public MazeWithSolutionCanvasViewModel MazeWithSolutionCanvasViewModel { get; }
        public List<Cell> UserSolution { get; set; }
        public Cell CurrentCell { get; set; }
        public Cell TargetCell { get; }
        public Direction StartDirection { get; }
        public Direction EndDirection { get; }
        private bool _examStageCompleted;

        public event Action SuccesfullyMoved;
        public event Action ReplayDone;
        public event Action MovedToEndCell;

        public Timer InputTimer { get; set; }

        public void StopTimer()
        {
            InputTimer?.Stop();
            InputTimer?.Dispose();
        }

        public ReplayMazeCanvasViewModel(MazeWithSolution mazeWithSolution, List<TimedCell> userSolutionWithTimes, bool examStageCompleted)
        {
            _userSolutionWithTimes = userSolutionWithTimes;

            this.MazeWithSolutionCanvasViewModel = new MazeWithSolutionCanvasViewModel(mazeWithSolution);
            this.TargetCell = GetMazeWithSolution().SolutionPath.Last();
            this.StartDirection = GetMazeWithSolution().StartDirection;
            this.EndDirection = GetMazeWithSolution().EndDirection;
            _examStageCompleted = examStageCompleted;

            this.UserSolution = new List<Cell>() { userSolutionWithTimes.First().Cell };
            InputTimer = new Timer();
            StartTimerIfInputsLeft();
        }

        private void DrawNextStep(object? sender, ElapsedEventArgs e)
        {
            StopTimer();

            Cell nextCell = _userSolutionWithTimes[_nextCellIndex].Cell;
            UserSolution.Add(nextCell);

            Application.Current.Dispatcher.Invoke(() => {
                SuccesfullyMoved?.Invoke();
            });

            _nextCellIndex++;
            StartTimerIfInputsLeft();
        }

        private void StartTimerIfInputsLeft()
        {
            if (_nextCellIndex < _userSolutionWithTimes.Count)
            {
                InputTimer = new Timer() { Interval = _userSolutionWithTimes[_nextCellIndex].TimeBeforeMove * 1000 };
                InputTimer.Elapsed += new ElapsedEventHandler(DrawNextStep);
                InputTimer.Start();
                return;
            }

            if (_examStageCompleted)
            {
                Application.Current.Dispatcher.Invoke(() => { MovedToEndCell?.Invoke(); });
            }

            Application.Current.Dispatcher.Invoke(() => { ReplayDone?.Invoke(); });
        }

        private MazeWithSolution GetMazeWithSolution() => MazeWithSolutionCanvasViewModel.MazeWithSolution;
    }
}