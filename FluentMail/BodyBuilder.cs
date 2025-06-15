using System.Text;
using FluentMail.Parts;

namespace FluentMail;

public class BodyBuilder
{
    private bool _isHtml;
    private string _content = string.Empty;
    private readonly Dictionary<string, string> _parts = [];

    public BodyBuilder IsHtml()
    {
        _isHtml = true;
        return this;
    }

    public BodyBuilder WithContent(string content)
    {
        _content = content;
        return this;
    }

    public BodyBuilder WithPart(string key, string value)
    {
        _parts.Add(key, value);
        return this;
    }

    public MailBody Build()
    {
        if (_content == string.Empty && _parts.Count > 0)
        {
            throw new InvalidOperationException("Body doesn't have any content to replace with body parts");
        }

        var stringBuilder = new StringBuilder(_content);
        foreach (var bodyPart in _parts)
        {
            stringBuilder.Replace("{" + bodyPart.Key + "}", bodyPart.Value);
        }

        return new MailBody
        {
            IsHtml = _isHtml,
            Content = stringBuilder.ToString(),
        };
    }
}