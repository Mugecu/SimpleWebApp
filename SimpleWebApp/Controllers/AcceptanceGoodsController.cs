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

        /// <summary>
        /// Добавляе продукты на склад
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="addedProduct"></param>
        /// <returns></returns>
        /// <exception cref="Exception"> Отсутствуют товары на складе или сам склад.</exception>
        [HttpPut("accept/warehouse/{warehoueId}")]
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

        /// <summary>
        /// Продает продукты со склада
        /// </summary>
        /// <param name="warehouseId"> Идентификатор склада</param>
        /// <param name="sellProducts">Продукты для продажи</param>
        /// <returns></returns>
        /// <exception cref="Exception">Отсутствует склад.</exception>
        
        [HttpPut("sell/warehouse/{warehoueId}")]
        public async Task SellProductsAsync([FromRoute] Guid warehouseId, [FromBody] GoodsValuesDTO sellProducts)
        {
            var warehouse = await _warehouseRepository.GetAsync(warehouseId);
            if (warehouse == null)
                throw new Exception($"Склад с идентиифиатором {warehouseId} не существует.");

            warehouse.AddProducts(sellProducts.Goods);
            await _warehouseRepository.SaveAsync();
        }
    }
}
