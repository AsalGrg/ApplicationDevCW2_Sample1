using BisleriumPvtLtdBackendSample1.Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace BisleriumPvtLtdBackendSample1
{

    public class SmtpClientOptionsSetup : IConfigureOptions<SmtpClientOptions>
    {
        private readonly SmtpSettings _settings;

        public SmtpClientOptionsSetup(IOptions<SmtpSettings> settings)
        {
            _settings = settings.Value;
        }

        public void Configure(SmtpClientOptions options)
        {
            options.Server = _settings.Server;
            options.Port = _settings.Port;
            options.Credentials = new System.Net.NetworkCredential(_settings.Username, _settings.Password);
            options.From = new MailAddress(_settings.FromAddress, _settings.DisplayName);
        }
    }
}
