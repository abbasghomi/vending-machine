using AutoMapper;
using MediatR;
using VendingMachine.Application.Common.Exceptions;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace VendingMachine.Application.Services.Product.Drinks.Queries
{
    public class GetDrinkByIdQuery : IRequest<DrinkDto>
    {
        public int Id { get; set; }
    }

    public class GetDrinkByIdQueryHandler : IRequestHandler<GetDrinkByIdQuery, DrinkDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDrinkByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DrinkDto> Handle(GetDrinkByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetDbSet<Drink>().FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Drink), request.Id);
            }

            return _mapper.Map<Drink>(entity);
        }
    }
}
