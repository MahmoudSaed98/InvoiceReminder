using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InvoiceReminder.Api.Application.DTOs.Customers.V1;

public sealed record CreateCustomerDto
{
    [Required]
    [Length(3, 50)]
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [Required]
    [EmailAddress]
    [JsonPropertyName("email")]
    public string Email { get; init; } = string.Empty;
}
