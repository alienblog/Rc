using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Internal;
using Microsoft.Extensions.Configuration;

namespace Rc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddCommandLine(args);
            var config = builder.Build();
            
            using(var app = CreateWebHost(config)){
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("I'm running...");
                Console.ReadLine();
            }
        }
        
        private static IApplication CreateWebHost(IConfigurationRoot config){
            var webHostBuilder = new WebHostBuilder(config)
            .UseServer("Microsoft.AspNet.Server.WebListener")
            .Build();
            return webHostBuilder.Start();
        }
    }
}
