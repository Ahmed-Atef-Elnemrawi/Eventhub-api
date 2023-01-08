using EventHub.EventManagement.Application.Models;

namespace EventHub.EventManagement.Application.Contracts.Infrastructure
{
   public interface IEmailSender
   {
      Task SendEmailAsync(Email email);
   }
}
