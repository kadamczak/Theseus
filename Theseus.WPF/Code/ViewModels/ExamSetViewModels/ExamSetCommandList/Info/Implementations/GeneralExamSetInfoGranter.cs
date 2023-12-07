using System;
using System.Linq;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info.Implementations
{
    public class GeneralExamSetInfoGranter : InfoGranter<ExamSet>
    {
        private readonly IGetOrderedMazesWithSolutionOfExamSetQuery _getMazes;
        private readonly IGetExamsOfExamSetQuery _getExams;

        public GeneralExamSetInfoGranter(IGetOrderedMazesWithSolutionOfExamSetQuery getMazes,
                                         IGetExamsOfExamSetQuery getExams)
        {
            _getMazes = getMazes;
            _getExams = getExams;
        }

        public override string GrantInfo(CommandViewModel<ExamSet> commandViewModel)
        {
            Guid examSetId = commandViewModel.Model.Id;

            int numberOfMazes = _getMazes.GetMazesWithSolution(examSetId).Count();
            int numberOfExams = _getExams.GetExams(examSetId).Count();
            return $"Amount of mazes: {numberOfMazes}\nAmount of exam attempts: {numberOfExams}";
        }
    }
}
