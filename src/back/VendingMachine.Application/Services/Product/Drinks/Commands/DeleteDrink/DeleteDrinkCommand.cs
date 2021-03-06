using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VendingMachine.Application.Common.Exceptions;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services.Product.Drinks.Commands.DeleteDrink
{
    public class DeleteDrinkCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteDrinkItemCommandHandler : IRequestHandler<DeleteDrinkCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteDrinkItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteDrinkCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetDbSet<Drink>().FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Drink), request.Id);
            }

            var slotEntity = _context.GetDbSet<Slot>().Where(ent => ent.ItemId == entity.Id).ToList();
            slotEntity.ForEach(ent => { if (ent.ItemId == entity.Id) { ent.ItemId = -1; } });

            _context.GetDbSet<Drink>().Remove(entity);
            //remove deleted item from machine slots
            _context.GetDbSet<Slot>().UpdateRange(slotEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
