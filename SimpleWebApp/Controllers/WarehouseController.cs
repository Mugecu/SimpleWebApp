using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.Domain.Abstracts;
using SimpleWebApp.Domain.DTOs;
using SimpleWebApp.Domain.Entities;
using System.Security.Cryptography.X509Certificates;

namespace SimpleWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly Repository<Warehouse> _warehouseRepository;

        public WarehouseController(Repository<Warehouse> warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        [HttpGet("{id}")]
        public async Task<WarehouseDTO> GetAsync([FromRoute] Guid id)
        {
            var warehouse = await _warehouseRepository.GetAsync(id);
            await _warehouseRepository.SaveAsync();
            return new WarehouseDTO().ToDto(warehouse);
        }

        [HttpPost]
        public async Task<Guid> CreateAsync([FromBody] WarehouseDTO warehouse)
        {
            var createdWarehouse = await _warehouseRepository.CreateAsync(warehouse.ToModel());
            await _warehouseRepository.SaveAsync();
            return createdWarehouse?.Id ?? Guid.Empty;
        }
    }
}
