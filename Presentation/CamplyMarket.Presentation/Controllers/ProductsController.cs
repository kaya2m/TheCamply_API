using CamplyMarket.Application.Repositories.ProductInterface;
using CamplyMarket.Application.RequestParameters;
using CamplyMarket.Application.Services;
using CamplyMarket.Application.ViewModels.Products;
using CamplyMarket.Domain.Entities;
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
        private readonly IFileService _fileService;
        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHost, IFileService fileService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHost = webHost;
            this._fileService = fileService;
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
        public async Task<IActionResult> Upload()
        {
          await _fileService.UploadAsync("resource/products-images", Request.Form.Files);
            return Ok();
        }

    }
}