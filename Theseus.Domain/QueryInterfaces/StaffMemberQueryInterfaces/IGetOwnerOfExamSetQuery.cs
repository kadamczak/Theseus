using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>StaffMember</c> that is the owner of an <c>ExamSet</c>.
    /// </summary>
    public interface IGetOwnerOfExamSetQuery
    {
        StaffMember GetOwner(Guid examSetId);
        Task<StaffMember> GetOwnerAsync(Guid examSetId);
    }
}