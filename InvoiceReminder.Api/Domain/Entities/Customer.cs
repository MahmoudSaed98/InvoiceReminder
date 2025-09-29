namespace InvoiceReminder.Api.Domain.Entities;

public class Customer
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public Customer(string name, string email) => (Name, Email) = (name, email);
    public IList<Invoice> Invoices { get; private set; } = default!;
    private Customer() { }
}