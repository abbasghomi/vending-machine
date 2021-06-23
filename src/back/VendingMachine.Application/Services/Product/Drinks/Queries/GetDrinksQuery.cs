using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Services.Product.Drinks.ViewModels;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services.Product.Drinks.Queries
{
    public class GetDrinksQuery : IRequest<DrinksViewModel>
    {
    }

    public class GetDrinksQueryHandler : IRequestHandler<GetDrinksQuery, DrinksViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDrinksQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DrinksViewModel> Handle(GetDrinksQuery request, CancellationToken cancellationToken)
        {

            return new DrinksViewModel
            {
                Lists = await _context.GetDbSet<Drink>()
                    . ProjectTo<DrinkDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Title)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
