using System;
using System.IO;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Sinks.SystemConsole.Themes;

namespace DepartmentAutomation.Shared.Logger
{
    public static class ProjectLoggerConfiguration
    {
        public static ILogger GetLoggerConfiguration(string logFileName)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @$"logs\{logFileName}.txt"),
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 1_000_000,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                    theme: AnsiConsoleTheme.Code)
                .Filter.ByExcluding(
                    Matching.FromSource("IdentityServer4.AccessTokenValidation.IdentityServerAuthenticationHandler"))
                .CreateLogger();
        }
    }
}