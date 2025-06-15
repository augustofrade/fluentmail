using System.Net.Mail;
using FluentMail.Parts;

namespace FluentMail;

public sealed class MailBuilder
{
    private string _subject = string.Empty;
    private MailAddress _sender;
    private readonly List<MailAddress> _recipients = [];
    private readonly List<MailAddress> _ccs = [];
    public BodyBuilder BodyBuilder { get; } = new BodyBuilder();

    public MailBuilder() { }
    
    public MailBuilder(string senderName, string senderAddress)
    {
        _sender = new MailAddress(senderAddress, senderName);
    }

    public MailBuilder WithSender(string name, string address)
    {
        _sender = new MailAddress(address, name);
        return this;
    }

    public MailBuilder WithRecipient(string name, string address)
    {
        _recipients.Add(new MailAddress(address, name));
        return this;
    }

    public MailBuilder WithRecipients(IEnumerable<MailAddress> recipients)
    {
        _recipients.AddRange(recipients);
        return this;
    }

    public MailBuilder WithCc(string name, string address)
    {
        _ccs.Add(new MailAddress(address, name));
        return this;
    }

    public MailBuilder WithCcs(IEnumerable<MailAddress> recipients)
    {
        _ccs.AddRange(recipients);
        return this;
    }

    public MailBuilder WithSubject(string subject)
    {
        _subject = subject;
        return this;
    }

    public MailBuilder WithBody(Action<BodyBuilder> action)
    {
        action(BodyBuilder);
        return this;
    }

    public MailScheme Build()
    {
        if (_sender == null)
        {
            throw new InvalidOperationException("Sender is null");
        }
        
        return new MailScheme
        {
            Sender = _sender,
            Recipients = _recipients,
            Ccs = _ccs,
            Subject = _subject,
            Body = BodyBuilder.Build()
        };
    }
}