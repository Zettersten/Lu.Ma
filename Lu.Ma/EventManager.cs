using Lu.Ma.Abstractions;
using Lu.Ma.Extensions;
using Lu.Ma.Models;
using Lu.Ma.Models.Shared;
using System.Runtime.CompilerServices;

namespace Lu.Ma;

/// <summary>
/// Manages event-related operations for the Luma API.
/// </summary>
/// <remarks>
/// Initializes a new instance of the EventManager class.
/// </remarks>
/// <param name="httpClient">The LumaHttpClient instance to use for API requests.</param>
public sealed class EventManager(ILumaHttpClient httpClient) : IEventManager
{
    private readonly ILumaHttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private const string EventEndpoint = "/public/v1/event";

    /// <inheritdoc />
    public Task<CreateEventResponse> CreateEventAsync(CreateEventRequest request, CancellationToken cancellationToken = default)
    {
        return _httpClient.SendRequestAsync<CreateEventResponse>((client, c) =>
            client.PostAsync($"{EventEndpoint}/create", request.ToJsonContent(), c), cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetEventResponse> GetEventAsync(string apiId, CancellationToken cancellationToken = default)
    {
        var url = $"{EventEndpoint}/get".AddQueryParam("api_id", apiId);
        return _httpClient.SendRequestAsync<GetEventResponse>((client, c) => client.GetAsync(url, c), cancellationToken);
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<EventEntry> GetEventGuestsAsync(
        string eventApiId,
        string? approvalStatus = null,
        string? sortColumn = null,
        string? sortDirection = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        const int paginationLimit = 15;
        string? paginationCursor = null;

        while (!cancellationToken.IsCancellationRequested)
        {
            var url = $"{EventEndpoint}/get-guests".AddQueryParam("event_api_id", eventApiId)
                .AddQueryParam("pagination_limit", paginationLimit.ToString());

            if (!string.IsNullOrEmpty(paginationCursor))
                url = url.AddQueryParam("pagination_cursor", paginationCursor);
            if (!string.IsNullOrEmpty(approvalStatus))
                url = url.AddQueryParam("approval_status", approvalStatus);
            if (!string.IsNullOrEmpty(sortColumn))
                url = url.AddQueryParam("sort_column", sortColumn);
            if (!string.IsNullOrEmpty(sortDirection))
                url = url.AddQueryParam("sort_direction", sortDirection);

            var response = await _httpClient.SendRequestAsync<GetEventGuestResponse>(
                (client, c) => client.GetAsync(url, c), cancellationToken);

            if (response.Entries != null)
            {
                foreach (var entry in response.Entries)
                {
                    yield return entry;
                }
            }

            if (string.IsNullOrEmpty(response.NextCursor))
                break;

            paginationCursor = response.NextCursor;
        }
    }

    /// <inheritdoc />
    public Task UpdateEventAsync(UpdateEventRequest request, CancellationToken cancellationToken = default)
    {
        return _httpClient.SendRequestAsync<object>((client, c) =>
            client.PostAsync($"{EventEndpoint}/update", request.ToJsonContent(), c), cancellationToken);
    }

    /// <inheritdoc />
    public Task<EventEntry> GetEventGuestAsync(string eventApiId, string? apiId = null, string? email = null, string? proxyKey = null, CancellationToken cancellationToken = default)
    {
        var url = $"{EventEndpoint}/get-guest".AddQueryParam("event_api_id", eventApiId);
        if (!string.IsNullOrEmpty(apiId))
            url = url.AddQueryParam("api_id", apiId);
        if (!string.IsNullOrEmpty(email))
            url = url.AddQueryParam("email", email);
        if (!string.IsNullOrEmpty(proxyKey))
            url = url.AddQueryParam("proxy_key", proxyKey);

        return _httpClient.SendRequestAsync<EventEntry>((client, c) => client.GetAsync(url, c), cancellationToken);
    }

    /// <inheritdoc />
    public Task AddGuestsAsync(AddGuestRequest request, CancellationToken cancellationToken = default)
    {
        return _httpClient.SendRequestAsync<object>((client, c) =>
            client.PostAsync($"{EventEndpoint}/add-guests", request.ToJsonContent(), c), cancellationToken);
    }

    /// <inheritdoc />
    public Task UpdateGuestStatusAsync(UpdateGuestStatusRequest request, CancellationToken cancellationToken = default)
    {
        return _httpClient.SendRequestAsync<object>((client, c) =>
            client.PostAsync($"{EventEndpoint}/update-guest-status", request.ToJsonContent(), c), cancellationToken);
    }

    /// <inheritdoc />
    public Task AddHostAsync(AddHostRequest request, CancellationToken cancellationToken = default)
    {
        return _httpClient.SendRequestAsync<object>((client, c) =>
            client.PostAsync($"{EventEndpoint}/add-host", request.ToJsonContent(), c), cancellationToken);
    }

    /// <inheritdoc />
    public Task CreateCouponAsync(CreateCouponRequest request, CancellationToken cancellationToken = default)
    {
        return _httpClient.SendRequestAsync<object>((client, c) =>
            client.PostAsync($"{EventEndpoint}/create-coupon", request.ToJsonContent(), c), cancellationToken);
    }

    /// <inheritdoc />
    public Task UpdateCouponAsync(UpdateCouponRequest request, CancellationToken cancellationToken = default)
    {
        return _httpClient.SendRequestAsync<object>((client, c) =>
            client.PostAsync($"{EventEndpoint}/update-coupon", request.ToJsonContent(), c), cancellationToken);
    }
}