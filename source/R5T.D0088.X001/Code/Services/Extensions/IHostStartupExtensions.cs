using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.T0063;
using R5T.T0073;

using Instances = R5T.D0088.X001.Instances;


namespace R5T.D0088
{
    public static class IHostStartupExtensions
    {
        public static THostBuilder UseHostStartup<THostStartup, THostBuilder>(this THostBuilder hostBuilder,
            IServiceAction<THostStartup> hostStartupAction)
            where THostStartup : IHostStartup
            where THostBuilder :
                IHasConfigureConfiguration<THostBuilder>,
                T0072.IHasConfigureServices<THostBuilder>
        {
            var startupServiceProvider = Instances.ServiceOperator.GetServiceInstance(
                hostStartupAction,
                out var hostStartup);

            hostBuilder.UseHostStartup(hostStartup);

            // Add a ConfigureServices() call (added after the startup instance's ConfigureServices() call) that will dispose of the startup service provider now that the startup service will have been used.
            hostBuilder.ConfigureServices(_ =>
            {
                startupServiceProvider.Dispose(); // Chose Dispose() over DisposeAsync().
            });

            return hostBuilder;
        }

        public static THostBuilder UseHostStartup<THostStartup, THostBuilder>(this THostBuilder hostBuilder,
            IServiceProvider hostStartupServiceProvider)
            where THostStartup : IHostStartup
            where THostBuilder :
            IHasConfigureConfiguration<THostBuilder>,
            T0072.IHasConfigureServices<THostBuilder>
        {
            var hostStartup = hostStartupServiceProvider.GetRequiredService<THostStartup>();

            hostBuilder.UseHostStartup(hostStartup);

            return hostBuilder;
        }

        public static THostBuilder UseHostStartup<THostStartup, THostBuilder>(this THostBuilder hostBuilder,
            THostStartup hostStartup)
            where THostStartup : IHostStartup
            where THostBuilder :
            IHasConfigureConfiguration<THostBuilder>,
            T0072.IHasConfigureServices<THostBuilder>
        {
            hostBuilder
                .ConfigureConfiguration(configurationBuilder =>
                {
                    SyncOverAsyncHelper.ExecuteTaskSynchronously(hostStartup.ConfigureConfiguration(configurationBuilder));
                })
                .ConfigureServices(services =>
                {
                    SyncOverAsyncHelper.ExecuteTaskSynchronously(hostStartup.ConfigureServices(services));
                })
                ;

            return hostBuilder;
        }
    }
}