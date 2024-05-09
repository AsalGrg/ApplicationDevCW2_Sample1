using System.Net.Mail;
using System.Net;

namespace BisleriumPvtLtdBackendSample1.Services
{
    public class EmailService
    {
        public void sendEmail(string subject, string to, string body)
        {
            string fromMail = "asalgoorong@gmail.com";
            string fromPassword = "uwfrgzhwmtcgjtjs"; //app password from gmail security two factor authenticator

            MailMessage message = new();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(to));
            message.Body = body;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true, //enable secured socket layer
            };

            smtpClient.Send(message);
        }

    }
}
