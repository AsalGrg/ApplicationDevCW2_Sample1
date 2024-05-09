using System.Net;
using System.Net.Mail;

namespace BisleriumPvtLtdBackendSample1
{
    public class SmtpClientOptions
    {
        public string Server {  get; set; }
        public int Port {  get; set; }
        public MailAddress From { get; set; }
        public NetworkCredential Credentials { get; set; }
    }
}
