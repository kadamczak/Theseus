using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.Exams;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels;

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
                services.AddSingleton<MazeReturnServiceStore>();

                services.AddSingleton<ExamSetReturnServiceStore>();

                services.AddSingleton<CurrentExamStore>();
                services.AddSingleton<ExamSetsInGroupStore>();

                services.AddSingleton<SelectedModelListStore<MazeWithSolutionCanvasViewModel>>();
                services.AddSingleton<SelectedModelListStore<ExamSet>>();
                services.AddSingleton<SelectedModelListStore<Patient>>();
                services.AddSingleton<SelectedModelListStore<StaffMember>>();
                services.AddSingleton<SelectedModelListStore<Group>>();

                services.AddSingleton<SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel>>();
                services.AddSingleton<SelectedModelDetailsStore<ExamSet>>();
                services.AddSingleton<SelectedModelDetailsStore<Patient>>();
                services.AddSingleton<SelectedModelDetailsStore<StaffMember>>();
                services.AddSingleton<SelectedModelDetailsStore<Group>>();

                services.AddSingleton<ICurrentStaffMemberStore, CurrentStaffMemberStore>();
                services.AddSingleton<ICurrentPatientStore, CurrentPatientStore>();

                services.AddSingleton<IStaffMemberAuthenticator, StaffMemberAuthenticator>();
                services.AddSingleton<IPatientAuthenticator, PatientAuthenticator>();
            });

            return hostBuilder;
        }
    }
}