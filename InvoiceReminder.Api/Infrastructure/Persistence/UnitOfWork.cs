using InvoiceReminder.Api.Application.Interfaces;

namespace InvoiceReminder.Api.Infrastructure.Persistence;

internal sealed class UnitOfWork(InMemoryDbContext context) : IUnitOfWork
{
    public async Task SaveAsync(CancellationToken cancellationToken = default) =>
                 await context.SaveChangesAsync(cancellationToken);
}