using InvoiceReminder.Api.Application.Common.Email;
using InvoiceReminder.Api.Application.Interfaces;
using InvoiceReminder.Api.Domain.Repositories;
using InvoiceReminder.Api.Infrastructure.BackgroundServices;
using InvoiceReminder.Api.Infrastructure.Email;
using InvoiceReminder.Api.Infrastructure.Persistence;
using InvoiceReminder.Api.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace InvoiceReminder.Api.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IInvoiceRepository, InvoiceRepository>()
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<IEmailSender, MockEmailSender>()
                .AddScoped<IInvoiceEmailReminder, InvoiceEmailReminder>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddQuartzJobs()
                .AddDbContext<InMemoryDbContext>(o => o.UseInMemoryDatabase("InvoiceDb"));

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }

    private static IServiceCollection AddQuartzJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            var jobKey = JobKey.Create(nameof(SendInvoiceReminderJob));

            q.AddJob<SendInvoiceReminderJob>(config => config.WithIdentity(jobKey));

            q.AddTrigger(config =>
                config.ForJob(jobKey)
                      .WithIdentity($"{jobKey.Name}-trigger")
                      .StartNow()
                      .WithSimpleSchedule(x => x.WithIntervalInHours(24).RepeatForever()));
        });

        return services;
    }
}
