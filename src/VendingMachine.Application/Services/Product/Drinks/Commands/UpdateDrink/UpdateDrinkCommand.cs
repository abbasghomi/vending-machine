using VendingMachine.Application.Common.Exceptions;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VendingMachine.Domain.DTOs;

namespace VendingMachine.Application.Services.Product.Drinks.Commands.UpdateDrink
{
    public partial class UpdateInvoiceCommand : IRequest
    {
        public DrinkDto Dto { get; set; }

    }

    public class UpdateDrinkItemCommandHandler : IRequestHandler<UpdateInvoiceCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateDrinkItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetDbSet<Drink>().FindAsync(request.Dto.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Drink), request.Dto.Id);
            }

            entity.Title = request.Dto.Title;

            _context.GetDbSet<Drink>().Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
