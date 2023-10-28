using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands;

namespace Theseus.WPF.Code.ViewModels.Components
{
    public class ExamMazeCanvasViewModel : ViewModelBase
    {
        public MazeWithSolutionCanvasViewModel MazeWithSolutionCanvasViewModel { get; }
        public List<Cell> UserSolution { get; set; }

        public Cell CurrentCell { get; set; }
        public Cell TargetCell { get; }
        public Direction StartDirection { get; }
        public Direction EndDirection { get; }

        public ICommand PerformMove { get; }
        public event Action SuccesfullyMoved;
        public event Action CompletedMaze;

        public ExamMazeCanvasViewModel(MazeWithSolution mazeWithSolution)
        {
            this.MazeWithSolutionCanvasViewModel = new MazeWithSolutionCanvasViewModel(mazeWithSolution);
            this.CurrentCell = GetMazeWithSolution().SolutionPath.First();
            this.TargetCell = GetMazeWithSolution().SolutionPath.Last();
            this.StartDirection = GetMazeWithSolution().StartDirection;
            this.EndDirection = GetMazeWithSolution().EndDirection;

            this.UserSolution = new List<Cell>() { CurrentCell };

            this.PerformMove = new PerformMoveCommand(this);
        }

        public void MoveFurther(Cell nextCell)
        {
            this.UserSolution.Add(nextCell);
            this.CurrentCell = nextCell;
            this.SuccesfullyMoved?.Invoke();
        }

        public void MoveBack()
        {
            this.UserSolution.Remove(this.UserSolution.Last());
            this.CurrentCell = UserSolution.Last();
            this.SuccesfullyMoved?.Invoke();
        }

        public void OnCompletedMaze()
        {
            this.CompletedMaze?.Invoke();
        }

        private MazeWithSolution GetMazeWithSolution() => MazeWithSolutionCanvasViewModel.MazeWithSolution;
    }
}