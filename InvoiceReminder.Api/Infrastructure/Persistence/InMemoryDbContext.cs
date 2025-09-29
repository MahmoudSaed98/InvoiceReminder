using InvoiceReminder.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceReminder.Api.Infrastructure.Persistence;

internal sealed class InMemoryDbContext : DbContext
{
    public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options)
        : base(options)
    { }

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Invoice> Invoices => Set<Invoice>();
}