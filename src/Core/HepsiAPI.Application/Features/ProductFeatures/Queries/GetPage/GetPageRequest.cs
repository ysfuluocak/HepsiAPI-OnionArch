using AutoMapper;
using HepsiAPI.Application.Features.ProductFeatures.Queries.GetProduct;
using HepsiAPI.Application.Interfaces.Repositories.ProductRepositories;
using HepsiAPI.Application.Wrappers.PageResponse;
using MediatR;

namespace HepsiAPI.Application.Features.ProductFeatures.Queries.GetPage
{
    public class GetPageQueryRequest : IRequest<PageResponse<List<GetProductQueryResponse>>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetPageQueryHandler : IRequestHandler<GetPageQueryRequest, PageResponse<List<GetProductQueryResponse>>>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;

        public GetPageQueryHandler(IProductReadRepository productReadRepository, IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _mapper = mapper;
        }

        public async Task<PageResponse<List<GetProductQueryResponse>>> Handle(GetPageQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productReadRepository.GetPaginatedListAsync(request.PageIndex, request.PageSize, null, null, p => p.CategoryProducts);

            var response = _mapper.Map<List<GetProductQueryResponse>>(products);

            var count = await _productReadRepository.CountAsync();

            return new PageResponse<List<GetProductQueryResponse>>(response, count, request.PageIndex, request.PageSize);
        }
    }


}
