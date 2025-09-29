namespace InvoiceReminder.Api.Domain.Entities;

public class Invoice
{
    public int Id { get; set; }
    public int CustomerId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime DueDate { get; private set; }
    public bool IsRemainderSent { get; private set; }
    public DateTime? DueAt { get; private set; }
    public Invoice(int customerId, decimal amount, DateTime dueDate)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(customerId);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        CustomerId = customerId;
        Amount = amount;
        DueDate = dueDate;
    }

    public Customer Customer { get; private set; } = default!;

    public void MarkRemainderAsSent()
    {
        if (IsRemainderSent)
        {
            return;
        }
        this.DueAt = DateTime.Now;
        this.IsRemainderSent = true;
    }
}
