namespace Theseus.Domain.Models.MazeRelated.Enums
{
    /// <summary>
    /// Represents available algorithms for generating a maze structure.
    /// </summary>
    public enum MazeStructureGenAlgorithm
    {
        AldousBroder = 0,
        HuntAndKill = 1,
        Kruskal = 2,
        Binary = 4,
        Sidewinder = 3,
    }
}