using Asp.Versioning;
using InvoiceReminder.Api.Application.DTOs.Invoices.V1;
using InvoiceReminder.Api.Application.Interfaces;
using InvoiceReminder.Api.Domain.Entities;
using InvoiceReminder.Api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceReminder.Api.Controllers.V1;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/invoices")]
public sealed class InvoiceController(IInvoiceRepository invoiceRepository,
                                      ICustomerRepository customerRepository,
                                      IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceDto invoiceDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        // TODO: Implement idempotency

        if (await customerRepository.GetById(invoiceDto.CustomerId) is null)
        {
            return BadRequest($"Customer with ID '{invoiceDto.CustomerId}' does not exist.");
        }

        var invoice = new Invoice(invoiceDto.CustomerId, invoiceDto.Amount, invoiceDto.DueDate);
        invoiceRepository.Add(invoice);
        await unitOfWork.SaveAsync();

        var responseDto = new InvoiceResponseDto
        {
            Id = invoice.Id,
            CustomerId = invoice.CustomerId,
            Amount = invoice.Amount,
            DueDate = invoice.DueDate
        };

        return CreatedAtAction(nameof(GetInvoice), new { invoiceId = invoice.Id }, responseDto);
    }

    [HttpGet("{invoiceId:int}")]
    public async Task<IActionResult> GetInvoice(int invoiceId, CancellationToken cancellationToken)
    {

        var invoice = await invoiceRepository.GetInvoiceAsync(invoiceId, cancellationToken);

        if (invoice is null)
        {
            return NotFound();
        }

        var responseDto = new InvoiceResponseDto
        {
            Id = invoice.Id,
            CustomerId = invoice.CustomerId,
            Amount = invoice.Amount,
            DueDate = invoice.DueDate
        };

        return Ok(responseDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetInvoicesList(int pageNumber = 1, int pageSize = 20, CancellationToken cancellationToken = default)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            return BadRequest("Page number and page size must be positive integers greater than zero.");
        }

        var list = await invoiceRepository
                        .GetInvoicesList(pageNumber, pageSize, cancellationToken);


        var listDto = list.Select(i => new InvoiceResponseDto
        {
            Id = i.Id,
            CustomerId = i.CustomerId,
            Amount = i.Amount,
            DueDate = i.DueDate
        });

        return Ok(listDto);
    }
}