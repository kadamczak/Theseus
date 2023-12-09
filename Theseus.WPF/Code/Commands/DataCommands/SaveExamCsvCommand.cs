using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.Commands.DataCommands
{
    public class SaveExamCsvCommand : AsyncCommandBase
    {
        private readonly SelectedModelDetailsStore<Exam> _selectedExamStore;
        private readonly IGetMazeOfExamStageQuery _getMazeQuery;

        public SaveExamCsvCommand(SelectedModelDetailsStore<Exam> selectedExamStore, IGetMazeOfExamStageQuery getMazeQuery)
        {
            _selectedExamStore = selectedExamStore;
            _getMazeQuery = getMazeQuery;
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
            string csvContent = "TimeBeforeInput,InputDirection,Correct\n";
            var sortedSteps = examStage.Steps.OrderBy(s => s.Index);
            var inputCorrectnessList = CreateInputCorrectnessList(examStage, sortedSteps);

            for(int i = 0; i < sortedSteps.Count(); i++)
            {
                ExamStep step = sortedSteps.ElementAt(i);
                csvContent += step.TimeBeforeStep.ToString(System.Globalization.CultureInfo.InvariantCulture) + ",";
                csvContent += step.StepTaken.ToString() + ",";
                csvContent += inputCorrectnessList[i].ToString() + "\n";
            }

            return csvContent;
        }

        public List<Cell> ConvertInputListToCellList(IEnumerable<ExamStep> inputs, MazeWithSolution maze)
        {
            Cell currentCell = maze.SolutionPath.First();
            List<Cell> patientInputPath = new List<Cell>() { currentCell };

            foreach (var input in inputs)
            {
                if (currentCell.IsLinkedToNeighbour(input.StepTaken))
                {
                    Cell nextCell = currentCell.AdjecentCellSpaces[input.StepTaken]!;
                    patientInputPath.Add(nextCell);
                    currentCell = nextCell;
                }
            }

            return patientInputPath;
        }

        public List<bool> CreateInputCorrectnessList(ExamStage examStage, IEnumerable<ExamStep> steps)
        {
            MazeWithSolution maze = _getMazeQuery.GetMaze(examStage.Id);
            var patientCellPath = ConvertInputListToCellList(steps, maze);

            int currentPatientCellPathIndex = 0;
            Cell currentPatientCell = patientCellPath.First();

            int currentCorrectCellIndex = 0;
            Cell currentCorrectCell = maze.SolutionPath.First();

            List<bool> inputCorrectnessList = new List<bool>();

            foreach (var step in steps)
            {
                bool wasAlongTheCorrectPath = false;

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

                inputCorrectnessList.Add(wasAlongTheCorrectPath);
            }

            return inputCorrectnessList;
        }
    }
}