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
    x.AddConsumer<CreateOrderEventConsumer>();

    var configuration = builder.Configuration;


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

    //x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(configure =>
    //{
    //    configure.Host(new Uri(configuration["RabbitMQ:Host"]), "/", host =>
    //        {
    //            host.Username(configuration["RabbitMQ:User"]);
    //            host.Password(configuration["RabbitMQ:Password"]);
    //        });

    //    configure.ReceiveEndpoint(QueueConst.CreateOrderQueueName, e => { e.ConfigureSaga<CreateOrderStateMachineInstance>(provider); });

    //}));


    x.UsingRabbitMq((context, transport) =>
    {
        transport.Host(new Uri(configuration["RabbitMQ:Host"]), "/", host =>
        {
            host.Username(configuration["RabbitMQ:User"]);
            host.Password(configuration["RabbitMQ:Password"]);
        });

        transport.ReceiveEndpoint(QueueConst.CreateOrderQueueName, x =>
        {
            x.ConfigureSaga<CreateOrderStateMachineInstance>(context);
            x.ConfigureConsumer<CreateOrderEventConsumer>(context);
        });


        transport.ConfigureEndpoints(context);
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
