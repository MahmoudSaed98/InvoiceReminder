using InvoiceReminder.Api.Application.Interfaces;
using InvoiceReminder.Api.Domain.Repositories;
using Quartz;

namespace InvoiceReminder.Api.Infrastructure.BackgroundServices;

internal sealed class SendInvoiceReminderJob : IJob
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IInvoiceEmailReminder _emailReminder;
    private readonly ILogger<SendInvoiceReminderJob> _logger;
    public SendInvoiceReminderJob(IInvoiceRepository invoiceRepository, ILogger<SendInvoiceReminderJob> logger, IInvoiceEmailReminder emailReminder)
    {
        _invoiceRepository = invoiceRepository;
        _logger = logger;
        _emailReminder = emailReminder;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Job execution started at {StartTime}", DateTime.Now);

        var currentDate = DateTime.Now;

        try
        {
            var dueInvoices = await _invoiceRepository
                                .GetInvoicesDueForReminderAsync(currentDate);

            foreach (var invoice in dueInvoices)
            {
                _logger.LogInformation("Sending reminder to {Email} for Invoice {InvoiceID}", invoice.Customer.Email, invoice.Id);

                await _emailReminder.SendReminder(invoice.Customer.Email);

                invoice.MarkRemainderAsSent();
            }

            _logger.LogInformation("Job execution completed successfully at {EndTime}", DateTime.Now);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during job execution at {ErrorTime}", DateTime.Now);
            throw;
        }
    }
}
