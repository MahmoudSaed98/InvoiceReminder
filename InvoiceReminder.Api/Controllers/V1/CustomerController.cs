using Asp.Versioning;
using InvoiceReminder.Api.Application.DTOs.Customers.V1;
using InvoiceReminder.Api.Application.Interfaces;
using InvoiceReminder.Api.Domain.Entities;
using InvoiceReminder.Api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceReminder.Api.Controllers.V1;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/customers")]
public sealed class CustomerController(ICustomerRepository customerRepository,
                                       IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto customerDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);

        }

        if (await customerRepository.IsCustomerUnique(customerDto.Email) is false)
        {
            return BadRequest("Customer with the given email already exists.");
        }

        var customer = new Customer(customerDto.Name, customerDto.Email);
        customerRepository.Add(customer);

        await unitOfWork.SaveAsync();

        var responseDto = new CustomerResponseDto
        {
            Id = customer.Id,
            Email = customer.Email,
            Name = customer.Name
        };

        return CreatedAtAction(nameof(GetCutomer), new { customer.Id }, responseDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCutomer(int id)
    {
        var customer = await customerRepository.GetById(id);

        if (customer is not null)
        {
            return Ok(new CustomerResponseDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            });
        }

        return NotFound();
    }
}