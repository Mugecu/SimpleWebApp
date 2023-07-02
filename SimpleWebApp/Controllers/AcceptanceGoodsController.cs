using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.Domain.Abstracts;
using SimpleWebApp.Domain.DTOs;
using SimpleWebApp.Domain.Entities;
using SimpleWebApp.Domain.Interfaces;

namespace SimpleWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcceptanceGoodsController : ControllerBase
    {
        private readonly ProductRepository _productRepository;
        private readonly Repository<Warehouse> _warehouseRepository;

        public AcceptanceGoodsController(ProductRepository productRepository, Repository<Warehouse> warehouseRepository)
        {
            _productRepository = productRepository;
            _warehouseRepository = warehouseRepository;
        }

        [HttpPut("accept/warewouse/{warehoueId}")]
        public async Task AddProductsAsync([FromRoute] Guid warehouseId, [FromBody] GoodsValuesDTO addedProduct)
        {
            var warehouse = await _warehouseRepository.GetAsync(warehouseId);
            var unavailableProducts = await _productRepository.ContainceProductInRepository(addedProduct.Goods.Select(i => i.Key));
            if (unavailableProducts != null && unavailableProducts.Count > 0)
                throw new Exception($"Занесите в репозиторий товаров {string.Join(", ", unavailableProducts.Select(i => i.Value).ToArray())}");

            if (warehouse == null)
                throw new Exception($"Склад с идентиифиатором {warehouseId} не существует.");

            warehouse.AddProducts(addedProduct.Goods);
            await _warehouseRepository.SaveAsync();
        }

        [HttpPut("sell/warewouse/{warehoueId}")]
        public async Task SellProductsAsync([FromRoute] Guid warehouseId, [FromBody] GoodsValuesDTO addedProduct)
        {
            var warehouse = await _warehouseRepository.GetAsync(warehouseId);
            if (warehouse == null)
                throw new Exception($"Склад с идентиифиатором {warehouseId} не существует.");

            warehouse.AddProducts(addedProduct.Goods);
            await _warehouseRepository.SaveAsync();
        }
    }
}
