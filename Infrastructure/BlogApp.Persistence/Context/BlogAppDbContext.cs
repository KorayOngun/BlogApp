using BlogApp.Domain.Entities;
using BlogApp.Domain.Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Persistence.Context
{
    public class BlogAppDbContext(DbContextOptions<BlogAppDbContext> options) : IdentityDbContext<AppUser,AppRole,Guid>(options)
    {


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
