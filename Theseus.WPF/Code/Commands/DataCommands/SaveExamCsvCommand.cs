using System.Threading.Tasks;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Services.ExamDataServices;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.Commands.DataCommands
{
    public class SaveExamCsvCommand : AsyncCommandBase
    {
        private readonly SelectedModelDetailsStore<Exam> _selectedExamStore;
        private readonly ExamCsvWriter _examCsvWriter;

        public SaveExamCsvCommand(SelectedModelDetailsStore<Exam> selectedExamStore, ExamCsvWriter examCsvWriter)
        {
            _selectedExamStore = selectedExamStore;
            _examCsvWriter = examCsvWriter;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();

            if (dialog.ShowDialog().Value)
            {
                string folderPath = dialog.SelectedPath;
                _examCsvWriter.WriteCsvFiles(folderPath, _selectedExamStore.SelectedModel);
            }
        }
    }
}