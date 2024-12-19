using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

public interface IEmailService
    {
    Task SendEmailAsync(string toEmail, string subject, string body);
    }

public class EmailService : IEmailService
    {
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
        {
        _configuration = configuration;
        }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("VirtualLibrary", _configuration["EmailSettings:SenderEmail"]));
        email.To.Add(new MailboxAddress(toEmail));
        email.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = body };
        email.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:Port"]), false);
        await client.AuthenticateAsync(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:SenderPassword"]);
        await client.SendAsync(email);
        await client.DisconnectAsync(true);
        }
    }
