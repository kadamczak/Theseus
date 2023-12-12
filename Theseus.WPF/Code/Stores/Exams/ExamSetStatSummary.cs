using System;
using System.Collections.Generic;
using Theseus.Domain.Models.ExamRelated;

namespace Theseus.WPF.Code.Stores.Exams
{
    public class ExamSetStatSummary
    {
        public Guid ExamSetId { get; set; }
        public Guid GroupId { get; set; }

        public IEnumerable<Exam> Exams { get; set; } = new List<Exam>();

        public int MazeAmount { get; set; }
        public int IdealStepAmount { get; set; }

        public float? AverageTotalTime { get; set; }
        public float? AverageTotalInputs { get; set; }
        public float? AverageCompletedMazes { get; set; }
    }
}