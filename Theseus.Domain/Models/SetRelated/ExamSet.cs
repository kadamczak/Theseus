using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.SetRelated
{
    public class ExamSet
    {
        public Guid Id { get; set; } = default;
        public List<MazeWithSolution> MazesWithSolution { get; } = new List<MazeWithSolution>();

        public ExamSet(Guid id, List<MazeWithSolution> mazesWithSolution)
        {
            Id = id;
            MazesWithSolution = mazesWithSolution;
        }
    }
}
