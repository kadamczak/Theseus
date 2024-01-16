using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;

namespace Theseus.Domain.Services.ExamDataServices.Summary.ExamSetGroup
{
    public class ExamSetGroupStatCalculator
    {
        private readonly ExamSetGroupStatsList _examSetSGroupStatsList;
        private readonly IGetExamsOfGroupOfExamSetWithFullIncludeQuery _getExamsQuery;
        private readonly IGetOrderedMazesWithSolutionOfExamSetQuery _getMazesQuery;

        public ExamSetGroupStatCalculator(ExamSetGroupStatsList examSetStatsStore,
                                         IGetExamsOfGroupOfExamSetWithFullIncludeQuery getExamsQuery,
                                         IGetOrderedMazesWithSolutionOfExamSetQuery getMazesQuery)
        {
            _examSetSGroupStatsList = examSetStatsStore;
            _getExamsQuery = getExamsQuery;
            _getMazesQuery = getMazesQuery;
        }

        public void Calculate(IEnumerable<Exam> examList, bool calculateAverages = true)
        {
            _examSetSGroupStatsList.ExamSetStatList.Clear();

            foreach (var exam in examList)
            {
                Guid examSetId = exam.ExamSet.Id;
                Guid groupId = exam.Patient.Group.Id;

                if (!ExamSetStatsInThisGroupAlreadyCalculated(_examSetSGroupStatsList.ExamSetStatList, examSetId, groupId))
                {
                    ExamSetGroupStatSummary statSummary = CalculateSummaryForGroupForExamSet(examSetId, groupId, calculateAverages);
                    _examSetSGroupStatsList.ExamSetStatList.Add(statSummary);
                }
            }
        }

        public ExamSetGroupStatSummary CalculateSummaryForGroupForExamSet(Guid examSetId, Guid groupId, bool calculateAverages = true)
        {
            var mazesInExamSet = _getMazesQuery.GetMazesWithSolution(examSetId);

            var statSummary = new ExamSetGroupStatSummary
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
            statSummary.AverageCompletedMazes = (float)statSummary.Exams.Average(e => e.Stages.Count(s => s.Completed));

            if (examsWithNoSkippedMazes.Any())
            {
                statSummary.AverageTotalInputs = (float)examsWithNoSkippedMazes.Average(e => e.Stages.Sum(s => s.Steps.Count));
                statSummary.AverageTotalTime = (float)examsWithNoSkippedMazes.Average(e => e.Stages.Sum(s => s.TotalTime));
                statSummary.AverageTotalRedundantInputs = (float)examsWithNoSkippedMazes.Average(e => e.Stages.Sum(s => s.Steps.Where(s => !s.HitWall && !s.Correct).Count()));
                statSummary.AverageTotalWallHits = (float)examsWithNoSkippedMazes.Average(e => e.Stages.Sum(s => s.Steps.Where(s => s.HitWall).Count()));
            }

            return statSummary;
        }

        private bool ExamSetStatsInThisGroupAlreadyCalculated(List<ExamSetGroupStatSummary> examSetStatsList, Guid examSetId, Guid groupId)
            => examSetStatsList.Where(e => e.ExamSetId == examSetId && e.GroupId == groupId).Any();
    }
}