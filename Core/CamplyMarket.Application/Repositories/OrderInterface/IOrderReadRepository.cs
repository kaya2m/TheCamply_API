﻿using CamplyMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Application.Repositories.OrderInterface
{
    public interface IOrderReadRepository : IReadRepository<Order>
    {
    }
}
