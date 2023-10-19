
using EventBus.Consts;
using MassTransit;
using Products.Consumers;

namespace Products
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Rebus
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<ReserveProductsConsumer>();

                var configuration = builder.Configuration;

                x.UsingRabbitMq((context, transport) =>
                {
                    transport.Host(new Uri(configuration["RabbitMQ:Host"]), "/", host =>
                    {
                        host.Username(configuration["RabbitMQ:User"]);
                        host.Password(configuration["RabbitMQ:Password"]);
                    });

                    transport.ReceiveEndpoint(QueueConst.ReserveProductsQueue, x =>
                    {
                        x.ConfigureConsumer<ReserveProductsConsumer>(context);
                    });


                    transport.ConfigureEndpoints(context);
                });
            });

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
        }
    }
}