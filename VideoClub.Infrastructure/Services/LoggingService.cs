using Microsoft.ApplicationInsights.Extensibility;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Infrastructure.Services
{
    public class LoggingService : ILoggingService
    {
        public ILogger Writer => Log.Logger;

        public LoggingService()
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File(
                path: AppDomain.CurrentDomain.BaseDirectory + "Log/logs.txt",
                rollingInterval: RollingInterval.Day,
                shared: true,
                retainedFileCountLimit: 100,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
            )
            .WriteTo.File(
                formatter: new CompactJsonFormatter(),
                path: AppDomain.CurrentDomain.BaseDirectory + "Log/logs.json",
                rollingInterval: RollingInterval.Day,
                shared: true,
                retainedFileCountLimit: 100
            )
            .CreateLogger();
        }
    }
}
