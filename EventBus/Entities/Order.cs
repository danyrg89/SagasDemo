using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public int UserId { get; set; }

        public string DeliveryAddress { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
