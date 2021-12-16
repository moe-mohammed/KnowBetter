namespace KnowBetter_WebApp.Models
{
    public interface IEmailService
    {
        void Send(EmailMessage message);
    }
}
