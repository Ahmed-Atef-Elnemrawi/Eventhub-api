using EventHub.EventManagement.Application.Contracts.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace EventHub.EventManagement.Logging
{
   public static class LoggingServiceRegistration
   {
      public static void ConfigureLoggingService(this IServiceCollection services)
      {
         services.AddSingleton<ILoggerManager, LoggerManager>();
      }
   }
}
