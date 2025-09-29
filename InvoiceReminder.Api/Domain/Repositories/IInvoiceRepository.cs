using InvoiceReminder.Api.Domain.Entities;

namespace InvoiceReminder.Api.Domain.Repositories;

public interface IInvoiceRepository
{
    void Add(Invoice invoice);

    Task<Invoice?> GetInvoiceAsync(int id, CancellationToken cancellationToken = default);

    Task<IList<Invoice>> GetInvoicesList(int pageNumber = 1, int pageSize = 20, CancellationToken cancellationToken
        = default);

    Task<IList<Invoice>> GetInvoicesDueForReminderAsync(DateTime currentDate, CancellationToken cancellationToken = default);
}
