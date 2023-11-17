using System;
using System.Windows.Input;
using Theseus.Domain.ExamSetCommandInterfaces;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.Mazes;

namespace Theseus.WPF.Code.ViewModels
{
    public class CreateSetManuallyViewModel : ViewModelBase
    {
        public AddToSetMazeCommandListViewModel AddToSetMazeCommandListViewModel { get; }
        public ICommand CreateSetManually { get; }

        public CreateSetManuallyViewModel(SelectedMazeListStore mazeListStore,
                                          IGetAllMazesWithSolutionOfStaffMemberQuery getAllMazesWithSolutionOfStaffMemberQuery,
                                          ICreateExamSetCommand createExamSetCommand,
                                          ICurrentStaffMemberStore currentStaffMemberStore,
                                          NavigationService<CreateSetViewModel> createSetNavigationService,
                                          AddToSetMazeCommandListViewModel addToSetMazeCommandListViewModel)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            LoadFullMazeListToStore(getAllMazesWithSolutionOfStaffMemberQuery, currentStaffMemberStore.StaffMember.Id, mazeListStore);
            this.CreateSetManually = new CreateExamSetManuallyCommand(addToSetMazeCommandListViewModel, createExamSetCommand, currentStaffMemberStore, createSetNavigationService);

            this.AddToSetMazeCommandListViewModel = addToSetMazeCommandListViewModel;
            this.AddToSetMazeCommandListViewModel.LoadMazesFromMazeListStore();
        }

        private void LoadFullMazeListToStore(IGetAllMazesWithSolutionOfStaffMemberQuery query, Guid staffMemberId, SelectedMazeListStore mazeListStore)
        {
            var fullMazeList = query.GetAllMazesWithSolutionOfStaffMemberQuery(staffMemberId);
            mazeListStore.MazesInList = fullMazeList;
        }
    }
}