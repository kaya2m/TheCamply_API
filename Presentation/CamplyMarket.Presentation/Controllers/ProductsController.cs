using CamplyMarket.Application.Abstraction.Storage;
using CamplyMarket.Application.Features.Commands.Product.CreateProduct;
using CamplyMarket.Application.Features.Commands.Product.DeleteProduct;
using CamplyMarket.Application.Features.Commands.Product.UpdateProduct;
using CamplyMarket.Application.Features.Commands.ProductImageFile.UploadProductImage;
using CamplyMarket.Application.Features.Queries.Product.GetAllProduct;
using CamplyMarket.Application.Features.Queries.Product.GetByIdProduct;
using CamplyMarket.Application.Repositories.FileInterface;
using CamplyMarket.Application.Repositories.InvoiceFileInterface;
using CamplyMarket.Application.Repositories.ProductImageFileInterface;
using CamplyMarket.Application.Repositories.ProductInterface;
using CamplyMarket.Application.RequestParameters;
using CamplyMarket.Application.ViewModels.Products;
using CamplyMarket.Domain.Entities;
using CamplyMarket.Infrastructure.Services.Storage;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CamplyMarket.Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHost;
        private readonly IFileWriteRepository _fileWriteRepository;
        IFileReadRepository _fileReadRepository;
        IProductImageFileReadRepository
             _productImageFileRead;
        IProductImageFileWriteRepository
            _productImageFileWrite;
        IInvoceFileReadRepository
            _invoceFileRead;
        IInvoceFileWriteRepository
            _invoceFileWrite;
        readonly IStorageService _storage;
        readonly IConfiguration configuration;

        readonly IMediator _mediator;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHost, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileRead, IProductImageFileWriteRepository productImageFileWrite, IInvoceFileReadRepository invoceFileRead, IInvoceFileWriteRepository invoceFileWrite, IStorageService storage, IConfiguration configuration, IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHost = webHost;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileRead = productImageFileRead;
            _productImageFileWrite = productImageFileWrite;
            _invoceFileRead = invoceFileRead;
            _invoceFileWrite = invoceFileWrite;
            _storage = storage;
            this.configuration = configuration;
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
        public async Task<IActionResult> GetProductImages(string id)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                    .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            return Ok(product.ProductImageFiles.Select(p => new
            {
                Path = $"{configuration["BaseStorageUrl"]}/{p.Path}",
                p.FileName,
                p.Id
            }));
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id, string imageId)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                  .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            ProductImageFiles productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));
            product.ProductImageFiles.Remove(productImageFile);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}