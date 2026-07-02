using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TaskManagement.Api.Application.Models;
using TaskManagement.Api.Tests.Infrastructure;

namespace TaskManagement.Api.Tests;

public class TaskApiTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    [Fact]
    public async Task GetTasks_WithoutToken_ReturnsUnauthorized()
    {
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/tasks");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTask_WithValidToken_ReturnsCreatedTask()
    {
        using var client = factory.CreateClient();
        var token = await AuthenticateAsAsync(client, "user", "user123");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var createRequest = new TaskItemRequest
        {
            Title = "Write tests",
            Description = "Add integration tests",
            IsCompleted = false
        };

        var createResponse = await client.PostAsJsonAsync("/api/tasks", createRequest);

        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var getResponse = await client.GetAsync("/api/tasks");
        getResponse.EnsureSuccessStatusCode();
        var tasks = await getResponse.Content.ReadFromJsonAsync<List<TaskItemDto>>();

        Assert.NotNull(tasks);
        Assert.Contains(tasks!, t => t.Title == "Write tests");
    }

    private static async Task<string> AuthenticateAsAsync(HttpClient client, string username, string password)
    {
        var response = await client.PostAsJsonAsync("/api/auth/token", new AuthRequest
        {
            Username = username,
            Password = password
        });

        response.EnsureSuccessStatusCode();
        var payload = await response.Content.ReadFromJsonAsync<AuthResponse>();
        return payload!.Token;
    }

    private sealed class TaskItemDto
    {
        public string Title { get; set; } = string.Empty;
    }
}
