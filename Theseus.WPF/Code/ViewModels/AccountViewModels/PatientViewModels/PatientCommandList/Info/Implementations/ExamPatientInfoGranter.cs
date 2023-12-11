using System;
using System.Linq;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.Info.Implementations
{
    public class ExamPatientInfoGranter : InfoGranter<Patient>
    {
        private readonly IGetExamsOfPatientQuery _getExamsQuery;

        public ExamPatientInfoGranter(IGetExamsOfPatientQuery getExamsQuery)
        {
            _getExamsQuery = getExamsQuery;
        }

        public override string GrantInfo(CommandViewModel<Patient> commandViewModel)
        {
            Guid patientId = commandViewModel.Model.Id;
            var exams = _getExamsQuery.GetExams(patientId);
            string examAmountInfo = "AmountOfExamAttempts:".Resource() + exams.Count();
            string latestExamDateInfo = string.Empty;

            if (exams.Any())
            {
                DateTime latestExamDate = exams.OrderByDescending(e => e.CreatedAt).First().CreatedAt;
                latestExamDateInfo = "\n" + "LatestExam:".Resource() + latestExamDate.ToString("dd/MM/yyyy HH:mm");
            }

            return examAmountInfo + latestExamDateInfo;
        }
    }
}