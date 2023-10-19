using EventBus.Consts;
using EventBus.Events.OrderEvents;
using EventBus.Messages.OrderMessages;
using MassTransit;
using MassTransit.Transports;

namespace Orders.Consumers
{
    public class CreateOrderMessageConsumer : IConsumer<CreateOrderMessage>
    {
        public CreateOrderMessageConsumer(IPublishEndpoint publishEndpointProvider)
        {
            PublishEndpointProvider = publishEndpointProvider;
        }

        public IPublishEndpoint PublishEndpointProvider { get; }

        public async Task Consume(ConsumeContext<CreateOrderMessage> context)
        {

            /// INICIA TRANSACCION CREANDO LA ORDEN EN LA BASE DE DATOS DEL MICROSERVICIO DE ORDENES
            /// ...
            Thread.Sleep(3000);
            /// ...
            /// FINALIZA TRANSACCION CREANDO LA ORDEN EN LA BASE DE DATOS DEL MICROSERVICIO DE ORDENES

            var message = new OrderInitiatedEvent(context.Message.CorrelationId, context.Message.Order);

            await PublishEndpointProvider.Publish(message);
        }
    }
}
