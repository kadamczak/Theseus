using System;
using System.Linq;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.Info.Implementations
{
    public class GeneralGroupInfoGranter : InfoGranter<Group>
    {
        private readonly IGetStaffMembersOfGroupQuery _getStaffMembersOfGroupQuery;
        private readonly IGetPatientsOfGroupQuery _getPatientsOfGroupQuery;
        private readonly IGetExamSetsOfGroupQuery _getExamSetsOfGroupQuery;

        public GeneralGroupInfoGranter(IGetStaffMembersOfGroupQuery getStaffMembersOfGroupQuery,
                                       IGetPatientsOfGroupQuery getPatientsOfGroupQuery,
                                       IGetExamSetsOfGroupQuery getExamSetsOfGroupQuery)
        {
            _getStaffMembersOfGroupQuery = getStaffMembersOfGroupQuery;
            _getPatientsOfGroupQuery = getPatientsOfGroupQuery;
            _getExamSetsOfGroupQuery = getExamSetsOfGroupQuery;
        }

        public override string GrantInfo(CommandViewModel<Group> commandViewModel)
        {
            Guid groupId = commandViewModel.Model.Id;

            int numberOfStaffMembers = _getStaffMembersOfGroupQuery.GetStaffMembers(groupId).Count();
            int numberOfPatients = _getPatientsOfGroupQuery.GetPatients(groupId).Count();
            int numberOfExamSets = _getExamSetsOfGroupQuery.GetExamSets(groupId).Count();

            string[] infoParts = new string[3];
            infoParts[0] = $"Amount of staff members: {numberOfStaffMembers}";
            infoParts[1] = $"Amount of patients: {numberOfPatients}";
            infoParts[2] = $"Amount of exam sets: {numberOfExamSets}";

            return String.Join("\n", infoParts);
        }
    }
}