using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.ViewModels
{
    public class GroupDetailsViewModel : ViewModelBase
    {
        public Group CurrentGroup { get; }
        public string GroupOwnerName { get; } = string.Empty;

        public StaffMemberGroupDashboardViewModel StaffMemberGroupDashboardViewModel { get; set; }
        public PatientGroupDashboardViewModel PatientGroupDashboardViewModel { get; set; }
        public ExamSetGroupDashboardViewModel ExamSetGroupDashboardViewModel { get; set; }

        public GroupDetailsViewModel(SelectedModelDetailsStore<Group> selectedGroupDetailsStore,     
                                     IGetOwnerOfGroupQuery getOwnerOfGroupQuery,
                                     StaffMemberGroupDashboardViewModel staffMemberGroupDashboardViewModel,
                                     PatientGroupDashboardViewModel patientGroupDashboardViewModel,
                                     ExamSetGroupDashboardViewModel examSetGroupDashboardViewModel)
        {
            CurrentGroup = selectedGroupDetailsStore.SelectedModel;
            CurrentGroup.Owner = getOwnerOfGroupQuery.GetOwner(CurrentGroup.Id);
            GroupOwnerName = CurrentGroup.Owner.Username;

            StaffMemberGroupDashboardViewModel = staffMemberGroupDashboardViewModel;
            PatientGroupDashboardViewModel = patientGroupDashboardViewModel;
            ExamSetGroupDashboardViewModel = examSetGroupDashboardViewModel;
        }
    }
}