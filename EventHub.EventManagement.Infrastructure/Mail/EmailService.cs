using EventHub.EventManagement.Application.Contracts.Infrastructure;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.ConfigurationModels;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;


namespace EventHub.EventManagement.Infrastructure.Mail
{
   public class EmailService : IEmailSender
   {
      private readonly EmailConfiguration _emailConfiguration;
      private readonly ILoggerManager _logger;

      public EmailService(IOptions<EmailConfiguration> emailConfiguration, ILoggerManager logger)
      {
         _emailConfiguration = emailConfiguration.Value;
         _logger = logger;
      }
      public async Task SendEmailAsync(Email email)
      {
         var emailMessage = CreateEmailMessage(email);
         await SendAsync(emailMessage);
      }

      private MimeMessage CreateEmailMessage(Email email)
      {
         var emailMessage = new MimeMessage();
         emailMessage.From.Add(new MailboxAddress("EventHub", _emailConfiguration.From));
         emailMessage.To.AddRange(email.To);
         emailMessage.Subject = email.Subject;
         emailMessage.Body = new TextPart(TextFormat.Text) { Text = email.Content };

         return emailMessage;
      }

      private async Task SendAsync(MimeMessage mailMessage)
      {
         using (var client = new SmtpClient())
         {
            try
            {
               await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port);
               client.AuthenticationMechanisms.Remove("XOAUTH2");
               await client.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);

               await client.SendAsync(mailMessage);
            }
            catch
            {
               _logger.LogError("some errors occurs while trying to connect to Smtp Server");
               throw;
            }
            finally
            {
               client.Disconnect(true);
               client.Dispose();
            }
         }
      }
   }
}
