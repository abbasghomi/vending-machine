﻿using FluentValidation;

namespace VendingMachine.Application.Services.Product.Drinks.Commands.UpdateDrink
{
    public class UpdateInvoiceCommandValidator : AbstractValidator<UpdateInvoiceCommand>
    {
        public UpdateInvoiceCommandValidator()
        {

            RuleFor(v => v.Dto.Title)
                .MaximumLength(200)
                .NotEmpty();
            RuleFor(v => v.Dto.Price)
                .GreaterThanOrEqualTo(0)
                .NotEmpty();
            RuleFor(v => v.Dto.SugarAmount)
                .GreaterThanOrEqualTo(0)
                .NotEmpty();

        }
    }
}
