using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

public sealed class TodoItem
{
    public long Id { get; set; }
    
    [Required]
    [Length(3, 36)]
    public string Name { get; set; }
    
    public bool IsComplete { get; set; }
}