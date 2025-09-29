using InvoiceReminder.Api.Application.Common.Email;
using InvoiceReminder.Api.Application.Interfaces;

namespace InvoiceReminder.Api.Infrastructure.Email;

internal sealed class InvoiceEmailReminder : IInvoiceEmailReminder
{
    private readonly IEmailSender _emailSender;
    public InvoiceEmailReminder(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task SendReminder(string toEmail, CancellationToken cancellationToken = default)
    {

        var reminderEmail = new EmailRequest
        {
            From = "no-reply@invoicereminder.com",
            To = toEmail,
            Subject = "Invoice Reminder",
            Message = "This is a friendly reminder to review and settle your outstanding invoice. Please contact us if you have any questions."
        };

        await _emailSender.SendAsync(reminderEmail, cancellationToken);
    }
}