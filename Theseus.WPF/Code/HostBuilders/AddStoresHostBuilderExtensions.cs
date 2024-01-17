using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Services.ExamDataServices.Summary.ExamSetGroup;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.Exams;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings.ExamBindings;

namespace Theseus.WPF.Code.HostBuilders
{
    /// <summary>
    /// The <c>AddStoresHostBuilderExtensions</c> class registers stores as singleton services.
    /// </summary>
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<NavigationEnabledStore>();

                services.AddSingleton<LastMazeGeneratorInputStore>();
                services.AddSingleton<MazeReturnServiceStore>();
                services.AddSingleton<MazesInExamSetStore>();

                services.AddSingleton<ExamSetReturnServiceStore>();
                services.AddSingleton<ExamSetsInGroupStore>();

                services.AddSingleton<CurrentExamStore>();

                services.AddSingleton<ExamSetGroupStatsList>();

                services.AddSingleton<SelectedModelListStore<MazeWithSolutionCanvasViewModel>>();
                services.AddSingleton<SelectedModelListStore<ExamSet>>();
                services.AddSingleton<SelectedModelListStore<Patient>>();
                services.AddSingleton<SelectedModelListStore<StaffMember>>();
                services.AddSingleton<SelectedModelListStore<Group>>();
                services.AddSingleton<SelectedModelListStore<Exam>>();
                services.AddSingleton<SelectedModelListStore<ExamStageWithMazeViewModel>>();

                services.AddSingleton<SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel>>();
                services.AddSingleton<SelectedModelDetailsStore<ExamSet>>();
                services.AddSingleton<SelectedModelDetailsStore<Patient>>();
                services.AddSingleton<SelectedModelDetailsStore<StaffMember>>();
                services.AddSingleton<SelectedModelDetailsStore<Group>>();
                services.AddSingleton<SelectedModelDetailsStore<Exam>>();
                services.AddSingleton<SelectedModelDetailsStore<ExamStageWithMazeViewModel>>();

                services.AddSingleton<ICurrentStaffMemberStore, CurrentStaffMemberStore>();
                services.AddSingleton<ICurrentPatientStore, CurrentPatientStore>();

                services.AddSingleton<IStaffMemberAuthenticator, StaffMemberAuthenticator>();
                services.AddSingleton<IPatientAuthenticator, PatientAuthenticator>();
            });

            return hostBuilder;
        }
    }
}