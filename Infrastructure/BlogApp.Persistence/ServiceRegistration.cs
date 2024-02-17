using BlogApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Persistence
{
    public static class ServiceRegistration
    {
        
        public static void AddPersistenceRegistration(this IServiceCollection service, string connectionString)
        {
            service.AddDbContext<BlogAppDbContext>(opt => opt.UseSqlServer(connectionString));
        }
    }
}
