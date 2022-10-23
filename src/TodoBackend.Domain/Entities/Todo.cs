using System;
using Starter.Domain.Interfaces;

namespace Starter.Domain.Entities;

public class Todo : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool Completed { get; set; }
    public string Title { get; set; }
    public string Url => $"https://localhost:44310/api/v1/todo/{Id}";
}