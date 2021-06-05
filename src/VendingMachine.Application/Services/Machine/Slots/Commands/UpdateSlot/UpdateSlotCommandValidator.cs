using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Constants;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Domain.DTOs;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services.Machine.Slots.Commands.UpdateSlot
{
    public class UpdateSlotCommandValidator : AbstractValidator<UpdateSlotCommand>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSlotCommandValidator(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

            RuleFor(v => v.Dto.SlotNumber)
                .MaximumLength(200)
                .NotEmpty();
            RuleFor(v => v.Dto.Quantity)
                .GreaterThanOrEqualTo(0)
                .NotEmpty();
            //based on project requirements
            //We have 20 food slots and 10 drink slots limit
            RuleFor(v => v.Dto.IsDrink)
                .MustAsync((isDrink, cancellation) => CheckIfChangeInSlotsIsPossible(isDrink, cancellation));

        }

        private async Task<bool> CheckIfChangeInSlotsIsPossible(bool isDrink, CancellationToken cancellationToken)
        {
            bool result = true;

            var slots = await _context.GetDbSet<Slot>()
                    .ProjectTo<SlotDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.SlotNumber)
                    .ToListAsync(cancellationToken);
            
            if (slots.Any())
            {
                if ((isDrink && slots.Count(ent => ent.IsDrink == true) >= Configs.DrinkCount) ||
                (!isDrink && slots.Count(ent => ent.IsDrink == false) >= Configs.FoodCount))
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
