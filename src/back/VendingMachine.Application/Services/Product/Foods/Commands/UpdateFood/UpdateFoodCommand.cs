using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VendingMachine.Application.Common.Exceptions;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services.Product.Foods.Commands.UpdateFood
{
    public partial class UpdateFoodCommand : IRequest
    {
        public FoodDto Dto { get; set; }

    }

    public class UpdateFoodItemCommandHandler : IRequestHandler<UpdateFoodCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateFoodItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetDbSet<Food>().FindAsync(request.Dto.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Food), request.Dto.Id);
            }

            entity.Title = request.Dto.Title;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
