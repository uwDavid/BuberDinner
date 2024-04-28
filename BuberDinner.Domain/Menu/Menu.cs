using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;

namespace BuberDinner.Domain.Menu;

// sealed keyword => prevent class to be inherited
public sealed class Menu : AggregateRoot<MenuId>
{
    public string Name { get; }
    public string Description { get; }
    public float AverageRating { get; }
    public HostId HostId { get; }
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    private readonly List<MenuSection> _sections = new();
    public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();

    // reference to other Aggregate Id's
    private readonly List<DinnerId> _dinnerIds = new();
    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();

    private readonly List<MenuReviewId> _menuReviewIds = new();
    public IReadOnlyList<MenuReviewId> MenuReviewIds = _menuReviewIds.AsReadOnly();

    // Constructor
    private Menu(
        MenuId menuId,
        string name,
        string description,
        HostId hostId,
        DateTime createdDateTime,
        DateTime updatedDateTime
    ) : base(menuId)
    {
        Name = name;
        Description = description;
        HostId = hostId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Menu Create(
        string name,
        string description,
        HostId hostId
    )
    {
        return new Menu(
            MenuId.CreateUnique(),
            name,
            description,
            hostId,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
    }

}