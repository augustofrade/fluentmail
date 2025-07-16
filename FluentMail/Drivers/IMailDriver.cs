using FluentMail.Parts;

namespace FluentMail.Drivers;

public interface IMailDriver<in T> where T : MailDriverOptions
{
    Task Configure(T options);
    Task<MailResult> SendAsync(MailScheme mailScheme);
}