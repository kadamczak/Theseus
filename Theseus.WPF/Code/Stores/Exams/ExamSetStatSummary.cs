using System;
using System.Collections.Generic;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.UserRelated.Enums;

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

        public Dictionary<AgeGroup, float?> AverageTotalTimeByAge { get; set; } = new Dictionary<AgeGroup, float?>();
        public Dictionary<Sex, float?> AverageTotalTimeBySex { get; set; } = new Dictionary<Sex, float?>();
        public Dictionary<EducationLevel, float?> AverageTotalTimeByEducationLevel { get; set; } = new Dictionary<EducationLevel, float?>();
        public Dictionary<ProfessionType, float?> AverageTotalTimeByProfessionType { get; set; } = new Dictionary<ProfessionType, float?>();

        public Dictionary<AgeGroup, float?> AverageTotalInputsByAge { get; set; } = new Dictionary<AgeGroup, float?>();
        public Dictionary<Sex, float?> AverageTotalInputsBySex { get; set; } = new Dictionary<Sex, float?>();
        public Dictionary<EducationLevel, float?> AverageTotalInputsByEducationLevel { get; set; } = new Dictionary<EducationLevel, float?>();
        public Dictionary<ProfessionType, float?> AverageTotalInputsByProfessionType { get; set; } = new Dictionary<ProfessionType, float?>();

        public Dictionary<AgeGroup, float?> AverageCompletedMazesByAge { get; set; } = new Dictionary<AgeGroup, float?>();
        public Dictionary<Sex, float?> AverageCompletedMazesBySex { get; set; } = new Dictionary<Sex, float?>();
        public Dictionary<EducationLevel, float?> AverageCompletedMazesByEducationLevel { get; set; } = new Dictionary<EducationLevel, float?>();
        public Dictionary<ProfessionType, float?> AverageCompletedMazesByProfessionType { get; set; } = new Dictionary<ProfessionType, float?>();
    }
}