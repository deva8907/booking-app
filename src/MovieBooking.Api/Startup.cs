using MongoDB.Driver;
using MovieBooking.Core;

namespace MovieBooking.Api;

public class Startup(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        services.ConfigureCoreServices(_configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMongoClient mongoClient)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieBooking.Api v1"));
        }
    }
}
