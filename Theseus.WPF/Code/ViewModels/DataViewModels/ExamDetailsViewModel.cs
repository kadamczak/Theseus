using System;
using System.Collections.Generic;
using System.Windows.Input;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.DataCommands;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.ExamBindings;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.Info;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamDetailsViewModel : ViewModelBase
    {
        public ExamStageCommandListViewModel ExamStageCommandListViewModel { get; set; }

        public ICommand SaveCsv { get; set; }

        public ExamDetailsViewModel(IGetExamStagesOfExamQuery getExamStagesQuery,
                                    IGetMazeOfExamStageQuery getMazeQuery,
                                    SelectedModelDetailsStore<Exam> examDetailsStore,
                                    SelectedModelListStore<ExamStageWithMazeViewModel> examStageListStore,
                                    ExamStageCommandListViewModelFactory examStageCommandListViewModelFactory)
        {
            LoadExamStagesOfExam(getExamStagesQuery, getMazeQuery, examDetailsStore.SelectedModel.Id, examStageListStore);

            ExamStageCommandListViewModel = examStageCommandListViewModelFactory.Create(ExamStageButtonCommand.None, ExamStageButtonCommand.None, ExamStageInfo.GeneralInfo);
            ExamStageCommandListViewModel.CreateModelCommandViewModels();

            SaveCsv = new SaveExamCsvCommand(examDetailsStore, getMazeQuery);
        }

        private void LoadExamStagesOfExam(IGetExamStagesOfExamQuery query,
                                          IGetMazeOfExamStageQuery mazeQuery,
                                          Guid examId,
                                          SelectedModelListStore<ExamStageWithMazeViewModel> examStageListStore)
        {
            var examStageList = query.GetExamStages(examId);

            var examStagesWithMaze = new List<ExamStageWithMazeViewModel>();
            foreach (var examStage in examStageList)
            {
                var mazeCanvas = new MazeWithSolutionCanvasViewModel(mazeQuery.GetMaze(examStage.Id));
                examStagesWithMaze.Add(new ExamStageWithMazeViewModel(examStage, mazeCanvas));
            }

            examStageListStore.ModelList = examStagesWithMaze;
        }
    }
}