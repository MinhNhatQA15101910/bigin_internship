using Application.Commands.Products;
using FluentValidation;

namespace Application.Validators.Products;

public class AddProductValidator : AbstractValidator<AddProductCommand>
{
    public AddProductValidator()
    {
        RuleFor(x => x.AddProductDto.ProductName)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters");

        RuleFor(x => x.AddProductDto.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.AddProductDto.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.AddProductDto.StockQuantity)
            .GreaterThan(0).WithMessage("Stock quantity must be greater than 0");
    }
}
