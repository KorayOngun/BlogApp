using BlogApp.Application.Abstraction.Services;
using BlogApp.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.Commands.AppUser.AppUserCreate
{
    public class AppUserCreateCommandHandler : IRequestHandler<AppUserCreateCommandRequest, AppUserCreateCommandResponse>
    {
        private readonly IUserService _userService;

        public AppUserCreateCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<AppUserCreateCommandResponse> Handle(AppUserCreateCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUser createUser = new()
            {
                Email = request.Email,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                UserName = request.UserName,
                
            };
            await _userService.CreateUser(createUser);

            return new() 
            {
                Success = true,
            };
        }
    }
}
