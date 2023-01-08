using MimeKit;
namespace EventHub.EventManagement.Application.Models
{
#nullable disable
   public sealed class Email
   {
      public List<MailboxAddress> To { get; set; }
      public string Subject { get; set; }
      public string Content { get; set; }

      public Email(IEnumerable<string> to, string subject, string content)
      {
         To = new List<MailboxAddress>();
         To.AddRange(to.Select(a => new MailboxAddress("EventHub", a)));
         Subject = subject;
         Content = content;
      }
   }
}
