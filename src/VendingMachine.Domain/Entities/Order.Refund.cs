using VendingMachine.Domain.DTOs;

namespace VendingMachine.Domain.Entities
{
    public class Refund : RefundDto
    {

        public virtual Invoice Invoice { get; set; }

    }
}
