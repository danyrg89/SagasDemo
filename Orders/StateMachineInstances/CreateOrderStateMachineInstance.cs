using EventBus.Entities;
using MassTransit;

namespace Orders.StateMachineInstances
{
    public class CreateOrderStateMachineInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public Order Order { get; set; }

        public string CurrentState { get; set; }
    }

}
