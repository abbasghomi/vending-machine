using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VendingMachine.Application.Common.Exceptions;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services.Order.Invoices.Commands.DeleteInvoice
{
    public class DeleteInvoiceCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteInvoiceItemCommandHandler : IRequestHandler<DeleteInvoiceCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteInvoiceItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetDbSet<Invoice>().FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Invoice), request.Id);
            }

            var paymentEntity = _context.GetDbSet<Payment>().Where(ent => ent.InvoiceId == entity.Id).FirstOrDefault();

            _context.GetDbSet<Invoice>().Remove(entity);
            if (paymentEntity != null)
            {
                paymentEntity.InvoiceId = -1;
                _context.GetDbSet<Payment>().Update(paymentEntity);
            }

            if (entity.RefundedInvoiceId != null)
            {
                var refundedInvoice = await _context.GetDbSet<Invoice>().FindAsync(entity.RefundedInvoiceId.Value);
                if (refundedInvoice != null)
                {
                    refundedInvoice.IsRefunded = false;
                    _context.GetDbSet<Invoice>().Update(refundedInvoice);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
