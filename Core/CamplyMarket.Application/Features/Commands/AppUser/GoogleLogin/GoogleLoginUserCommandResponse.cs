using CamplyMarket.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginUserCommandResponse
    {
        public Token Token{ get; set; }
    }
}
