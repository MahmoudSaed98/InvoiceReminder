using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InvoiceReminder.Api.Application.DTOs.Invoices.V1;

public sealed record InvoiceResponseDto
{
    [JsonPropertyName("id")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("customer_id")]
    [Required]
    public int CustomerId { get; init; }

    [JsonPropertyName("amount")]
    [Required]
    public decimal Amount { get; init; }

    [JsonPropertyName("due_date")]
    [Required]
    public DateTime DueDate { get; init; }
}