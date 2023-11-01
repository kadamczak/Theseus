﻿using System.Windows.Input;
using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.QueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.ViewModels
{
    public class CreateSetManuallyViewModel : ViewModelBase
    {
        public AddToSetMazeCommandListViewModel AddToSetMazeCommandListViewModel { get; }
        public ICommand CreateSetManually { get; }

        public CreateSetManuallyViewModel(SelectedMazeListStore mazeListStore,
                                          IGetAllMazesWithSolutionQuery getAllMazesWithSolutionQuery,
                                          ICreateExamSetCommand createExamSetCommand,
                                          NavigationService<CreateSetViewModel> createSetNavigationService,
                                          AddToSetMazeCommandListViewModel addToSetMazeCommandListViewModel)
        {
            LoadFullMazeListToStore(getAllMazesWithSolutionQuery, mazeListStore);
            this.CreateSetManually = new CreateSetManuallyCommand(addToSetMazeCommandListViewModel, createExamSetCommand, createSetNavigationService);

            this.AddToSetMazeCommandListViewModel = addToSetMazeCommandListViewModel;
            this.AddToSetMazeCommandListViewModel.LoadMazesFromMazeListStore();
        }

        private void LoadFullMazeListToStore(IGetAllMazesWithSolutionQuery getAllMazesWithSolutionQuery, SelectedMazeListStore mazeListStore)
        {
            var fullMazeList = getAllMazesWithSolutionQuery.GetAllMazesWithSolution();
            mazeListStore.MazesInList = fullMazeList;
        }
    }
}