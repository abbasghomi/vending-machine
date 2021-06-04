using System.Collections.Generic;
using VendingMachine.Domain.DTOs;

namespace VendingMachine.Domain.Entities
{
    public class Invoice : InvoiceDto
    {

        public virtual Payment Payment { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new HashSet<InvoiceDetail>();
        public virtual ICollection<Refund> Refunds { get; set; } = new HashSet<Refund>();

    }
}
