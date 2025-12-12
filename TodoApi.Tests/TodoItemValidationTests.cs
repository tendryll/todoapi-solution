using TodoApi.Models;
using TodoApi.Tests.Helpers;

namespace TodoApi.Tests;

public class TodoItemValidationTests
{
    [Fact]
    public void Assert_Todo_All_Fields()
    {
        var todo = new TodoItem
        {
            Id = 1,
            Name = "create unit tests",
            IsComplete = false,
        };

        var results = ValidationTestHelper.Validate(todo);
        
        Assert.Empty(results);
    }
    
    [Fact]
    public void Assert_Todo_Name_Missing()
    {
        var todo = new TodoItem
        {
            Id = 1,
            IsComplete = false,
        };

        var results = ValidationTestHelper.Validate(todo);
        
        Assert.Contains(results, r => r.MemberNames.Contains("Name"));
        Assert.Equal("The Name field is required.", results[0].ErrorMessage);
    }
    
    [Fact]
    public void Assert_Todo_Name_Is_Too_Short()
    {
        var todo = new TodoItem
        {
            Id = 1,
            Name = "ab",
            IsComplete = false,
        };

        var results = ValidationTestHelper.Validate(todo);
        
        Assert.Contains(results, r => r.MemberNames.Contains("Name"));
        Assert.Equal("The field Name must be a string or collection type with a minimum length of '3' and maximum length of '36'.", results[0].ErrorMessage);
    }     
    
    [Fact]
    public void Assert_Todo_Name_Is_Too_Long()
    {
        var todo = new TodoItem
        {
            Id = 1,
            Name = "Lorem ipsum dolor sit amet, consectet",
            IsComplete = false,
        };

        var results = ValidationTestHelper.Validate(todo);
        
        Assert.Contains(results, r => r.MemberNames.Contains("Name"));
        Assert.Equal("The field Name must be a string or collection type with a minimum length of '3' and maximum length of '36'.", results[0].ErrorMessage);
    }     
}