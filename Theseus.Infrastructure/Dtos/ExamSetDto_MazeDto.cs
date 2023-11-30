using System.ComponentModel.DataAnnotations;

namespace Theseus.Infrastructure.Dtos
{
    public class ExamSetDto_MazeDto
    {
        [Key]
        public Guid Id { get; set; }
        public ExamSetDto ExamSetDto { get; set; }
        public MazeDto MazeDto { get; set; }
        public int Index { get; set; }
    }
}