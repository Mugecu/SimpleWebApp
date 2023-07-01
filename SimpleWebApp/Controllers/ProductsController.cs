using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.Domain.DTOs;

namespace SimpleWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController(//добавить репозитория ыв конструкторе)
        {

        }

        [HttpGet]
        public async Task GetAsync([FromRoute] Guid id)
        { 
        }

        [HttpPost]
        public async Task<Guid> CreateAsync([FromBody] ProductDTO product);
        //=> await _productRepository.Create(product.ToModel());

        [HttpPut]
        public async Task UpdateAsync([FromRoute]Guid productId,  [FromBody]ProductDTO product)
        {
            var updateParameters = product.ToModel();
            var productForUpdate = await _productRepository.GetAsync(productId);
            if (productForUpdate == null)
                return;

            productForUpdate.Update(updateParameters);
            await _productRepository.SaveChangeAsync();
        }
    }
}
