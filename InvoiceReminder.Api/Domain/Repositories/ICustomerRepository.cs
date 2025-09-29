using InvoiceReminder.Api.Domain.Entities;

namespace InvoiceReminder.Api.Domain.Repositories;

public interface ICustomerRepository
{
    void Add(Customer customer);

    Task<Customer?> GetById(int id);

    Task<bool> IsCustomerUnique(string email, CancellationToken cancellationToken = default);
}