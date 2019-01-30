using System.Threading.Tasks;
using D1.Model.Services.Abstract;
using MailKit.Net.Smtp;
using MimeKit;

namespace D1.Model.Services.Concrete
{
    public class EmailService : IEmailService
    {
        
        public async Task SendEmailAsync(string email, string subject, string messageText)
        {

            var message = new MimeMessage();
         //   message.From.Add(new MailboxAddress("Admin", "admin@archysoft.com"));
            message.From.Add(new MailboxAddress("Admin", "ArchysoftTest@yandex.ru"));

            message.To.Add(new MailboxAddress("", "stepnaturesto@gmail.com"));
            message.Subject = subject;


            message.Body = new TextPart("plain")
            {
                Text =messageText
            };


            //using (var client = new SmtpClient())
            //{
            //    await client.ConnectAsync("smtp.gmail.com", 465, true);
            //    await client.AuthenticateAsync("admin@archysoft.com", "pass");
            //    await client.SendAsync(message);

            //    await client.DisconnectAsync(true);
            //}



            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 465, true);
                await client.AuthenticateAsync("ArchysoftTest@yandex.ru", "12345qwerty");
                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }
    }
}
