using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelDepartment.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message, IConfiguration configuration)
        {
            string senderEmail = configuration["SMTP:Email"];
            string server = configuration["SMTP:Server"];
            int port = int.Parse(configuration["SMTP:Port"]);
            string password = configuration["SMTP:Password"];

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Администрация PersonnelDepartment", email));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(server, port);

                await client.AuthenticateAsync(senderEmail, password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
