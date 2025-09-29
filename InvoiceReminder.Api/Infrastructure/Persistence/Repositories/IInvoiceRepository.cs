using InvoiceReminder.Api.Domain.Entities;
using InvoiceReminder.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvoiceReminder.Api.Infrastructure.Persistence.Repositories;

internal sealed class InvoiceRepository(InMemoryDbContext context) : IInvoiceRepository
{
    public void Add(Invoice invoice) =>
                   context.Add(invoice);

    public async Task<Invoice?> GetInvoiceAsync(int id, CancellationToken cancellationToken = default) =>
                          await context.Invoices.FirstOrDefaultAsync(i => i.Id == id);


    public async Task<IList<Invoice>> GetInvoicesDueForReminderAsync(DateTime currentDate, CancellationToken cancellationToken = default) =>
           await context.Invoices
                .Include(i => i.Customer)
                .Where(i => !i.IsRemainderSent && i.DueDate.Date == currentDate.Date)
                .ToListAsync();

    public async Task<IList<Invoice>> GetInvoicesList(int pageNumber = 1, int pageSize = 20, CancellationToken cancellationToken = default)
            => await context.Invoices.Include(i => i.Customer).OrderBy(i => i.Id)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync(cancellationToken);
}
