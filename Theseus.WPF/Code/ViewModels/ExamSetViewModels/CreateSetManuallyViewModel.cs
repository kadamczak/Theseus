using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class CreateSetManuallyViewModel : ViewModelBase
    {
        public AddToSetMazeCommandListViewModel AddToSetMazeCommandListViewModel { get; }
        
        private string _examSetName = string.Empty;
        public string ExamSetName
        {
            get => _examSetName;
            set
            {
                _examSetName = value;
                OnPropertyChanged(nameof(ExamSetName));
                OnPropertyChanged(nameof(CanCreate));
            }
        }

        public List<MazeWithSolution> SelectedMazes => AddToSetMazeCommandListViewModel.SelectedMazes.ToList();
        public bool CanCreate => !string.IsNullOrEmpty(_examSetName) && AddToSetMazeCommandListViewModel.SelectedMazes.Any();
        public ICommand CreateSetManually { get; }

        public CreateSetManuallyViewModel(SelectedModelListStore<MazeWithSolution> mazeListStore,
                                          IGetMazesWithSolutionOfStaffMemberQuery getAllMazesWithSolutionOfStaffMemberQuery,
                                          ICreateExamSetCommand createExamSetCommand,
                                          ICurrentStaffMemberStore currentStaffMemberStore,
                                          NavigationService<CreateSetViewModel> createSetNavigationService,
                                          AddToSetMazeCommandListViewModel addToSetMazeCommandListViewModel)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            LoadFullMazeListToStore(getAllMazesWithSolutionOfStaffMemberQuery, currentStaffMemberStore.StaffMember!.Id, mazeListStore);
            this.CreateSetManually = new CreateExamSetManuallyCommand(this, createExamSetCommand, currentStaffMemberStore, createSetNavigationService);

            this.AddToSetMazeCommandListViewModel = addToSetMazeCommandListViewModel;
            this.AddToSetMazeCommandListViewModel.CreateMazeCommandViewModels();
            this.AddToSetMazeCommandListViewModel.SelectedMazes.CollectionChanged += OnCollectionChanged;
        }

        private void LoadFullMazeListToStore(IGetMazesWithSolutionOfStaffMemberQuery query, Guid staffMemberId, SelectedModelListStore<MazeWithSolution> mazeListStore)
        {
            var fullMazeList = query.GetMazesWithSolution(staffMemberId);
            mazeListStore.ModelList = fullMazeList;
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CanCreate));
        }
    }
}