using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectName.Application.ViewModels;
using MediatR;

namespace ProjectName.Application.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}