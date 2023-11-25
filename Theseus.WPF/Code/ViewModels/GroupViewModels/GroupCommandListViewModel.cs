using System.Collections.ObjectModel;
using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Groups;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public abstract class GroupCommandListViewModel : ViewModelBase
    {
        public SelectedGroupListStore SelectedGroupListStore { get; }
        public ObservableCollection<CommandViewModel<Group>> ActionableGroups { get; } = new ObservableCollection<CommandViewModel<Group>>();

        protected GroupCommandListViewModel(SelectedGroupListStore selectedGroupListStore)
        {
            SelectedGroupListStore = selectedGroupListStore;
        }

        public void CreateGroupCommandViewModels()
        {
            this.ActionableGroups.Clear();

            foreach(var group in SelectedGroupListStore.Groups)
            {
                AddGroupToActionableGroups(group);
            }
        }

        protected abstract void AddGroupToActionableGroups(Group group);
    }
}