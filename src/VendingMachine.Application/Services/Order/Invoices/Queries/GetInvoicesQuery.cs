using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Services.Order.Invoices.ViewModels;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services.Order.Invoices.Queries
{
    public class GetInvoicesQuery : IRequest<InvoicesViewModel>
    {
    }

    public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, InvoicesViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetInvoicesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InvoicesViewModel> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {

            return new InvoicesViewModel
            {
                Lists = await _context.GetDbSet<Invoice>()
                    .ProjectTo<InvoiceDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Date)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
