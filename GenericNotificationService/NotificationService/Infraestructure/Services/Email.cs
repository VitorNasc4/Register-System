using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NotificationService.Infraestructure.Services
{
    public class Email
    {
        public string Provedor { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public Email(string provedor, string username, string password)
        {
            Provedor = provedor ?? throw new ArgumentNullException(nameof(provedor));
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public void SendEmail(string emailTo, string subject, string body)
        {
            var message = PrepareteMessage(emailTo, subject, body);

            SendEmailBySmtp(message);
        }

        private MailMessage PrepareteMessage(string emailTo, string subject, string body)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(Username)
            };


            if (ValidateEmail(emailTo))
            {
                mail.To.Add(emailTo);
            }


            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;



            return mail;
        }

        private bool ValidateEmail(string email)
        {
            Regex expression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
            if (expression.IsMatch(email))
                return true;

            return false;
        }

        private void SendEmailBySmtp(MailMessage message)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com")
            {
                Host = Provedor,
                Port = 587,
                EnableSsl = true,
                Timeout = 50000,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Username, Password)
            };
            smtpClient.Send(message);
            smtpClient.Dispose();
        }

    }
}