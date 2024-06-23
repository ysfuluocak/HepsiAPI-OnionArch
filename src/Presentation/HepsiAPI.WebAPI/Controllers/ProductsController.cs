using HepsiAPI.Application.Features.ProductFeatures.Commands.CreateProduct;
using HepsiAPI.Application.Features.ProductFeatures.Commands.DeleteProduct;
using HepsiAPI.Application.Features.ProductFeatures.Commands.UpdateProduct;
using HepsiAPI.Application.Features.ProductFeatures.Queries.GetAllProducts;
using HepsiAPI.Application.Features.ProductFeatures.Queries.GetPage;
using HepsiAPI.Application.Features.ProductFeatures.Queries.GetProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HepsiAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _mediator.Send(new GetAllProductsQueryRequest());
            return Ok(result);
        }

        [HttpGet("{index}&{size}")]
        public async Task<IActionResult> GetAllProductPage(int index, int size)
        {
            var result = await _mediator.Send(new GetPageQueryRequest() { PageIndex = index, PageSize = size });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllProduct(int id)
        {
            var result = await _mediator.Send(new GetProductQueryRequest() { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommandRequest request)
        {
            request.Id = id;
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            await _mediator.Send(new DeleteProductCommandRequest() { Id = id });
            return NoContent();
        }
    }
}
