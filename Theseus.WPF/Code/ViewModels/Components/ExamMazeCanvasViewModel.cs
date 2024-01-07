using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.ExamCommands;
using Theseus.WPF.Code.Stores.Exams;

namespace Theseus.WPF.Code.ViewModels.Components
{
    public class ExamMazeCanvasViewModel : ViewModelBase
    {
        public MazeWithSolutionCanvasViewModel MazeWithSolutionCanvasViewModel { get; }
        public List<Cell> UserSolution { get; set; }

        public Cell CurrentCell { get; set; }
        public int? NextCorrectCellIndex { get; set; }
        public Cell TargetCell { get; }
        public Direction StartDirection { get; }
        public Direction EndDirection { get; }

        public ICommand PerformMove { get; }
        public event Action SuccesfullyMoved;
        public event Action CompletedMaze;

        private bool _mazeExamFinished = false;
        public bool MazeExamFinished
        {
            get => _mazeExamFinished;
            set
            {
                _mazeExamFinished = value;
                OnPropertyChanged(nameof(MazeExamFinished));
            }
        }

        public ExamMazeCanvasViewModel(MazeWithSolution mazeWithSolution, CurrentExamStore currentExamStore, bool rememberSteps)
        {
            this.MazeWithSolutionCanvasViewModel = new MazeWithSolutionCanvasViewModel(mazeWithSolution);
            this.CurrentCell = GetMazeWithSolution().SolutionPath.First();
            this.TargetCell = GetMazeWithSolution().SolutionPath.Last();
            this.StartDirection = GetMazeWithSolution().StartDirection;
            this.EndDirection = GetMazeWithSolution().EndDirection;
            this.UserSolution = new List<Cell>() { CurrentCell };
            this.NextCorrectCellIndex = 1;

            this.PerformMove = new PerformMoveCommand(this, currentExamStore, rememberSteps);
        }

        public void OnSuccesfullyMoved()
        {
            this.SuccesfullyMoved?.Invoke();
        }

        public void MoveFurther(Cell nextCell)
        {
            this.UserSolution.Add(nextCell);
            this.CurrentCell = nextCell;
            OnSuccesfullyMoved();
        }

        public void MoveBack()
        {
            this.UserSolution.Remove(this.UserSolution.Last());
            this.CurrentCell = UserSolution.Last();
            OnSuccesfullyMoved();
        }

        public void UpdateNextCorrectCellIndex()
        {
            NextCorrectCellIndex = (NextCorrectCellIndex == GetMazeWithSolution().SolutionPath.Count) ? null : NextCorrectCellIndex + 1;
        }

        public void OnCompletedMaze()
        {
            this.MazeExamFinished = true;
            this.CompletedMaze?.Invoke();
        }

        private MazeWithSolution GetMazeWithSolution() => MazeWithSolutionCanvasViewModel.MazeWithSolution;
        public Cell GetNextSolutionCell() => MazeWithSolutionCanvasViewModel.MazeWithSolution.SolutionPath.ElementAt(NextCorrectCellIndex.Value);
    }
}