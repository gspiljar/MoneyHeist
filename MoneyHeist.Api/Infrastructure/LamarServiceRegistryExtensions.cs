using Lamar;
using Microsoft.AspNetCore.Hosting;

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
            serviceRegistry.AddAutoMapper(typeof(Program));
        }
    }
}
