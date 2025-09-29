namespace InvoiceReminder.Api.Application.Common.Email;

public interface IEmailSender
{
    Task SendAsync(EmailRequest request, CancellationToken cancellationToken = default);
}