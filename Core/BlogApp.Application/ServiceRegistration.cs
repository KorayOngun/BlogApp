using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace BlogApp.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplication(this IServiceCollection service)
        {
            service.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(ServiceRegistration)));
        }
    }
}
