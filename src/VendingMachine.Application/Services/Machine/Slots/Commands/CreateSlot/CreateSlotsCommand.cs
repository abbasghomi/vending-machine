using AutoMapper;
using MediatR;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace VendingMachine.Application.Services.Machine.Slots.Commands.CreateSlot
{
    public class CreateSlotsCommand : IRequest<int>
    {
        public SlotDto Dto { get; set; }
    }

    public class CreateSlotCommandHandler : IRequestHandler<CreateSlotsCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateSlotCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateSlotsCommand request, CancellationToken cancellationToken)
        {

            Slot entity = _mapper.Map<Slot>(request.Dto);
            await _context.GetDbSet<Slot>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
