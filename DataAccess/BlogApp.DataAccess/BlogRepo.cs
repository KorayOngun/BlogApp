using BlogApp.Entities;
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

        public Task AddAsync(Blog entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Blog entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Blog entity)
        {
            throw new NotImplementedException();
        }

        public Blog Get(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Blog> GetAll()
        {
            return context.Blog.ToList();
        }

        public ICollection<Blog> GetAllWithPredicate(Expression<Func<Blog, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(Blog entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Blog entity)
        {
            throw new NotImplementedException();
        }
    }
}
