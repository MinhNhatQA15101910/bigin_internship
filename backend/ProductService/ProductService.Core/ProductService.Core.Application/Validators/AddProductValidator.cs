using FluentValidation;
using ProductService.Core.Application.Commands;

namespace ProductService.Core.Application.Validators;

public class AddProductValidator : AbstractValidator<AddProductCommand>
{
    public AddProductValidator()
    {
        RuleFor(x => x.AddProductDto.ProductName)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(50).WithMessage("Product name must not exceed 50 characters");

        RuleFor(x => x.AddProductDto.Category)
            .NotEmpty().WithMessage("Category is required")
            .MaximumLength(50).WithMessage("Category must not exceed 50 characters");

        RuleFor(x => x.AddProductDto.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.AddProductDto.Price)
            .NotEmpty().WithMessage("Price is required")
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.AddProductDto.StockQuantity)
            .NotEmpty().WithMessage("Stock quantity is required")
            .GreaterThan(0).WithMessage("Stock quantity must be greater than 0");
    }
}
