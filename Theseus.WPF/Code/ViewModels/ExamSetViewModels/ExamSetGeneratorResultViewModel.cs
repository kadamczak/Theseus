using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.CommandInterfaces.MazeCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.Info;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamSetGeneratorResultViewModel : ViewModelBase
    {
        public MazeCommandListViewModel ShowDetailsMazeCommandViewModel { get; }

        private string _examSetName = string.Empty;
        public string ExamSetName
        {
            get => _examSetName;
            set
            {
                _examSetName = value;
                OnPropertyChanged(nameof(ExamSetName));
                OnPropertyChanged(nameof(ExamSetNameEntered));
            }
        }
        public bool ExamSetNameEntered => !string.IsNullOrWhiteSpace(_examSetName);

        public ICommand GoBack { get; }
        public ICommand SaveExamSet { get; }

        public ExamSetGeneratorResultViewModel(SelectedModelListStore<MazeWithSolutionCanvasViewModel> mazeListStore,
                                               SelectedModelDetailsStore<ExamSet> examSetDetailsStore,
                                               MazeCommandListViewModelFactory showDetailsMazeCommandListViewModel,
                                               NavigationStore navigationStore,
                                               Func<CreateSetViewModel> viewModelGenerator,
                                               ExamSetReturnServiceStore examSetReturnServiceStore,
                                               ICreateMazeWithSolutionCommand createMazeCommand,
                                               ICreateExamSetCommand createExamSetCommand,
                                               NavigationService<CreateSetViewModel> generatorNavigationService)
        {
            LoadMazesFromSelectedExamSet(examSetDetailsStore, mazeListStore);
            examSetReturnServiceStore.ExamSetNavigationService = new NavigationService<ViewModelBase>(navigationStore, viewModelGenerator);
            GoBack = new NavigateCommand<ViewModelBase>(examSetReturnServiceStore.ExamSetNavigationService);
            SaveExamSet = new SaveGeneratedExamSetCommand(this, examSetDetailsStore, createMazeCommand, createExamSetCommand, generatorNavigationService);

            this.ShowDetailsMazeCommandViewModel = showDetailsMazeCommandListViewModel.Create(MazeButtonCommand.None, MazeButtonCommand.None, MazeInfo.None);
            this.ShowDetailsMazeCommandViewModel.CreateModelCommandViewModels();
        }

        private void LoadMazesFromSelectedExamSet(SelectedModelDetailsStore<ExamSet> examSetDetailsStore,
                                                  SelectedModelListStore<MazeWithSolutionCanvasViewModel> mazeListStore)
        {
            var mazes = examSetDetailsStore.SelectedModel.ExamSetMazeIndexes.Select(i => i.MazeWithSolution);

            var mazeCanvases = new List<MazeWithSolutionCanvasViewModel>();
            foreach (var maze in mazes)
            {
                mazeCanvases.Add(new MazeWithSolutionCanvasViewModel(maze));
            }

            mazeListStore.ModelList = mazeCanvases;
        }
    }
}