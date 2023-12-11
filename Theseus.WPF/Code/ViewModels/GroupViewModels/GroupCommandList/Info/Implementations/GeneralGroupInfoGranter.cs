using System;
using System.Linq;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.Info.Implementations
{
    public class GeneralGroupInfoGranter : InfoGranter<Group>
    {
        private readonly IGetOwnerOfGroupQuery _getOwnerOfGroupQuery;
        private readonly IGetStaffMembersOfGroupQuery _getStaffMembersOfGroupQuery;
        private readonly IGetPatientsOfGroupQuery _getPatientsOfGroupQuery;
        private readonly IGetExamSetsOfGroupQuery _getExamSetsOfGroupQuery;

        public GeneralGroupInfoGranter(IGetOwnerOfGroupQuery getOwnerOfGroupQuery,
                                       IGetStaffMembersOfGroupQuery getStaffMembersOfGroupQuery,
                                       IGetPatientsOfGroupQuery getPatientsOfGroupQuery,
                                       IGetExamSetsOfGroupQuery getExamSetsOfGroupQuery)
        {
            _getOwnerOfGroupQuery = getOwnerOfGroupQuery;
            _getStaffMembersOfGroupQuery = getStaffMembersOfGroupQuery;
            _getPatientsOfGroupQuery = getPatientsOfGroupQuery;
            _getExamSetsOfGroupQuery = getExamSetsOfGroupQuery;
        }

        public override string GrantInfo(CommandViewModel<Group> commandViewModel)
        {
            Guid groupId = commandViewModel.Model.Id;

            string ownerName = _getOwnerOfGroupQuery.GetOwner(groupId).Username;
            int numberOfStaffMembers = _getStaffMembersOfGroupQuery.GetStaffMembers(groupId).Count();
            int numberOfPatients = _getPatientsOfGroupQuery.GetPatients(groupId).Count();
            int numberOfExamSets = _getExamSetsOfGroupQuery.GetExamSets(groupId).Count();

            string[] infoParts = new string[4];
            infoParts[0] = $"{"Owner:".Resource()}{ownerName}";
            infoParts[1] = $"{"AmountOfStaffMembers:".Resource()}{numberOfStaffMembers}";
            infoParts[2] = $"{"AmountOfPatients:".Resource()}{numberOfPatients}";
            infoParts[3] = $"{"AmountOfExamSets:".Resource()}{numberOfExamSets}";

            return String.Join("\n", infoParts);
        }
    }
}