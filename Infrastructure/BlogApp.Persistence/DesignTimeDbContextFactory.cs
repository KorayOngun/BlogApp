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
            optionBuilder.UseSqlServer("");
            return new BlogAppDbContext(optionBuilder.Options);
        }
    }
}
