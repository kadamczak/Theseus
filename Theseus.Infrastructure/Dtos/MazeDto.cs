using System.ComponentModel.DataAnnotations;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Infrastructure.Dtos
{
    /// <summary>
    /// Class representing <c>MazeWithSolution</c> structure as a database entry.
    /// </summary>
    public class MazeDto
    {
        [Key]
        public Guid? Id { get; set; } = default;

        public int Height { get; set; } = default!;
        public int Width { get; set; } = default!;
        public byte[] Structure { get; set; } = default!;

        public int SolutionStartRow { get; set; } = default!;
        public int SolutionStartColumn { get; set; } = default!;
        public byte[] Solution { get; set; } = default!;
        public byte StartDirection { get; set; } = default!;
        public byte EndDirection { get; set; } = default!;

        public virtual StaffMemberDto Owner { get; set; } = default!;

        public virtual ICollection<ExamSetDto_MazeDto> ExamSetDto_MazeDto { get; set; } = default!;

        public MazeDto(MazeWithSolution maze, byte[] structureAsBytes, byte[] solutionAsBytes)
        {
            Id = maze.Id;
            Height = maze.Grid.RowAmount;
            Width = maze.Grid.ColumnAmount;
            SolutionStartRow = maze.SolutionPath[0].RowIndex;
            SolutionStartColumn = maze.SolutionPath[0].ColumnIndex;
            Structure = structureAsBytes;
            Solution = solutionAsBytes;
            StartDirection = (byte)maze.StartDirection;
            EndDirection = (byte)maze.EndDirection;
        }

        public MazeDto()
        {
        }
    }
}