using Lu.Ma.Extensions;
using Lu.Ma.Http;
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
public sealed class EventManager(LumaHttpClient httpClient)
{
    private readonly LumaHttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private const string EventEndpoint = "/public/v1/event";

    /// <summary>
    /// Creates an event under your Luma account.
    /// </summary>
    /// <param name="request">The event creation request details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created event response.</returns>
    public Task<CreateEventResponse> CreateEventAsync(CreateEventRequest request, CancellationToken cancellationToken = default)
    {
        return _httpClient.SendRequestAsync<CreateEventResponse>((client, c) =>
            client.PostAsync($"{EventEndpoint}/create", request.ToJsonContent(), c), cancellationToken);
    }

    /// <summary>
    /// Gets details about an event you are a host of.
    /// </summary>
    /// <param name="apiId">ID of the event you want to get.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the event details.</returns>
    public Task<GetEventResponse> GetEventAsync(string apiId, CancellationToken cancellationToken = default)
    {
        var url = $"{EventEndpoint}/get".AddQueryParam("api_id", apiId);
        return _httpClient.SendRequestAsync<GetEventResponse>((client, c) => client.GetAsync(url, c), cancellationToken);
    }

    /// <summary>
    /// Gets a list of guests who have registered or been invited to an event.
    /// </summary>
    /// <param name="eventApiId">The API ID of the event.</param>
    /// <param name="approvalStatus">If provided, only guests of the provided status will be returned.</param>
    /// <param name="sortColumn">The column to sort by.</param>
    /// <param name="sortDirection">The direction to sort.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An asynchronous enumerable of event entries.</returns>
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

    /// <summary>
    /// Updates attributes on the event.
    /// </summary>
    /// <param name="request">The event update request details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task UpdateEventAsync(UpdateEventRequest request, CancellationToken cancellationToken = default)
    {
        return _httpClient.SendRequestAsync<object>((client, c) =>
            client.PostAsync($"{EventEndpoint}/update", request.ToJsonContent(), c), cancellationToken);
    }

    /// <summary>
    /// Gets a guest by their Guest API ID, email or Proxy Key.
    /// </summary>
    /// <param name="eventApiId">The API ID of the event.</param>
    /// <param name="apiId">The API ID of the guest.</param>
    /// <param name="email">The email of the guest.</param>
    /// <param name="proxyKey">The proxy key of the guest.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the event guest entry.</returns>
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

    /// <summary>
    /// Adds guests to the event. They will be added with the status "Going".
    /// </summary>
    /// <param name="request">The add guest request details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task AddGuestsAsync(AddGuestRequest request, CancellationToken cancellationToken = default)
    {
        return _httpClient.SendRequestAsync<object>((client, c) =>
            client.PostAsync($"{EventEndpoint}/add-guests", request.ToJsonContent(), c), cancellationToken);
    }

    /// <summary>
    /// Updates the status of a guest for an event.
    /// </summary>
    /// <param name="request">The update guest status request details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task UpdateGuestStatusAsync(UpdateGuestStatusRequest request, CancellationToken cancellationToken = default)
    {
        return _httpClient.SendRequestAsync<object>((client, c) =>
            client.PostAsync($"{EventEndpoint}/update-guest-status", request.ToJsonContent(), c), cancellationToken);
    }

    /// <summary>
    /// Adds a host to help manage the event.
    /// </summary>
    /// <param name="request">The add host request details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task AddHostAsync(AddHostRequest request, CancellationToken cancellationToken = default)
    {
        return _httpClient.SendRequestAsync<object>((client, c) =>
            client.PostAsync($"{EventEndpoint}/add-host", request.ToJsonContent(), c), cancellationToken);
    }

    /// <summary>
    /// Creates a coupon that can be applied to an event.
    /// </summary>
    /// <param name="request">The create coupon request details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task CreateCouponAsync(CreateCouponRequest request, CancellationToken cancellationToken = default)
    {
        return _httpClient.SendRequestAsync<object>((client, c) =>
            client.PostAsync($"{EventEndpoint}/create-coupon", request.ToJsonContent(), c), cancellationToken);
    }

    /// <summary>
    /// Updates a coupon on an event.
    /// </summary>
    /// <param name="request">The update coupon request details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task UpdateCouponAsync(UpdateCouponRequest request, CancellationToken cancellationToken = default)
    {
        return _httpClient.SendRequestAsync<object>((client, c) =>
            client.PostAsync($"{EventEndpoint}/update-coupon", request.ToJsonContent(), c), cancellationToken);
    }
}