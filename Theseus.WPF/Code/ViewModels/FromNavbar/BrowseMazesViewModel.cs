using System;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.Mazes;

namespace Theseus.WPF.Code.ViewModels
{
    public class BrowseMazesViewModel : ViewModelBase
    {
        public ShowDetailsDeleteMazeCommandListViewModel ShowDetailsMazeCommandViewModel { get; }

        public BrowseMazesViewModel(SelectedModelListStore<MazeWithSolution> mazeListStore,
                                    IGetMazesWithSolutionOfStaffMemberQuery getAllMazesWithSolutionOfStaffMemberQuery,
                                    ICurrentStaffMemberStore currentStaffMemberStore,
                                    ShowDetailsDeleteMazeCommandListViewModel showDetailsMazeCommandListViewModel,
                                    MazeReturnServiceStore mazeReturnServiceStore,
                                    NavigationStore navigationStore,
                                    Func<BrowseMazesViewModel> viewModelGenerator)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            LoadMazesOfStaffMember(getAllMazesWithSolutionOfStaffMemberQuery, currentStaffMemberStore.StaffMember!.Id, mazeListStore);
            mazeReturnServiceStore.MazeReturnNavigationService = new NavigationService<ViewModelBase>(navigationStore, viewModelGenerator);

            this.ShowDetailsMazeCommandViewModel = showDetailsMazeCommandListViewModel;
            this.ShowDetailsMazeCommandViewModel.CreateMazeCommandViewModels();
        }

        private void LoadMazesOfStaffMember(IGetMazesWithSolutionOfStaffMemberQuery query,
                                            Guid staffMemberId,
                                            SelectedModelListStore<MazeWithSolution> mazeListStore)
        {
            var mazeList = query.GetMazesWithSolution(staffMemberId);
            mazeListStore.ModelList = mazeList;
        }
    }
}