using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.Info;

namespace Theseus.WPF.Code.ViewModels
{
    public class CreateSetManuallyViewModel : ErrorCheckingViewModel
    {
        public MazeCommandListViewModel AddToSetMazeCommandListViewModel { get; }
        
        private string _examSetName = string.Empty;
        public string ExamSetName
        {
            get => _examSetName;
            set
            {
                _examSetName = value;
                OnPropertyChanged(nameof(ExamSetName));
                ClearErrors(nameof(ExamSetName));

                if (!Regex.IsMatch(ExamSetName, @"^[\w_]*$"))
                {
                    AddError(nameof(ExamSetName), "InvalidValue".Resource());
                }

                OnPropertyChanged(nameof(CanSave));
            }
        }

        public bool CanSave => !HasErrors && !string.IsNullOrWhiteSpace(_examSetName);
        public ICommand CreateSetManually { get; }

        public CreateSetManuallyViewModel(SelectedModelListStore<MazeWithSolutionCanvasViewModel> mazeListStore,
                                          IGetMazesWithSolutionOfStaffMemberQuery getAllMazesWithSolutionOfStaffMemberQuery,
                                          ICreateExamSetCommand createExamSetCommand,
                                          ICurrentStaffMemberStore currentStaffMemberStore,
                                          NavigationService<CreateSetViewModel> createSetNavigationService,
                                          MazeCommandListViewModelFactory addToSetMazeCommandListViewModel,
                                          MazesInExamSetStore mazesInExamSetStore)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            try
            {
                LoadFullMazeListToStore(getAllMazesWithSolutionOfStaffMemberQuery, currentStaffMemberStore.StaffMember!.Id, mazeListStore);
            }
            catch(SqlException)
            {
                string messageBoxText = "CouldNotConnectToDatabase".Resource();
                string caption = "ActionFailed".Resource();
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;
                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            }

            this.CreateSetManually = new CreateExamSetManuallyCommand(this, createExamSetCommand, currentStaffMemberStore, mazesInExamSetStore, createSetNavigationService);

            this.AddToSetMazeCommandListViewModel = addToSetMazeCommandListViewModel.Create(MazeButtonCommand.AddToExamSet, MazeButtonCommand.None, MazeInfo.None);
            this.AddToSetMazeCommandListViewModel.CreateModelCommandViewModels();
        }

        private void LoadFullMazeListToStore(IGetMazesWithSolutionOfStaffMemberQuery query, Guid staffMemberId, SelectedModelListStore<MazeWithSolutionCanvasViewModel> mazeListStore)
        {
            var fullMazeList = query.GetMazesWithSolution(staffMemberId);

            var mazeCanvases = new List<MazeWithSolutionCanvasViewModel>();
            foreach (var maze in fullMazeList)
            {
                mazeCanvases.Add(new MazeWithSolutionCanvasViewModel(maze));
            }

            mazeListStore.ModelList = mazeCanvases;
        }
    }
}