using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SendEmailWithGoogleSMTP
{
    class Program
    {
        static void Main(string[] args)
        {
            string fromMail = "asalgoorong@gmail.com";
            string fromPassword = "uwfrgzhwmtcgjtjs"; //app password from gmail security two factor authenticator

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Test Subject";
            message.To.Add(new MailAddress("asal.gurung.a21.2@icp.edu.np"));
            message.Body = "<html><body> Hello there! This is a test message to test SMTP. </body></html>";
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