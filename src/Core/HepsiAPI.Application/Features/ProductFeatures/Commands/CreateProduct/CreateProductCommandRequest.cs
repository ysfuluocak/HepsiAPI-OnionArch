using AutoMapper;
using HepsiAPI.Application.Interfaces.UnitOfWorks;
using HepsiAPI.Domain.Entities;
using MediatR;

namespace HepsiAPI.Application.Features.ProductFeatures.Commands.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int BrandId { get; set; }
        public IEnumerable<int> CategoryIds { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var addedProduct = _mapper.Map<Product>(request);

            await _unitOfWork.GetProductWriteRepository.AddAsync(addedProduct);

            await _unitOfWork.SaveAsync();

            return addedProduct.Id;

        }
    }

}