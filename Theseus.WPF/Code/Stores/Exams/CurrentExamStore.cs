using System.Collections.Generic;
using System.Diagnostics;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.WPF.Code.Stores.Exams
{
    public class CurrentExamStore
    {
        public IEnumerable<MazeWithSolution> Mazes { get; set; }
        public int CurrentIndex { get; set; } = 0;
        public Exam CurrentExam { get; set; }
        public Stopwatch TimeSinceLastStep { get; set; } = new Stopwatch();
        public Stopwatch TimeSinceBeginningOfStage { get; set; } = new Stopwatch();
    }
}