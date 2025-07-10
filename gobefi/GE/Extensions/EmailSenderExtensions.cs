using GobEfi.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace GobEfi.Web.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(
                email,
                "Confirme su correo",
                $"Favor confirme su cuenta haciendo click en el siguiente link : <a href='{HtmlEncoder.Default.Encode(link)}'>confirmar</a>.");
        }
    }
}
