namespace Theseus.Domain.Models.ExamSetRelated.Enums
{
    /// <summary>
    /// Represents the difficulty (which correlates with target <c>Patient</c> group) of an <c>ExamSet</c>.
    /// </summary>
    public enum ExamSetDifficulty
    {
        /// <summary>
        /// For <c>Patient</c>s that have visible problems with solving mazes.
        /// </summary>
        Easy,
        /// <summary>
        /// For the general <c>Patient</c> group.
        /// </summary>
        Standard,
        /// <summary>
        /// For <c>Patient</c>s that are skilled and need to be challenged more.
        /// </summary>
        Difficult,
        /// <summary>
        /// Unspecified, customizable difficulty.
        /// </summary>
        Custom
    }
}