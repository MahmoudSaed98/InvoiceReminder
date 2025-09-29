namespace InvoiceReminder.Api.Application.Interfaces;

public interface IUnitOfWork
{
    Task SaveAsync(CancellationToken cancellationToken = default);
}