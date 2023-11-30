using System;
using System.Collections.Generic;
using System.Windows.Input;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.Info;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamSetDetailsViewModel : ViewModelBase
    {
        public MazeCommandListViewModel ShowDetailsMazeCommandViewModel { get; }
        public ICommand GoBack { get; }

        public ExamSetDetailsViewModel(SelectedModelListStore<MazeWithSolutionCanvasViewModel> mazeListStore,
                                       SelectedModelDetailsStore<ExamSet> examSetDetailsStore,
                                       IGetOrderedMazesWithSolutionOfExamSetQuery getAllMazesOfExamSetQuery,
                                       ICurrentStaffMemberStore currentStaffMemberStore,
                                       MazeCommandListViewModelFactory showDetailsMazeCommandListViewModel,
                                       MazeReturnServiceStore mazeReturnServiceStore,
                                       NavigationStore navigationStore,
                                       Func<ExamSetDetailsViewModel> viewModelGenerator,
                                       ExamSetReturnServiceStore examSetReturnServiceStore)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            LoadMazesFromSelectedExamSet(getAllMazesOfExamSetQuery, examSetDetailsStore.SelectedModel.Id, mazeListStore);
            mazeReturnServiceStore.MazeReturnNavigationService = new NavigationService<ViewModelBase>(navigationStore, viewModelGenerator);
            GoBack = new NavigateCommand<ViewModelBase>(examSetReturnServiceStore.ExamSetNavigationService);

            this.ShowDetailsMazeCommandViewModel = showDetailsMazeCommandListViewModel.Create(MazeButtonCommand.ShowDetails, MazeButtonCommand.None, MazeInfo.GeneralInfo);
            this.ShowDetailsMazeCommandViewModel.CreateModelCommandViewModels();
        }

        private void LoadMazesFromSelectedExamSet(IGetOrderedMazesWithSolutionOfExamSetQuery getAllMazesOfExamSetQuery,
                                                  Guid examSetId,
                                                  SelectedModelListStore<MazeWithSolutionCanvasViewModel> mazeListStore)
        {
            var mazes = getAllMazesOfExamSetQuery.GetMazesWithSolution(examSetId);

            var mazeCanvases = new List<MazeWithSolutionCanvasViewModel>();
            foreach (var maze in mazes)
            {
                mazeCanvases.Add(new MazeWithSolutionCanvasViewModel(maze));
            }

            mazeListStore.ModelList = mazeCanvases;
        }
    }
}