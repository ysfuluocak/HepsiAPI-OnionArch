using FluentValidation;

namespace HepsiAPI.Application.Features.ProductFeatures.Commands.DeleteProduct
{
    public class DeleteProductCommandRequestValidator : AbstractValidator<DeleteProductCommandRequest>
    {
        public DeleteProductCommandRequestValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Id).GreaterThan(0);
        }
    }
}
