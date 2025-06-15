using System.Net.Mail;

namespace FluentMail.Parts;

public class MailScheme
{
    public MailAddress Sender { get; init; }
    public List<MailAddress> Recipients { get; init; } = [];
    public List<MailAddress> Ccs { get; init; } = [];
    public string Subject { get; set; } = string.Empty;
    public MailBody? Body { get; init; }
}