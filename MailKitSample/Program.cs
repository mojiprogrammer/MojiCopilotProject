using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Net.Mail;

class Program
{
    static async Task Main()
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Moji", "mojtaba.tavakoli2@hotmail.com"));
            message.To.Add(new MailboxAddress("Test", "mojtaba.tavakoli2@gmail.com"));
            message.Subject = "Test Email";
            message.Body = new TextPart("plain") { Text = "This is a test email" };

            using var client = new MailKit.Net.Smtp.SmtpClient();

            Console.WriteLine("Connecting...");
            await client.ConnectAsync("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);

            Console.WriteLine("Authenticating...");
            await client.AuthenticateAsync("mojtaba.tavakoli2@hotmail.com", "fuioaxiuewsdshov");

            Console.WriteLine("Sending...");
            await client.SendAsync(message);

            Console.WriteLine("Disconnecting...");
            await client.DisconnectAsync(true);

            Console.WriteLine("Email sent successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
    }
}