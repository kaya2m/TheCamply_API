using CamplyMarket.Application.Repositories.CustomerInterface;
using CamplyMarket.Application.Repositories.OrderInterface;
using CamplyMarket.Application.Repositories.ProductInterface;
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
        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id,false);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = _productReadRepository.GetAll(false);
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product product)
        {
            await _productWriteRepository.AddAsync(
                new Product 
                {
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock
                });
            await _productWriteRepository.SaveAsync();
            return Ok((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
          Product product =  await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Name = model.Name;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.Remove(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}