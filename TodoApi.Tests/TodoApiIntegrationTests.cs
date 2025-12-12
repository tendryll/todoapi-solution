using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TodoApi.Tests;

public class TodoApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    
    public TodoApiIntegrationTests(WebApplicationFactory<Program> factory)
        => _client = factory.CreateClient();

    [Fact]
    public async Task Create_TodoItem_Invalid_Body()
    {
        var payload = new
        {
        };
        
        var resp = await _client.PostAsJsonAsync("/api/todoitems", payload);
        Assert.Equal(HttpStatusCode.BadRequest, resp.StatusCode);
        
        // ASP.NET Core returns ProblemDetails for validation by default
        var problem = await resp.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problem);
        Assert.Contains("Name", problem!.Errors.Keys);
    }
    
    [Fact]
    public async Task Create_TodoItem_Name_Too_Short()
    {
        var payload = new
        {
            Name = "ab"
        };
        
        var resp = await _client.PostAsJsonAsync("/api/todoitems", payload);
        Assert.Equal(HttpStatusCode.BadRequest, resp.StatusCode);
        
        // ASP.NET Core returns ProblemDetails for validation by default
        var problem = await resp.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problem);
        Assert.Contains("Name", problem!.Errors.Keys);
        Assert.Equal("The field Name must be a string or collection type with a minimum length of '3' and maximum length of '36'.", 
            problem.Errors["Name"][0]);
    }
    
    [Fact]
    public async Task Create_TodoItem_Name_Too_Long()
    {
        var payload = new
        {
            Name = "Lorem ipsum dolor sit amet, consectet",
        };
        
        var resp = await _client.PostAsJsonAsync("/api/todoitems", payload);
        Assert.Equal(HttpStatusCode.BadRequest, resp.StatusCode);
        
        // ASP.NET Core returns ProblemDetails for validation by default
        var problem = await resp.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problem);
        Assert.Contains("Name", problem!.Errors.Keys);
        Assert.Equal("The field Name must be a string or collection type with a minimum length of '3' and maximum length of '36'.", 
            problem.Errors["Name"][0]);
    }    
}
