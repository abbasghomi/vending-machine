using System.Linq;
using AutoMapper;
using MediatR;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Services.Machine.Slots.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace VendingMachine.Application.Services.Machine.Slots.Queries
{
    public class GetSlotsQuery : IRequest<SlotsViewModel>
    {
    }

    public class GetSlotsQueryHandler : IRequestHandler<GetSlotsQuery, SlotsViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSlotsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SlotsViewModel> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
        {

            return new SlotsViewModel
            {
                Lists = await _context.GetDbSet<Slot>()
                    . ProjectTo<SlotDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.SlotNumber)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
