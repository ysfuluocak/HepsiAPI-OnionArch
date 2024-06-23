using HepsiAPI.Application.Features.ProductFeatures.Rules;
using HepsiAPI.Application.Interfaces.Repositories.ProductRepositories;
using MediatR;

namespace HepsiAPI.Application.Features.ProductFeatures.Commands.DeleteProduct
{
    public class DeleteProductCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, Unit>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly ProductBusinessRules _productBusinessRules;
        public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, ProductBusinessRules productBusinessRules)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductExistsAsync(request.Id);

            var deletedProduct = await _productReadRepository.GetSingleEntityAsync(p => p.Id == request.Id);

            deletedProduct.IsDeleted = true;

            await _productWriteRepository.UpdateAsync(deletedProduct);


            return Unit.Value;
        }
    }
}
