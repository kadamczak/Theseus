using System;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.Mazes;

namespace Theseus.WPF.Code.ViewModels
{
    public class BrowseMazesViewModel : ViewModelBase
    {
        public ShowDetailsMazeCommandListViewModel ShowDetailsMazeCommandViewModel { get; }

        public BrowseMazesViewModel(SelectedMazeListStore mazeListStore,
                                    IGetAllMazesWithSolutionOfStaffMemberQuery getAllMazesWithSolutionOfStaffMemberQuery,
                                    ICurrentStaffMemberStore currentStaffMemberStore,
                                    ShowDetailsMazeCommandListViewModel showDetailsMazeCommandListViewModel)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            LoadFullMazeList(getAllMazesWithSolutionOfStaffMemberQuery, currentStaffMemberStore.StaffMember!.Id, mazeListStore);

            this.ShowDetailsMazeCommandViewModel = showDetailsMazeCommandListViewModel;
            this.ShowDetailsMazeCommandViewModel.LoadMazesFromMazeListStore();
        }

        private void LoadFullMazeList(IGetAllMazesWithSolutionOfStaffMemberQuery query,
                                      Guid staffMemberId,
                                      SelectedMazeListStore mazeListStore)
        {
            var fullMazeList = query.GetAllMazesWithSolutionOfStaffMemberQuery(staffMemberId);
            mazeListStore.MazesInList = fullMazeList;
        }
    }
}