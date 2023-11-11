using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels.Components;

namespace Theseus.WPF.Code.Commands.ExamCommands
{
    public class PerformMoveCommand : CommandBase
    {
        private readonly ExamMazeCanvasViewModel _viewModel;

        public PerformMoveCommand(ExamMazeCanvasViewModel examMazeCanvasViewModel)
        {
            _viewModel = examMazeCanvasViewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        protected override void Dispose()
        {
            _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
            base.Dispose();
        }

        public override void Execute(object? parameter)
        {
            string parameterText = (string)parameter!;
            Direction moveDirection = (Direction)int.Parse(parameterText);

            Cell currentCell = _viewModel.CurrentCell;
            if (MazeCompleted(moveDirection))
            {
                _viewModel.OnCompletedMaze();
                return;
            }

            Cell? nextCell = currentCell.AdjecentCellSpaces[moveDirection];
            if (currentCell.IsLinked(nextCell))
            {
                MoveTo(nextCell!);
            }
        }

        private void MoveTo(Cell nextCell)
        {
            List<Cell> userSolution = _viewModel.UserSolution;

            Cell? previousCell = _viewModel.UserSolution.ElementAtOrDefault(_viewModel.UserSolution.Count - 2);
            if (nextCell == previousCell)
            {
                _viewModel.MoveBack();
            }
            else
            {
                _viewModel.MoveFurther(nextCell!);
            }
        }

        private bool MazeCompleted(Direction moveDirection) => _viewModel.CurrentCell == _viewModel.TargetCell && moveDirection == _viewModel.EndDirection;

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.MazeExamFinished))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !_viewModel.MazeExamFinished && base.CanExecute(parameter);
        }
    }
}