using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.QueryInterfaces.MazeQueryInterfaces
{
    public interface IGetMazesWithSolutionOfStaffMemberQuery
    {
        IEnumerable<MazeWithSolution> GetMazesWithSolution(Guid staffMemberId);
    }
}