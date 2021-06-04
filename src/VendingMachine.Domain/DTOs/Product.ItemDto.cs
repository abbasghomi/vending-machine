using VendingMachine.Domain.Common;

namespace VendingMachine.Domain.DTOs
{
    public class ItemDto : AuditableEntity
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDrink { get; set; }
        public decimal Price { get; set; }

    }
}
