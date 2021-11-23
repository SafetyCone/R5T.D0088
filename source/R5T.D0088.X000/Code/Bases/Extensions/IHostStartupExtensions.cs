using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.T0073;

using Instances = R5T.D0088.X000.Instances;


namespace R5T.D0088
{
    public static class IHostStartupExtensions
    {
        public static THostBuilder UseHostStartup<THostStartup, THostBuilder>(this THostBuilder hostBuilder)
            where THostStartup : class, IHostStartup
            where THostBuilder :
            IHasConfigureConfiguration<THostBuilder>,
            T0072.IHasConfigureServices<THostBuilder>
        {
            var hostStartupServiceProvider = Instances.ServiceCollection.New()
                .AddWithDefaultLifetime<THostStartup>()
                .BuildServiceProvider();

            hostBuilder.UseHostStartup<THostStartup, THostBuilder>(hostStartupServiceProvider);

            return hostBuilder;
        }
    }
}