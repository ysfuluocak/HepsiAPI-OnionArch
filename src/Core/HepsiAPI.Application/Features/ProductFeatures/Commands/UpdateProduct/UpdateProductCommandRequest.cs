using AutoMapper;
using HepsiAPI.Application.Features.ProductFeatures.Queries.GetProduct;
using HepsiAPI.Application.Interfaces.UnitOfWorks;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetProductQueryResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var existingProduct = await _unitOfWork.GetProductReadRepository.GetAsync(true, p => p.Id == request.Id, p => p.CategoryProducts);

            await _unitOfWork.GetCategoryProductWriteRepository.DeleteRangeAsync(existingProduct.CategoryProducts);

            var updatedProduct = _mapper.Map(request, existingProduct);

            var response = await _unitOfWork.GetProductWriteRepository.UpdateAsync(updatedProduct);

            await _unitOfWork.SaveAsync();

            response = await _unitOfWork.GetProductReadRepository.GetProductByIdWithDetail(response.Id);

            return _mapper.Map<GetProductQueryResponse>(response);
        }
    }

}
