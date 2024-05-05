using BlogApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BlogAppDbContext>
    {
        public BlogAppDbContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<BlogAppDbContext>();
            optionBuilder.UseSqlServer("Data Source=DESKTOP-1QVKUDC;Initial Catalog=BlogApp;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            return new BlogAppDbContext(optionBuilder.Options);
        }
    }
}
