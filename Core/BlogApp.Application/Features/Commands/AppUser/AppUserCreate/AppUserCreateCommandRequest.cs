using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.Commands.AppUser.AppUserCreate
{
    public class AppUserCreateCommandRequest : IRequest<AppUserCreateCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string UserName { get; set; }
    }
}
