using System.Text;

namespace FluentMail.Parts;

public class MailBody
{
    public bool IsHtml { get; init; }
    public string Content { get; init; }

    public MailBody()
    {
        
    }

    public override string ToString()
    {
        var htmlContent = "[HTML] " + Content;
        return IsHtml ? htmlContent : Content;
    }
}