using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.CommandInterfaces.MazeCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.ExamSetCommands
{
    public class SaveGeneratedExamSetCommand : AsyncCommandBase
    {
        private readonly ExamSetGeneratorResultViewModel _resultViewModel;
        private readonly SelectedModelDetailsStore<ExamSet> _examSetDetailsStore;
        private readonly ICreateMazeWithSolutionCommand _createMazeCommand;
        private readonly ICreateExamSetCommand _createExamSetCommand;
        private readonly NavigationService<CreateSetViewModel> _generatorNavigationService;

        public SaveGeneratedExamSetCommand(ExamSetGeneratorResultViewModel resultViewModel,
                                           SelectedModelDetailsStore<ExamSet> examSetDetailsStore,
                                           ICreateMazeWithSolutionCommand createMazeCommand,
                                           ICreateExamSetCommand createExamSetCommand,
                                           NavigationService<CreateSetViewModel> generatorNavigationService)
        {
            _resultViewModel = resultViewModel;
            _examSetDetailsStore = examSetDetailsStore;
            _createMazeCommand = createMazeCommand;
            _createExamSetCommand = createExamSetCommand;
            _generatorNavigationService = generatorNavigationService;
            _resultViewModel.PropertyChanged += OnPropertyChanged;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                _examSetDetailsStore.SelectedModel.Name = _resultViewModel.ExamSetName;
                var mazes = _examSetDetailsStore.SelectedModel.ExamSetMazeIndexes.Select(m => m.MazeWithSolution);

                foreach (var maze in mazes)
                {
                    await _createMazeCommand.Create(maze);
                }

                await _createExamSetCommand.CreateExamSet(_examSetDetailsStore.SelectedModel);
            }
            catch(SqlException)
            {
                string messageBoxText = "CouldNotConnectToDatabase".Resource();
                string caption = "ActionFailed".Resource();
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;
                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            }

            _generatorNavigationService.Navigate();
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_resultViewModel.CanSave))
                OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _resultViewModel.CanSave && base.CanExecute(parameter);
        }
    }
}