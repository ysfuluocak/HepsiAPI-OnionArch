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
        public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var deletedProduct = await _productReadRepository.GetSingleEntityAsync(p => p.Id == request.Id);

            if (deletedProduct is null)
            {
                throw new Exception("YOK");
            }

            deletedProduct.IsDeleted = true;

            await _productWriteRepository.UpdateAsync(deletedProduct);


            return Unit.Value;
        }
    }
}
