using EventHub.EventManagement.Application.Contracts.Infrastructure;
using EventHub.EventManagement.Infrastructure.Mail;
using Microsoft.Extensions.DependencyInjection;


namespace EventHub.EventManagement.Infrastructure
{
   public static class InfrastructureServiceRegistration
   {
      public static void ConfigureLoggerService(this IServiceCollection services)
      {
         services.AddSingleton<ILoggerManager, LoggerManager>();
      }

      public static void ConfigureEmailService(this IServiceCollection services)
      {
         services.AddSingleton<IEmailSender, EmailService>();
      }
   }
}
