using AutoMapper;
using MediatR;
using VendingMachine.Application.Common.Exceptions;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace VendingMachine.Application.Services.Product.Foods.Queries
{
    public class GetFoodByIdQuery : IRequest<FoodDto>
    {
        public int Id { get; set; }
    }

    public class GetFoodByIdQueryHandler : IRequestHandler<GetFoodByIdQuery, FoodDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFoodByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FoodDto> Handle(GetFoodByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetDbSet<Food>().FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Food), request.Id);
            }

            return _mapper.Map<Food>(entity);
        }
    }
}
