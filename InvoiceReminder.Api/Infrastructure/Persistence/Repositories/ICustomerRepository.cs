using InvoiceReminder.Api.Domain.Entities;
using InvoiceReminder.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvoiceReminder.Api.Infrastructure.Persistence.Repositories;

internal sealed class CustomerRepository(InMemoryDbContext context) : ICustomerRepository
{
    public void Add(Customer customer) =>
                context.Customers.Add(customer);

    public async Task<Customer?> GetById(int id) =>
              await context.Customers.FirstOrDefaultAsync(c => c.Id == id);

    public async Task<bool> IsCustomerUnique(string email, CancellationToken cancellationToken = default) =>
           !await context.Customers.AnyAsync(c => c.Email == email, cancellationToken);
}