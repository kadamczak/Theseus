using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.QueryInterfaces.MazeQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>MazeWithSolution</c>s belonging to a specific <c>StaffMember</c>.
    /// </summary>
    public interface IGetMazesWithSolutionOfStaffMemberQuery
    {
        IEnumerable<MazeWithSolution> GetMazesWithSolution(Guid staffMemberId);
    }
}