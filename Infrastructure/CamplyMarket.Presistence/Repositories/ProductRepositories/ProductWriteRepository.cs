using CamplyMarket.Application.Repositories.ProductInterface;
using CamplyMarket.Domain.Entities;
using CamplyMarket.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Persistence.Repositories.ProductRepositories
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(CamplyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
