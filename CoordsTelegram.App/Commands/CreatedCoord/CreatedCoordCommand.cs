using CoordsTelegram.Domain.ViewModels;
using MediatR;

namespace CoordsTelegram.App.Commands.CreatedCoord
{
    public record CreatedCoordCommand(CoordDetailsViewModel? Data) : IRequest<CreatedCoordCommandResult>;
}
