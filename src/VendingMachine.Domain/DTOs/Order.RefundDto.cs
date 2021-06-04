using AutoMapper;
using VendingMachine.Domain.Common;
using VendingMachine.Domain.Common.Mappings;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Domain.DTOs
{
    public class RefundDto : AuditableEntity, IMapFrom<Refund>
    {

        public int Id { get; set; }
        public int Amount { get; set; }
        public int ItemId { get; set; }
        public int ItemPrice { get; set; }

        public int InvoiceId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Refund, RefundDto>().ReverseMap();
        }

    }
}
