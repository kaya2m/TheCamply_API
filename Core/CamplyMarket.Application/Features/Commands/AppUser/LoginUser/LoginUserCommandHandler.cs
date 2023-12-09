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

namespace CamplyMarket.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<entity.AppUser> _userManager;
        readonly SignInManager<entity.AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;


        public LoginUserCommandHandler(UserManager<entity.AppUser> userManager, SignInManager<entity.AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            entity.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user == null)
                throw new NotFoundUserException("Kullanıcı adı veya şifre hatalı...");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)  // buraya kadar gelindiğinde kullanıcı doğrulanmıştır.
            {
                Token token = _tokenHandler.CreateAccessToken(12000);
                return new LoginUserSuccessResponse()
                {
                    Token = token
                };
            }
            return new LoginUserFailResponse()
            {
                Message = "Kullanıcı adı veya şifre hatalı..."
            };

        }
    }
}
