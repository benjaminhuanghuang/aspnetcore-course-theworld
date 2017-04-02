using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class DebugMailService : IMailService
    {
        public void  SendMail(string to, string from, string subject, string body)
        {
            //var emailMessage = new MimeMessage();

            //emailMessage.From.Add(new MailboxAddress(fullName, email));
            //emailMessage.To.Add(new MailboxAddress("To Name", "toEmail@website.com"));
            //emailMessage.Subject = "Email from ToDoApp";
            //emailMessage.Body = new TextPart("plain") { Text = message };

            //using (var client = new SmtpClient())
            //{
            //    client.LocalDomain = "some.domain.com";
            //    client.Connect("smtp.relay.uri", 25, SecureSocketOptions.None);
            //    client.Send(emailMessage);
            //    client.Disconnect(true);
            //}
            Debug.WriteLine($"Sending Mail: To: {to} From: {from} Subject: {subject}");
        }
    }
}
