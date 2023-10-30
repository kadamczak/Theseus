﻿using Theseus.Domain.QueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.ViewModels
{
    public class CreateSetManuallyViewModel : ViewModelBase
    {
        public AddToSetMazeCommandListViewModel AddToSetMazeCommandListViewModel { get; }

        public CreateSetManuallyViewModel(MazeListStore mazeListStore,
                                          IGetAllMazesWithSolutionQuery getAllMazesWithSolutionQuery,
                                          AddToSetMazeCommandListViewModel addToSetMazeCommandListViewModel)
        {
            LoadFullMazeList(getAllMazesWithSolutionQuery, mazeListStore);
            this.AddToSetMazeCommandListViewModel = addToSetMazeCommandListViewModel;
            this.AddToSetMazeCommandListViewModel.LoadMazesFromMazeListStore();
        }

        private void LoadFullMazeList(IGetAllMazesWithSolutionQuery getAllMazesWithSolutionQuery, MazeListStore mazeListStore)
        {
            var fullMazeList = getAllMazesWithSolutionQuery.GetAllMazesWithSolution();
            mazeListStore.MazesInList = fullMazeList;
        }
    }
}