using GreenSphere.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSphere.Application.Features.Auth.Requests.Commands
{
    public class ForgotPasswordCommand : IRequest<Result<string>>
    {
        public string Email { get; set; } = string.Empty;
    }
}
