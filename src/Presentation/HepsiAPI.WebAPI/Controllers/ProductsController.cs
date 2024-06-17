using HepsiAPI.Application.Interfaces.UnitOfWorks;
using HepsiAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HepsiAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();
            return Ok(result);
        }

    }
}
