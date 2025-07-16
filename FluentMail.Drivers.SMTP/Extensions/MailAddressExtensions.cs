using System.Net.Mail;
using MimeKit;

namespace FluentMail.Drivers.SMTP.Extensions;

public static class MailAddressExtensions
{
    public static MailboxAddress ToMailboxAddress(this MailAddress mailAddress)
    {
        return new MailboxAddress(mailAddress.Address, mailAddress.DisplayName);
    }
}