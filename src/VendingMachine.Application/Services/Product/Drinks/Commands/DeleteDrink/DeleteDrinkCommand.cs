using VendingMachine.Application.Common.Exceptions;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace VendingMachine.Application.Services.Product.Drinks.Commands.DeleteDrink
{
    public class DeleteInvoiceCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteDrinkItemCommandHandler : IRequestHandler<DeleteInvoiceCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteDrinkItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetDbSet<Drink>().FindAsync(request.Id);
            var slotEntity = _context.GetDbSet<Slot>().Where(ent => ent.ItemId == entity.Id).ToList();
            slotEntity.ForEach(ent => { if (ent.ItemId == entity.Id) { ent.ItemId = -1; } });

            if (entity == null)
            {
                throw new NotFoundException(nameof(Drink), request.Id);
            }

            _context.GetDbSet<Drink>().Remove(entity);
            //remove deleted item from machine slots
            _context.GetDbSet<Slot>().UpdateRange(slotEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
