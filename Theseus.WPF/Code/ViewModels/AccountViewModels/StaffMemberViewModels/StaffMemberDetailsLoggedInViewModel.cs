using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class StaffMemberDetailsLoggedInViewModel : ErrorCheckingViewModel
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

                ClearErrors(nameof(Email));

                if (!_emailValidator.IsValid(Email))
                {
                    AddError(nameof(Email), "Email is invalid.");
                }

                OnPropertyChanged(nameof(CanUpdateStaffMember));
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

                ClearErrors(nameof(Name));

                if (string.IsNullOrWhiteSpace(Name))
                {
                    AddError(nameof(Name), "Name can't be empty.");
                }

                OnPropertyChanged(nameof(CanUpdateStaffMember));
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

                ClearErrors(nameof(Surname));  
                
                if(string.IsNullOrWhiteSpace(Surname))
                {
                    AddError(nameof(Surname), "Surname can't be empty.");
                }

                OnPropertyChanged(nameof(CanUpdateStaffMember));
            }
        }

        public StaffMember CurrentStaffMember { get; }

        public ICommand Save { get; }
        public ICommand Logout { get; }

        private readonly IEmailValidator _emailValidator;
        public bool CanUpdateStaffMember => !HasErrors && FormHasChanges();

        public StaffMemberDetailsLoggedInViewModel(IStaffMemberAuthenticator authenticator,
                                                   IEmailValidator emailValidator,
                                                   IUpdateStaffMemberCommand updateStaffMemberCommand,
                                                   NavigationService<StaffMemberLoginRegisterViewModel> staffMemberLoginRegisterNavigationService)
        {
            if (!authenticator.IsLoggedInAsStaffMember)
                return;

            this._emailValidator = emailValidator;
            this.CurrentStaffMember = authenticator.CurrentStaffMember!;
            LoadStaffMemberInfo(authenticator.CurrentStaffMember!);

            Save = new SaveStaffMemberInfoCommand(this, updateStaffMemberCommand);
            Logout = new LogoutStaffMemberCommand(authenticator, staffMemberLoginRegisterNavigationService);
        }

        private void LoadStaffMemberInfo(StaffMember staffMember)
        {
            this._username = staffMember.Username;
            this._email = staffMember.Email;
            this._name = staffMember.Name;
            this._surname = staffMember.Surname;
        }

        public void UpdateCurrentStaffMemberInfoFromViewModel()
        {
            CurrentStaffMember.Email = Email.Trim();
            CurrentStaffMember.Name = Name.Trim();
            CurrentStaffMember.Surname = Surname.Trim();
        }

        public bool FormHasChanges()
        {
            var currentStaffMemberInfo = (CurrentStaffMember.Email, CurrentStaffMember.Name, CurrentStaffMember.Surname);
            var infoFromViewModel = (Email, Name, Surname);
            return currentStaffMemberInfo != infoFromViewModel;
        }
    }
}