using FluentMail.Drivers.SMTP.Extensions;
using FluentMail.Parts;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace FluentMail.Drivers.SMTP;

public class SmtpDriver : IMailDriver<SmtpOptions>
{
    private SmtpOptions _options;
    
    public Task Configure(SmtpOptions options)
    {
        _options = options;
        return Task.CompletedTask;
    }
    public async Task<MailResult> SendAsync(MailScheme mailScheme)
    {
        var message = new MimeMessage();
        
        message.From.Add(mailScheme.Sender.ToMailboxAddress());
        message.To.AddRange(mailScheme.Recipients.Select(r => r.ToMailboxAddress()));
        if (mailScheme.Ccs.Count > 0)
        {
            message.Cc.AddRange(mailScheme.Ccs.Select(c => c.ToMailboxAddress()));
        }
        message.Subject = mailScheme.Subject;
        
        var bodyBuilder = new MimeKit.BodyBuilder();
        if (mailScheme.Body != null)
        {
            if (mailScheme.Body.IsHtml)
                bodyBuilder.HtmlBody = mailScheme.Body.Content;
            else
                bodyBuilder.TextBody = mailScheme.Body.Content;
            
            message.Body = bodyBuilder.ToMessageBody();
        }

        using var smtpClient = new SmtpClient();
        try
        {
            await smtpClient.ConnectAsync(_options.Host, _options.Port,
                _options.Tls ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
            await smtpClient.AuthenticateAsync(_options.Username, _options.Password);
            await smtpClient.SendAsync(message);
            return MailResult.Success();
        }
        catch (Exception ex)
        {
            return MailResult.Failure(ex.Message);
        }
    }
}