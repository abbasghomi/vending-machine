using AutoMapper;
using VendingMachine.Domain.Common.Mappings;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Domain.DTOs
{
    public class DrinkDto : ItemDto, IMapFrom<Drink>
    {

        public DrinkDto()
        {
            IsDrink = true;
        }

        public bool IsHotDrink { get; set; }
        public int SugarAmount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Drink, DrinkDto>().ReverseMap();
        }

    }
}
