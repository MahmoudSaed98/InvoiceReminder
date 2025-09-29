using InvoiceReminder.Api.Application.Common.Email;

namespace InvoiceReminder.Api.Infrastructure.Email;

internal sealed class MockEmailSender : IEmailSender
{
    private readonly ILogger<MockEmailSender> _logger;

    public MockEmailSender(ILogger<MockEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(EmailRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Simulating email sending. From: {From}, To: {To}, Subject: {Subject}",
            request.From, request.To, request.Subject);

        return Task.CompletedTask;
    }
}