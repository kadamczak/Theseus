using System.ComponentModel;
using System.Threading.Tasks;
using Theseus.Domain.CommandInterfaces;
using Theseus.WPF.Code.Bases;
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

        public override async Task ExecuteAsync(object parameter)
        {
            _staffMemberDetailsLoggedInViewModel.UpdateCurrentStaffMemberInfoFromViewModel();
            await _updateStaffMemberCommand.Update(_staffMemberDetailsLoggedInViewModel.CurrentStaffMember);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _staffMemberDetailsLoggedInViewModel.CheckIfStaffMemberCanSaveChanges()
                && base.CanExecute(parameter);
        }
    }
}