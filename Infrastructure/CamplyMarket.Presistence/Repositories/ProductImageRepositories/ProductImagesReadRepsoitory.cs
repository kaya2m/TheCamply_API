using CamplyMarket.Application.Repositories;
using CamplyMarket.Application.Repositories.ProductImageFileInterface;
using CamplyMarket.Domain.Entities;
using CamplyMarket.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Persistence.Repositories.ProductImageRepositories
{
    public class ProductImagesReadRepsoitory : ReadRepository<ProductImageFiles>, IProductImageFileReadRepository
    {
        public ProductImagesReadRepsoitory(CamplyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
