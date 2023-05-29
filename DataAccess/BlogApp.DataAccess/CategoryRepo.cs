using BlogApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DataAccess
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly BlogAppContext _context;

        public CategoryRepo(BlogAppContext context)
        {
            _context = context;
        }

        public void Add(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Category Get(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Category> GetAll()
        {
            return _context.Category.ToList();
        }

        public ICollection<Category> GetAllWithPredicate(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
