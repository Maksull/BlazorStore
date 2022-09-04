using MailKit.Net.Smtp;
using MimeKit;

namespace MyStore.Models.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;   
        }

        public void SendEmail(EmailDto emailDto)
        {

            MimeMessage email = new();
            email.From.Add(MailboxAddress.Parse(_config["Email:Username"]));
            email.To.Add(MailboxAddress.Parse(emailDto.To));
            email.Subject = emailDto.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = GenerateAnswer(emailDto.Order!) };

            using SmtpClient smtp = new();
            smtp.Connect(_config["Email:Host"], 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config["Email:Username"], _config["Email:Password"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        private string GenerateAnswer(Order order)
        {
            string products = "";
            foreach(var line in order.Lines)
            {
                products += $"{line.Product.Name} - {line.Quantity} thing(s), ";
            }
            products = products.Substring(0, products.Length - 2);
            string answer = $"<h1>{order.Name}, You have ordered {products}. </h1>";
            answer += $"<h2>Your OrderId is {order.OrderId}. </h2>";
            return answer;
        }
    }
}
