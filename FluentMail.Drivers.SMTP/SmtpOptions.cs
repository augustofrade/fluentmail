namespace FluentMail.Drivers.SMTP;

public class SmtpOptions : MailDriverOptions
{
    public string Host { get; set; }
    public int Port { get; set; }
    public bool Tls { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}