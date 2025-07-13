using RadioManager.Application;
using RadioManager.Domain;
using RadioManager.Infrastructure;
namespace RadioManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services
                .AddApplication()
                .AddDomain()
                .AddInfrastructure(builder.Configuration, builder.Host);

            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseInfrastructure();
            app.Run();
        }
    }
}
