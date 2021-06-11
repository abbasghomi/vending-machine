using System;
using System.ComponentModel.DataAnnotations;

namespace VendingMachine.Domain.Common
{
    public abstract class AuditableEntity
    {
        public int CreatedBy { get; set; } = -1;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int LastModifiedBy { get; set; } = -1;
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; } = false;

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
