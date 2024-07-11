using Lu.Ma.Abstractions;
using Lu.Ma.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Lu.Ma;

public static class ServiceCollectionExtensions
{
    [RequiresDynamicCode("config.GetSection(\"LumaApi\").Bind(options);")]
    [RequiresUnreferencedCode("config.GetSection(\"LumaApi\").Bind(options);")]
    public static IServiceCollection AddLuma(this IServiceCollection services, Action<LumaApiOptions>? configureOptions = null)
    {
        services.AddOptions<LumaApiOptions>()
            .Configure<IConfiguration>((options, config) =>
            {
                config.GetSection("LumaApi").Bind(options);
                configureOptions?.Invoke(options);
            })
            .PostConfigure(options =>
            {
                if (string.IsNullOrEmpty(options.ApiKey))
                {
                    throw new InvalidOperationException("Luma API key is not configured.");
                }
            });

        services
            .AddHttpClient<ILumaHttpClient, LumaHttpClient>()
            .ConfigureHttpClient((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<LumaApiOptions>>().Value;
                client.BaseAddress = new Uri("https://api.lu.ma");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-luma-api-key", options.ApiKey);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            });

        services.AddTransient<IEventManager, EventManager>();
        services.AddTransient<ICalendarManager, CalendarManager>();

        return services;
    }
}