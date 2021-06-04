using AutoMapper;
using VendingMachine.Domain.Common;
using VendingMachine.Domain.Common.Mappings;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Domain.DTOs
{
    public class SlotDto : AuditableEntity, IMapFrom<Slot>
    {

        public int Id { get; set; }
        public string SlotNumber { get; set; }
        public int Quantity { get; set; }
        public bool IsDrink { get; set; }
        public int ItemId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Slot, SlotDto>().ReverseMap();
        }

    }
}
