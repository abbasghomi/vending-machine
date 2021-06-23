using VendingMachine.Application.Common.Exceptions;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace VendingMachine.Application.Services.Machine.Slots.Commands.DeleteSlot
{
    public class DeleteSlotCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteSlotItemCommandHandler : IRequestHandler<DeleteSlotCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteSlotItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSlotCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetDbSet<Slot>().FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Slot), request.Id);
            }

            _context.GetDbSet<Slot>().Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
