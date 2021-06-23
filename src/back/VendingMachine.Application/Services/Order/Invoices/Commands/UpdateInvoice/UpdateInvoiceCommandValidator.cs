using FluentValidation;

namespace VendingMachine.Application.Services.Order.Invoices.Commands.UpdateInvoice
{
    public class UpdateInvoiceCommandValidator : AbstractValidator<UpdateInvoiceCommand>
    {
        public UpdateInvoiceCommandValidator()
        {

            RuleFor(v => v.Dto.ItemPrice)
                .GreaterThanOrEqualTo(0)
                .NotEmpty();
            RuleFor(v => v.Dto.RefundAmount)
                .GreaterThanOrEqualTo(0)
                .NotEmpty();

        }
    }
}
