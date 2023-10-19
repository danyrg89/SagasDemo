using EventBus.Consts;
using EventBus.Entities;
using EventBus.Messages.OrderMessages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Orders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly ISendEndpointProvider sendEndpointProvider;

        public OrderController(ISendEndpointProvider sendEndpointProvider)
        {
            this.sendEndpointProvider = sendEndpointProvider;
        }

        [HttpGet(Name = "StartOrder")]

        public async Task<IActionResult> StartOrder()
        {
            string queueName = QueueConst.StartOrderCreationQueue;
            var order = new Order();
            order.OrderId = Guid.NewGuid();
            order.DeliveryAddress = "La ceja";
            order.UserId = 1;
            order.OrderItems = new List<OrderItem>(){
                new OrderItem() { OrderItemId = Guid.NewGuid(), ProductId = 1, Quantity = 4 },
                new OrderItem() { OrderItemId = Guid.NewGuid(), ProductId = 2, Quantity = 2 }
            };

            var message = new StartOrderCreationEvent(order.OrderId, order);

            string uri = $"queue:{queueName}";

            var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri(uri));

            await sendEndpoint.Send(message);
            return Json("Message sent");
        }
    }
}
