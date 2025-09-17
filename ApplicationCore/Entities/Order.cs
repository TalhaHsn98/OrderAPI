using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Order_Date { get; set; }

        public string CustomerId { get; set; } = default!;
        public string CustomerName { get; set; } = default!;

        public int? PaymentMethodId { get; set; }
        public string? PaymentName { get; set; }

        public string? ShippingAddress { get; set; }
        public string? ShippingMethod { get; set; }

        public decimal BillAmount { get; set; }
        public string? Order_Status { get; set; }

        public ICollection<OrderDetail> Order_Details { get; set; } = new List<OrderDetail>();
    }
}
