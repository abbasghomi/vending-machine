using VendingMachine.Application.Common.Exceptions;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VendingMachine.Domain.DTOs;

namespace VendingMachine.Application.Services.Machine.Slots.Commands.UpdateSlot
{
    public partial class UpdateSlotCommand : IRequest
    {
        public SlotDto Dto { get; set; }

    }

    public class UpdateSlotItemCommandHandler : IRequestHandler<UpdateSlotCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSlotItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateSlotCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetDbSet<Slot>().FindAsync(request.Dto.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Slot), request.Dto.Id);
            }

            entity.ItemId = request.Dto.ItemId;
            entity.Quantity = request.Dto.Quantity;
            entity.IsDrink = request.Dto.IsDrink;
            entity.SlotNumber = request.Dto.SlotNumber;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
