﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Application.Features.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandRequest :IRequest<DeleteProductCommandResponse>
    {
        public string id { get; set; }
    }
}
