using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Services.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands
{
    public class RegisterStaffMemberCommand : AsyncCommandBase
    {
        private readonly StaffMemberRegisterViewModel _staffMemberRegisterViewModel;
        private readonly IStaffMemberAuthenticator _authenticator;

        public RegisterStaffMemberCommand(StaffMemberRegisterViewModel staffMemberRegisterViewModel,
                                          IStaffMemberAuthenticator authenticator)
        {
            _staffMemberRegisterViewModel = staffMemberRegisterViewModel;
            _authenticator = authenticator;

            _staffMemberRegisterViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            StaffMember newStaffMember = new StaffMember()
            {
                Id = Guid.NewGuid(),
                Username = _staffMemberRegisterViewModel.Username,
                Email = _staffMemberRegisterViewModel.Email,
                Name = _staffMemberRegisterViewModel.Name,
                Surname = _staffMemberRegisterViewModel.Surname,
                PasswordHash = _staffMemberRegisterViewModel.Password
            };

            StaffMemberRegistrationResult registrationResult = await _authenticator.Register(newStaffMember, _staffMemberRegisterViewModel.ConfirmPassword);
            this._staffMemberRegisterViewModel.RegistrationResponse = GetRegistrationResponse(registrationResult);
        }

        private string GetRegistrationResponse(StaffMemberRegistrationResult registrationResult)
        {
            return registrationResult switch {
                StaffMemberRegistrationResult.Success => "Succesfully registered.",
                StaffMemberRegistrationResult.UsernameAlreadyExists => "Username already exists.",
                StaffMemberRegistrationResult.EmailAlreadyExists => "Email already exists.",
                StaffMemberRegistrationResult.PasswordsDoNotMatch => "Passwords do not match.",
                _ => throw new ArgumentException("Invalid parameter.")
            };
        }

        private void ViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_staffMemberRegisterViewModel.CanRegister))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _staffMemberRegisterViewModel.CanRegister && base.CanExecute(parameter);
        }
    }
}