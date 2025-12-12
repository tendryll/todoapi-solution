using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

/// <summary>
///     A <c>TodoContext</c> instance represents a session with the database and can be used to query and save
///     instances of a <c>TodoItem</c>.
/// </summary>
public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;
}