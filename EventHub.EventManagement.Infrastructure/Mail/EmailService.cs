using EventHub.EventManagement.Application.Contracts.Infrastructure;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.ConfigurationModels;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;


namespace EventHub.EventManagement.Infrastructure.Mail
{
   public class EmailService : IEmailSender
   {
      private readonly EmailConfiguration _emailConfiguration;
      private readonly ILoggerManager _logger;

      public EmailService(EmailConfiguration emailConfiguration, ILoggerManager logger)
      {
         _emailConfiguration = emailConfiguration;
         _logger = logger;
      }
      public void SendEmail(Email email)
      {
         var emailMessage = CreateEmailMessage(email);
      }

      private MimeMessage CreateEmailMessage(Email email)
      {
         var emailMessage = new MimeMessage();
         emailMessage.From.Add(new MailboxAddress("email", _emailConfiguration.From));
         emailMessage.To.AddRange(email.To);
         emailMessage.Subject = email.Subject;
         emailMessage.Body = new TextPart(TextFormat.Html);

         return emailMessage;
      }

      private void Send(MimeMessage mailMessage)
      {
         using (var client = new SmtpClient())
         {
            try
            {
               client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port);
               client.AuthenticationMechanisms.Remove("XOAUTH2");
               client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);

               client.Send(mailMessage);
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
