using Lamar;

namespace MoneyHeist.Api.Infrastructure
{
    public static class LamarServiceRegistryExtensions
    {
        public static void RegisterServices(this ServiceRegistry serviceRegistry)
        {
            serviceRegistry.Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions(ServiceLifetime.Scoped);
            });
        }
    }
}
