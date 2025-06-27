using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserOrderApp.Model
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; set; }
        public string TransactionId { get; set; }
    }


}
