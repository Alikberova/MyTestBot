using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IUB.Db;
using Serilog;

namespace IUB.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            try
            {
                CreateDbIfNotExists(host);
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((ctx, config) =>
                {
                    string path = @"Logs\iub.txt";

                    if (!ctx.HostingEnvironment.IsDevelopment())
                    {
                        path = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), path);
                    }
                    else
                    {
                        path = Path.Combine(@"bin\Debug", path);
                        config.WriteTo.Console();
                    }

                    //todo later only errors and warnings logs into file
                    config.WriteTo.File(path,
                            rollingInterval: RollingInterval.Day,
                            retainedFileCountLimit: 7);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void CreateDbIfNotExists(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ActivityContext>();
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An error occurred creating the DB.");
            }
        }
    }
}
