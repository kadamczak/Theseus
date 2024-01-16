using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;

namespace Theseus.Domain.Services.ExamDataServices.Summary.ExamStats
{
    public class ExamCalculator
    {
        private readonly ScoreCalculator _scoreCalculator;
        private readonly IGetExamsOfPatientWithFullIncludeQuery _getExamsOfPatientQuery;
        public ExamCalculator(ScoreCalculator scoreCalculator,
                              IGetExamsOfPatientWithFullIncludeQuery getExamsOfPatientQuery)
        {
            _scoreCalculator = scoreCalculator;
            _getExamsOfPatientQuery = getExamsOfPatientQuery;
        }

        public ExamStats CalculateExamStats(Exam exam)
        {
            var examStages = exam.Stages;
            var examSteps = exam.Stages.SelectMany(s => s.Steps);

            return new ExamStats()
            {
                PatientId = exam.Patient.Id,
                ExamSetId = exam.ExamSet.Id,
                CreatedAt = exam.CreatedAt,
                Score = _scoreCalculator.CalculateScoreForExam(exam),
                AttemptNumber = CalculateAttemptNumber(exam),
                NoSkips = CalculateNoSkips(examStages),
                TotalExamTime = CalculateTotalExamTime(examStages),
                CompletedMazeAmount = CalculateCompletedMazeAmount(examStages),
                TotalInputs = CalculateTotalInputs(examSteps),
                RedundantInputs = CalculateRedundantInputs(examSteps),
                WallHits = CalculateWallHits(examSteps),
            };
        }

        private int CalculateAttemptNumber(Exam exam) => _getExamsOfPatientQuery.GetExams(exam.Patient.Id)
                                                                                .Where(e => e.ExamSet.Id == exam.ExamSet.Id)
                                                                                .Where(e => e.CreatedAt < exam.CreatedAt)
                                                                                .Count() + 1;

        private bool CalculateNoSkips(IEnumerable<ExamStage> examStages) => examStages.All(e => e.Completed);
        private float CalculateTotalExamTime(IEnumerable<ExamStage> examStages) => examStages.Sum(s => s.TotalTime);
        private int CalculateCompletedMazeAmount(IEnumerable<ExamStage> examStages) => examStages.Where(s => s.Completed).Count();
        private int CalculateTotalInputs(IEnumerable<ExamStep> examSteps) => examSteps.Count();
        private int CalculateRedundantInputs(IEnumerable<ExamStep> examSteps) => examSteps.Where(s => !s.Correct && !s.HitWall).Count();
        private int CalculateWallHits(IEnumerable<ExamStep> examSteps) => examSteps.Where(s => s.HitWall).Count();


        public AverageExamStats CalculateAverageStats(IEnumerable<Exam> exams)
        {
            var examsByOtherPatientsWithNoSkips = exams.Where(e => !e.Stages.Where(s => !s.Completed).Any());
            bool anyCompletedExamsByOtherPatient = examsByOtherPatientsWithNoSkips.Any();

            return new AverageExamStats()
            {
                TotalExamAmount = exams.Count(),
                ExamsWithNoSkipsAmount = examsByOtherPatientsWithNoSkips.Count(),
                AverageCompletedMazes = CalculateAverageCompletedMazes(exams),
                AverageTotalTime = anyCompletedExamsByOtherPatient ? CalculateAverageTotalTime(examsByOtherPatientsWithNoSkips) : null,
                AverageTotalInputs = anyCompletedExamsByOtherPatient ? CalculateAverageTotalInputs(examsByOtherPatientsWithNoSkips) : null,
                AverageReduntantInputs = anyCompletedExamsByOtherPatient ? CalculateAverageTotalReduntantInputs(examsByOtherPatientsWithNoSkips) : null,
                AverageWallHits = anyCompletedExamsByOtherPatient ? CalculateAverageTotalWallHits(examsByOtherPatientsWithNoSkips) : null,
            };
        }

        private float CalculateAverageCompletedMazes(IEnumerable<Exam> exams) => (float)exams.Average(e => e.Stages.Count(s => s.Completed));
        private float CalculateAverageTotalTime(IEnumerable<Exam> exams) => exams.Average(e => CalculateTotalExamTime(e.Stages));
        private float CalculateAverageTotalInputs(IEnumerable<Exam> exams) => (float)exams.Average(e => e.Stages.Sum(s => CalculateTotalInputs(s.Steps)));
        private float CalculateAverageTotalReduntantInputs(IEnumerable<Exam> exams) => (float)exams.Average(e => e.Stages.Sum(s => CalculateRedundantInputs(s.Steps)));
        private float CalculateAverageTotalWallHits(IEnumerable<Exam> exams) => (float)exams.Average(e => e.Stages.Sum(s => CalculateWallHits(s.Steps)));
    }
}