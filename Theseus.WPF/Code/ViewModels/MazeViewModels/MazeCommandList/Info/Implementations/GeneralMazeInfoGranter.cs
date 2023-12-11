using System;
using System.Linq;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.Info.Implementations
{
    public class GeneralMazeInfoGranter : InfoGranter<MazeWithSolutionCanvasViewModel>
    {
        private readonly IGetExamSetsWithMazeQuery _getExamSetsWithMazeQuery;

        public GeneralMazeInfoGranter(IGetExamSetsWithMazeQuery getExamSetsWithMazeQuery)
        {
            _getExamSetsWithMazeQuery = getExamSetsWithMazeQuery;
        }

        public override string GrantInfo(CommandViewModel<MazeWithSolutionCanvasViewModel> commandViewModel)
        {
            Guid mazeId = commandViewModel.Model.MazeWithSolution.Id.Value;
            return $"{"PresentInExamSets:".Resource()}{_getExamSetsWithMazeQuery.GetExamSets(mazeId).Count()}";
        }
    }
}