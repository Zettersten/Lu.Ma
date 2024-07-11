![PathCat Icon](https://raw.githubusercontent.com/Zettersten/Lu.Ma/main/logo.png)

# Lu.Ma ✨

[![NuGet Badge](https://buildstats.info/nuget/lu.ma)](https://www.nuget.org/packages/Lu.Ma/)

Welcome to the unofficial Lu.Ma .NET SDK! This SDK is designed to provide a seamless way to interact with the Lu.Ma API, allowing you to manage events and calendars programmatically. The SDK is available under the MIT license, and contributions are welcome.

## Overview

The Lu.Ma .NET SDK offers two primary classes that developers will interact with:

- `CalendarManager`
- `EventManager`

These classes provide methods to create, update, and manage events and calendars on Lu.Ma. 

## Getting Started

### Installation

To get started with the Lu.Ma .NET SDK, you can download the package from [NuGet](https://www.nuget.org/packages/Lu.Ma/).

```sh
dotnet add package Lu.Ma
```

### Setting Up Dependency Injection

To use the Lu.Ma SDK with dependency injection in your .NET application, you need to configure your services. Here’s how you can do it:

1. Add the necessary services to your `Startup.cs` or `Program.cs`.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddLuMa(options =>
    {
        options.ApiKey = "YOUR_API_KEY";
    });

    // Other service configurations...
}
```

You can find the `ServiceCollectionExtensions` class [here](path/to/ServiceCollectionExtenstions.cs).

## Supported API Functions

### Lu.Ma.EventManager Class

| Function                | Required Parameters                                                                                                         | Description                                          | Return Type                           | DOC URL                                    | 
|-------------------------|-----------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------|---------------------------------------|-------------------------------------------|
| `CreateEventAsync`      | `CreateEventRequest request`                                                                                                | Create an event under your Luma account              | `Task<CreateEventResponse>`           | [Create Event](https://docs.lu.ma/reference/create-event)              |
| `GetEventAsync`         | `string apiId`                                                                                                              | Get details about an event you are a host of         | `Task<GetEventResponse>`              | [Get Event](https://docs.lu.ma/reference/get-event)                    |
| `GetEventGuestsAsync`   | `string eventApiId`, `string? approvalStatus = null`, `string? sortColumn = null`, `string? sortDirection = null`            | Get list of guests who have registered or been invited to an event | `IAsyncEnumerable<EventEntry>`       | [Get Event Guests](https://docs.lu.ma/reference/get-event-guests)      |
| `UpdateEventAsync`      | `UpdateEventRequest request`                                                                                                | Update attributes on the event                       | `Task`                                | [Update Event](https://docs.lu.ma/reference/update-event)              |
| `GetEventGuestAsync`    | `string eventApiId`, `string? apiId = null`, `string? email = null`, `string? proxyKey = null`                               | Get a guest by their Guest API ID, email or Proxy Key | `Task<EventEntry>`                   | [Get Event Guest](https://docs.lu.ma/reference/get-event-guests-copy) |
| `AddGuestsAsync`        | `AddGuestRequest request`                                                                                                   | Add a guest to the event. They will be added with the status "Going" | `Task`                                | [Add Guests](https://docs.lu.ma/reference/add-guest)                   |
| `UpdateGuestStatusAsync` | `UpdateGuestStatusRequest request`                                                                                          | Update the status of a guest                          | `Task`                                | [Update Guest Status](https://docs.lu.ma/reference/update-guest-status) |
| `AddHostAsync`          | `AddHostRequest request`                                                                                                    | Add a host to help you manage the event              | `Task`                                | [Add Host](https://docs.lu.ma/reference/add-host)                      |
| `CreateCouponAsync`     | `CreateCouponRequest request`                                                                                               | Create a coupon that can be applied to an event      | `Task`                                | [Create Coupon](https://docs.lu.ma/reference/create-coupon)            |
| `UpdateCouponAsync`     | `UpdateCouponRequest request`                                                                                               | Update a coupon on an event                          | `Task`                                | [Update Coupon](https://docs.lu.ma/reference/create-coupon-copy)       |

### Lu.Ma.CalendarManager Class

| Function                | Required Parameters                                                                                                         | Description                                          | Return Type                           | DOC URL                                    | 
|-------------------------|-----------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------|---------------------------------------|-------------------------------------------|
| `ListEventsAsync`       | `DateTime? before = null`, `DateTime? after = null`                                                                         | List all events managed by your calendar             | `IAsyncEnumerable<CalendarEntry>`     | [List Events](https://docs.lu.ma/reference/calendar-list-events)          |
| `ImportPeopleAsync`     | `ImportPeopleRequest request`                                                                                               | Import people to your calendar to invite them to events and send them newsletters | `Task`                                | [Import People](https://docs.lu.ma/reference/calendar-list-events)         |



## Usage

Below are examples of how to use the `CalendarManager` and `EventManager` classes.

### CalendarManager

#### List Events

```csharp
var calendarManager = serviceProvider.GetService<CalendarManager>();

await foreach (var eventItem in calendarManager.ListEventsAsync(DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow))
{
    Console.WriteLine($"{eventItem.Name} - {eventItem.StartTime}");
}
```

### EventManager

#### Create Event

```csharp
var eventManager = serviceProvider.GetService<EventManager>();
var newEvent = new CreateEventRequest
{
    Name = "Sample Event",
    StartTime = DateTime.UtcNow.AddDays(7),
    EndTime = DateTime.UtcNow.AddDays(7).AddHours(1),
    Description = "This is a sample event created using Lu.Ma SDK."
};

var createdEvent = await eventManager.CreateEventAsync(newEvent);
Console.WriteLine($"Event Created: {createdEvent.Id}");
```

#### Get Event Details

```csharp
var eventId = "EVENT_ID";
var eventDetails = await eventManager.GetEventAsync(eventId);
Console.WriteLine($"Event Name: {eventDetails.Name}");
```

## API Documentation

The Lu.Ma API is a JSON-based RESTful API that allows you to read data and take actions on your Lu.Ma events and guests. For detailed API documentation, visit the [Lu.Ma API Docs](https://docs.lu.ma).

### Requirements

- To use the Lu.Ma API, you'll need to pay for Luma Plus. [See our pricing page](https://lu.ma/pricing).

### Authentication

To authenticate your requests, you'll need an API key. You can generate an API key on your [Lu.Ma dashboard](https://lu.ma/personal/settings/options).

Here is an example CURL request to check authentication:

```sh
curl -X GET https://api.lu.ma/public/v1/event/get \
     -H "x-luma-api-key: YOUR_API_KEY"
```

## License

This SDK is available under the [MIT License](LICENSE).

## Contributions

Pull requests and contributions are welcome. Please open an issue to discuss any changes before submitting a pull request.

## About

For more information about the business and its offerings, visit [Lu.Ma](https://lu.ma/).

---

Thank you for using the Lu.Ma .NET SDK. We look forward to your contributions and feedback!