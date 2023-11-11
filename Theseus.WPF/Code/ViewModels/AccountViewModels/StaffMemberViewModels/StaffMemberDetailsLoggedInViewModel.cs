using System;
using System.Windows.Input;
using Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication;
using Theseus.WPF.Code.ViewModels.AccountViewModels.Interfaces;

namespace Theseus.WPF.Code.ViewModels
{
    public class StaffMemberDetailsLoggedInViewModel : AccountDetailsViewModel
    {
        private string _username = string.Empty;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _email = string.Empty;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _name = string.Empty;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _surname = string.Empty;

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public ICommand Logout { get; }

        public StaffMemberDetailsLoggedInViewModel(ICurrentUserStore currentUserStore,
                                                   IAuthenticator authenticator,
                                                   NavigationService<StaffMemberLoginRegisterViewModel> staffMemberLoginRegisterNavigationService)
        {
            LoadStaffMemberInfo(currentUserStore);
            Logout = new LogoutStaffMemberCommand(authenticator, staffMemberLoginRegisterNavigationService);
        }

        private void LoadStaffMemberInfo(ICurrentUserStore currentUserStore)
        {
            this.Username = currentUserStore.CurrentStaffMember.Username;
            this.Email = currentUserStore.CurrentStaffMember.Email;
            this.Name = currentUserStore.CurrentStaffMember.Name;
            this.Surname = currentUserStore.CurrentStaffMember.Surname;
        }
    }
}