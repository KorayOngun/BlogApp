using BlogApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DataAccess
{
    public class UserRepo : IUserRepo
    {
        private readonly BlogAppContext _context;

        public UserRepo(BlogAppContext context)
        {
            _context = context;
        }

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public ICollection<User> GetAllWithPredicate(Expression<Func<User, bool>> predicate)
        {
            return _context.User.Where(predicate).ToList();
        }

      

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
