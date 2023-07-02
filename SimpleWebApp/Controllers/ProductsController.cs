using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.Domain.Abstracts;
using SimpleWebApp.Domain.DTOs;
using SimpleWebApp.Domain.Entities;

namespace SimpleWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Repository<Product> _productRepository;

        public ProductsController(Repository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ProductDTO> GetAsync([FromRoute] Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            return new ProductDTO().ToDto(product);
        }

        [HttpPost]
        public async Task<Guid> CreateAsync([FromBody] ProductDTO product)
        {
            var createdProduct = await _productRepository.CreateAsync(product.ToModel());
            await _productRepository.SaveAsync();
            return createdProduct?.Id ?? Guid.Empty;
        }

        [HttpPut]
        public async Task UpdateAsync([FromRoute] Guid productId, [FromBody] ProductDTO product)
        {
            var updateParameters = product.ToModel();
            var productForUpdate = await _productRepository.GetAsync(productId);

            if (productForUpdate == null)
                return;

            productForUpdate.Update(updateParameters);
            await _productRepository.SaveAsync();
        }
    }
}
