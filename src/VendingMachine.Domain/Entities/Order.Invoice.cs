using VendingMachine.Domain.DTOs;

namespace VendingMachine.Domain.Entities
{
    public class Invoice : InvoiceDto
    {

        public virtual Payment Payment { get; set; }
        public virtual InvoiceDetail InvoiceDetail { get; set; }
        public virtual Refund Refund { get; set; }

    }
}
