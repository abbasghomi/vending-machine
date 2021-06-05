using System.Collections.Generic;
using VendingMachine.Domain.DTOs;

namespace VendingMachine.Application.Services.Machine.Slots.ViewModels
{
    public class SlotsViewModel
    {
        public SlotDto Dto { get; set; }
        public IList<SlotDto> Lists { get; set; }
    }
}
