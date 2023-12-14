using CamplyMarket.Application.Features.Commands.AppUser.FacebookLogin;
using CamplyMarket.Application.Features.Commands.AppUser.GoogleLogin;
using CamplyMarket.Application.Features.Commands.AppUser.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CamplyMarket.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
            LoginUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginUserCommandRequest request)
        {
            GoogleLoginUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginUserCommandRequest request)
        {
            FacebookLoginUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
