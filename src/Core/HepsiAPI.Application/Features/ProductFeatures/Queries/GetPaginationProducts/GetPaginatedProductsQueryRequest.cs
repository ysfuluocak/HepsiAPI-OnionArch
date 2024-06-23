using AutoMapper;
using HepsiAPI.Application.Features.ProductFeatures.Queries.GetProduct;
using HepsiAPI.Application.Interfaces.Repositories.ProductRepositories;
using HepsiAPI.Application.Parameters;
using HepsiAPI.Application.Wrappers.PageResponse;
using MediatR;

namespace HepsiAPI.Application.Features.ProductFeatures.Queries.GetPaginationProducts
{
    public class GetPaginatedProductsQueryRequest : IRequest<PageResponse<GetProductQueryResponse>>
    {
        public RequestParameters RequestParameters { get; set; }
    }

    public class GetPaginatedProductsQueryHandler : IRequestHandler<GetPaginatedProductsQueryRequest, PageResponse<GetProductQueryResponse>>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;

        public GetPaginatedProductsQueryHandler(IProductReadRepository productReadRepository, IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _mapper = mapper;
        }

        public async Task<PageResponse<GetProductQueryResponse>> Handle(GetPaginatedProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productReadRepository.GetPaginatedListAsync(request.RequestParameters.PageIndex, request.RequestParameters.PageSize, null, null, p => p.CategoryProducts);

            var response = _mapper.Map<List<GetProductQueryResponse>>(products);

            var count = await _productReadRepository.CountAsync();

            return new PageResponse<GetProductQueryResponse>(response, count, request.RequestParameters.PageIndex, request.RequestParameters.PageSize);
        }
    }


}
