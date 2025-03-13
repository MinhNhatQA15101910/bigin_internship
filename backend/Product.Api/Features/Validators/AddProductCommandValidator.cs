using FluentValidation;
using Product.Api.Features.Commands;

namespace Product.Api.Features.Validators;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.AddUpdateProductDto.ProductName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.AddUpdateProductDto.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.AddUpdateProductDto.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.AddUpdateProductDto.StockQuantity).GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than or equal to 0");
    }
}
