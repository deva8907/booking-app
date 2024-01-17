using MongoDB.Driver;
using MovieBooking.Core;

namespace MovieBooking.Api;

public class Startup(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureCoreServices(_configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMongoClient mongoClient)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieBooking.Api v1"));
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
