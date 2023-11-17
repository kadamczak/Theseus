using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.QueryInterfaces.MazeQueryInterfaces
{
    public interface IGetAllMazesWithSolutionOfStaffMemberQuery
    {
        IEnumerable<MazeWithSolution> GetAllMazesWithSolutionOfStaffMemberQuery(Guid staffMemberId, bool loadOwner = false, bool loadExamSets = false);
    }
}