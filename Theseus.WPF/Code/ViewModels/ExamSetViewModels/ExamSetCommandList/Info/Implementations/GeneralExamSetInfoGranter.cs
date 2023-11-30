using System.Linq;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info.Implementations
{
    public class GeneralExamSetInfoGranter : InfoGranter<ExamSet>
    {
        private readonly IGetOrderedMazesWithSolutionOfExamSetQuery _getMazes;

        public GeneralExamSetInfoGranter(IGetOrderedMazesWithSolutionOfExamSetQuery getMazes)
        {
            _getMazes = getMazes;
        }

        public override string GrantInfo(CommandViewModel<ExamSet> commandViewModel)
        {
            int numberOfMazes = _getMazes.GetMazesWithSolution(commandViewModel.Model.Id).Count();
            return $"Amount of mazes: {numberOfMazes}";
        }
    }
}
