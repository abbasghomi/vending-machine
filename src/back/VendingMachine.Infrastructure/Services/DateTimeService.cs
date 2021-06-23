using System;
using VendingMachine.Application.Common.Interfaces;

namespace VendingMachine.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
