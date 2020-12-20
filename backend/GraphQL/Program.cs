using System;
using System.Diagnostics;
using System.IO;
using Data;
using GraphQL.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace GraphQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            try
            {
                var host = CreateHostBuilder(args).Build();

                if (environmentName != "Production")
                {
                    using (var serviceScope = host.Services.CreateScope())
                    {
                        var serviceProvider = serviceScope.ServiceProvider;

                        var databaseContext = serviceProvider.GetRequiredService<DatabaseContext>();

                        databaseContext.Database.Migrate();
                        DataSeeder.Seed(databaseContext).Wait();
                    }
                }

                Log.Information("Application starting");
                host.Run();
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
                webBuilder
                    .UseStartup<Startup>()
                    .UseSerilog((hostingContext, loggingConfiguration) => loggingConfiguration
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Application", "envoy")
                    .Enrich.WithProperty("MachineName", Environment.MachineName)
                    .Enrich.WithProperty("CurrentManagedThreadId", Environment.CurrentManagedThreadId)
                    .Enrich.WithProperty("OSVersion", Environment.OSVersion)
                    .Enrich.WithProperty("Version", Environment.Version)
                    .Enrich.WithProperty("UserName", Environment.UserName)
                    .Enrich.WithProperty("ProcessId", Process.GetCurrentProcess().Id)
                    .Enrich.WithProperty("ProcessName", Process.GetCurrentProcess().ProcessName)
                    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                    .WriteTo.File(
                        formatter: new LogTextFormatter(),
                        path: Path.Combine(
                            hostingContext.HostingEnvironment.ContentRootPath +
                            $"{Path.DirectorySeparatorChar}Logs{Path.DirectorySeparatorChar}",
                            $"envoy_log_{DateTime.Now:yyyyMMdd}.txt"
                        )
                    ).ReadFrom.Configuration(hostingContext.Configuration)
                )
            );
    }
}
