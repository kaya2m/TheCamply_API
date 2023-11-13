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
    public class CostumerWriteRepository : WriteRepository<Customer>, ICostumerWriteRepository
    {
        public CostumerWriteRepository(CamplyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
