using AutoMapper;
using HepsiAPI.Application.Dtos;
using HepsiAPI.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace HepsiAPI.Application.Features.ProductFeatures.Queries.GetProduct
{
    public class GetProductQueryRequest : IRequest<GetProductQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest, GetProductQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetProductQueryResponse> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetProductReadRepository.GetProductByIdWithDetail(request.Id);

            var response = _mapper.Map<GetProductQueryResponse>(product);


            return response;
        }
    }

    public class GetProductQueryResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public BrandDto Brand { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }

    }
}
