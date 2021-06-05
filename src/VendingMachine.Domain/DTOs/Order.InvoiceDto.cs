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
        public decimal TotalPrice { get; set; }
        public decimal TotalRefund { get; set; }
        public bool IsRefunded { get; set; } = false;

        public int InvoiceDetailId { get; set; }
        public int? RefundID { get; set; }
        public int PaymentId { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Invoice, InvoiceDto>().ReverseMap();
        }

    }
}
