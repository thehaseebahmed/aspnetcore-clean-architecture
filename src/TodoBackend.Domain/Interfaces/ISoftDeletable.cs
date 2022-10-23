namespace Starter.Domain.Interfaces;

public interface ISoftDeletable
{
    bool IsActive { get; set; }
}