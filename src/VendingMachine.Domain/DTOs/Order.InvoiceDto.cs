using AutoMapper;
using System;
using VendingMachine.Domain.Common;
using VendingMachine.Domain.Common.Mappings;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Domain.DTOs
{
    public class InvoiceDto : AuditableEntity, IMapFrom<Invoice>
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ItemId { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal RefundAmount { get; set; }
        public bool IsRefunded { get; set; } = false;

        public int? RefundedInvoiceId { get; set; }
        public int PaymentId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Invoice, InvoiceDto>().ReverseMap();
        }

    }
}
