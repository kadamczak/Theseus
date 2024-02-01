namespace Theseus.Domain.Models.MazeRelated.Enums
{
    /// <summary>
    /// Represents available algorithms for generating a maze structure.
    /// </summary>
    public enum MazeStructureGenAlgorithm
    {
        AldousBroder = 0,
        HuntAndKill = 1,
        TruePrim = 2,
        Kruskal = 3,
        Sidewinder = 4,
        Binary = 5
    }
}