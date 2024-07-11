using Lu.Ma.Abstractions;
using Lu.Ma.Extensions;
using Lu.Ma.Models;
using Lu.Ma.Models.Shared;
using System.Runtime.CompilerServices;

namespace Lu.Ma;

public class CalendarManager(ILumaHttpClient httpClient) : ICalendarManager
{
    private readonly ILumaHttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private const string CalendarEndpoint = "/public/v1/calendar";

    /// <inheritdoc />
    public async IAsyncEnumerable<CalendarEntry> ListEventsAsync(
        DateTime? before = null,
        DateTime? after = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        const int paginationLimit = 15;
        string? paginationCursor = null;

        while (!cancellationToken.IsCancellationRequested)
        {
            var url = $"{CalendarEndpoint}/list-events"
                .AddQueryParam("pagination_limit", paginationLimit.ToString());

            if (before.HasValue)
                url = url.AddQueryParam("before", before.Value.ToString("o"));
            if (after.HasValue)
                url = url.AddQueryParam("after", after.Value.ToString("o"));
            if (!string.IsNullOrEmpty(paginationCursor))
                url = url.AddQueryParam("pagination_cursor", paginationCursor);

            var response = await _httpClient.SendRequestAsync<ListEventsResponse>(
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
    public Task ImportPeopleAsync(ImportPeopleRequest request, CancellationToken cancellationToken = default)
    {
        return _httpClient.SendRequestAsync<object>((client, c) =>
            client.PostAsync($"{CalendarEndpoint}/import-people", request.ToJsonContent(), c), cancellationToken);
    }
}