using FluentValidation;

namespace Product.Api.Features.Commands;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.StockQuantity).GreaterThan(0);
    }
}
