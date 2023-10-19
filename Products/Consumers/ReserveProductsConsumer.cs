using EventBus.Events.OrderEvents;
using EventBus.EventsProductEvents;
using EventBus.Messages.PaymentMessages;
using MassTransit;

namespace Products.Consumers
{
    public class ReserveProductsConsumer : IConsumer<ReserveProductsMessage>
    {
        public ReserveProductsConsumer(IPublishEndpoint publishEndpointProvider)
        {
            PublishEndpointProvider = publishEndpointProvider;
        }

        public IPublishEndpoint PublishEndpointProvider { get; }

        public async Task Consume(ConsumeContext<ReserveProductsMessage> context)
        {

            /// INICIA TRANSACCION RESERVANDO LOS PRODUCTOS EN LA BASE DE DATOS DEL MICROSERVICIO DE PRODUCTOS
            /// ...
            Thread.Sleep(3000);
            /// ...
            /// FINALIZA TRANSACCION RESERVANDO LOS PRODUCTOS EN LA BASE DE DATOS DEL MICROSERVICIO DE PRODUCTOS


            var message = new ProductsReservedEvent(context.Message.CorrelationId, context.Message.Order);

            await PublishEndpointProvider.Publish<ProductsReservedEvent>(message);
        }
    }
}
