using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.Services.ExamDataServices
{
    /// <summary>
    /// The <c>ExamCsvWriter</c> creates one .csv file for each <c>ExamStage</c> included in an <c>Exam</c>.
    /// </summary>
    public class ExamCsvWriter
    {
        public void WriteCsvFiles(string folderPath, Exam exam)
        {
            var sortedStages = exam.Stages.OrderBy(s => s.Index);
            string fileNameBeginning = CreateFileNamePrefix(exam);

            foreach (var examStage in sortedStages)
            {
                string completed = examStage.Completed ? "Finished" : "Unfinished";
                string fileName = folderPath + Path.DirectorySeparatorChar + fileNameBeginning + examStage.Index + "_" + completed + ".csv";

                string content = CreateCsvForExamStage(examStage);
                File.WriteAllText(fileName, content);
            }
        }

        private string CreateFileNamePrefix(Exam exam)
        {
            string patientUsername = exam.Patient.Username;
            string examSetName = exam.ExamSet.Name;
            string patientGroup = exam.Patient.Group.Name;
            string examDate = exam.CreatedAt.ToString("yyyy-MM-dd-HH-mm");

            return $"{patientUsername}_{examSetName}_{patientGroup}_{examDate}_";
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