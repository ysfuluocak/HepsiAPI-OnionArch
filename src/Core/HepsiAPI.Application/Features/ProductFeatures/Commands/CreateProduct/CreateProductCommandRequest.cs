using AutoMapper;
using HepsiAPI.Application.Features.ProductFeatures.Rules;
using HepsiAPI.Application.Interfaces.Repositories.ProductRepositories;
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
        private readonly ProductBusinessRules _productRules;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IMapper mapper, ProductBusinessRules productRules)
        {
            _productWriteRepository = productWriteRepository;
            _mapper = mapper;
            _productRules = productRules;
        }

        public async Task<int> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productRules.ProductTitleCanNotBeDuplicatedWhenInserted(request.Title);

            var addedProduct = _mapper.Map<Product>(request);

            await _productWriteRepository.AddAsync(addedProduct);
            await _productWriteRepository.CommitAsync();

            return addedProduct.Id;

        }
    }

}