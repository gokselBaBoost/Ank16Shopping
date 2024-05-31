namespace Shopping.Services.Mail
{
    public interface IMailService
    {
        void Send(string email, string displayName, string subject, string body);
    }
}
