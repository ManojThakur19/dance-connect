using System.Net.Mail;
using System.Net;
using System.Text;
using DanceConnect.Server.Entities;
using DanceConnect.Server.Models;

namespace DanceConnect.Server.Helpers
{
    public class EmailHelper
    {
        public static bool SendEmail(EmailMessage emailMessage, string subject)
        {
            try
            {

                // If Gmail is your primary email, then would recommend using a test address
                // E.g. I have one that just adds ".consulting" to the end of my usual one.
                string fromAddress = "mnjthakur19@gmail.com";
                //string toAddress = "TRY-YOUR-CONESTOGA-ADDRESS";
                string toAddress = string.Empty;

                // NOTE: to make this work "2FA" must be turned on for gmail
                // and then you add an app and they generate a password for use with that app
                // see here: https://stackoverflow.com/questions/32260/sending-email-in-net-through-gmail
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromAddress, "bohn qpty bztv klep"),
                    EnableSsl = true,
                };

                var message = new StringBuilder();
                message.AppendLine($"<h1>Hello {emailMessage.ReceivingUser.Name}:</h1>");
                message.AppendLine($"<p>My name is \"{emailMessage.SendingUser.Name}\" and i would like to learn dance from you");
                message.AppendLine($"<p>Sincerely,</p>");
                message.AppendLine($"<p>{emailMessage.SendingUser.Name}</p>");

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress(fromAddress),
                    Subject = subject,
                    Body = message.ToString(),
                    IsBodyHtml = true
                };

                mailMessage.To.Add(emailMessage.ReceivingUser.AppUser.Email);

                smtpClient.SendAsync(mailMessage, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool SendEmail(ContactUs contactUs, string subject)
        {
            try
            {

                // If Gmail is your primary email, then would recommend using a test address
                // E.g. I have one that just adds ".consulting" to the end of my usual one.
                string fromAddress = "mnjthakur19@gmail.com";
                //string toAddress = "TRY-YOUR-CONESTOGA-ADDRESS";
                string toAddress = string.Empty;

                // NOTE: to make this work "2FA" must be turned on for gmail
                // and then you add an app and they generate a password for use with that app
                // see here: https://stackoverflow.com/questions/32260/sending-email-in-net-through-gmail
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromAddress, "bohn qpty bztv klep"),
                    EnableSsl = true,
                };

                var message = new StringBuilder();
                message.AppendLine($"<h1>Hello {contactUs.Name}:</h1>");
                message.AppendLine($"<p>Please find the Answer of the Question: {contactUs.Message} below:</p>");
                message.AppendLine($"<p>\"{contactUs.MessageResponse}\"");
                message.AppendLine($"<p>Sincerely,</p>");
                message.AppendLine($"<p>Admin</p>");

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress(fromAddress),
                    Subject = subject,
                    Body = message.ToString(),
                    IsBodyHtml = true
                };

                mailMessage.To.Add(contactUs.Email);

                smtpClient.SendAsync(mailMessage, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
