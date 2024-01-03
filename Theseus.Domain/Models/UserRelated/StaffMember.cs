using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.GroupRelated;
using System.ComponentModel.DataAnnotations;

namespace Theseus.Domain.Models.UserRelated
{
    /// <summary>
    /// The <c>StaffMember</c> class represents the account data of a staff member.
    /// </summary>
    public class StaffMember
    {
        /// <summary>
        /// Gets or sets <c>StaffMember</c> ID.
        /// </summary>
        public Guid Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the username of the account.
        /// </summary>
        /// <remarks>
        /// The username is unique across the entire <c>StaffMember</c> database.
        /// </remarks>
        [Required]
        [StringLength(16)]
        [RegularExpression(@"^[\w_]+$")]
        public string Username { get; set; } = default!;

        /// <summary>
        /// Gets or sets the hashed password for this account.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; } = default!;

        /// <summary>
        /// Gets or sets the e-mail of <c>StaffMember</c> account owner.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        /// <summary>
        /// Gets or sets first name of <c>StaffMember</c> account owner.
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Name { get; set; } = default!;

        /// <summary>
        /// Gets or sets surname of <c>StaffMember</c> account owner.
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Surname { get; set; } = default!;

        /// <summary>
        /// Gets or sets the <c>DateTime</c> when the account was created.
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the list of <c>Group</c>s that this <c>StaffMember</c> is the owner of.
        /// </summary>
        public List<Group> OwnedGroups { get; set; } = new List<Group>();

        /// <summary>
        /// Gets or sets the list of <c>Group</c>s that this <c>StaffMember</c> is a member of.
        /// </summary>
        /// <remarks>
        /// <c>Group</c>s that this <c>StaffMember</c> owns are included in this list.
        /// </remarks>
        public List<Group> Groups { get; set; } = new List<Group>();

        /// <summary>
        /// Gets or sets a list of <c>MazeWithSolution</c>s that this <c>StaffMember</c> created.
        /// </summary>
        public List<MazeWithSolution> MazesWithSolutions { get; set; } = new List<MazeWithSolution>();

        /// <summary>
        /// Gets or sets a list of <c>ExamSet</c>s that this <c>StaffMember</c> created.
        /// </summary>
        public List<ExamSet> ExamSets { get; set; } = new List<ExamSet>();
    }
}