using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands
{
    public class SaveStaffMemberInfoCommand : AsyncCommandBase
    {
        private readonly StaffMemberDetailsLoggedInViewModel _staffMemberDetailsLoggedInViewModel;
        private readonly IUpdateStaffMemberCommand _updateStaffMemberCommand;

        public SaveStaffMemberInfoCommand(StaffMemberDetailsLoggedInViewModel staffMemberDetailsLoggedInViewModel,
                                          IUpdateStaffMemberCommand updateStaffMemberCommand)
        {
            _staffMemberDetailsLoggedInViewModel = staffMemberDetailsLoggedInViewModel;
            _updateStaffMemberCommand = updateStaffMemberCommand;

            _staffMemberDetailsLoggedInViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                _staffMemberDetailsLoggedInViewModel.UpdateCurrentStaffMemberInfoFromViewModel();
                await _updateStaffMemberCommand.Update(_staffMemberDetailsLoggedInViewModel.CurrentStaffMember);
            }
            catch(ArgumentException)
            {
                string messageBoxText = "EmailAlreadyExists".Resource();
                string caption = "ActionFailed".Resource();
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;
                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            }
            catch (SqlException)
            {
                string messageBoxText = "CouldNotConnectToDatabase".Resource();
                string caption = "ActionFailed".Resource();
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;
                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_staffMemberDetailsLoggedInViewModel.CanUpdateStaffMember))
                OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _staffMemberDetailsLoggedInViewModel.CanUpdateStaffMember && base.CanExecute(parameter);
        }
    }
}