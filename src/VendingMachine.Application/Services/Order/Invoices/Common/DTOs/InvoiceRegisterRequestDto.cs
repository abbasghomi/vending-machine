using VendingMachine.Domain.DTOs;

namespace Application.Services.Order.Invoices.Common.DTOs
{
    public class InvoiceRegisterRequestDto
    {

        public InvoiceDto InvoiceData { get; set; }
        public int? RefundedInvoiceId { get; set; }
        public PaymentDto PaymentData { get; set; }

    }
}
