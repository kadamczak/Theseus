using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.ExamSetCommands
{
    public class CreateExamSetManuallyCommand : AsyncCommandBase
    {
        private readonly CreateSetManuallyViewModel _createSetManuallyViewModel;
        private readonly ICreateExamSetCommand _createExamSetCommand;
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;
        private readonly NavigationService<CreateSetViewModel> _createSetNavigationService;

        public CreateExamSetManuallyCommand(CreateSetManuallyViewModel createSetManuallyViewModel,
                                            ICreateExamSetCommand createExamSetCommand,
                                            ICurrentStaffMemberStore currentStaffMemberStore,
                                            NavigationService<CreateSetViewModel> createSetNavigationService)
        {
            _createSetManuallyViewModel = createSetManuallyViewModel;
            _createExamSetCommand = createExamSetCommand;
            _currentStaffMemberStore = currentStaffMemberStore;
            _createSetNavigationService = createSetNavigationService;
            _createSetManuallyViewModel.PropertyChanged += OnPropertyChanged;
        }

        protected override void Dispose()
        {
            _createSetManuallyViewModel.PropertyChanged -= OnPropertyChanged;
            base.Dispose();
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            ExamSet examSet = new ExamSet(Guid.NewGuid(), _createSetManuallyViewModel.SelectedMazes)
            {
                Name = _createSetManuallyViewModel.ExamSetName,
                StaffMember = _currentStaffMemberStore.StaffMember ?? throw new StaffMemberNotLoggedInException()
            };

            await _createExamSetCommand.CreateExamSet(examSet);
            _createSetNavigationService.Navigate();
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_createSetManuallyViewModel.CanCreate))
                OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _createSetManuallyViewModel.CanCreate && base.CanExecute(parameter);
        }
    }
}