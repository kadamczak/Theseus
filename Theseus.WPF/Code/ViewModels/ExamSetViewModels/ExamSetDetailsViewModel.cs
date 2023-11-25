using System;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamSetDetailsViewModel : ViewModelBase
    {
        public ShowDetailsMazeCommandListViewModel ShowDetailsMazeCommandViewModel { get; }

        public ExamSetDetailsViewModel(SelectedModelListStore<MazeWithSolution> mazeListStore,
                                       SelectedModelDetailsStore<ExamSet> examSetDetailsStore,
                                       IGetMazesWithSolutionOfExamSetQuery getAllMazesOfExamSetQuery,
                                       ICurrentStaffMemberStore currentStaffMemberStore,
                                       ShowDetailsMazeCommandListViewModel showDetailsMazeCommandListViewModel)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            LoadMazesFromSelectedExamSet(getAllMazesOfExamSetQuery, examSetDetailsStore.SelectedModel.Id, mazeListStore);

            this.ShowDetailsMazeCommandViewModel = showDetailsMazeCommandListViewModel;
            this.ShowDetailsMazeCommandViewModel.CreateMazeCommandViewModels();
        }

        private void LoadMazesFromSelectedExamSet(IGetMazesWithSolutionOfExamSetQuery getAllMazesOfExamSetQuery,
                                                  Guid examSetId,
                                                  SelectedModelListStore<MazeWithSolution> mazeListStore)
        {
            var mazes = getAllMazesOfExamSetQuery.GetMazesWithSolution(examSetId);
            mazeListStore.ModelList = mazes;
        }
    }
}