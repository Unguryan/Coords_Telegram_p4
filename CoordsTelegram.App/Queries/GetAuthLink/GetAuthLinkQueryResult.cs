using CoordsTelegram.Domain.Models;

namespace CoordsTelegram.App.Queries.GetAuthLink
{
    public record GetAuthLinkQueryResult(bool IsFound, AuthLink? AuthLink, string? ErrorMessage = null); 
}
