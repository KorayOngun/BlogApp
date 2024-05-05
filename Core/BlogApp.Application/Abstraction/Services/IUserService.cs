using BlogApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task CreateUser(CreateUser createUser); 
    }
}
