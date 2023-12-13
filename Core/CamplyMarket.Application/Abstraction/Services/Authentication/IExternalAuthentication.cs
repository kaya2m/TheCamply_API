using CamplyMarket.Application.DTOs.Facebook;
using CamplyMarket.Application.DTOs.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Application.Abstraction.Services.Authentication
{
    public interface IExternalAuthentication
    {
        Task<FacebookLoginResponse> FacebookLoginAsync(FacebookLogin model);
        Task<GoogleLoginResponse> GoogleLoginAsync(GoogleLogin model);
    }
}
