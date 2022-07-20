namespace Dotnet.Services;

public class MessageServices : IEmailSender, ISmsSender
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        return Task.CompletedTask;
    }

    public Task SendSmsAsync(string number, string message)
    {
        return Task.CompletedTask;        
    }
}