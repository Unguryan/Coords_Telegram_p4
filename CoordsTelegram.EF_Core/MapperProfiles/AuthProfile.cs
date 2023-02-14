using AutoMapper;
using CoordsTelegram.App.Commands.CreateAuthLink;
using CoordsTelegram.Domain.Models;
using CoordsTelegram.Domain.ViewModels;
using CoordsTelegram.EF_Core.Dbo;

namespace CoordsTelegram.EF_Core.MapperProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<CreateAuthLinkCommandResult, CreateAuthLinkViewModel>()
                .ReverseMap();
            CreateMap<AuthLinkDbo, AuthLink>()
               .ReverseMap();
            CreateMap<AuthLink, AuthLinkExpiredViewModel>()
               .ReverseMap();
        }
    }
}
