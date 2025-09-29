namespace InvoiceReminder.Api.Application.Common.Email;

public sealed record EmailRequest
{
    public string From { get; init; } = string.Empty;
    public string To { get; init; } = string.Empty;
    public string Subject { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}