using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;
//using System.Net.Mail;
namespace expenseManagement.Models
{
    public class EmailHelper
    {
        public bool SendEmail(string userEmail, string confirmationLink)
        {
            //MailMessage mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress("exp.mgmt.project@gmail.com");
            //mailMessage.To.Add(new MailAddress(userEmail));

            //mailMessage.Subject = "Confirm your email";
            //mailMessage.IsBodyHtml = true;
            //mailMessage.Body = confirmationLink;

            //SmtpClient client = new SmtpClient("smtp.gmail.com",587);
            ////client.UseDefaultCredentials = true;
            //client.EnableSsl = true;
            //client.Credentials = new System.Net.NetworkCredential("exp.mgmt.project@gmail.com", "Exp.Mgmt.Project");
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            ////client.Host = "smtp.gmail.com";
            ////client.Port = 587;

            //try
            //{
            //    client.Send(mailMessage);
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    //Logger.Error(ex);
            //    Console.WriteLine(ex.ToString());

            //}
            //return false;


            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("kamille27@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(userEmail));

            email.Subject = "Click to confirm you email";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) {Text = confirmationLink };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("kamille27@ethereal.email", "N45y5HZFXEtYRnJ4dA");
            try
            {
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }      
            return false;
        }

    }
}
