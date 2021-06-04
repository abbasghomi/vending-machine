using AutoMapper;
using VendingMachine.Domain.Common.Mappings;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Domain.DTOs
{
    public class FoodDto : ItemDto, IMapFrom<Food>
    {

        public FoodDto()
        {
            IsDrink = false;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Food, FoodDto>().ReverseMap();
        }

    }
}
