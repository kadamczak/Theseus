using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
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
            ExamSet examSet = new ExamSet(Guid.NewGuid())
            {
                Name = _createSetManuallyViewModel.ExamSetName.Trim(),
                StaffMember = _currentStaffMemberStore.StaffMember ?? throw new StaffMemberNotLoggedInException()
            };

            examSet.ExamSetMazeIndexes = CreateMazeIndexesList(examSet);

            try
            {
                await _createExamSetCommand.CreateExamSet(examSet);
            }
            catch(SqlException)
            {
                string messageBoxText = "CouldNotConnectToDatabase".Resource();
                string caption = "ActionFailed".Resource();
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;
                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            }

            _createSetNavigationService.Navigate();
        }

        private List<ExamSetMazeIndex> CreateMazeIndexesList(ExamSet examSet)
        {
            List<ExamSetMazeIndex> mazeIndexes = new List<ExamSetMazeIndex>();
            var mazes = _mazesInExamSetStore.SelectedMazes;
            for (int i = 0; i < mazes.Count(); i++)
            {
                mazeIndexes.Add(new ExamSetMazeIndex(Guid.NewGuid(), examSet, mazes[i], i));
            }
            return mazeIndexes;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_createSetManuallyViewModel.CanSave))
                OnCanExecuteChanged();
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _createSetManuallyViewModel.CanSave && _mazesInExamSetStore.SelectedMazes.Any() && base.CanExecute(parameter);
        }
    }
}