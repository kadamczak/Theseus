﻿using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.NavigationCommands
{
    public class OpenAccountViewModelCommand : CommandBase
    {
        private readonly NavigationService<LoggedInViewModel> _loggedInNavigationService;
        private readonly NavigationService<NotLoggedInViewModel> _notLoggedInNavigationService;
        private readonly ICurrentUserStore _currentUserStore;

        public OpenAccountViewModelCommand(NavigationService<LoggedInViewModel> loggedInNavigationService,
                                           NavigationService<NotLoggedInViewModel> notLoggedInNavigationService,
                                           ICurrentUserStore currentUserStore)
        {
            _loggedInNavigationService = loggedInNavigationService;
            _notLoggedInNavigationService = notLoggedInNavigationService;
            _currentUserStore = currentUserStore;
        }

        public override void Execute(object? parameter)
        {
            if (_currentUserStore.CurrentStaffMember is null)
            {
                _notLoggedInNavigationService.Navigate();
            }
            else
            {
                _loggedInNavigationService.Navigate();
            }
        }
    }
}