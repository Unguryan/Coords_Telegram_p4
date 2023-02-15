using AutoMapper;
using CoordsTelegram.App.Commands.AddTelegramUser;
using CoordsTelegram.App.Commands.CreateAuthLink;
using CoordsTelegram.Domain.Models;
using CoordsTelegram.Domain.ViewModels;
using CoordsTelegram.EF_Core.Dbo;

namespace CoordsTelegram.EF_Core.MapperProfiles
{
    public class TelegramUserProfile : Profile
    {
        public TelegramUserProfile()
        {
            CreateMap<CreateAuthLinkCommandResult, CreateAuthLinkViewModel>()
                .ReverseMap();
            CreateMap<TelegramUserDbo, TelegramUser>()
               .ReverseMap();
            CreateMap<AddTelegramUserCommand, CreateTelegramUserViewModel>()
               .ReverseMap();

            CreateMap<TelegramUser, TelegramUserInfoViewModel>()
               .ReverseMap();
        }
    }
}
