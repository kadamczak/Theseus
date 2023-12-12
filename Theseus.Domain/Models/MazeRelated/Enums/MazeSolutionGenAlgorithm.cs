namespace Theseus.Domain.Models.MazeRelated.Enums
{
    /// <summary>
    /// Represents available algorithms for generating a maze solution.
    /// </summary>
    public enum MazeSolutionGenAlgorithm
    {
        Dijkstra = 0,
        RandomBorderCells = 1
    }
}