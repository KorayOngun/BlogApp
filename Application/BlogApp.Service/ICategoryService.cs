using BlogApp.Entities;
using BlogApp.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Service
{
    public interface ICategoryService
    {
        ICollection<Category> GetAll();
    }
}
