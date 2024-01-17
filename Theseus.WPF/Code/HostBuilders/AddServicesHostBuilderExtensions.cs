using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Domain.Services.Authentication.PatientAuthentication;
using Theseus.Domain.Services.Authentication.StaffMemberAuthentication;
using Theseus.Domain.Services.ExamDataServices;
using Theseus.Domain.Services.ExamDataServices.Summary;
using Theseus.Domain.Services.ExamDataServices.Summary.ExamStats;
using Theseus.Domain.Services.ExamDataServices.Summary.ExamSetGroup;
using Theseus.Infrastructure.Mappings;
using Theseus.WPF.Code.Services;
using Theseus.Domain.Services.ExamDataServices.Summary.ExamStageStats;

namespace Theseus.WPF.Code.HostBuilders
{
    /// <summary>
    /// The <c>AddServicesHostBuilderExtensions</c> class registers service classes as singleton services.
    /// </summary>
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IPasswordHasher, PasswordHasher>();
                services.AddSingleton<EmailValidator>();
                services.AddSingleton<ScoreCalculator>();
                services.AddSingleton<ExamStageCalculator>();
                services.AddSingleton<ExamCalculator>();
                services.AddSingleton<ExamSetGroupStatCalculator>();
                services.AddSingleton<DescriptiveValueComparer>();
                services.AddSingleton<InputListToTimedCellPathConverter>();
                services.AddSingleton<ExamCsvWriter>();
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