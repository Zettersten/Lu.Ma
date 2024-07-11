using Lu.Ma.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Lu.Ma.Tests;

public static class TestHelpers
{
    public static readonly EventManager EventManager;
    public static readonly CalendarManager CalendarManager;

    static TestHelpers()
    {
        var apiKey = GetEnvVar("LUMA_API_KEY");

        var logger = LoggerFactory.Create(x => x.ClearProviders()).CreateLogger<LumaHttpClient>();
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.lu.ma")
        };

        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var lumaHttpClient = new LumaHttpClient(httpClient, logger, Options.Create(new LumaApiOptions { ApiKey = apiKey }));

        EventManager = new EventManager(lumaHttpClient);
        CalendarManager = new CalendarManager(lumaHttpClient);
    }

    public static string GetEnvVar(string key)
    {
        var directory = GetCurrentProjectDirectory();
        var envFilePath = Path.Combine(directory, ".env");

        if (!File.Exists(envFilePath))
        {
            throw new LumaApiException("Make sure you .env file is created and has the proper key. Use the sample.env file for an example.", null);
        }

        DotNetEnv.Env.Load(envFilePath);

        var result = DotNetEnv.Env.GetString(key);

        ArgumentNullException.ThrowIfNull(result, nameof(result));

        return result;
    }

    private static string GetCurrentProjectDirectory()
    {
        // Get the executing assembly
        Assembly assembly = Assembly.GetExecutingAssembly();

        // Get the path of the executing assembly
        string assemblyPath = assembly.Location;

        ArgumentNullException.ThrowIfNull(assemblyPath, nameof(assemblyPath));

        // Get the directory of the executing assembly
        var directoryPath = new DirectoryInfo(Path.GetDirectoryName(assemblyPath)!).Parent!.Parent!.Parent!.FullName;

        ArgumentNullException.ThrowIfNull(directoryPath, nameof(directoryPath));

        // Search for the .csproj file in the directory and its subdirectories
        var csprojFile = Directory.GetFiles(directoryPath, "*.csproj", SearchOption.AllDirectories).FirstOrDefault();

        ArgumentNullException.ThrowIfNull(csprojFile, nameof(csprojFile));

        // Return the path of the .csproj file if found, or null if not found
        return Path.GetDirectoryName(csprojFile) ?? throw new ArgumentNullException("csprojFile");
    }
}