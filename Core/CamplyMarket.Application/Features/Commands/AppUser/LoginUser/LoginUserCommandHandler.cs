using entity = CamplyMarket.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CamplyMarket.Application.Exceptions;
using CamplyMarket.Application.Abstraction.Token;
using CamplyMarket.Application.DTOs;
using CamplyMarket.Application.Abstraction.Services;

namespace CamplyMarket.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
         
           Token token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password);
            return new LoginUserSuccessResponse()
            {
                Token = token
            };
        }
    }
}
