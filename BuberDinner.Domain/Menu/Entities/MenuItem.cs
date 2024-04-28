using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;

namespace BuberDinner.Domain.Menu.Entities;

public sealed class MenuItem : Entity<MenuItemId>
{
    // constructor
    private MenuItem(MenuItemId menuItemId, string name, string description)
        : base(menuItemId)
    {
        // We use the base class constructor to generate menuItemId
        // - this also ensures that base constructor is called before this constructor
        // - you can pass additional args + one required by the base constructor like this
        //   additional args are used to initialize additional field/properties in derived class
        Name = name;
        Description = description;
    }

    // properties
    public string Name { get; }
    public string Description { get; }


    // public constructor
    public static MenuItem Create(string name, string description)
    {
        return new(MenuItemId.CreateUnique(), name, description);
    }

}