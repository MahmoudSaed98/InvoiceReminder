using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InvoiceReminder.Api.Application.DTOs.Invoices.V1;

public sealed record CreateInvoiceDto
{
    [Required]
    [Range(1, double.MaxValue)]
    [JsonPropertyName("customer_id")]
    public int CustomerId { get; init; }

    [Required]
    [Range(0.01, double.MaxValue)]
    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }

    [Required]
    [JsonPropertyName("due_date")]
    public DateTime DueDate { get; init; }
}