using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hulk.WebAPI.Services
{
    public static class EmailService
    {
        public static void SendEmail(string host, string emailFrom, string emailTo, string subject, string body, string user, string password, int port, bool sll = true, bool useDefaultCredentials = false)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = useDefaultCredentials;
                    smtp.EnableSsl = sll;
                    smtp.Host = host;
                    smtp.Port = port;
#if !DEBUG
                    smtp.TargetName = "STARTTLS/smtp.office365.com";
#endif
                    smtp.Credentials = new NetworkCredential(user, password);

                    MailMessage message = new MailMessage();
                    message.IsBodyHtml = true;
                    message.From = new MailAddress(emailFrom);
                    message.To.Add(new MailAddress(emailTo));
                    message.Subject = subject;
                    message.Body = body;
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
