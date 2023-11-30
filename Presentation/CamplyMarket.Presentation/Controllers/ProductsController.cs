using CamplyMarket.Application.Abstraction.Storage;
using CamplyMarket.Application.Repositories.FileInterface;
using CamplyMarket.Application.Repositories.InvoiceFileInterface;
using CamplyMarket.Application.Repositories.ProductImageFileInterface;
using CamplyMarket.Application.Repositories.ProductInterface;
using CamplyMarket.Application.RequestParameters;
using CamplyMarket.Application.ViewModels.Products;
using CamplyMarket.Domain.Entities;
using CamplyMarket.Infrastructure.Services.Storage;
using Microsoft.AspNetCore.Mvc;
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

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHost, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileRead, IProductImageFileWriteRepository productImageFileWrite, IInvoceFileReadRepository invoceFileRead, IInvoceFileWriteRepository invoceFileWrite, IStorageService storage)
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
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id, false);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false)
                .Count();

            var products = _productReadRepository.GetAll(false)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.Stock,
                    p.CreateDate,
                    p.UpdatedDate
                }).Skip(pagination.Page * pagination.Size)
                  .Take(pagination.Size);
            return Ok(new { products, totalCount });
        }
        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product product)
        {
            if (ModelState.IsValid)
            {
                await _productWriteRepository.AddAsync(
                 new Product
                 {
                     Name = product.Name,
                     Price = product.Price,
                     Stock = product.Stock
                 });
            }

            await _productWriteRepository.SaveAsync();
            return Ok((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Name = model.Name;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.Remove(id);
            await _productWriteRepository.SaveAsync();
            return Ok(new
            {
                message = "silme işlemi başarılı"
            });
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storage.UploadAsync("product-images", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id);

          await  _productImageFileWrite.AddRangeAsync(result.Select(d => new ProductImageFiles()
          {
              FileName = d.fileName,
              Path = d.pathOrContainerName,
              Storage =_storage.StorageName,
              Products = new List<Product>() {product}

          }).ToList());
            await _productImageFileWrite.SaveAsync();
                return Ok();
        }

    }
}