using System;

namespace Starter.Domain.Interfaces;

public interface IEntity
{
    Guid Id { get; set; }
}