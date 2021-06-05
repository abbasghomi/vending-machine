using FluentValidation;

namespace VendingMachine.Application.Services.Product.Foods.Commands.UpdateFood
{
    public class UpdateFoodCommandValidator : AbstractValidator<UpdateFoodCommand>
    {
        public UpdateFoodCommandValidator()
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
