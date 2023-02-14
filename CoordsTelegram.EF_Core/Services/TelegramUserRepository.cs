using AutoMapper;
using CoordsTelegram.App.Services;
using CoordsTelegram.Domain.Models;
using CoordsTelegram.Domain.ViewModels;
using CoordsTelegram.EF_Core.Context;
using CoordsTelegram.EF_Core.Dbo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoordsTelegram.EF_Core.Services
{
    public class TelegramUserRepository : ITelegramUserRepository
    {
        private readonly TelegramUserContext _context;
        private readonly IMapper _mapper;

        public TelegramUserRepository(TelegramUserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddUserAsync(CreateTelegramUserViewModel request)
        {
            var user = await GetUserAsync(request.ChatId);

            if(user != null)
            {
                return false;
            }

            var addedEntity = await _context.TelegramUsers.AddAsync(
               new TelegramUserDbo()
               {
                   ChatId = request.ChatId,
                   FullName = request.FullName,
                   PhoneNumber = request.PhoneNumber,
                   UserName = request.UserName
               });
            var res = await _context.SaveChangesAsync();

            return res > 0;
        }

        public async Task<TelegramUser?> GetUserAsync(string id)
        {
            var res = await _context.TelegramUsers.FirstOrDefaultAsync(x => x.ChatId == id);
            if (res == null)
            {
                return null;
            }

            return _mapper.Map<TelegramUser>(res);
        }
    }
}
