using AsyncMediator.Extensions.DependencyInjection;
using AsyncMediatorEvents.Application.UseCases.Demo;
using AsyncMediatorEvents.WebApi.Presentation.Controllers;
using System.Reflection;

namespace AsyncMediatorEvents.WebApi;

public static class Program
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

        Assembly presentationAssembly = typeof(DemoController).Assembly;
        builder.Services.AddMvc().AddApplicationPart(presentationAssembly);

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
