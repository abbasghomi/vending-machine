using System.Collections.Generic;
using VendingMachine.Domain.DTOs;

namespace VendingMachine.Application.Services.Product.Drinks.ViewModels
{
    public class InvoicesViewModel
    {
        public DrinkDto Dto { get; set; }
        public IList<DrinkDto> Lists { get; set; }
    }
}
