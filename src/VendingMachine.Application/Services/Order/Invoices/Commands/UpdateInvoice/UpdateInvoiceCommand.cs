using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VendingMachine.Application.Common.Exceptions;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services.Order.Invoices.Commands.UpdateInvoice
{
    public partial class UpdateInvoiceCommand : IRequest
    {
        public InvoiceDto Dto { get; set; }

    }

    public class UpdateInvoiceItemCommandHandler : IRequestHandler<UpdateInvoiceCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateInvoiceItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetDbSet<Invoice>().FindAsync(request.Dto.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Invoice), request.Dto.Id);
            }

            entity.Date = request.Dto.Date;

            _context.GetDbSet<Invoice>().Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
