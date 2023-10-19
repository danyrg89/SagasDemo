using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Consts
{
    public class QueueConst
    {
        public const string CreateOrderQueue = "CreateOrderQueue";

        public const string StartOrderCreationQueue = "StartOrderCreationQueue";

        public const string ReserveProductsQueue = "ReserveProductsQueue";

        public const string OrderCompletedQueue = "OrderCompletedQueue";
    }
}
