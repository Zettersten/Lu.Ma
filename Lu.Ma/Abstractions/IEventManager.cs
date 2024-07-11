using Lu.Ma.Models;
using Lu.Ma.Models.Shared;
using System.Runtime.CompilerServices;

namespace Lu.Ma.Abstractions;

public interface IEventManager
{
    /// <summary>
    /// Adds guests to the event. They will be added with the status "Going".
    /// </summary>
    /// <param name="request">The add guest request details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddGuestsAsync(AddGuestRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a host to help manage the event.
    /// </summary>
    /// <param name="request">The add host request details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddHostAsync(AddHostRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a coupon that can be applied to an event.
    /// </summary>
    /// <param name="request">The create coupon request details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CreateCouponAsync(CreateCouponRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates an event under your Luma account.
    /// </summary>
    /// <param name="request">The event creation request details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created event response.</returns>
    Task<CreateEventResponse> CreateEventAsync(CreateEventRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets details about an event you are a host of.
    /// </summary>
    /// <param name="apiId">ID of the event you want to get.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the event details.</returns>
    Task<GetEventResponse> GetEventAsync(string apiId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a guest by their Guest API ID, email or Proxy Key.
    /// </summary>
    /// <param name="eventApiId">The API ID of the event.</param>
    /// <param name="apiId">The API ID of the guest.</param>
    /// <param name="email">The email of the guest.</param>
    /// <param name="proxyKey">The proxy key of the guest.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the event guest entry.</returns>
    Task<EventEntry> GetEventGuestAsync(string eventApiId, string? apiId = null, string? email = null, string? proxyKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of guests who have registered or been invited to an event.
    /// </summary>
    /// <param name="eventApiId">The API ID of the event.</param>
    /// <param name="approvalStatus">If provided, only guests of the provided status will be returned.</param>
    /// <param name="sortColumn">The column to sort by.</param>
    /// <param name="sortDirection">The direction to sort.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An asynchronous enumerable of event entries.</returns>
    IAsyncEnumerable<EventEntry> GetEventGuestsAsync(string eventApiId, string? approvalStatus = null, string? sortColumn = null, string? sortDirection = null, [EnumeratorCancellation] CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a coupon on an event.
    /// </summary>
    /// <param name="request">The update coupon request details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateCouponAsync(UpdateCouponRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates attributes on the event.
    /// </summary>
    /// <param name="request">The event update request details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateEventAsync(UpdateEventRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the status of a guest for an event.
    /// </summary>
    /// <param name="request">The update guest status request details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateGuestStatusAsync(UpdateGuestStatusRequest request, CancellationToken cancellationToken = default);
}