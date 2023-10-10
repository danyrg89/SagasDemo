using EventBus.Messages.OrderMessages;
using MassTransit;

namespace Orders.Consumers
{
    public class CreateOrderEventConsumer : IConsumer<CreateOrderMessage>
    {
        public Task Consume(ConsumeContext<CreateOrderMessage> context)
        {
            Console.WriteLine("new message!");
            return Task.CompletedTask;
        }
    }
}
