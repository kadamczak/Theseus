using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using Theseus.Domain.CommandInterfaces.ExamCommandInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Exams;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamEndViewModel : ViewModelBase
    {
        public ICommand GoBack { get; }
        public bool DataUploadedSuccessfully { get; set; } = false;

        public ExamEndViewModel(ICreateExamCommand createExamCommand,
                                CurrentExamStore currentExamStore,
                                NavigationService<BeginTestViewModel> navigationService)
        {
            try
            {
                createExamCommand.Create(currentExamStore.CurrentExam);
                DataUploadedSuccessfully = true;
            }
            catch(SqlException)
            {
                DisplayConnectionFailedMessage();
            }

            GoBack = new NavigateCommand<BeginTestViewModel>(navigationService);
        }

        private void DisplayConnectionFailedMessage()
        {
            string messageBoxText = "CouldNotConnectToDatabase".Resource();
            string caption = "ActionFailed".Resource();
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Exclamation;
            MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
        }
    }
}