using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Exams;
using Theseus.WPF.Code.ViewModels.Components;

namespace Theseus.WPF.Code.Commands.ExamCommands
{
    public class PerformMoveCommand : CommandBase
    {
        private readonly ExamMazeCanvasViewModel _viewModel;
        private readonly CurrentExamStore _currentExamStore;
        private readonly bool _rememberSteps;

        public PerformMoveCommand(ExamMazeCanvasViewModel examMazeCanvasViewModel,
                                  CurrentExamStore currentExamStore,
                                  bool rememberSteps)
        {
            _viewModel = examMazeCanvasViewModel;
            _currentExamStore = currentExamStore;
            _rememberSteps = rememberSteps;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        protected override void Dispose()
        {
            _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
            base.Dispose();
        }

        public override void Execute(object? parameter)
        {
            _currentExamStore.TimeSinceLastStep.Stop();

            string parameterText = (string)parameter!;
            Direction moveDirection = (Direction)int.Parse(parameterText);

            Cell currentCell = _viewModel.CurrentCell;
            if (MazeCompleted(moveDirection))
            {
                EndExamStage(moveDirection);
                return;
            }

            Cell? nextCell = currentCell.GetAdjecentCellSpace(moveDirection);
            if (currentCell.IsLinked(nextCell))
            {
                MoveTo(nextCell!, moveDirection);
            }
            else
            {
                if (_rememberSteps)
                    SaveExamStep(moveDirection, false, true);
            }

            _currentExamStore.TimeSinceLastStep.Restart();
        }

        private void EndExamStage(Direction moveDirection)
        {
            if (_rememberSteps)
                SaveExamStep(moveDirection, true, false);

            _viewModel.OnCompletedMaze();
        }

        private void SaveExamStep(Direction moveDirection, bool correct, bool hitWall)
        {
            ExamStage currentExamStage = _currentExamStore.CurrentExam.Stages.Last();

            ExamStep examStep = new ExamStep(Guid.NewGuid())
            {
                Stage = currentExamStage,
                StepTaken = moveDirection,
                TimeBeforeStep = (float) _currentExamStore.TimeSinceLastStep.Elapsed.TotalSeconds,
                Index = currentExamStage.Steps.Count,
                Correct = correct,
                HitWall = hitWall
            };

            currentExamStage.Steps.Add(examStep);
        }

        private void MoveTo(Cell nextCell, Direction moveDirection)
        {
            Cell? previousCell = _viewModel.UserSolution.ElementAtOrDefault(_viewModel.UserSolution.Count - 2);
            if (nextCell == previousCell)
            {
                _viewModel.MoveBack();

                if (_rememberSteps)
                    SaveExamStep(moveDirection, false, false);
            }
            else
            {
                _viewModel.MoveFurther(nextCell!);

                if (_rememberSteps)
                    SaveForwardStep(nextCell, moveDirection);
            }
        }

        private void SaveForwardStep(Cell nextCell, Direction moveDirection)
        {
            if (_viewModel.NextCorrectCellIndex is not null && nextCell == _viewModel.GetNextSolutionCell())
            {
                SaveExamStep(moveDirection, true, false);
                _viewModel.UpdateNextCorrectCellIndex();
            }
            else
            {
                SaveExamStep(moveDirection, false, false);
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