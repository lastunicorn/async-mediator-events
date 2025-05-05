
using AsyncMediator;
using AsyncMediator.Extensions.DependencyInjection;
using AsyncMediatorEvents.Business.Events.Demo;
using AsyncMediatorEvents.Business.UseCases.Demo;
using System.Reflection;

namespace AsyncMediatorEvents.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            Assembly useCaseAssembly = typeof(DemoCommand).Assembly;
            builder.Services.AddAsyncMediator(useCaseAssembly);

            builder.Services.AddTransient<ICommandHandler<DemoCommand>, DemoCommandHandler>();
            builder.Services.AddTransient<IEventHandler<DemoEvent>, DemoEventHandler>();

            WebApplication app = builder.Build();

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
