using AutoMapper;
using CoordsTelegram.App.Queries.GetTokenInfo;
using CoordsTelegram.Domain.Models;
using CoordsTelegram.Domain.ViewModels;
using CoordsTelegram.EF_Core.Dbo;

namespace CoordsTelegram.EF_Core.MapperProfiles
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<TokenDbo, TokenInfoViewModel>()
               .ReverseMap();

            CreateMap<AuthLink, TokenInfoViewModel>()
               .ReverseMap();

            CreateMap<AuthLink, AddTokenViewModel>()
               .ReverseMap();

            CreateMap<GetTokenInfoViewModel, GetTokenInfoQueryResult>()
               .ReverseMap();
        }
    }
}
