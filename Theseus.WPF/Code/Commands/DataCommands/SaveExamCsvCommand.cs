using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.Commands.DataCommands
{
    public class SaveExamCsvCommand : AsyncCommandBase
    {
        private readonly SelectedModelDetailsStore<Exam> _selectedExamStore;
        private readonly IGetMazeOfExamStageQuery _getMazeQuery;
        private readonly InputListToTimedCellPathConverter _inputConverter;

        public SaveExamCsvCommand(SelectedModelDetailsStore<Exam> selectedExamStore,
                                  IGetMazeOfExamStageQuery getMazeQuery,
                                  InputListToTimedCellPathConverter inputConverter)
        {
            _selectedExamStore = selectedExamStore;
            _getMazeQuery = getMazeQuery;
            _inputConverter = inputConverter;
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
            var inputCorrectnessList = CreateInputCorrectnessList(examStage, sortedSteps);

            for(int i = 0; i < sortedSteps.Count(); i++)
            {
                ExamStep step = sortedSteps.ElementAt(i);
                csvContent += step.TimeBeforeStep.ToString(System.Globalization.CultureInfo.InvariantCulture) + ",";
                csvContent += step.StepTaken.ToString() + ",";
                csvContent += inputCorrectnessList[i].Correct.ToString() + ",";
                csvContent += inputCorrectnessList[i].HitWall.ToString() + "\n";
            }

            return csvContent;
        }

        public record StepAttributes(bool Correct, bool HitWall);

        public List<StepAttributes> CreateInputCorrectnessList(ExamStage examStage, IEnumerable<ExamStep> steps)
        {
            MazeWithSolution maze = _getMazeQuery.GetMaze(examStage.Id);
            var patientCellPath = _inputConverter.ConvertInputListToTimedCellList(steps, maze).Select(e => e.Cell).ToList();

            int currentPatientCellPathIndex = 0;
            Cell currentPatientCell = patientCellPath.First();

            int currentCorrectCellIndex = 0;
            Cell currentCorrectCell = maze.SolutionPath.First();

            List<StepAttributes> inputCorrectnessList = new List<StepAttributes>();

            foreach (var step in steps)
            {
                bool wasAlongTheCorrectPath = false;
                bool hitWall = false;

                if (steps.Last() == step && examStage.Completed)
                {
                    wasAlongTheCorrectPath = true;
                }
                else if (currentPatientCell.IsLinkedToNeighbour(step.StepTaken))
                {
                    if (currentPatientCell == currentCorrectCell)
                    {
                        currentCorrectCellIndex++;
                        currentCorrectCell = maze.SolutionPath[currentCorrectCellIndex];
                        wasAlongTheCorrectPath = true;
                    }
                    currentPatientCellPathIndex++;
                    currentPatientCell = patientCellPath[currentPatientCellPathIndex];
                }
                else
                {
                    hitWall = true;
                }

                inputCorrectnessList.Add(new StepAttributes(wasAlongTheCorrectPath, hitWall));
            }

            return inputCorrectnessList;
        }
    }
}