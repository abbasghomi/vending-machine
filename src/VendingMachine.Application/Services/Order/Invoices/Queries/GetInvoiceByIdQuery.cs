using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VendingMachine.Application.Common.Exceptions;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services.Order.Invoices.Queries
{
    public class GetInvoiceByIdQuery : IRequest<InvoiceDto>
    {
        public int Id { get; set; }
    }

    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetInvoiceByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InvoiceDto> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetDbSet<Invoice>().FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Invoice), request.Id);
            }

            return _mapper.Map<Invoice>(entity);
        }
    }
}
