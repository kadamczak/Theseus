using System.ComponentModel.DataAnnotations;

namespace Theseus.Infrastructure.Dtos
{
    /// <summary>
    /// Class representing <c>ExamSetMazeIndex</c> structure as a database entry.
    /// </summary>
    public class ExamSetDto_MazeDto
    {
        [Key]
        public Guid Id { get; set; }
        public ExamSetDto ExamSetDto { get; set; }
        public MazeDto MazeDto { get; set; }
        public int Index { get; set; }
    }
}