using FluentValidation;

namespace VendingMachine.Application.Services.Product.Foods.Commands.CreateFood
{
    public class CreateFoodCommandValidator : AbstractValidator<CreateFoodCommand>
    {
        public CreateFoodCommandValidator()
        {
            
            RuleFor(v => v.Dto.Title)
                .MaximumLength(200)
                .NotEmpty();
            RuleFor(v => v.Dto.Price)
                .GreaterThanOrEqualTo(0)
                .NotEmpty();

        }
    }
}
