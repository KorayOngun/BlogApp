using BlogApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DataAccess
{
    public class BlogAppContext : DbContext
    {
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().Property(p => p.Title).HasMaxLength(150);
            modelBuilder.Entity<Blog>().HasOne(b => b.Category).WithMany().HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Blog>().HasOne(b => b.Admin).WithMany().HasForeignKey(c => c.AdminId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Blog>().HasMany(b => b.Comments).WithOne(c => c.Blog).HasForeignKey(c => c.BlogId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasMany(u=>u.comments).WithOne(c => c.User).HasForeignKey(c=>c.UserId).OnDelete(DeleteBehavior.NoAction);
           
            modelBuilder.Entity<User>().HasIndex(u=>u.Email).IsUnique();


            modelBuilder.Entity<Comment>().HasKey("BlogId", "UserId");

            
            

            

            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BlogAppDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
