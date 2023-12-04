using System.Collections.Generic;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.WPF.Code.Stores.Exams
{
    public class CurrentExamStore
    {
        public IEnumerable<MazeWithSolution> Mazes { get; set; }
        public int CurrentIndex { get; set; } = 0;
        public Exam CurrentExam { get; set; }
    }
}