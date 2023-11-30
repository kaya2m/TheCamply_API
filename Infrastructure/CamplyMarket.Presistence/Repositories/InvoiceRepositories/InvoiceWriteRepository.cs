using CamplyMarket.Application.Repositories.InvoiceFileInterface;
using CamplyMarket.Domain.Entities;
using CamplyMarket.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Persistence.Repositories.InvoiceRepositories
{
    public class InvoiceWriteRepository : WriteRepository<InvoiceFiles>, IInvoceFileWriteRepository
    {
        public InvoiceWriteRepository(CamplyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
