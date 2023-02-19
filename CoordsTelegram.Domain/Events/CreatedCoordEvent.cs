using Coords.Domain.Events;
using CoordsTelegram.Domain.ViewModels;

namespace Coord.Domain.Events
{
    public record CreatedCoordEvent (bool IsCreated, CoordDetailsViewModel? Data, string? ErrorMessage) : IBaseEvent;  
}
