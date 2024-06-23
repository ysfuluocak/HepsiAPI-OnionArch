using FluentValidation;

namespace HepsiAPI.Application.Features.ProductFeatures.Commands.UpdateProduct
{
    public class UpdateProductCommandRequestValidator : AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandRequestValidator()
        {
            RuleFor(p => p.Id).NotEmpty();

            RuleFor(p => p.Id).GreaterThan(0);

            RuleFor(p => p.Title).NotEmpty();

            RuleFor(p => p.Description).NotEmpty();

            RuleFor(p => p.BrandId).GreaterThan(0);

            RuleFor(p => p.Price).GreaterThan(0);

            RuleFor(p => p.Discount).GreaterThanOrEqualTo(0);

            RuleFor(p => p.CategoryIds).NotEmpty()
                .Must(categories => categories.Any());
        }
    }
}
