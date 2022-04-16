using System;
using Entities.Enums;

namespace Entities.Models
{
    public class Payment
    {
        public int Id { get; set; }
        
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentType PaymentType { get; set; }
        
        public string CustomerCard { get; set; }
        public decimal Amount { get; set; }
        
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateUpdated { get; set; }
    }
}