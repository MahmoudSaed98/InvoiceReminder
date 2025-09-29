using InvoiceReminder.Api.Extensions;

WebApplication app = WebApplication
                    .CreateBuilder(args)
                    .BuildInvoiceReminderApp();

await app.RunAsync();