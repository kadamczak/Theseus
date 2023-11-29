using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.ExamSetCommands
{
    public class CreateExamSetManuallyCommand : AsyncCommandBase
    {
        private readonly CreateSetManuallyViewModel _createSetManuallyViewModel;
        private readonly ICreateExamSetCommand _createExamSetCommand;
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;
        private readonly MazesInExamSetStore _mazesInExamSetStore;
        private readonly NavigationService<CreateSetViewModel> _createSetNavigationService;

        public CreateExamSetManuallyCommand(CreateSetManuallyViewModel createSetManuallyViewModel,
                                            ICreateExamSetCommand createExamSetCommand,
                                            ICurrentStaffMemberStore currentStaffMemberStore,
                                            MazesInExamSetStore mazesInExamSetStore,
                                            NavigationService<CreateSetViewModel> createSetNavigationService)
        {
            _createSetManuallyViewModel = createSetManuallyViewModel;
            _createExamSetCommand = createExamSetCommand;
            _currentStaffMemberStore = currentStaffMemberStore;
            _createSetNavigationService = createSetNavigationService;
            _mazesInExamSetStore = mazesInExamSetStore;

            _createSetManuallyViewModel.PropertyChanged += OnPropertyChanged;
            _mazesInExamSetStore.SelectedMazes.CollectionChanged += OnCollectionChanged;
        }

        protected override void Dispose()
        {
            _createSetManuallyViewModel.PropertyChanged -= OnPropertyChanged;
            base.Dispose();
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            ExamSet examSet = new ExamSet(Guid.NewGuid(), _mazesInExamSetStore.SelectedMazes.ToList())
            {
                Name = _createSetManuallyViewModel.ExamSetName,
                StaffMember = _currentStaffMemberStore.StaffMember ?? throw new StaffMemberNotLoggedInException()
            };

            await _createExamSetCommand.CreateExamSet(examSet);
            _createSetNavigationService.Navigate();
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_createSetManuallyViewModel.ExamSetNameEntered))
                OnCanExecuteChanged();
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _createSetManuallyViewModel.ExamSetNameEntered && _mazesInExamSetStore.SelectedMazes.Any() && base.CanExecute(parameter);
        }
    }
}