using System.Collections.Generic;
using VendingMachine.Domain.DTOs;

namespace VendingMachine.Application.Services.Order.Invoices.ViewModels
{
    public class InvoicesViewModel
    {
        public InvoiceDto Dto { get; set; }
        public IList<InvoiceDto> Lists { get; set; }
    }
}
