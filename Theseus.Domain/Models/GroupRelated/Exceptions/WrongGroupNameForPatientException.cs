namespace Theseus.Domain.Models.GroupRelated.Exceptions
{
    /// <summary>
    /// Exception thrown when the user enters a <c>Group</c> name that does not correspond to the specified <c>Patient</c>.
    /// </summary>
    public class WrongGroupNameForPatientException : Exception
    {
        /// <summary>
        /// Gets or sets the name of the <c>Group</c>.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the <c>Patient</c>.
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// Initialize with a <c>Group</c> name and a <c>Patient</c> name.
        /// </summary>
        /// <param name="group"><c>Group</c> name.</param>
        /// <param name="patient"><c>Patient</c> name.</param>
        public WrongGroupNameForPatientException(string group, string patient) : base($"Patient {patient} does not belong to group {group}.")
        {
            GroupName = group;
            PatientName = patient;
        }

        /// <summary>
        /// Initialize with a <c>Group</c> name, a <c>Patient</c> name and a custom message.
        /// </summary>
        /// <param name="message">Custom message.</param>
        /// <param name="group"><c>Group</c> name.</param>
        /// <param name="patient"><c>Patient</c> name.</param>
        public WrongGroupNameForPatientException(string message, string group, string patient) : base(message)
        {
            GroupName = group;
            PatientName = patient;
        }

        /// <summary>
        /// Initialize with a <c>Group</c> name, a <c>Patient</c> name, a custom message and an inner exception.
        /// </summary>
        /// <param name="message">Custom message.</param>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="group"><c>Group</c> name.</param>
        /// <param name="patient"><c>Patient</c> name.</param>
        public WrongGroupNameForPatientException(string message, Exception innerException, string group, string patient) : base(message, innerException)
        {
            GroupName = group;
            PatientName = patient;
        }
    }
}
