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
                                    IGetMazesWithSolutionOfStaffMemberQuery getAllMazesWithSolutionOfStaffMemberQuery,
                                    ICurrentStaffMemberStore currentStaffMemberStore,
                                    ShowDetailsMazeCommandListViewModel showDetailsMazeCommandListViewModel)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            LoadMazesOfStaffMember(getAllMazesWithSolutionOfStaffMemberQuery, currentStaffMemberStore.StaffMember!.Id, mazeListStore);

            this.ShowDetailsMazeCommandViewModel = showDetailsMazeCommandListViewModel;
            this.ShowDetailsMazeCommandViewModel.CreateMazeCommandViewModels();
        }

        private void LoadMazesOfStaffMember(IGetMazesWithSolutionOfStaffMemberQuery query,
                                            Guid staffMemberId,
                                            SelectedMazeListStore mazeListStore)
        {
            var mazeList = query.GetMazesWithSolution(staffMemberId);
            mazeListStore.Mazes = mazeList;
        }
    }
}