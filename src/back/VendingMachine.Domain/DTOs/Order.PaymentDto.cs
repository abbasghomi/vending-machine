using AutoMapper;
using System;
using VendingMachine.Domain.Common;
using VendingMachine.Domain.Common.Mappings;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Domain.DTOs
{
    public class PaymentDto : AuditableEntity, IMapFrom<Payment>
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int PaymentType { get; set; }
        public decimal Amount { get; set; }

        public int InvoiceId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Payment, PaymentDto>().ReverseMap();
        }

    }
}
