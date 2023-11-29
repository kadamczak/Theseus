using System;
using System.Collections.Generic;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.Info;

namespace Theseus.WPF.Code.ViewModels
{
    public class BrowseMazesViewModel : ViewModelBase
    {
        public MazeCommandListViewModel ShowDetailsMazeCommandViewModel { get; }

        public BrowseMazesViewModel(SelectedModelListStore<MazeWithSolutionCanvasViewModel> mazeListStore,
                                    IGetMazesWithSolutionOfStaffMemberQuery getAllMazesWithSolutionOfStaffMemberQuery,
                                    ICurrentStaffMemberStore currentStaffMemberStore,
                                    MazeCommandListViewModelFactory mazeCommandListViewModelFactory,
                                    MazeReturnServiceStore mazeReturnServiceStore,
                                    NavigationStore navigationStore,
                                    Func<BrowseMazesViewModel> viewModelGenerator)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            LoadMazesOfStaffMember(getAllMazesWithSolutionOfStaffMemberQuery, currentStaffMemberStore.StaffMember!.Id, mazeListStore);
            mazeReturnServiceStore.MazeReturnNavigationService = new NavigationService<ViewModelBase>(navigationStore, viewModelGenerator);

            this.ShowDetailsMazeCommandViewModel = mazeCommandListViewModelFactory.Create(MazeButtonCommand.ShowDetails, MazeButtonCommand.Delete, MazeInfo.GeneralInfo);
            this.ShowDetailsMazeCommandViewModel.CreateModelCommandViewModels();
        }

        private void LoadMazesOfStaffMember(IGetMazesWithSolutionOfStaffMemberQuery query,
                                            Guid staffMemberId,
                                            SelectedModelListStore<MazeWithSolutionCanvasViewModel> mazeListStore)
        {
            var mazeList = query.GetMazesWithSolution(staffMemberId);

            var mazeCanvases = new List<MazeWithSolutionCanvasViewModel>();
            foreach(var maze in mazeList)
            {
                mazeCanvases.Add(new MazeWithSolutionCanvasViewModel(maze));
            }

            mazeListStore.ModelList = mazeCanvases;
        }
    }
}