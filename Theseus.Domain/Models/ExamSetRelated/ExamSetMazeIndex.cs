using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.ExamSetRelated
{
    public class ExamSetMazeIndex
    {
        public Guid Id { get; set; }
        public ExamSet ExamSet { get; set; }
        public MazeWithSolution MazeWithSolution { get; set; }
        public int Index { get; set; }

        public ExamSetMazeIndex()
        {

        }

        public ExamSetMazeIndex(Guid id, ExamSet examSet, MazeWithSolution mazeWithSolution, int index)
        {
            Id = id;
            ExamSet = examSet;
            MazeWithSolution = mazeWithSolution;
            Index = index;
        }
    }
}