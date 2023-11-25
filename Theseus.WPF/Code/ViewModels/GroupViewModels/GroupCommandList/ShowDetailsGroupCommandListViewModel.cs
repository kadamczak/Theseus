using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class ShowDetailsGroupCommandListViewModel : CommandListViewModel<Group>
    {
        private readonly SelectedModelDetailsStore<Group> _selectedGroupDetailsStore;
        private readonly NavigationService<GroupDetailsViewModel> _navigationService;

        public ShowDetailsGroupCommandListViewModel(SelectedModelListStore<Group> selectedGroupListStore,
                                                    SelectedModelDetailsStore<Group> selectedGroupDetailsStore,
                                                    NavigationService<GroupDetailsViewModel> navigationService) : base(selectedGroupListStore)
        {
            _selectedGroupDetailsStore = selectedGroupDetailsStore;
            _navigationService = navigationService;
        }

        protected override void AddModelToActionableModels(Group group)
        {
            CommandViewModel<Group> groupCommandViewModel = new CommandViewModel<Group>(group)
            {
                Command1Name = "Details",
                ShowCommand1 = true,
                ShowCommand2 = false
            };

            groupCommandViewModel.Command1 = new ShowGroupDetailsCommand(groupCommandViewModel,
                                                                         _selectedGroupDetailsStore,
                                                                         _navigationService);

            this.ActionableModels.Add(groupCommandViewModel);
        }
    }
}