namespace BuberDinner.Domain.Common.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    public TId Id { get; protected set; }
    // we want to set id via protected constructor

    protected Entity(TId id)
    {
        Id = id;
    }

    // object? obj - means obj can be of type object, or it can be null
    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
        // true if same type && same id
        // obj is Entity<TId> entity - check if obj is of type Entity<TId>
        // if so => cast obj to Entity<TId> + assign result to variable entity

        // Note: this method works w/o explicitly checking if obj is null
        // pattern matching auto fails because null is not an instance of any type
    }

    // IEquatable interface - uses previous method
    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}