using CamplyMarket.Application.Repositories.FileInterface;
using CamplyMarket.Domain.Entities;
using CamplyMarket.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Persistence.Repositories.FileRepositories
{
    public class FileReadRepository : ReadRepository<Files>, IFileReadRepository
    {
        public FileReadRepository(CamplyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
