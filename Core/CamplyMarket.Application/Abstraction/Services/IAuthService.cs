using CamplyMarket.Application.Abstraction.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Application.Abstraction.Services
{
    public interface IAuthService: IExternalAuthentication,IInternalAuthentication
    {
       
        
    }
}
