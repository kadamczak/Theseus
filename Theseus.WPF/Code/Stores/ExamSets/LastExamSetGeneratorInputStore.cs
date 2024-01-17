using Theseus.Domain.Models.ExamSetRelated.Enums;

namespace Theseus.WPF.Code.Stores.ExamSets
{
    /// <summary>
    /// The <c>LastExamSetGeneratorInputStore</c> class stores last user settings in <c>ExamSet</c> generator.
    /// </summary>
    public class LastExamSetGeneratorInputStore
    {
        public ExamSetDifficulty SelectedDifficulty { get; set; } = ExamSetDifficulty.Standard;
        public string MazeAmount { get; set; } = "7";
        public string BeginningMaxMazeDimension { get; set; } = "8";
        public string EndingMaxMazeDimension { get; set; } = "23";
    }
}