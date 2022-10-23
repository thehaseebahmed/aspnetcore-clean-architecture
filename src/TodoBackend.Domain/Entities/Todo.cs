using System;
using Starter.Domain.Interfaces;

namespace Starter.Domain.Entities;

public class Todo : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool Completed { get; set; }
    public int Order { get; set; }
    public string Title { get; set; }
}