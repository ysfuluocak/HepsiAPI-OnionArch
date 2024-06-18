using AutoMapper;
using HepsiAPI.Application.Features.ProductFeatures.Queries.GetProduct;
using HepsiAPI.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace HepsiAPI.Application.Features.ProductFeatures.Queries.GetAllProducts
{
    public class GetAllProductsQueryRequest : IRequest<IEnumerable<GetProductQueryResponse>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IEnumerable<GetProductQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetProductQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.GetProductReadRepository.GetAllProductWithDetail();

            var response = _mapper.Map<List<GetProductQueryResponse>>(products);

            return response;
        }
    }

}
