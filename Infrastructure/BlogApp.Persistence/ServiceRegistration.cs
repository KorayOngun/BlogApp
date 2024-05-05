using BlogApp.Application.Abstraction.Services;
using BlogApp.Domain.Identities;
using BlogApp.Persistence.Context;
using BlogApp.Persistence.Services;
using Microsoft.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

           service.AddIdentity<AppUser,AppRole>(opt =>
           {
               opt.User.RequireUniqueEmail = true;
               opt.Password.RequiredLength = 3;
               opt.Password.RequireLowercase = false;
               opt.Password.RequireUppercase = false;
               opt.Password.RequireDigit = false;
               opt.Password.RequireNonAlphanumeric = false;
           }).AddEntityFrameworkStores<BlogAppDbContext>(); 
            service.AddScoped<IUserService, UserService>();
        }
    }
}
