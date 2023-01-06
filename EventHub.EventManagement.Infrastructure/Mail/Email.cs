using MimeKit;
using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.EventManagement.Infrastructure.Mail
{
#nullable disable
   public sealed class Email
   {
      public List<MailboxAddress>To { get; set; }
      public string Subject { get; set; }
      public string Content { get; set; }

      public Email(IEnumerable<string> to, string subject, string content)
      {
         To = new List<MailboxAddress>();
         To.AddRange(to.Select(a => new MailboxAddress(null,a));
         Subject = subject;
         Content = content;
      }
   }
}
