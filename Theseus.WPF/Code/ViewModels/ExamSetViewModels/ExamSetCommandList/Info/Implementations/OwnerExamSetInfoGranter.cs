using System;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info.Implementations
{
    public class OwnerExamSetInfoGranter : InfoGranter<ExamSet>
    {
        private readonly IGetOwnerOfExamSetQuery _getOwnerQuery;

        public OwnerExamSetInfoGranter(IGetOwnerOfExamSetQuery getOwnerQuery)
        {
            _getOwnerQuery = getOwnerQuery;
        }

        public override string GrantInfo(CommandViewModel<ExamSet> commandViewModel)
        {
            Guid examSetId = commandViewModel.Model.Id;
            return "Owner:".Resource() + _getOwnerQuery.GetOwner(examSetId).Username;
        }
    }
}