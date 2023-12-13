using CamplyMarket.Application.Abstraction.Services;
using CamplyMarket.Application.Abstraction.Token;
using CamplyMarket.Application.DTOs;
using CamplyMarket.Application.DTOs.Facebook;
using CamplyMarket.Application.DTOs.Google;
using CamplyMarket.Domain.Entities;
using CamplyMarket.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CamplyMarket.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly HttpClient _httpClient;
        readonly IConfiguration configuration;
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        public AuthService(IHttpClientFactory httpClientFactory, UserManager<AppUser> userManager, ITokenHandler tokenHandler, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            this.configuration = configuration;
        }
        public async Task<FacebookLoginResponse> FacebookLoginAsync(FacebookLogin model)
        {
            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={configuration["ExternalLoginSettings:Facebook:Client_Id"]}&client_secret={configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");
            FacebookAccessTokenResponse? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);

            string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AuthToken}&access_token={facebookAccessTokenResponse.AccessToken}");

            FacebookUserAccessTokenValidation? validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);

            if (validation is not null && validation.Data.IsValid)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={model.AuthToken}");

                FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

                var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
                AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                bool result = user != null;
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(userInfo?.Email);
                    if (user == null)
                    {
                        user = new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Email = userInfo?.Email,
                            UserName = userInfo?.Email,
                            FirstName = userInfo?.Name
                        };
                        var identityResult = await _userManager.CreateAsync(user);
                        result = identityResult.Succeeded;
                    }
                }

                if (result)
                {
                    await _userManager.AddLoginAsync(user, info); //AspNetUserLogins

                    Token token = _tokenHandler.CreateAccessToken(5);
                    return new()
                    {
                        Token = token
                    };
                }
            }
            throw new Exception("Invalid external authentication.");

        }

        public Task<GoogleLoginResponse> GoogleLoginAsync(GoogleLogin model)
        {
            throw new NotImplementedException();
        }

        public Task LoginAsync()
        {
            throw new NotImplementedException();
        }
    }
}
