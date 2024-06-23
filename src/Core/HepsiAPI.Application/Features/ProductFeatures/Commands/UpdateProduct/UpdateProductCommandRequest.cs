using AutoMapper;
using HepsiAPI.Application.Features.ProductFeatures.Queries.GetProduct;
using HepsiAPI.Application.Interfaces.Repositories.CategoryProductRepositories;
using HepsiAPI.Application.Interfaces.Repositories.ProductRepositories;
using MediatR;

namespace HepsiAPI.Application.Features.ProductFeatures.Commands.UpdateProduct
{
    public class UpdateProductCommandRequest : IRequest<GetProductQueryResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int BrandId { get; set; }
        public IEnumerable<int> CategoryIds { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, GetProductQueryResponse>
    {

        private readonly ICategoryProductReadRepository _categoryProductReadRepository;
        private readonly ICategoryProductWriteRepository _categoryProductWriteRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;


        public UpdateProductCommandHandler(ICategoryProductReadRepository categoryProductReadRepository, ICategoryProductWriteRepository categoryProductWriteRepository, IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IMapper mapper)
        {
            _categoryProductReadRepository = categoryProductReadRepository;
            _categoryProductWriteRepository = categoryProductWriteRepository;
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _mapper = mapper;
        }

        public async Task<GetProductQueryResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productReadRepository.GetSingleEntityAsync(p => p.Id == request.Id, true, p => p.CategoryProducts);

            await _categoryProductWriteRepository.DeleteRangeAsync(existingProduct.CategoryProducts);

            var updatedProduct = _mapper.Map(request, existingProduct);

            var response = await _productWriteRepository.UpdateAsync(updatedProduct);

            await _productWriteRepository.CommitAsync();

            response = await _productReadRepository.GetProductByIdWithDetail(response.Id);

            return _mapper.Map<GetProductQueryResponse>(response);
        }
    }

}
