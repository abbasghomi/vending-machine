using System.Linq;
using AutoMapper;
using MediatR;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Services.Product.Foods.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace VendingMachine.Application.Services.Product.Foods.Queries
{
    public class GetFoodsQuery : IRequest<FoodsViewModel>
    {
    }

    public class GetFoodsQueryHandler : IRequestHandler<GetFoodsQuery, FoodsViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFoodsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FoodsViewModel> Handle(GetFoodsQuery request, CancellationToken cancellationToken)
        {

            return new FoodsViewModel
            {
                Lists = await _context.GetDbSet<Food>()
                    . ProjectTo<FoodDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Title)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
