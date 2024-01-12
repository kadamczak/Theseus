using System.ComponentModel.DataAnnotations;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Models.GroupRelated
{
    /// <summary>
    /// The <c>Group</c> class represents a collection of <c>Patient</c>s that have access to a specific list of <c>ExamSet</c>s
    /// and whose <c>Exam</c> results can be seen by a specific list of <c>StaffMember</c>s.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Gets or sets <c>Group</c> ID.
        /// </summary>
        public Guid Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets a displayable name of <c>Group</c>.
        /// </summary>
        [Required]
        [StringLength(16)]
        [RegularExpression(@"^[\w_-]+$")]
        public string Name { get; set; } = default!;

        /// <summary>
        /// Gets or sets the owner of the <c>Group</c>.
        /// <remarks>
        /// Only the owner can add/remove other <c>StaffMember</c>s.
        /// </remarks>
        /// </summary>
        public StaffMember Owner { get; set; } = default!;

        /// <summary>
        /// Gets or sets a list of <c>StaffMember</c>s who are members of the <c>Group</c>.
        /// </summary>
        /// <remarks>
        /// The <see cref="Owner"/> is included in this list.
        /// A single <c>StaffMember</c> can be members of multiple <c>Group</c>s.
        /// </remarks>
        public List<StaffMember> StaffMembers { get; set; } = new List<StaffMember>();

        /// <summary>
        /// Gets or sets a list of <c>Patient</c>s who are members of the <c>Group</c>.
        /// </summary>
        /// <remarks>
        /// A <c>Patient</c> can be a member of only one <c>Group</c>.
        /// </remarks>
        public List<Patient> Patients { get; set; } = new List<Patient>();

        /// <summary>
        /// Gets or sets a list of <c>ExamSet</c>s available to <c>Patient</c>s that are a member of this <c>Group</c>.
        /// </summary>
        public List<ExamSet> ExamSets { get; set; } = new List<ExamSet>();
    }
}