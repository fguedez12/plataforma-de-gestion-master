using GobEfi.Web.Models.EmailModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class EmailSender : IEmailSender
    {
        private EmailConfigurationModel options;
        private ILogger logger;

        public EmailSender(
            IOptions<EmailConfigurationModel> options,
            ILoggerFactory factory)
        {
            this.options = options.Value;
            this.logger = factory.CreateLogger<EmailSender>();
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(options.Email, options.DisplayName)
                };

                mail.To.Add(new MailAddress(email));

                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(options.Host, options.Port))
                {
                    if (options.UseCredentials) smtp.Credentials = new NetworkCredential(options.User, options.Password);
                    if (options.UseSsl) smtp.EnableSsl = true;

                    smtp.Send(mail);
                }

            } catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}
