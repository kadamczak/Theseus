using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info.Implementations
{
    public class GeneralExamInfoGranter : InfoGranter<Exam>
    {
        private readonly IGetStepsOfExamQuery _getStepsQuery;

        public override string GrantInfo(CommandViewModel<Exam> commandViewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}