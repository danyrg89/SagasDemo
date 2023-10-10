
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
            //builder.Services.AddRebus(rebus => rebus
            //    .Routing(r => r.TypeBased().MapAssemblyOf<Program>("eshop-queue"))
            //    .Transport(t =>
            //        t.UseRabbitMq(
            //            builder.Configuration.GetConnectionString("MessageBroker"),
            //            "eshop-queue"))
            //    .Sagas(s => s.StoreInSqlServer(
            //            builder.Configuration.GetConnectionString("Database"),
            //            "Sagas",
            //            "Sagas_Indexes"
            //        )
            //    ));

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