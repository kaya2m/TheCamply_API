using CamplyMarket.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Application.Features.Commands.AppUser.FacebookLogin
{
    public class FacebookLoginUserCommandResponse
    {
        public Token Token{ get; set; }
    }
}
