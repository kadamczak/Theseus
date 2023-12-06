using System;
using System.Collections.Generic;
using System.Linq;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Exams;

namespace Theseus.WPF.Code.Services
{
    public class ExamSetStatCalculator
    {
        private readonly ExamSetStatsStore _examSetStatsStore;
        private readonly SelectedModelListStore<Exam> _selectedExamListStore;

        private readonly IGetExamsOfGroupOfExamSetQuery _getExamsQuery;
        private readonly IGetOrderedMazesWithSolutionOfExamSetQuery _getMazesQuery;

        public ExamSetStatCalculator(ExamSetStatsStore examSetStatsStore,
                                     SelectedModelListStore<Exam> selectedExamListStore,
                                     IGetExamsOfGroupOfExamSetQuery getExamsQuery,
                                     IGetOrderedMazesWithSolutionOfExamSetQuery getMazesQuery)
        {
            _examSetStatsStore = examSetStatsStore;
            _selectedExamListStore = selectedExamListStore;
            _getExamsQuery = getExamsQuery;
            _getMazesQuery = getMazesQuery;
        }

        public void Calculate(bool calculateAverages = true)
        {
            _examSetStatsStore.ExamSetStatList.Clear();

            foreach (var exam in _selectedExamListStore.ModelList)
            {
                Guid examSetId = exam.ExamSet.Id;
                Guid groupId = exam.Patient.Group.Id;

                if (!ExamSetStatsInThisGroupAlreadyCalculated(_examSetStatsStore.ExamSetStatList, examSetId, groupId))
                {
                    ExamSetStatSummary statSummary = CalculateSummaryForGroupForExamSet(examSetId, groupId, calculateAverages);
                    _examSetStatsStore.ExamSetStatList.Add(statSummary);
                }
            }
        }

        public ExamSetStatSummary CalculateSummaryForGroupForExamSet(Guid examSetId, Guid groupId, bool calculateAverages = true)
        {
            var mazesInExamSet = _getMazesQuery.GetMazesWithSolution(examSetId);

            ExamSetStatSummary statSummary = new ExamSetStatSummary
            {
                ExamSetId = examSetId,
                GroupId = groupId,
                MazeAmount = mazesInExamSet.Count(),
                IdealStepAmount = mazesInExamSet.Sum(m => m.SolutionPath.Count),
                Exams = _getExamsQuery.GetExams(groupId, examSetId)
            };

            if (!calculateAverages)
                return statSummary;

            var examsWithNoSkippedMazes = statSummary.Exams.Where(s => !s.Stages.Where(s => !s.Completed).Any());
            statSummary.AverageCompletedMazes = (float) statSummary.Exams.Average(e => e.Stages.Count(s => s.Completed));

            if (examsWithNoSkippedMazes.Any())
            {
                statSummary.AverageTotalInputs = (float) examsWithNoSkippedMazes.Average(e => e.Stages.Sum(s => s.Steps.Count));
                statSummary.AverageTotalTime = (float) examsWithNoSkippedMazes.Average(e => e.Stages.Sum(s => s.Steps.Sum(s => s.TimeBeforeStep)));
            }

            return statSummary;
        }

        private bool ExamSetStatsInThisGroupAlreadyCalculated(List<ExamSetStatSummary> examSetStatsList, Guid examSetId, Guid groupId)
            => examSetStatsList.Where(e => e.ExamSetId == examSetId && e.GroupId == groupId).Any();      
    }
}