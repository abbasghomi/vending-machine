using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services.Product.Drinks.Commands.CreateDrink
{
    public class CreateDrinkCommand : IRequest<int>
    {
        public DrinkDto Dto { get; set; }
    }

    public class CreateDrinkCommandHandler : IRequestHandler<CreateDrinkCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateDrinkCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateDrinkCommand request, CancellationToken cancellationToken)
        {

            Drink entity = _mapper.Map<Drink>(request.Dto);
            await _context.GetDbSet<Drink>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
