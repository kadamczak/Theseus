using System;
using System.Collections.Generic;
using System.Linq;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Services.ExamDataServices.Summary.ExamStats;
using Theseus.Domain.Services.ExamDataServices.Summary.ExamSetGroup;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info.Implementations
{
    public class GeneralExamInfoGranter : InfoGranter<Exam>
    {
        private readonly ExamCalculator _examCalculator;
        private readonly ExamSetGroupStatsList _examSetGroupStatsList;
        private readonly DescriptiveValueComparer _valueComparer;

        public GeneralExamInfoGranter(DescriptiveValueComparer valueComparer,
                                      ExamCalculator examCalculator,
                                      ExamSetGroupStatsList examSetGroupStatsList)
        {
            _valueComparer = valueComparer;
            _examCalculator = examCalculator;
            _examSetGroupStatsList = examSetGroupStatsList;
        }

        public override string GrantInfo(CommandViewModel<Exam> commandViewModel)
        {
            Guid examSetId = commandViewModel.Model.ExamSet.Id;
            Guid groupId = commandViewModel.Model.Patient.Group.Id;

            ExamSetGroupStatSummary examSetStatSummary = _examSetGroupStatsList.ExamSetStatList.Where(e => e.GroupId == groupId && e.ExamSetId == examSetId).First();
            ExamStats currentExamStats = _examCalculator.CalculateExamStats(commandViewModel.Model);

            List<string> displayedInfoText = CreateBasicTextSummary(currentExamStats, examSetStatSummary);
            displayedInfoText.AddRange(CreateComparisonTextToOtherPatients(currentExamStats, examSetStatSummary));
            displayedInfoText.AddRange(CreateComparisonTextToPreviousAttempt(currentExamStats, examSetStatSummary));

            return string.Join("\n", displayedInfoText.ToArray());
        }


        private List<string> CreateBasicTextSummary(ExamStats currentExamStats, ExamSetGroupStatSummary examSetStatSummary)
        {
            return new List<string>
            {
               "Attempt#".Resource() + currentExamStats.AttemptNumber,
               "CompletedMazes:".Resource() + $"{currentExamStats.CompletedMazeAmount}/{examSetStatSummary.MazeAmount}",
               "TotalTime:".Resource() + $"{Round(currentExamStats.TotalExamTime)} s",
               "InputsMade:".Resource() + $"{currentExamStats.TotalInputs}/{examSetStatSummary.IdealStepAmount}",
               "RedundantInputs:".Resource() + $"{currentExamStats.RedundantInputs}",
               "WallHits:".Resource() + $"{currentExamStats.WallHits}",
               $"{"Score".Resource()}: {Math.Round(currentExamStats.Score, 2)}/100"
            };
        }

        private List<string> CreateComparisonTextToOtherPatients(ExamStats currentExamStats, ExamSetGroupStatSummary examSetStatSummary)
        {
            var examsByOtherPatients = examSetStatSummary.Exams
                                                         .Where(e => e.Patient.Id != currentExamStats.PatientId);

            return examsByOtherPatients.Any() ? CreateFullComparisonTextToOtherPatients(currentExamStats, _examCalculator.CalculateAverageStats(examsByOtherPatients)) :
                                                new List<string>() { "OtherPatientsInGroupDidNotCompleteThisExamSet".Resource() };
        }

        private List<string> CreateFullComparisonTextToOtherPatients(ExamStats currentExamStats, AverageExamStats otherPatientsStats)
        {
            string completedMazesComparison = _valueComparer.Compare(currentExamStats.CompletedMazeAmount, otherPatientsStats.AverageCompletedMazes, higherIsBetter: true);
            string averageCompletedMazesFormatted = Round(otherPatientsStats.AverageCompletedMazes);
            string formattedPercentNoSkips = Round((float) otherPatientsStats.ExamsWithNoSkipsAmount / otherPatientsStats.TotalExamAmount * 100f);

            List<string> comparisonText =  new List<string>() {
                    $"{"AmountOfAttemptsByOtherPatients:".Resource()}{otherPatientsStats.TotalExamAmount}",
                    $"{"AmountOfAttemptsWithNoSkipsByOtherPatients:".Resource()}{otherPatientsStats.ExamsWithNoSkipsAmount} ({formattedPercentNoSkips}%)",
                    "", "ComparisonToAverage:".Resource(),
                    $"\t{"CompletedMazes:".Resource()}{completedMazesComparison} ({"Avg".Resource()}: {averageCompletedMazesFormatted})"
            };

            if (currentExamStats.NoSkips && otherPatientsStats.ExamsWithNoSkipsAmount > 0)
                comparisonText.AddRange(CreateComparisonTextForNoSkipExams(currentExamStats,
                                                                           otherPatientsStats.AverageTotalTime.Value,
                                                                           otherPatientsStats.AverageTotalInputs.Value,
                                                                           otherPatientsStats.AverageReduntantInputs.Value,
                                                                           otherPatientsStats.AverageWallHits.Value,
                                                                           "Avg".Resource()));

            return comparisonText;
        }

        private string Round(float value) => Math.Round(value, 1).ToString();

        private IEnumerable<string> CreateComparisonTextToPreviousAttempt(ExamStats currentExamStats, ExamSetGroupStatSummary examSetStatSummary)
        {
            var previousAttempt = examSetStatSummary.Exams
                                                    .Where(e => e.Patient.Id == currentExamStats.PatientId)
                                                    .Where(e => e.CreatedAt < currentExamStats.CreatedAt)
                                                    .OrderByDescending(e => e.CreatedAt)
                                                    .FirstOrDefault();

            return (previousAttempt is not null) ? CreateFullComparisonTextToPreviousAttempt(currentExamStats, _examCalculator.CalculateExamStats(previousAttempt)) :
                                                   new List<string>() { "", "NoPreviousAttemptsByThisPatient".Resource() };
        }

        private List<string> CreateFullComparisonTextToPreviousAttempt(ExamStats currentExamStats, ExamStats previousExamStats)
        {
            string completedMazesComparison = _valueComparer.Compare(currentExamStats.CompletedMazeAmount, previousExamStats.CompletedMazeAmount, higherIsBetter: true);
            var comparisonText = new List<string>
            {
                "", $"{"ComparisonToPreviousAttemptOn:".Resource()}{previousExamStats.CreatedAt.ToString("dd/MM/yyyy HH:mm")}:",
                    $"\t{"CompletedMazes:".Resource()}{completedMazesComparison} ({"Prev".Resource()}: {previousExamStats.CompletedMazeAmount})",
            };

            if (currentExamStats.NoSkips && previousExamStats.NoSkips)
                comparisonText.AddRange(CreateComparisonTextForNoSkipExams(currentExamStats,
                                                                           previousExamStats.TotalExamTime,
                                                                           previousExamStats.TotalInputs,
                                                                           previousExamStats.RedundantInputs,
                                                                           previousExamStats.WallHits,
                                                                           "Prev".Resource()));

            return comparisonText;
               
        }

        private List<string> CreateComparisonTextForNoSkipExams(ExamStats currentExamStats, float time, float inputs, float redundantInputs, float wallHits, string valueType)
        {
            string timeComparison = _valueComparer.Compare(currentExamStats.TotalExamTime, time, higherIsBetter: false);
            string inputComparison = _valueComparer.Compare(currentExamStats.TotalInputs, inputs, higherIsBetter: false);
            string redundantInputsComparison = _valueComparer.Compare(currentExamStats.RedundantInputs, redundantInputs, higherIsBetter: false);
            string wallHitsComparison = _valueComparer.Compare(currentExamStats.WallHits, wallHits, higherIsBetter: false);

            string timeFormatted = Round(time);
            string inputsFormatted = Round(inputs);
            string redundantInputsFormatted = Round(redundantInputs);
            string wallHitsFormatted = Round(wallHits);

            return new List<string>
            {
                $"\t{"TotalTime:".Resource()}{timeComparison} ({valueType}: {timeFormatted} s)",
                $"\t{"InputsMade:".Resource()}{inputComparison} ({valueType}: {inputsFormatted})",
                $"\t{"RedundantInputs:".Resource()}{redundantInputsComparison} ({valueType}: {redundantInputsFormatted})",
                $"\t{"WallHits:".Resource()}{wallHitsComparison} ({valueType}: {wallHitsFormatted})",
            };
        }
    }
}