using VendingMachine.Domain.DTOs;

namespace VendingMachine.Domain.Entities
{
    public class Payment : PaymentDto
    {

        public virtual Invoice Invoice { get; set; }

    }
}
