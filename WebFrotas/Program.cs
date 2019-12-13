using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace WebFrotas
{
    public class Program
    {
        public static void Main(string[] args)
        {

            string caminhoLog=Path.Combine(Directory.GetCurrentDirectory(), "Logs.txt");

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.File(caminhoLog)
            .CreateLogger();

            try
            {
                Log.Information("----Servidor Iniciando----");
                CreateWebHostBuilder(args).Build().Run();
                
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "----Servidor Terminou Inesperadamente----");
                
            }
            finally
            {
                Log.CloseAndFlush();
            }

            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();
    }
}
