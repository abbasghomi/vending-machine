using AutoMapper;
using MediatR;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace VendingMachine.Application.Services.Product.Foods.Commands.CreateFood
{
    public class CreateFoodCommand : IRequest<int>
    {
        public FoodDto Dto { get; set; }
    }

    public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateFoodCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
        {

            Food entity = _mapper.Map<Food>(request.Dto);
            await _context.GetDbSet<Food>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
