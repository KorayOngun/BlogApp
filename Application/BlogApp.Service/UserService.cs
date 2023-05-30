using AutoMapper;
using BlogApp.DataAccess;
using BlogApp.Results.DTOs.Response;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepo repo;
        private readonly IMapper _mapper;
        public UserService(IUserRepo repo,IMapper mapper)
        {
            this.repo = repo;
            _mapper = mapper;
        }

        public UserLoginResponse Login(string username, string password)
        {
            var user = repo.GetAllWithPredicate(u => u.Name == username && u.Password == password).FirstOrDefault();
            if (user!=default)  return _mapper.Map<UserLoginResponse>(user); return default;
        }
      
    }
}
