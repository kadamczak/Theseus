using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Domain.Services.Authentication.PatientAuthentication;
using Theseus.Domain.Services.Authentication.StaffMemberAuthentication;
using Theseus.Infrastructure.Mappings;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IPasswordHasher, PasswordHasher>();
                services.AddSingleton<IEmailValidator, EmailValidator>();
                services.AddSingleton<ExamSetStatCalculator>();
                services.AddSingleton<DescriptiveValueComparer>();
                services.AddSingleton<IStaffMemberAuthenticationService, StaffMemberAuthenticationService>();
                services.AddSingleton<IPatientAuthenticationService, PatientAuthenticationService>();

                var config = new MapperConfiguration(cfg => {
                    cfg.AddProfile<TheseusMappingProfile>();
                });

                var mapper = config.CreateMapper();
                mapper.ConfigurationProvider.AssertConfigurationIsValid();
                services.AddSingleton(mapper);
            });

            return hostBuilder;
        }
    }
}