﻿using CamplyMarket.Application.Abstraction.Storage;
using CamplyMarket.Application.Features.Commands.Product.CreateProduct;
using CamplyMarket.Application.Features.Commands.Product.DeleteProduct;
using CamplyMarket.Application.Features.Commands.Product.UpdateProduct;
using CamplyMarket.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using CamplyMarket.Application.Features.Commands.ProductImageFile.UploadProductImage;
using CamplyMarket.Application.Features.Queries.Product.GetAllProduct;
using CamplyMarket.Application.Features.Queries.Product.GetByIdProduct;
using CamplyMarket.Application.Features.Queries.ProductImageFile.GetProductImageFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CamplyMarket.Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]DeleteProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok(new
            {
                message = "silme işlemi başarılı"
            });
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery]UploadProductImageCommandRequest request)
        {
            request.Files = Request.Form.Files;
           UploadProductImageCommandResponse response= await _mediator.Send(request);
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImageFileQueryRequest request)
        {
            List<GetProductImageFileQueryResponse> response = await _mediator.Send(request);
            return Ok(response);

        }


        [HttpDelete("[action]/{id}")]
          public async Task<IActionResult> DeleteProductImage([FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest, [FromQuery] string imageId)
        {
            removeProductImageCommandRequest.ImageId = imageId;
            RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);
            return Ok();
        }
    }
}