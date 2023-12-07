using System;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.Info;

namespace Theseus.WPF.Code.ViewModels.DataViewModels
{
    public class PatientsExamsSummaryViewModel : ViewModelBase
    {
        public PatientCommandListViewModel PatientCommandListViewModel { get; set; }

        public PatientsExamsSummaryViewModel(IGetPatientsOfStaffMemberQuery getPatientsQuery,
                                             ICurrentStaffMemberStore currentStaffMemberStore,
                                             SelectedModelListStore<Patient> patientListStore,
                                             PatientCommandListViewModelFactory patientCommandListFactory)
        {
            LoadPatientsOfStaffMemberToStore(getPatientsQuery, currentStaffMemberStore.StaffMember.Id, patientListStore);
            PatientCommandListViewModel = patientCommandListFactory.Create(PatientButtonCommand.None, PatientButtonCommand.None, PatientInfo.ExamInfo);
            PatientCommandListViewModel.CreateModelCommandViewModels();
        }

        private void LoadPatientsOfStaffMemberToStore(IGetPatientsOfStaffMemberQuery query,
                                                      Guid staffMemberId,
                                                      SelectedModelListStore<Patient> selectedPatientListStore)
        {
            var patients = query.GetPatients(staffMemberId);
            selectedPatientListStore.ModelList = patients;
        }
    }
}