using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Common;
using NLog;

namespace LegionOfCommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
			SetUplogger();
			CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

		private static void SetUplogger()
		{
			string date = $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}";
			InternalLogger.LogToConsole = true;
			InternalLogger.LogFile = "Q:\\Legion Of Commerce\\LegionOfCommerce\\LegionOfCommerce\\logs\\internal_logfile.txt";
			InternalLogger.LogWriter = new StringWriter();
			InternalLogger.LogLevel = NLog.LogLevel.Trace;
			var config = new NLog.Config.LoggingConfiguration();
			var logfile = new NLog.Targets.FileTarget("logfile")
			{
				FileName = $"Q:\\Legion Of Commerce\\LegionOfCommerce\\LegionOfCommerce\\logs\\{date}_logfile.txt"
			};
			config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, logfile);
			NLog.LogManager.Configuration = config;
		}

	}
}
