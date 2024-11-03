using GreenSphere.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSphere.Application.Features.Auth.Requests.Commands
{
    public class ConfirmForgotPasswordCodeCommand : IRequest<Result<string>>
    {
        public string Email { get; set; } = string.Empty ;
        public string Code { get; set; } = string.Empty ;
        public string NewPassword { get; set; } = string.Empty ;
        public string ConfirmPassword { get; set; } = string.Empty ;
    }
}
