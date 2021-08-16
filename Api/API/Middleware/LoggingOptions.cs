using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace VacationTrackerApi.Middleware
{
    public class LoggingOptions
    {
        public Func<HttpContext, Dictionary<string, object>> GetLogScopeFunc { get; set; }
    }
}
