using System;

using R5T.T0061;

using IServiceProvider = R5T.T0061.IServiceProvider;


namespace R5T.D0088.X000
{
    public static class Instances
    {
        public static IServiceCollection ServiceCollection { get; } = T0061.ServiceCollection.Instance;
        public static IServiceProvider ServiceProvider { get; } = T0061.ServiceProvider.Instance;
    }
}
