using MassTransit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Orders.StateMachineInstances;

namespace Orders.DbContext
{
    public class StateMachineMap : SagaClassMap<CreateOrderStateMachineInstance>
    {
        protected override void Configure(EntityTypeBuilder<CreateOrderStateMachineInstance> entity, ModelBuilder model)
        {

        }
    }
}
