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
        TruePrim = 3,
        Binary = 5,
        Sidewinder = 4,
    }
}