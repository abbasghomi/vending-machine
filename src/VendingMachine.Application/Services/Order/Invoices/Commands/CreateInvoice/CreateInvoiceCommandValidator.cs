using AutoMapper;
using FluentValidation;
using System.Linq;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services.Order.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateInvoiceCommandValidator(IApplicationDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;

            RuleFor(v => v.Dto.InvoiceData.ItemPrice)
                .GreaterThanOrEqualTo(0)
                .NotEmpty();
            RuleFor(v => v.Dto.InvoiceData.RefundAmount)
                .GreaterThanOrEqualTo(0)
                .NotEmpty();
            RuleFor(v => v.Dto.RefundedInvoiceId)
                    .Must((refundedInvoiceId) => CheckIfInvoiceIsRefundable(refundedInvoiceId));


        }

        private bool CheckIfInvoiceIsRefundable(int? refundedInvoiceId)
        {
            bool result = true;

            if (refundedInvoiceId != null)
            {
                var refundedInvoice = _context.GetDbSet<Invoice>()
                    .Where(ent => ent.Id == refundedInvoiceId.Value && ent.IsRefunded == false)
                    .FirstOrDefault();

                if (refundedInvoice == null)
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
