using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddAuthenticationViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddAuthenticationViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                AddViewModels(services);
                AddViewModelFactories(services);
                AddNavigationServices(services);
            });

            return hostBuilder;
        }


        private static void AddViewModels(IServiceCollection services)
        {
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
        }

        private static void AddViewModelFactories(IServiceCollection services)
        {
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
        }

        private static void AddNavigationServices(IServiceCollection services)
        {
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
        }
    }
}