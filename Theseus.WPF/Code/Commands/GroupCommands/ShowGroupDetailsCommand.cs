using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    /// <summary>
    /// The <c>ShowGroupDetailsCommand</c> class reads the <c>Group</c> object stored in linked <c>CommandViewModel</c> instance,
    /// saves it in <c>SelectedModelDetailsStore</c> and then opens the Group Details View.
    /// </summary>
    public class ShowGroupDetailsCommand : CommandBase
    {
        private readonly CommandViewModel<Group> _groupCommandViewModel;
        private readonly SelectedModelDetailsStore<Group> _selectedGroupDetailsStore;
        private readonly NavigationService<GroupDetailsViewModel> _groupDetailsNavigationService;

        public ShowGroupDetailsCommand(CommandViewModel<Group> groupCommandViewModel,
                                       SelectedModelDetailsStore<Group> selectedGroupDetailsStore,
                                       NavigationService<GroupDetailsViewModel> groupDetailsNavigationService)
        {
            _groupCommandViewModel = groupCommandViewModel;
            _selectedGroupDetailsStore = selectedGroupDetailsStore;
            _groupDetailsNavigationService = groupDetailsNavigationService;
        }

        public override void Execute(object? parameter)
        {
            Group group = _groupCommandViewModel.Model;
            _selectedGroupDetailsStore.SelectedModel = group;
            _groupDetailsNavigationService.Navigate();
        }
    }
}