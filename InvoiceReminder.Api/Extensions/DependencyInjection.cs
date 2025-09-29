using Asp.Versioning;
using InvoiceReminder.Api.Exceptions;
using InvoiceReminder.Api.Infrastructure.Extensions;
using Scalar.AspNetCore;

namespace InvoiceReminder.Api.Extensions;
public static class DependencyInjection
{
    private static void AddPresentationServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddOpenApi();

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
         .AddMvc()
         .AddApiExplorer(options =>
          {
              options.GroupNameFormat = "'v'VVV";
              options.SubstituteApiVersionInUrl = true;
          });

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

    }

    private static void UseScalarDocumentationIfDevelopment(this WebApplication app)
    {

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            app.MapScalarApiReference(options =>
            {
                options.WithTitle("Invoice Reminder API")
                       .WithSidebar(true)
                       .WithDarkMode(true)
                       .WithDefaultOpenAllTags(true)
                       .WithLayout(ScalarLayout.Modern)
                       .WithTheme(ScalarTheme.Purple)
                       .DocumentDownloadType = DocumentDownloadType.None;
            });
        }
    }

    public static WebApplication BuildInvoiceReminderApp(this WebApplicationBuilder builder)
    {
        builder.Services.AddPresentationServices();
        builder.Services.AddInfrastructure(builder.Configuration);

        WebApplication app = builder.Build();

        app.UseScalarDocumentationIfDevelopment();

        app.UseExceptionHandler();

        app.MapControllers();

        return app;
    }
}