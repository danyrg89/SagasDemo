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

        public async Task<IActionResult> StartOrder(string queueName)
        {
            Guid orderId = Guid.NewGuid();
            var message = new StartOrderCreationSagaMessage(orderId, orderId);
            
            var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));

            await sendEndpoint.Send(message);
            return Json("Message sent");
        }
    }
}
