using System.Linq;
using AutoMapper;
using MediatR;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Services.Product.Drinks.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace VendingMachine.Application.Services.Product.Drinks.Queries
{
    public class GetInvoicesQuery : IRequest<InvoicesViewModel>
    {
    }

    public class GetDrinksQueryHandler : IRequestHandler<GetInvoicesQuery, InvoicesViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDrinksQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InvoicesViewModel> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {

            return new InvoicesViewModel
            {
                Lists = await _context.GetDbSet<Drink>()
                    . ProjectTo<DrinkDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Title)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
