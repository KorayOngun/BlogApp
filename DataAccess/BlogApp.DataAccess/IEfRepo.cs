using BlogApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DataAccess
{
    public interface IEfRepo<T> where T : class, IEntity,new()
    {
        T Get(int id);
        ICollection<T> GetAll();
        ICollection<T> GetAllWithPredicate(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        Task AddAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
    }
}
