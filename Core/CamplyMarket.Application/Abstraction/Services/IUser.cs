using CamplyMarket.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Application.Abstraction.Services
{
    public interface IUser
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);

    }
}
