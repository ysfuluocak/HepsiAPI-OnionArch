using HepsiAPI.Application.Features.ProductFeatures.Commands.CreateProduct;
using HepsiAPI.Application.Features.ProductFeatures.Commands.DeleteProduct;
using HepsiAPI.Application.Features.ProductFeatures.Commands.UpdateProduct;
using HepsiAPI.Application.Features.ProductFeatures.Queries.GetAllProducts;
using HepsiAPI.Application.Features.ProductFeatures.Queries.GetPaginationProducts;
using HepsiAPI.Application.Features.ProductFeatures.Queries.GetProduct;
using HepsiAPI.Application.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace HepsiAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await Mediator.Send(new GetAllProductsQueryRequest());
            return Ok(result);
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginationProducts([FromQuery] RequestParameters request)
        {
            var result = await Mediator.Send(new GetPaginatedProductsQueryRequest() { RequestParameters = request });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllProduct(int id)
        {
            var result = await Mediator.Send(new GetProductQueryRequest() { Id = id });
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommandRequest request)
        {
            request.Id = id;
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            await Mediator.Send(new DeleteProductCommandRequest() { Id = id });
            return NoContent();
        }
    }
}
