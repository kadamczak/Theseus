using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info.Implementations
{
    public class GeneralExamInfoGranter : InfoGranter<Exam>
    {
        private readonly IGetStagesOfExamQuery _getStagesQuery;
        private readonly IGetStepsOfExamQuery _getStepsQuery;
        private readonly IGetOrderedMazesWithSolutionOfExamSetQuery _getMazesQuery;

        public GeneralExamInfoGranter(IGetStagesOfExamQuery getStagesQuery,
                                      IGetStepsOfExamQuery getStepsQuery,
                                      IGetOrderedMazesWithSolutionOfExamSetQuery getMazesQuery)
        {
            _getStagesQuery = getStagesQuery;
            _getStepsQuery = getStepsQuery;
            _getMazesQuery = getMazesQuery;
        }

        public override string GrantInfo(CommandViewModel<Exam> commandViewModel)
        {
            Guid examId = commandViewModel.Model.Id;
            var examStages = _getStagesQuery.GetStages(examId);
            var examSteps = _getStepsQuery.GetSteps(examId);
            var examMazes = _getMazesQuery.GetMazesWithSolution(commandViewModel.Model.ExamSet.Id);

            float totalExamTime = examSteps.Sum(s => s.TimeBeforeStep);

            int totalMazeAmount = examStages.Count();
            int completedMazeAmount = examStages.Where(s => s.Completed).Count();

            int idealInputAmount = examMazes.Sum(m => m.SolutionPath.Count);

            string[] displayedInfoText = new string[4];
            displayedInfoText[0] = "Total time: " + Math.Round(totalExamTime, 2) + "s";
            displayedInfoText[1] = $"Completed mazes: {completedMazeAmount}/{totalMazeAmount}";
            displayedInfoText[2] = "Inputs made: " + examSteps.Count();
            displayedInfoText[3] = "Ideal input amount: " + idealInputAmount;

            return string.Join("\n", displayedInfoText);
        }
    }
}