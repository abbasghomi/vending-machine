using System.Collections.Generic;
using VendingMachine.Domain.DTOs;

namespace VendingMachine.Application.Services.Product.Foods.ViewModels
{
    public class FoodsViewModel
    {
        public FoodDto Dto { get; set; }
        public IList<FoodDto> Lists { get; set; }
    }
}
