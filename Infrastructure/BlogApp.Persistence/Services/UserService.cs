using BlogApp.Application.Abstraction.Services;
using BlogApp.Application.Dtos;
using BlogApp.Domain.Identities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task CreateUser(CreateUser createUser)
        {
            var result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid(),
                UserName = createUser.UserName,
                Email = createUser.Email,
            }, createUser.Password);

            if (!result.Succeeded)
                throw new Exception("kullanıcı kayıt edilemedi");

        }
    }
}
