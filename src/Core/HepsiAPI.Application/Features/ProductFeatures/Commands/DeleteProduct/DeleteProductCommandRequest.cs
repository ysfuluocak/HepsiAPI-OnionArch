using HepsiAPI.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace HepsiAPI.Application.Features.ProductFeatures.Commands.DeleteProduct
{
    public class DeleteProductCommandRequest : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var deletedProduct = await _unitOfWork.GetProductReadRepository.GetAsync(false, p => p.Id == request.Id);

            deletedProduct.IsDeleted = true;

            await _unitOfWork.GetProductWriteRepository.UpdateAsync(deletedProduct);

            await _unitOfWork.SaveAsync();
        }
    }
}
