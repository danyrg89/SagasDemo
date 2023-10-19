using EventBus.Consts;
using EventBus.Messages.OrderMessages;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Orders.Consumers;
using Orders.DbContext;
using Orders.Sagas;
using Orders.StateMachineInstances;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{

    var configuration = builder.Configuration;

    x.AddConsumer<CreateOrderMessageConsumer>();
    x.AddConsumer<OrderCompletedMessageConsumer>();

    x.AddSagaStateMachine<CreateOrderStateMachine, CreateOrderStateMachineInstance>().EntityFrameworkRepository(opt =>
    {
        opt.AddDbContext<DbContext, StateMachineDbContext>((provider, builder) =>
        {
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => { 
                    m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name); 
                });
        });

        opt.ConcurrencyMode = ConcurrencyMode.Optimistic;
    });

    x.UsingRabbitMq((context, transport) =>
    {
        transport.Host(new Uri(configuration["RabbitMQ:Host"]), "/", host =>
        {
            host.Username(configuration["RabbitMQ:User"]);
            host.Password(configuration["RabbitMQ:Password"]);
        });


        transport.ReceiveEndpoint(QueueConst.StartOrderCreationQueue, x =>
        {
            x.ConfigureSaga<CreateOrderStateMachineInstance>(context);
        });

        transport.ReceiveEndpoint(QueueConst.CreateOrderQueue, x =>
        {
            x.ConfigureConsumer<CreateOrderMessageConsumer>(context);
        });

        transport.ReceiveEndpoint(QueueConst.OrderCompletedQueue, x =>
        {
            x.ConfigureConsumer<OrderCompletedMessageConsumer>(context);
        });

        //transport.ConfigureEndpoints(context);
    });

});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
