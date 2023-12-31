﻿using CamplyMarket.Application.Abstraction.Services;
using CamplyMarket.Application.Abstraction.Token;
using CamplyMarket.Application.DTOs;
using CamplyMarket.Application.DTOs.Google;
using CamplyMarket.Application.Exceptions;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using appUser = CamplyMarket.Domain.Entities.Identity.AppUser;

namespace CamplyMarket.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginUserCommandHandler : IRequestHandler<GoogleLoginUserCommandRequest, GoogleLoginUserCommandResponse>
    {
        IAuthService _authService;

        public GoogleLoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GoogleLoginUserCommandResponse> Handle(GoogleLoginUserCommandRequest request, CancellationToken cancellationToken)
        {
           DTOs.Google.GoogleLogin query = new DTOs.Google.GoogleLogin()
           {
                Id = request.Id,
                IdToken = request.IdToken,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Name = request.Name,
                PhotoUrl = request.PhotoUrl,
                Provider = request.Provider
            };
            GoogleLoginResponse token = await _authService.GoogleLoginAsync(query);
            return new()
            {
                Token = token.Token
            };  

        }
    }
}

