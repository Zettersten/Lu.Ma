using Lu.Ma.Models;
using Lu.Ma.Models.Shared;

namespace Lu.Ma.Abstractions;

public interface ICalendarManager
{
    /// <summary>
    /// Imports people to your calendar.
    /// </summary>
    /// <param name="request">The import people request details.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ImportPeopleAsync(ImportPeopleRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists events managed by your Calendar.
    /// </summary>
    /// <param name="before">Get events at or before this timestamp.</param>
    /// <param name="after">Get events at or after this timestamp.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>An asynchronous enumerable of calendar entries.</returns>
    IAsyncEnumerable<CalendarEntry> ListEventsAsync(DateTime? before = null, DateTime? after = null, CancellationToken cancellationToken = default);
}