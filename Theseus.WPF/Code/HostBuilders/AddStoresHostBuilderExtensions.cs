using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.Stores.Groups;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.Stores.Patients;
using Theseus.WPF.Code.Stores.StaffMembers;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<NavigationStore>();

                services.AddSingleton<LastMazeGeneratorInputStore>();
                services.AddSingleton<SelectedMazeListStore>();
                services.AddSingleton<SelectedMazeDetailsStore>();

                services.AddSingleton<SelectedExamSetDetailsStore>();
                services.AddSingleton<SelectedExamSetListStore>();

                services.AddSingleton<SelectedGroupDetailsStore>();
                services.AddSingleton<SelectedGroupListStore>();

                services.AddSingleton<SelectedPatientDetailsStore>();
                services.AddSingleton<SelectedPatientListStore>();

                services.AddSingleton<SelectedStaffMemberDetailsStore>();
                services.AddSingleton<SelectedStaffMemberListStore>();

                services.AddSingleton<ICurrentStaffMemberStore, CurrentStaffMemberStore>();
                services.AddSingleton<ICurrentPatientStore, CurrentPatientStore>();

                services.AddSingleton<IStaffMemberAuthenticator, StaffMemberAuthenticator>();
                services.AddSingleton<IPatientAuthenticator, PatientAuthenticator>();
            });

            return hostBuilder;
        }
    }
}