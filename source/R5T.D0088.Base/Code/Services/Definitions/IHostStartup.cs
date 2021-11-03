using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.T0064;


namespace R5T.D0088
{
    /// <summary>
    /// Startup service for a host.
    /// </summary>
    [ServiceDefinitionMarker]
    public interface IHostStartup : IServiceDefinition
    {
        Task ConfigureConfiguration(IConfigurationBuilder configurationBuilder);
        Task ConfigureServices(IServiceCollection services);
    }
}
