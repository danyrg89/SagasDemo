using EventBus.Events.OrderEvents;
using EventBus.Messages.NotificationMessages;
using EventBus.Messages.OrderMessages;
using MassTransit;
using Orders.StateMachineInstances;

namespace Orders.Sagas
{

    public class CreateOrderStateMachine : MassTransitStateMachine<CreateOrderStateMachineInstance>
    {

        // Commands
        public Event<StartOrderCreationSagaMessage> StartOrderCreationSagaMessage { get; set; }

        public Event<CreateOrderMessage> CreateOrderMessage { get; set; }

        public Event<NotifyOrderCreatedMessage> NotifyOrderCreatedMessage { get; set; }

        // Events
        public Event<OrderCreatedEvent> OrderCreatedEvent { get; set; }

        public Event<OrderCreationFailEvent> OrderCreationFailEvent { get; set; }

        // States
        private State PlacingOrder { get; set; }

        private State OrderCreated { get; set; }

        private State PaymentProcessed { get; set; }


        public CreateOrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Initially(
                When(StartOrderCreationSagaMessage)
                    .Then(context => {
                        Console.WriteLine("Message received!!");

                    })
                    .Send(context => new CreateOrderMessage(context.Message.CorrelationId, context.Message.OrderId))
                    .TransitionTo(PlacingOrder));
        }
    }
}
