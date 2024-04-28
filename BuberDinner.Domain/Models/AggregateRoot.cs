namespace BuberDinner.Domain.Common.Models;

// AggregrateRoot is an Entity, just a wrapper
public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    // This just calls the base class constructor
    protected AggregateRoot(TId id) : base(id)
    {

    }
}