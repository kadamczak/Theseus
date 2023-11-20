using System;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.Stores.Mazes;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamSetDetailsViewModel : ViewModelBase
    {
        public ShowDetailsMazeCommandListViewModel ShowDetailsMazeCommandViewModel { get; }

        public ExamSetDetailsViewModel(SelectedMazeListStore mazeListStore,
                                       SelectedExamSetDetailsStore examSetDetailsStore,
                                       IGetMazesWithSolutionOfExamSetQuery getAllMazesOfExamSetQuery,
                                       ICurrentStaffMemberStore currentStaffMemberStore,
                                       ShowDetailsMazeCommandListViewModel showDetailsMazeCommandListViewModel)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            LoadMazesFromSelectedExamSet(getAllMazesOfExamSetQuery, examSetDetailsStore.SelectedExamSet.Id, mazeListStore);

            this.ShowDetailsMazeCommandViewModel = showDetailsMazeCommandListViewModel;
            this.ShowDetailsMazeCommandViewModel.CreateMazeCommandViewModels();
        }

        private void LoadMazesFromSelectedExamSet(IGetMazesWithSolutionOfExamSetQuery getAllMazesOfExamSetQuery,
                                                  Guid examSetId,
                                                  SelectedMazeListStore mazeListStore)
        {
            var mazes = getAllMazesOfExamSetQuery.GetMazesWithSolution(examSetId);
            mazeListStore.Mazes = mazes;
        }
    }
}