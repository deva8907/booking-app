using System.Diagnostics.CodeAnalysis;
using Serilog;

namespace MovieBooking.Api
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
            Log.Information("Movie booking api Starting...");

            try
            {
                CreateHostBuilder(args).Build()
                    .Run();
                Log.Information("Movie booking api Stopped...");
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An unhandled exception occured during Movie booking api bootstrapping....");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder AddApiLogger(this IHostBuilder builder)
        {
            builder.ConfigureLogging((hostingContext, logging) =>
            {
                var environment = hostingContext.Configuration["ASPNETCORE_ENVIRONMENT"];
                var logConfiguration = new LoggerConfiguration()
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Environment", environment)
                    .WriteTo.Console()
                    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day);

                Log.Logger = logConfiguration.CreateLogger();
            });
            return builder.UseSerilog();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .AddApiLogger()
                .ConfigureWebHostDefaults(builder => { builder.UseStartup<Startup>(); });
    }
}
