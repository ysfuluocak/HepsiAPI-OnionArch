using AutoMapper;
using HepsiAPI.Application.Features.ProductFeatures.Queries.GetProduct;
using HepsiAPI.Application.Interfaces.Repositories.ProductRepositories;
using MediatR;

namespace HepsiAPI.Application.Features.ProductFeatures.Queries.GetAllProducts
{
    public class GetAllProductsQueryRequest : IRequest<IEnumerable<GetProductQueryResponse>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IEnumerable<GetProductQueryResponse>>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductReadRepository productReadRepository, IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetProductQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {

            var products = await _productReadRepository.GetAllProductWithDetail();

            var response = _mapper.Map<List<GetProductQueryResponse>>(products);

            return response;
        }
    }

}
