using System.Text.Json.Serialization;

namespace InvoiceReminder.Api.Application.DTOs.Customers.V1;

public sealed record CustomerResponseDto
{
    [JsonPropertyName("Id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; init; } = string.Empty;
}
