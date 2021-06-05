using AutoMapper;
using MediatR;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace VendingMachine.Application.Services.Product.Drinks.Commands.CreateDrink
{
    public class CreateInvoiceCommand : IRequest<int>
    {
        public DrinkDto Dto { get; set; }
    }

    public class CreateDrinkCommandHandler : IRequestHandler<CreateInvoiceCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateDrinkCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {

            Drink entity = _mapper.Map<Drink>(request.Dto);
            await _context.GetDbSet<Drink>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
