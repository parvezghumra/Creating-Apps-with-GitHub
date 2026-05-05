using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using Xunit;

namespace Api.Tests;

public class EntriesTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    // Initializes an HTTP client for integration testing against the in-memory API host.
    public EntriesTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    // Verifies GET /entries returns a successful response with at least one well-formed entry.
    [Fact]
    public async Task GetEntries_Returns200AndExpectedData()
    {
        // Arrange
        var expectedProperties = new[]
        {
            "id",
            "tool",
            "task",
            "expected",
            "actual",
            "verdict",
            "timestamp"
        };

        // Act
        var response = await _client.GetAsync("/entries");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        response.EnsureSuccessStatusCode();

        var document = JsonDocument.Parse(content);
        Assert.Equal(JsonValueKind.Array, document.RootElement.ValueKind);
        Assert.NotEmpty(document.RootElement.EnumerateArray());

        var firstEntry = document.RootElement.EnumerateArray().First();
        foreach (var propertyName in expectedProperties)
        {
            Assert.True(firstEntry.TryGetProperty(propertyName, out _));
        }
    }

    // Verifies an invalid entries route returns 404 as an error case.
    [Fact]
    public async Task GetEntries_InvalidRoute_Returns404()
    {
        // Arrange
        const string invalidRoute = "/entry";

        // Act
        var response = await _client.GetAsync(invalidRoute);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}