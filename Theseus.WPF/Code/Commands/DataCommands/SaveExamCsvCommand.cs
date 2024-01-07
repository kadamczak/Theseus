using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.Commands.DataCommands
{
    public class SaveExamCsvCommand : AsyncCommandBase
    {
        private readonly SelectedModelDetailsStore<Exam> _selectedExamStore;

        public SaveExamCsvCommand(SelectedModelDetailsStore<Exam> selectedExamStore)
        {
            _selectedExamStore = selectedExamStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            var sortedStages = _selectedExamStore.SelectedModel.Stages.OrderBy(s => s.Index);

            if (dialog.ShowDialog().Value)
            {
                string folderPath = dialog.SelectedPath;
                string fileNameBeginning = CreateFileNamePrefix(_selectedExamStore.SelectedModel);

                foreach (var examStage in sortedStages)
                {
                    string completed = examStage.Completed ? "Finished" : "Unfinished";
                    string fileName = folderPath + Path.DirectorySeparatorChar + fileNameBeginning + examStage.Index + "_" + completed + ".csv";

                    string content = CreateCsvForExamStage(examStage);
                    File.WriteAllText(fileName, content);
                }
            }
        }

        private string CreateFileNamePrefix(Exam exam)
        {
            string patientUsername = exam.Patient.Username;
            string examSetName = exam.ExamSet.Name;
            string examDate = exam.CreatedAt.ToString("yyyy-MM-dd-HH-mm");

            return $"{patientUsername}_{examSetName}_{examDate}_";
        }

        private string CreateCsvForExamStage(ExamStage examStage)
        {
            string csvContent = "TimeBeforeInput,InputDirection,Correct,HitWall\n";
            var sortedSteps = examStage.Steps.OrderBy(s => s.Index);

            for (int i = 0; i < sortedSteps.Count(); i++)
            {
                ExamStep step = sortedSteps.ElementAt(i);
                csvContent += step.TimeBeforeStep.ToString(System.Globalization.CultureInfo.InvariantCulture) + ",";
                csvContent += step.StepTaken.ToString() + ",";
                csvContent += step.Correct.ToString() + ",";
                csvContent += step.HitWall.ToString() + "\n";
            }

            return csvContent;
        }
    }
}