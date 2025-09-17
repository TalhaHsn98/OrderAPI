using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int Order_Id { get; set; }


        public int Product_Id { get; set; }
        public string Product_name { get; set; } = default!;
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }

        public Order Order { get; set; } = default!;
    }
}
