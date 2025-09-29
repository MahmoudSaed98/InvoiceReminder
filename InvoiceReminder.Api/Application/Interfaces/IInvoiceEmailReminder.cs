namespace InvoiceReminder.Api.Application.Interfaces;

public interface IInvoiceEmailReminder
{
    Task SendReminder(string toEmail, CancellationToken cancellationToken = default);
}
