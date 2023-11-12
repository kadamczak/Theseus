using System.Windows.Input;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
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

        public StaffMemberDetailsLoggedInViewModel(IStaffMemberAuthenticator authenticator,
                                                   NavigationService<StaffMemberLoginRegisterViewModel> staffMemberLoginRegisterNavigationService)
        {
            if(authenticator.IsLoggedInAsStaffMember)
                LoadStaffMemberInfo(authenticator.CurrentStaffMember!);

            Logout = new LogoutStaffMemberCommand(authenticator, staffMemberLoginRegisterNavigationService);
        }

        private void LoadStaffMemberInfo(StaffMember staffMember)
        {
            this.Username = staffMember.Username;
            this.Email = staffMember.Email;
            this.Name = staffMember.Name;
            this.Surname = staffMember.Surname;
        }
    }
}