using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace item_workflow
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // Use static logging to allow non injected logging to support the workflow test framework
            // Serilog allows static logging
            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Verbose()
              .Enrich.FromLogContext()
              .WriteTo.Console(LogEventLevel.Information)
              //.WriteTo.RollingFile("Logs/MainLog-{Date}.log", LogEventLevel.Verbose)
              .CreateLogger();

            Log.Information("[MAIN] Starting Application.");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
