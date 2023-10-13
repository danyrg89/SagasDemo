using EventBus.Consts;
using EventBus.Events.OrderEvents;
using EventBus.Messages.OrderMessages;
using MassTransit;
using MassTransit.Transports;

namespace Orders.Consumers
{
    public class CreateOrderEventConsumer : IConsumer<CreateOrderMessage>
    {
        public CreateOrderEventConsumer(ISendEndpointProvider sendEndpointProvider)
        {
            SendEndpointProvider = sendEndpointProvider;
        }

        public ISendEndpointProvider SendEndpointProvider { get; }

        public async Task Consume(ConsumeContext<CreateOrderMessage> context)
        {
            Console.WriteLine("new message!");
            var message = new OrderCreatedEvent(context.Message.CorrelationId, context.Message.OrderId);

            var sendEndpoint = await SendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueConst.CreateOrderQueueName}"));

            await sendEndpoint.Send(message);
        }
    }
}
