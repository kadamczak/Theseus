using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Groups;
using Theseus.WPF.Code.ViewModels.Bindings.GroupBindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class ShowDetailsGroupCommandListViewModel : GroupCommandListViewModel
    {
        private readonly SelectedGroupDetailsStore _selectedGroupDetailsStore;
        private readonly NavigationService<GroupDetailsViewModel> _navigationService;

        public ShowDetailsGroupCommandListViewModel(SelectedGroupListStore selectedGroupListStore,
                                                    SelectedGroupDetailsStore selectedGroupDetailsStore,
                                                    NavigationService<GroupDetailsViewModel> navigationService) : base(selectedGroupListStore)
        {
            _selectedGroupDetailsStore = selectedGroupDetailsStore;
            _navigationService = navigationService;
        }

        protected override void AddGroupToActionableGroups(Group group)
        {
            GroupCommandViewModel groupCommandViewModel = new GroupCommandViewModel(group)
            {
                Command1Name = "Details",
                ShowCommand2 = false
            };

            groupCommandViewModel.Command1 = new ShowGroupDetailsCommand(groupCommandViewModel,
                                                                         _selectedGroupDetailsStore,
                                                                         _navigationService);

            this.ActionableGroups.Add(groupCommandViewModel);
        }
    }
}