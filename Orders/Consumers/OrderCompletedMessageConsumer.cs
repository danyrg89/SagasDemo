using EventBus.Consts;
using EventBus.Events.OrderEvents;
using EventBus.Messages.OrderMessages;
using MassTransit;
using MassTransit.Transports;

namespace Orders.Consumers
{
    public class OrderCompletedMessageConsumer : IConsumer<OrderCompletedMessage>
    {
        public OrderCompletedMessageConsumer(IPublishEndpoint publishEndpointProvider)
        {
            PublishEndpointProvider = publishEndpointProvider;
        }

        public IPublishEndpoint PublishEndpointProvider { get; }

        public async Task Consume(ConsumeContext<OrderCompletedMessage> context)
        {
            /// INICIA TRANSACCION ACTUALIZANDO EL ESTADO DE LA ORDEN EN LA BASE DE DATOS DEL MICROSERVICIO DE ORDENES
            /// ...
            Thread.Sleep(3000);

            if(context.Message.Order.UserId > 10)
            {
                var message = new OrderFinalizedEvent(context.Message.CorrelationId, context.Message.Order);
                await PublishEndpointProvider.Publish(message);
            }
            else
            {
                var message = new OrderFinalizedFailureEvent(context.Message.CorrelationId, context.Message.Order);
                await PublishEndpointProvider.Publish(message);
            }

            /// ...
            /// FINALIZA TRANSACCION ACTUALIZANDO EL ESTADO DE LA ORDEN EN LA BASE DE DATOS DEL MICROSERVICIO DE ORDENES

        }
    }
}
