using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings;
using Theseus.WPF.Code.ViewModels.SetViewModels;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                AddTransientViewModels(services);
                AddSingletonViewModels(services);
                AddViewModelFactories(services);
                AddNavigationServices(services);
            });

            return hostBuilder;
        }

        private static void AddTransientViewModels(IServiceCollection services)
        {
            services.AddTransient<BeginTestViewModel>();
            services.AddTransient<ViewDataViewModel>();
            services.AddTransient<CreateMazeViewModel>();
            services.AddTransient<CreateSetViewModel>();
            services.AddTransient<BrowseMazesViewModel>();
            services.AddTransient<BrowseSetsViewModel>();

            services.AddTransient<HomeViewModel>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<AccountViewModel>();

            services.AddTransient<LoggedInViewModel>();
            services.AddTransient<NotLoggedInViewModel>();

            services.AddTransient<PatientDetailsLoggedInViewModel>();
            services.AddTransient<PatientDetailsNotLoggedInViewModel>();

            services.AddTransient<PatientLoginRegisterViewModel>();
            services.AddTransient<PatientLoginViewModel>();
            services.AddTransient<PatientRegisterViewModel>();

            services.AddTransient<StaffMemberDetailsLoggedInViewModel>();
            services.AddTransient<StaffMemberDetailsNotLoggedInViewModel>();

            services.AddTransient<StaffMemberLoginRegisterViewModel>();
            services.AddTransient<StaffMemberLoginViewModel>();
            services.AddTransient<StaffMemberRegisterViewModel>();

            services.AddTransient<ShowDetailsMazeCommandListViewModel>();
            services.AddTransient<AddToSetMazeCommandListViewModel>();
            services.AddTransient<MazeDetailsViewModel>();
            services.AddTransient<MazeGeneratorViewModel>();
            services.AddTransient<MinimalCellSizeSetterViewModel>();

            services.AddTransient<SetGeneratorViewModel>();
            services.AddTransient<CreateSetManuallyViewModel>();
            services.AddTransient<ExamSetDetailsViewModel>();
            services.AddTransient<ExamSetCommandViewModel>();
            services.AddTransient<ShowDetailsExamSetCommandListViewModel>();
        }

        private static void AddSingletonViewModels(IServiceCollection services)
        {
            services.AddSingleton<NavigationBarViewModel>();
            services.AddSingleton<MainViewModel>();
        }

        private static void AddViewModelFactories(IServiceCollection services)
        {            
            services.AddSingleton<Func<BeginTestViewModel>>((s) => () => s.GetRequiredService<BeginTestViewModel>());
            services.AddSingleton<Func<ViewDataViewModel>>((s) => () => s.GetRequiredService<ViewDataViewModel>());
            services.AddSingleton<Func<CreateMazeViewModel>>((s) => () => s.GetRequiredService<CreateMazeViewModel>());
            services.AddSingleton<Func<CreateSetViewModel>>((s) => () => s.GetRequiredService<CreateSetViewModel>());
            services.AddSingleton<Func<BrowseMazesViewModel>>((s) => () => s.GetRequiredService<BrowseMazesViewModel>());
            services.AddSingleton<Func<BrowseSetsViewModel>>((s) => () => s.GetRequiredService<BrowseSetsViewModel>());

            services.AddSingleton<Func<HomeViewModel>>((s) => () => s.GetRequiredService<HomeViewModel>());
            services.AddSingleton<Func<SettingsViewModel>>((s) => () => s.GetRequiredService<SettingsViewModel>());
            services.AddSingleton<Func<AccountViewModel>>((s) => () => s.GetRequiredService<AccountViewModel>());

            services.AddSingleton<Func<LoggedInViewModel>>((s) => () => s.GetRequiredService<LoggedInViewModel>());
            services.AddSingleton<Func<NotLoggedInViewModel>>((s) => () => s.GetRequiredService<NotLoggedInViewModel>());

            services.AddSingleton<Func<PatientDetailsLoggedInViewModel>>((s) => () => s.GetRequiredService<PatientDetailsLoggedInViewModel>());
            services.AddSingleton<Func<PatientDetailsNotLoggedInViewModel>>((s) => () => s.GetRequiredService<PatientDetailsNotLoggedInViewModel>());

            services.AddSingleton<Func<PatientLoginRegisterViewModel>>((s) => () => s.GetRequiredService<PatientLoginRegisterViewModel>());
            services.AddSingleton<Func<PatientLoginViewModel>>((s) => () => s.GetRequiredService<PatientLoginViewModel>());
            services.AddSingleton<Func<PatientRegisterViewModel>>((s) => () => s.GetRequiredService<PatientRegisterViewModel>());

            services.AddSingleton<Func<StaffMemberDetailsLoggedInViewModel>>((s) => () => s.GetRequiredService<StaffMemberDetailsLoggedInViewModel>());
            services.AddSingleton<Func<StaffMemberDetailsNotLoggedInViewModel>>((s) => () => s.GetRequiredService<StaffMemberDetailsNotLoggedInViewModel>());

            services.AddSingleton<Func<StaffMemberLoginRegisterViewModel>>((s) => () => s.GetRequiredService<StaffMemberLoginRegisterViewModel>());
            services.AddSingleton<Func<StaffMemberLoginViewModel>>((s) => () => s.GetRequiredService<StaffMemberLoginViewModel>());
            services.AddSingleton<Func<StaffMemberRegisterViewModel>>((s) => () => s.GetRequiredService<StaffMemberRegisterViewModel>());

            services.AddSingleton<Func<ShowDetailsMazeCommandListViewModel>>((s) => () => s.GetRequiredService<ShowDetailsMazeCommandListViewModel>());
            services.AddSingleton<Func<AddToSetMazeCommandListViewModel>>((s) => () => s.GetRequiredService<AddToSetMazeCommandListViewModel>());
            services.AddSingleton<Func<MazeDetailsViewModel>>((s) => () => s.GetRequiredService<MazeDetailsViewModel>());
            services.AddSingleton<Func<MazeGeneratorViewModel>>((s) => () => s.GetRequiredService<MazeGeneratorViewModel>());
            services.AddSingleton<Func<MinimalCellSizeSetterViewModel>>((s) => () => s.GetRequiredService<MinimalCellSizeSetterViewModel>());

            services.AddSingleton<Func<SetGeneratorViewModel>>((s) => () => s.GetRequiredService<SetGeneratorViewModel>());
            services.AddSingleton<Func<CreateSetManuallyViewModel>>((s) => () => s.GetRequiredService<CreateSetManuallyViewModel>());
            services.AddSingleton<Func<ExamSetDetailsViewModel>>((s) => () => s.GetRequiredService<ExamSetDetailsViewModel>());
            services.AddSingleton<Func<ExamSetCommandViewModel>>((s) => () => s.GetRequiredService<ExamSetCommandViewModel>());
            services.AddSingleton<Func<ShowDetailsExamSetCommandListViewModel>>((s) => () => s.GetRequiredService<ShowDetailsExamSetCommandListViewModel>());
        }

        private static void AddNavigationServices(IServiceCollection services)
        {
            services.AddSingleton<NavigationService<BeginTestViewModel>>();
            services.AddSingleton<NavigationService<ViewDataViewModel>>();
            services.AddSingleton<NavigationService<CreateMazeViewModel>>();
            services.AddSingleton<NavigationService<CreateSetViewModel>>();
            services.AddSingleton<NavigationService<BrowseMazesViewModel>>();
            services.AddSingleton<NavigationService<BrowseSetsViewModel>>();

            services.AddSingleton<NavigationService<HomeViewModel>>();
            services.AddSingleton<NavigationService<SettingsViewModel>>();
            services.AddSingleton<NavigationService<AccountViewModel>>();

            services.AddSingleton<NavigationService<LoggedInViewModel>>();
            services.AddSingleton<NavigationService<NotLoggedInViewModel>>();

            services.AddSingleton<NavigationService<PatientDetailsLoggedInViewModel>>();
            services.AddSingleton<NavigationService<PatientDetailsNotLoggedInViewModel>>();

            services.AddSingleton<NavigationService<PatientLoginRegisterViewModel>>();
            services.AddSingleton<NavigationService<PatientLoginViewModel>>();
            services.AddSingleton<NavigationService<PatientRegisterViewModel>>();

            services.AddSingleton<NavigationService<StaffMemberDetailsLoggedInViewModel>>();
            services.AddSingleton<NavigationService<StaffMemberDetailsNotLoggedInViewModel>>();

            services.AddSingleton<NavigationService<StaffMemberLoginRegisterViewModel>>();
            services.AddSingleton<NavigationService<StaffMemberLoginViewModel>>();
            services.AddSingleton<NavigationService<StaffMemberRegisterViewModel>>();

            services.AddSingleton<NavigationService<ShowDetailsMazeCommandListViewModel>>();
            services.AddSingleton<NavigationService<AddToSetMazeCommandListViewModel>>();
            services.AddSingleton<NavigationService<MazeDetailsViewModel>>();
            services.AddSingleton<NavigationService<MazeGeneratorViewModel>>();
            services.AddSingleton<NavigationService<MinimalCellSizeSetterViewModel>>();

            services.AddSingleton<NavigationService<SetGeneratorViewModel>>();
            services.AddSingleton<NavigationService<CreateSetManuallyViewModel>>();
            services.AddSingleton<NavigationService<ExamSetDetailsViewModel>>();
            services.AddSingleton<NavigationService<ExamSetCommandViewModel>>();
            services.AddSingleton<NavigationService<ShowDetailsExamSetCommandListViewModel>>();
        }
    }
}