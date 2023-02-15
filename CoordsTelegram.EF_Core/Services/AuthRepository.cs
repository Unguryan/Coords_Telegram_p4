using AutoMapper;
using CoordsTelegram.App.Repositories;
using CoordsTelegram.Domain.Models;
using CoordsTelegram.Domain.ViewModels;
using CoordsTelegram.EF_Core.Context;
using CoordsTelegram.EF_Core.Dbo;
using Microsoft.EntityFrameworkCore;

namespace CoordsTelegram.EF_Core.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthLinkContext _context;
        private readonly IMapper _mapper;

        public AuthRepository(AuthLinkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AuthLink>> GetAuthLinksAsync()
        {
            var result = new List<AuthLink>();
            await _context.AuthLinks.ForEachAsync(a => result.Add(_mapper.Map<AuthLink>(a)));
            return result;
        }

        public async Task<List<AuthLink>> GetAuthLinksByChatIdAsync(string chatId)
        {
            var result = new List<AuthLink>();
            await _context.AuthLinks.Where(x => x.ChatId == chatId).ForEachAsync(a => result.Add(_mapper.Map<AuthLink>(a)));
            return result;
        }

        public async Task<AuthLink> GetAuthLinkByKeyAsync(string key)
        {
            var entity = await _context.AuthLinks.FirstOrDefaultAsync(x => x.Key == key);
            return _mapper.Map<AuthLink>(entity);
        }

        public async Task<bool> AddNewKeyAsync(string key)
        {
            var addedEntity = await _context.AuthLinks.AddAsync(
                new AuthLinkDbo() { 
                    Key = key,
                    Expired = DateTime.Now.AddMinutes(3),
                    ChatId = string.Empty,
                    FullName = string.Empty,
                    PhoneNumber = string.Empty,
                    UserName = string.Empty
                });
            var res = await _context.SaveChangesAsync();

            return res > 0;
        }

        public async Task<bool> ChangeByKeyAsync(string key, TelegramUserInfoViewModel infoViewModel)
        {
            var entity = await _context.AuthLinks.FirstOrDefaultAsync(x => x.Key == key);

            if(entity == null)
            {
                return false;
            }

            entity.Expired = entity.Expired.AddMinutes(1);
            entity.ChatId = infoViewModel.ChatId;
            entity.FullName = infoViewModel.FullName;
            entity.PhoneNumber = infoViewModel.PhoneNumber;
            entity.UserName = infoViewModel.UserName;

            var res = await _context.SaveChangesAsync();

            return res > 0;
        }

        public async Task<bool> RemoveByKeyAsync(string key)
        {
            var entity = await _context.AuthLinks.FirstOrDefaultAsync(x => x.Key == key);

            if (entity == null)
            {
                return false;
            }

            _context.Remove(entity);
            var res = await _context.SaveChangesAsync();

            return res > 0;
        }

        public async Task<int> RemoveRangeByKeyAsync(List<string> keys)
        {
            var entityToRemove = new List<AuthLinkDbo>();

            foreach (var key in keys)
            {
                var entity = await _context.AuthLinks.FirstOrDefaultAsync(x => x.Key == key);

                if (entity == null)
                {
                    continue;
                }

                entityToRemove.Add(entity);
            }

            _context.RemoveRange(entityToRemove);
            var res = await _context.SaveChangesAsync();

            return res;
        }

        public async Task<bool> ChangeByKeyAsync(string key, string ChatId)
        {
            var entity = await _context.AuthLinks.FirstOrDefaultAsync(x => x.Key == key);

            if (entity == null)
            {
                return false;
            }

            entity.Expired = entity.Expired.AddMinutes(1);
            entity.ChatId = ChatId;

            var res = await _context.SaveChangesAsync();

            return res > 0;
        }

    
    }
}
