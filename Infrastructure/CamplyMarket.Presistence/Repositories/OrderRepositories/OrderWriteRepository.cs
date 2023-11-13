using CamplyMarket.Application.Repositories.OrderInterface;
using CamplyMarket.Domain.Entities;
using CamplyMarket.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Persistence.Repositories.OrderRepositories
{
    public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(CamplyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
