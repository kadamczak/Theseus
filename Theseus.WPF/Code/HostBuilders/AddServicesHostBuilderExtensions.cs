using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Theseus.Domain.Services.Authentication;
using Theseus.Infrastructure.Mappings;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IPasswordHasher, PasswordHasher>();
                services.AddSingleton<IStaffMemberAuthenticationService, StaffMemberAuthenticationService>();

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
