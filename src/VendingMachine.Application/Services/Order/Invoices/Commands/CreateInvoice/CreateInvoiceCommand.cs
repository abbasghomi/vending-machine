using Application.Services.Order.Invoices.Common.DTOs;
using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services.Order.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommand : IRequest<int>
    {
        public InvoiceRegisterRequestDto Dto { get; set; }
    }

    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateInvoiceCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {

            Invoice invoiceEntity = _mapper.Map<Invoice>(request.Dto.InvoiceData);
            Payment paymentEntity = _mapper.Map<Payment>(request.Dto.PaymentData);
            await _context.GetDbSet<Invoice>().AddAsync(invoiceEntity, cancellationToken);
            if (request.Dto.RefundedInvoiceId != null)
            {
                var refundedInvoice = _context.GetDbSet<Invoice>()
                    .Where(ent => ent.Id == request.Dto.RefundedInvoiceId.Value)
                    .FirstOrDefault();

                if (refundedInvoice == null)
                {
                    refundedInvoice.IsRefunded = true;
                    _context.GetDbSet<Invoice>().Update(refundedInvoice);
                }
            }
            await _context.SaveChangesAsync(cancellationToken);

            paymentEntity.InvoiceId = invoiceEntity.Id;
            await _context.GetDbSet<Payment>().AddAsync(paymentEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return invoiceEntity.Id;
        }
    }
}
