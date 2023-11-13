using CamplyMarket.Application.Repositories.CustomerInterface;
using CamplyMarket.Domain.Entities;
using CamplyMarket.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Persistence.Repositories.CostumerRepositories
{
    public class CustomerReadRepository : ReadRepository<Customer>, ICostumerReadRepository
    {
        public CustomerReadRepository(CamplyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
