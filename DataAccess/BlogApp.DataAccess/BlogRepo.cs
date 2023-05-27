using BlogApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DataAccess
{
    public class BlogRepo : IBlogRepo
    {
        private readonly BlogAppContext context;

        public BlogRepo(BlogAppContext context)
        {
            this.context = context;
        }

        public void Add(Blog entity)
        {
            context.Blog.Add(entity);
            context.SaveChanges();
        }

        public async Task AddAsync(Blog entity)
        {
            await context.Blog.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public void Delete(Blog entity)
        {
           context.Remove(entity);
            context.SaveChanges();
        }

        public async Task DeleteAsync(Blog entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public Blog Get(int id)
        {
            return context.Blog.FirstOrDefault(b => b.Id == id);
        }

        public ICollection<Blog> GetAll()
        {
            return context.Blog.ToList();
        }

        public ICollection<Blog> GetAllWithPredicate(Expression<Func<Blog, bool>> predicate)
        {
            return context.Blog.Where(predicate).AsNoTracking().ToList();   
        }

        public void Update(Blog entity)
        {
            context.Blog.Update(entity);
            context.SaveChanges();  
        }

        public async Task UpdateAsync(Blog entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
