using AutoMapper;
using MediatR;
using VendingMachine.Application.Common.Exceptions;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace VendingMachine.Application.Services.Machine.Slots.Queries
{
    public class GetSlotByIdQuery : IRequest<SlotDto>
    {
        public int Id { get; set; }
    }

    public class GetSlotByIdQueryHandler : IRequestHandler<GetSlotByIdQuery, SlotDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSlotByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SlotDto> Handle(GetSlotByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetDbSet<Slot>().FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Slot), request.Id);
            }

            return _mapper.Map<Slot>(entity);
        }
    }
}
