using AutoMapper;
using VendingMachine.Domain.Common;
using VendingMachine.Domain.Common.Mappings;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Domain.DTOs
{
    public class InvoiceDetailDto : AuditableEntity, IMapFrom<InvoiceDetail>
    {

        public int Id { get; set; }
        public int Amount { get; set; }
        public int ItemId { get; set; }
        public int ItemPrice { get; set; }

        public int InvoiceId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InvoiceDetail, InvoiceDetailDto>().ReverseMap();
        }

    }
}
