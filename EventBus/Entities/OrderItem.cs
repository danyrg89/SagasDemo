using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Entities
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
