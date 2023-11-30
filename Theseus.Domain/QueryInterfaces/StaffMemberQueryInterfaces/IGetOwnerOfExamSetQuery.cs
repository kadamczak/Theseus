using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces
{
    public interface IGetOwnerOfExamSetQuery
    {
        StaffMember GetOwner(Guid examSetId);
        Task<StaffMember> GetOwnerAsync(Guid examSetId);
    }
}