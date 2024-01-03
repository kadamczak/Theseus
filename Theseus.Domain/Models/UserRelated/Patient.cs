using System.ComponentModel.DataAnnotations;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.UserRelated.Enums;

namespace Theseus.Domain.Models.UserRelated
{
    /// <summary>
    /// The <c>Patient</c> class represents the account data of a patient.
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Gets or sets <c>Patient</c> ID.
        /// </summary>
        public Guid Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the username of the account. Throws ArgumentException if username is empty/whitespace, is too long or contains
        /// invalid characters.
        /// </summary>
        /// <remarks>
        /// The username is unique across the entire <c>Patient</c> database.
        /// </remarks>
        [Required]
        [StringLength(16)]
        [RegularExpression(@"^[\w_]+$")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the age of the <c>Patient</c>.
        /// </summary>
        /// <remarks>
        /// Age does not have to be specified. It is represented as a null in that case.
        /// </remarks>
        [Range(0, 125)]
        public int? Age { get; set; }

        /// <summary>
        /// Gets or sets the sex of the <c>Patient</c>.
        /// </summary>
        /// <remarks>
        /// The value of the <c>Sex</c> enum can be set to "Undisclosed".
        /// </remarks>
        public Sex Sex { get; set; }

        /// <summary>
        /// Gets or sets the profession type of the <c>Patient</c>.
        /// </summary>
        /// <remarks>
        /// The value of the <c>ProfessionType</c> enum can be set to "Undisclosed".
        /// </remarks>
        public ProfessionType ProfessionType { get; set; }

        /// <summary>
        /// Gets or sets the education level of the <c>Patient</c>.
        /// </summary>
        /// <remarks>
        /// The value of the <c>EducationLevel</c> enum can be set to "Undisclosed".
        /// </remarks>
        public EducationLevel EducationLevel { get; set; }

        /// <summary>
        /// Gets or sets the <c>DateTime</c> when the account was created.
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the <c>Group</c> that the <c>Patient</c> belongs to.
        /// </summary>
        /// <remarks>
        /// The <c>Patient</c> can be without a group. The value is null in that case.
        /// </remarks>
        public GroupRelated.Group? Group { get; set; } = default!;

        /// <summary>
        /// Gets or sets the <c>Exam</c>s completed by this <c>Patient</c>.
        /// </summary>
        public List<Exam> Exams { get; set; } = new List<Exam>();
    }
}