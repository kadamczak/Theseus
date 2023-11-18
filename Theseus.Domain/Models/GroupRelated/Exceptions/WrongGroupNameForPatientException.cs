namespace Theseus.Domain.Models.GroupRelated.Exceptions
{
    public class WrongGroupNameForPatientException : Exception
    {
        public string GroupName { get; set; }
        public string PatientName { get; set; }

        public WrongGroupNameForPatientException(string group, string patient) : base($"Patient {patient} does not belong to group {group}.")
        {
            GroupName = group;
            PatientName = patient;
        }

        public WrongGroupNameForPatientException(string message, string group, string patient) : base(message)
        {
            GroupName = group;
            PatientName = patient;
        }

        public WrongGroupNameForPatientException(string message, Exception innerException, string group, string patient) : base(message, innerException)
        {
            GroupName = group;
            PatientName = patient;
        }
    }
}
