using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Infrastructure.Services
{
    public interface ILoggingService
    {
        ILogger Writer { get; }
    }
}
