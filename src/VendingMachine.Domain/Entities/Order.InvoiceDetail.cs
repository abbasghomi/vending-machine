using VendingMachine.Domain.DTOs;

namespace VendingMachine.Domain.Entities
{
    public class InvoiceDetail : InvoiceDetailDto
    {

        public virtual Invoice Invoice { get; set; }

    }
}
