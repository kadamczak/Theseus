using System.Collections.ObjectModel;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.StaffMembers;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public abstract class StaffMemberCommandListViewModel : ViewModelBase
    {
        public SelectedStaffMemberListStore SelectedStaffMemberListStore { get; }
        public ObservableCollection<CommandViewModel<StaffMember>> ActionableStaffMembers { get; } = new ObservableCollection<CommandViewModel<StaffMember>>();

        public StaffMemberCommandListViewModel(SelectedStaffMemberListStore selectedStaffMemberListStore)
        {
            SelectedStaffMemberListStore = selectedStaffMemberListStore;
        }

        public void CreateStaffMemberCommandViewModels()
        {
            this.ActionableStaffMembers.Clear();

            foreach (var staffMember in SelectedStaffMemberListStore.StaffMembers)
            {
                AddStaffMemberToActionableStaffMembers(staffMember);
            }
        }

        protected abstract void AddStaffMemberToActionableStaffMembers(StaffMember staffMember);
    }
}