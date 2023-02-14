using CoordsTelegram.Domain.Models;

namespace CoordsTelegram.App.Queries.GetAuthLinkByChatId
{
    public record GetAuthLinkByChatIdQueryResult(bool IsFound, AuthLink? AuthLink, string? ErrorMessage = null); 
}
