using FluentValidation;

namespace HepsiAPI.Application.Features.ProductFeatures.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
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
